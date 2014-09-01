using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using M2E.Models;
using zestork.Service;
using Facebook;
using M2E.Common.Logger;
using System.Reflection;
using M2E.CommonMethods;
using System.Data.Entity.Validation;
using M2E.Session;
using M2E.Encryption;
using M2E.Models.DataResponse;
using System.Globalization;

namespace M2E.Controllers
{
    public class SocialAuthController : Controller
    {
        //
        // GET: /SocialAuth/
        private static readonly ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        private DbContextException _dbContextException = new DbContextException();
        private readonly M2EContext _db = new M2EContext();

        public ActionResult Index()
        {
            return View();
        }
        
        public JsonResult Login(string type)
        {            
            return Json("test",JsonRequestBehavior.AllowGet);
        }

        public JsonResult userMapping()
        {
            var response = new ResponseModel<LoginResponse>();

            String fid = Request.QueryString["fid"];
            var headers = new HeaderManager(Request);
            if (headers.AuthToken != null)
            {
                M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
                var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
                if (isValidToken)
                {
                    var facebookUserMap = _db.FacebookAuths.SingleOrDefault(x => x.facebookId == fid);
                    facebookUserMap.username = session.UserName;
                    try
                    {
                        _db.SaveChanges();
                        response.Status = 200;
                        response.Message = "success-";
                    }
                    catch (DbEntityValidationException e)
                    {
                        DbContextException.LogDbContextException(e);
                        response.Status = 500;
                        response.Message = "Failed";
                    }
                }
            }
            else
            {
                var ifFacebookUserAlreadyRegistered = _db.FacebookAuths.SingleOrDefault(x => x.facebookId == fid);
                if (ifFacebookUserAlreadyRegistered != null)
                {
                    if (_db.Users.Any(x => x.Username == ifFacebookUserAlreadyRegistered.username))
                    {
                        var user = _db.Users.SingleOrDefault(x => x.Username == ifFacebookUserAlreadyRegistered.username);
                        if (user != null)
                        {
                            string Authkey = ConfigurationManager.AppSettings["AuthKey"];
                            response.Payload = new LoginResponse();
                            response.Payload.UTMZK = EncryptionClass.GetEncryptionKey(user.Username, Authkey);
                            response.Payload.UTMZV = EncryptionClass.GetEncryptionKey(user.Password, Authkey);
                            response.Payload.TimeStamp = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                            response.Payload.Code = "210";
                            response.Status = 210;
                            response.Message = "user Login via facebook";
                            try
                            {
                                user.KeepMeSignedIn = "true";//keepMeSignedIn.Equals("true", StringComparison.OrdinalIgnoreCase) ? "true" : "false";
                                _db.SaveChanges();

                                var session = new M2ESession(ifFacebookUserAlreadyRegistered.username);
                                TokenManager.CreateSession(session);
                                response.Payload.UTMZT = session.SessionId;
                                return Json(response, JsonRequestBehavior.AllowGet);
                                
                            }
                            catch (DbEntityValidationException e)
                            {
                                DbContextException.LogDbContextException(e);
                                response.Payload.Code = "500";
                                
                                return Json(response, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                            response.Payload.Code = "403";
                    }
                }
                else
                {
                    //save user details in database ..
                }
                
            }
            
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FBLogin(string type)
        {
            var response = new ResponseModel<string>();

            String code = Request.QueryString["code"];
            string app_id = string.Empty;
            string app_secret = string.Empty;
            string returnUrl = "http://"+Request.Url.Authority+"/SocialAuth/FBLogin/facebook/";
            app_id = ConfigurationManager.AppSettings["FacebookAppID"].ToString();
            app_secret = ConfigurationManager.AppSettings["FacebookAppSecret"].ToString();

            string scope = "";
            if (code == null)
            {
                response.Status = 199;
                response.Message = "reload page with given url";
                response.Payload = (string.Format(
                    "https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri={1}&scope={2}",
                    app_id, returnUrl, scope));
                
                //return Json(response,JsonRequestBehavior.AllowGet);
                Response.Redirect(response.Payload);
            }
            else
            {
                try
                {
                    string access_token = new FacebookService().getFacebookAuthToken(returnUrl, scope, code, app_id, app_secret);
                    var fb = new FacebookClient(access_token);
                    //dynamic result = fb.Get("fql",
                    //            new { q = "SELECT uid, name, first_name, middle_name, last_name, sex, locale, pic_small_with_logo, pic_big_with_logo, pic_square_with_logo, pic_with_logo, username FROM user WHERE uid=me()" });

                    dynamic fqlResponse = fb.Get("fql",
                                new { q = "SELECT uid, username FROM user WHERE uid=me()" });
                    var FacebookAuthData = new FacebookAuth();
                    string fid = Convert.ToString(fqlResponse.data[0].uid);
                    FacebookAuthData.username = "NA";
                    FacebookAuthData.AuthToken = access_token;
                    FacebookAuthData.datetime = DateTime.Now.ToString();
                    FacebookAuthData.facebookId = Convert.ToString(fqlResponse.data[0].uid);
                    FacebookAuthData.facebookUsername = fqlResponse.data[0].username;

                    var ifAlreadyExists = _db.FacebookAuths.SingleOrDefault(x => x.facebookId == fid);
                    if (ifAlreadyExists == null)
                    {                        
                        _db.FacebookAuths.Add(FacebookAuthData);
                    }
                    else
                    {
                        // refresh the token
                        ifAlreadyExists.AuthToken = access_token;
                        ifAlreadyExists.datetime = DateTime.Now.ToString();
                    }
                    try
                    {
                        _db.SaveChanges();                        
                        response.Status = 200;
                        response.Message = "success-";                        
                    }
                    catch (DbEntityValidationException e)
                    {
                        DbContextException.LogDbContextException(e);
                        response.Status = 500;
                        response.Message = "Failed";                        
                    }

                    ViewBag.facebookId = fqlResponse.data[0].uid;
                    return View(FacebookAuthData);
                }
                catch (Exception ex)
                {

                    logger.Error("Error Occured while getting Facebook Auth Token",ex);
                }
                
                //var fb = new FacebookClient();
                //dynamic result = fb.Get("fql",
                    //new { q = "SELECT page_id FROM page_fan WHERE uid=100001648098091 AND page_id=223215721036909" });  
                
 
                //To obtain an App Access Token, invoke the following HTTP GET request

                //GET https://graph.facebook.com/oauth/access_token?
                //            client_id=YOUR_APP_ID
                //           &client_secret=YOUR_APP_SECRET
                //           &grant_type=client_credentials

                //The API will respond with a query-string formatted string of the form:

                //    access_token=YOUR_APP_ACCESS_TOKEN
            }
            return View();
        }

        
    }
}
