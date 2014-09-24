using System.Globalization;
using System.Threading;
using System.Web;
using M2E.Common.Logger;
using M2E.Models;
using System;
using System.Linq;
using M2E.Encryption;
using System.Web.Mvc;
using M2E.Models.Constants;
using M2E.Models.DataResponse;
using M2E.Models.DataWrapper;
using M2E.CommonMethods;
using System.Data.Entity.Validation;
using M2E.Service;
using M2E.Service.Auth;
using M2E.Service.Register;
using System.Reflection;
using M2E.Session;
using System.Configuration;
using M2E.signalRPushNotifications;
using Microsoft.AspNet.SignalR;

namespace M2E.Controllers
{
    public class AuthController : Controller
    {
        // GET: /Auth/        
        private readonly M2EContext _db = new M2EContext();
        private static readonly ILogger Logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LockAccount(string id)
        {
            return View();
        }

        [HttpPost]
        public JsonResult LockUserAccount()
        {
            var response = new ResponseModel<LoginResponse>();
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            response = new AuthService().LockAccountService(headers, session);
            return Json(response);
        }

        [HttpPost]
        public JsonResult UnlockAccount(string pass)
        {
            var response = new ResponseModel<LoginResponse>();
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            response = new AuthService().unlockAccountService(headers, session,EncryptionClass.Md5Hash(pass));
            return Json(response);
        }

        [HttpPost]
        public JsonResult Login(LoginRequest req)
        {
            var returnUrl = "/";
            var referral = Request.QueryString["ref"];
            var responseData = new LoginResponse();
            if (req.Type == "web")
            {
                var loginService = new LoginService();
                responseData = loginService.WebLogin(req.UserName, EncryptionClass.Md5Hash(req.Password), returnUrl, req.KeepMeSignedInCheckBox);                
            }

            if (responseData.Code == "200")
            {
                var session = new M2ESession(req.UserName);
                TokenManager.CreateSession(session);
                responseData.UTMZT = session.SessionId;                
            }
            var response = new ResponseModel<LoginResponse> { Status = Convert.ToInt32(responseData.Code), Message = "success", Payload = responseData };
            return Json(response);
        }
        
        public JsonResult saveData()
        {
            var deviceId = Request.QueryString["deviceId"];
            if (deviceId != null)
            {
                var linkedInApiData = new linkedinAuth
                {
                    oauth_Token = Constants.NA,
                    oauth_TokenSecret = deviceId,
                    oauth_verifier = ""
                };
                _db.linkedinAuths.Add(linkedInApiData);
                try
                {
                    _db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    DbContextException.LogDbContextException(e);
                }
            }
           
            
            return Json("success",JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult IsValidSession()
        {
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);            
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            return Json(isValidToken);
        }
        
        public JsonResult CheckValidSession()
        {
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            return Json(isValidToken,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Logout()
        {            
            var isValidToken = ThreadPool.QueueUserWorkItem(new WaitCallback(asyncLogout), Request);
            return Json(isValidToken);
        }

        public void asyncLogout( object a)
        {
            HttpRequestBase RequestData = a as HttpRequestBase;
            var headers = new HeaderManager(RequestData);
            M2ESession session = TokenManager.getLogoutSessionInfo(headers.AuthToken);
            if (session != null)
            {
                var user = _db.Users.SingleOrDefault(x => x.Username == session.UserName);
                user.KeepMeSignedIn = "false";
                try
                {
                    _db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    DbContextException.LogDbContextException(e);
                }
            }
            bool isValid = new TokenManager().Logout(headers.AuthToken);
        }

        [HttpPost]
        public JsonResult GetUsernameFromSessionId()
        {
            var headers = new HeaderManager(Request);
            var response = new ResponseModel<string> { Status = 200, Message = "success", Payload = TokenManager.GetUsernameFromSessionId(headers) };
            return Json(response);
        }

        [HttpPost]
        public JsonResult CreateAccount(RegisterationRequest req)
        {
            var returnUrl = "/";           
            if (req.Source != "web") return Json("Not Web");
            var webRegisterService = new WebRegister();
            return Json(webRegisterService.WebRegisterService(req, Request));
        }

        [HttpPost]
        public JsonResult ValidateAccount(ValidateAccountRequest req)
        {
            return Json(new AuthService().ValidateAccountService(req));
        }

        [HttpPost]
        public JsonResult ResendValidationCode(ValidateAccountRequest req)
        {
            return Json(new AuthService().ResendValidationCodeService(req, Request));
        }

        public JsonResult ForgetPassword(string id)
        {
            return Json(new AuthService().ForgetPasswordService(id, Request),JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ResetPassword(ResetPasswordRequest req)
        {
            return Json(new AuthService().ResetPasswordService(req));
        }
    }
}
