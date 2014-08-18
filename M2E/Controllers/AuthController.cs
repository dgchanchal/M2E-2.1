using System.Globalization;
using M2E.Common.Logger;
using M2E.Models;
using System;
using System.Linq;
using M2E.Encryption;
using System.Web.Mvc;
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

        [HttpPost]
        public JsonResult IsValidSession(isValidSessionRequest req)
        {
            var response = new ResponseModel<bool> { Status = 200, Message = "success", Payload = TokenManager.IsValidSession(req.UTMZT) };
            return Json(response);
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
            var referral = Request.QueryString["ref"];
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
