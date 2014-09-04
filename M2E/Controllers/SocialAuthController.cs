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
using M2E.signalRPushNotifications;
using Microsoft.AspNet.SignalR;
using M2E.Models.Constants;
using M2E.Service.SocialNetwork.linkedin;
using System.Xml;
using Newtonsoft.Json;
using M2E.Models.DataWrapper;
using System.Net;
using System.IO;
using M2E.Service.SocialNetwork.google;
using System.Text;

namespace M2E.Controllers
{
    public class SocialAuthController : Controller
    {
        //
        // GET: /SocialAuth/
        private static readonly ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        private DbContextException _dbContextException = new DbContextException();
        private readonly M2EContext _db = new M2EContext();
        private oAuthLinkedIn _oauth = new oAuthLinkedIn();

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
            String refKey = Request.QueryString["refKey"];
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
                        response.Status = 209;
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
                if (ifFacebookUserAlreadyRegistered.username != Constants.NA)
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

                    var fb = new FacebookClient(ifFacebookUserAlreadyRegistered.AuthToken);
                    dynamic result = fb.Get("fql",
                                new { q = "SELECT uid, first_name, last_name, sex, pic_big_with_logo, username FROM user WHERE uid=me()" });
                    
                    var guid = Guid.NewGuid().ToString();
                    var user = new User
                    {
                        Username = result.data[0].username+"@facebook.com",
                        Password = EncryptionClass.Md5Hash(Guid.NewGuid().ToString()),
                        Source = "facebook",
                        isActive = "true",
                        Type = "user",
                        guid = Guid.NewGuid().ToString(),
                        FirstName = result.data[0].first_name,
                        LastName = result.data[0].last_name,
                        gender = result.data[0].sex,
                        ImageUrl = result.data[0].pic_big_with_logo
                    };
                    _db.Users.Add(user);

                    if (!string.IsNullOrEmpty(refKey))
                    {
                        var dbRecommedBy = new RecommendedBy
                        {
                            RecommendedFrom = refKey,
                            RecommendedTo = user.Username
                        };
                        _db.RecommendedBies.Add(dbRecommedBy);
                    }
                                                           
                    try
                    {
                        ifFacebookUserAlreadyRegistered.username = user.Username;
                        _db.SaveChanges();
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
                            var session = new M2ESession(ifFacebookUserAlreadyRegistered.username);
                            TokenManager.CreateSession(session);
                            response.Payload.UTMZT = session.SessionId;                            
                        }
                        catch (DbEntityValidationException e)
                        {
                            DbContextException.LogDbContextException(e);
                            response.Status = 500;
                            response.Message = "Internal Server Error !!";
                        }
                        var signalRHub = new SignalRHub();
                        string totalProjects = "";
                        string successRate = "";
                        string totalUsers = _db.Users.Count().ToString(CultureInfo.InvariantCulture);
                        string projectCategories = "";
                        var hubContext = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>();
                        hubContext.Clients.All.updateBeforeLoginUserProjectDetails(totalProjects, successRate, totalUsers, projectCategories);                        
                    }
                    catch (DbEntityValidationException e)
                    {
                        DbContextException.LogDbContextException(e);
                        response.Status = 500;
                        response.Message = "Internal Server Error !!!";                        
                    }

                }
                
            }
            
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GoogleLoginGetRedirectUri(string type)
        {
            var response = new ResponseModel<LoginResponse>();
            String code = Request.QueryString["code"];
            String refKey = Request.QueryString["refKey"];
            string app_id = "";
            string app_secret = "";

            if (Request.Url.Authority.Contains("localhost"))
            {
                app_id = ConfigurationManager.AppSettings["googleAppID"].ToString();
                app_secret = ConfigurationManager.AppSettings["googleAppSecret"].ToString();
            }
            else
            {
                app_id = ConfigurationManager.AppSettings["googleAppIDCautom"].ToString();
                app_secret = ConfigurationManager.AppSettings["googleAppSecretCautom"].ToString();
            }

            string scope = "email%20profile";
            string returnUrl = "http://" + Request.Url.Authority + "/SocialAuth/GoogleLogin";
            if (code == null)
            {
                response.Status = 199;
                response.Message = (string.Format(
                    "https://accounts.google.com/o/oauth2/auth?scope={0}&state=%2Fprofile&redirect_uri={1}&response_type=code&client_id={2}&approval_prompt=force",
                    scope, returnUrl, app_id));
                //Response.Redirect(ReturnUrl);
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GoogleLogin(string type)
        {
            var response = new ResponseModel<LoginResponse>();
            String code = Request.QueryString["code"];
            String refKey = Request.QueryString["refKey"];
            string app_id = "";
            string app_secret = "";
            
            if (Request.Url.Authority.Contains("localhost"))
            {
                app_id = ConfigurationManager.AppSettings["googleAppID"].ToString();
                app_secret = ConfigurationManager.AppSettings["googleAppSecret"].ToString();
            }
            else
            {
                app_id = ConfigurationManager.AppSettings["googleAppIDCautom"].ToString();
                app_secret = ConfigurationManager.AppSettings["googleAppSecretCautom"].ToString();
            }

            string scope = "email%20profile";
            string returnUrl = "http://" + Request.Url.Authority + "/SocialAuth/GoogleLogin";
            if (code == null)
            {
                var ReturnUrl = (string.Format(
                    "https://accounts.google.com/o/oauth2/auth?scope={0}&state=%2Fprofile&redirect_uri={1}&response_type=code&client_id={2}&approval_prompt=force",
                    scope, returnUrl, app_id));                
                Response.Redirect(ReturnUrl);
            }
            else
            {
                string access_token = getGoogleAuthToken(returnUrl, scope, code, app_id, app_secret);
                String URI = "https://www.googleapis.com/oauth2/v1/userinfo?access_token=" + access_token;                
                WebClient webClient = new WebClient();
                Stream stream = webClient.OpenRead(URI);
                string googleUserDetailString;

                /*I have not used any JSON parser because I do not want to use any extra dll/3rd party dll*/
                using (StreamReader br = new StreamReader(stream))
                {
                    googleUserDetailString = br.ReadToEnd();
                }
                var googleUserDetails = JsonConvert.DeserializeObject<googleUserDetails>(Convert.ToString(googleUserDetailString));
                var ifUserAlreadyRegistered = _db.Users.SingleOrDefault(x => x.Username == googleUserDetails.email);
                if (ifUserAlreadyRegistered != null)
                {
                    string Authkey = ConfigurationManager.AppSettings["AuthKey"];
                    response.Payload = new LoginResponse();
                    response.Payload.UTMZK = EncryptionClass.GetEncryptionKey(ifUserAlreadyRegistered.Username, Authkey);
                    response.Payload.UTMZV = EncryptionClass.GetEncryptionKey(ifUserAlreadyRegistered.Password, Authkey);
                    response.Payload.TimeStamp = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                    response.Payload.Code = "210";
                    response.Status = 210;
                    response.Message = "user Login via google";
                    try
                    {
                        ifUserAlreadyRegistered.KeepMeSignedIn = "true";//keepMeSignedIn.Equals("true", StringComparison.OrdinalIgnoreCase) ? "true" : "false";
                        _db.SaveChanges();

                        var session = new M2ESession(ifUserAlreadyRegistered.Username);
                        TokenManager.CreateSession(session);
                        response.Payload.UTMZT = session.SessionId;
                        ViewBag.umtzt = response.Payload.UTMZT;
                        ViewBag.umtzk = response.Payload.UTMZK;
                        ViewBag.umtzv = response.Payload.UTMZV;
                        return View();

                    }
                    catch (DbEntityValidationException e)
                    {
                        DbContextException.LogDbContextException(e);
                        response.Payload.Code = "500";

                        return Json(response, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    //add user to database.

                    var guid = Guid.NewGuid().ToString();

                    if (googleUserDetails.picture == null || googleUserDetails.picture == "") googleUserDetails.picture = Constants.NA; // if picture is not available.
                    if (googleUserDetails.gender == null || googleUserDetails.gender == "") googleUserDetails.gender = Constants.NA; // if picture is not available.

                    var user = new User
                    {
                        Username = googleUserDetails.email,
                        Password = EncryptionClass.Md5Hash(Guid.NewGuid().ToString()),
                        Source = "google",
                        isActive = "true",
                        Type = "user",
                        guid = Guid.NewGuid().ToString(),
                        FirstName = googleUserDetails.given_name,
                        LastName = googleUserDetails.family_name,
                        gender = googleUserDetails.gender,
                        ImageUrl = googleUserDetails.picture
                    };
                    _db.Users.Add(user);

                    if (!string.IsNullOrEmpty(refKey))
                    {
                        var dbRecommedBy = new RecommendedBy
                        {
                            RecommendedFrom = refKey,
                            RecommendedTo = user.Username
                        };
                        _db.RecommendedBies.Add(dbRecommedBy);
                    }

                    try
                    {
                        _db.SaveChanges();
                        string Authkey = ConfigurationManager.AppSettings["AuthKey"];
                        response.Payload = new LoginResponse();
                        response.Payload.UTMZK = EncryptionClass.GetEncryptionKey(user.Username, Authkey);
                        response.Payload.UTMZV = EncryptionClass.GetEncryptionKey(user.Password, Authkey);
                        response.Payload.TimeStamp = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        response.Payload.Code = "210";
                        response.Status = 210;
                        response.Message = "user Login via google";
                        try
                        {
                            var session = new M2ESession(user.Username);
                            TokenManager.CreateSession(session);
                            response.Payload.UTMZT = session.SessionId;

                            ViewBag.umtzt = response.Payload.UTMZT;
                            ViewBag.umtzk = response.Payload.UTMZK;
                            ViewBag.umtzv = response.Payload.UTMZV;
                            return View();
                        }
                        catch (DbEntityValidationException e)
                        {
                            DbContextException.LogDbContextException(e);
                            response.Status = 500;
                            response.Message = "Internal Server Error !!";
                        }
                        var signalRHub = new SignalRHub();
                        string totalProjects = "";
                        string successRate = "";
                        string totalUsers = _db.Users.Count().ToString(CultureInfo.InvariantCulture);
                        string projectCategories = "";
                        var hubContext = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>();
                        hubContext.Clients.All.updateBeforeLoginUserProjectDetails(totalProjects, successRate, totalUsers, projectCategories);
                    }
                    catch (DbEntityValidationException e)
                    {
                        DbContextException.LogDbContextException(e);
                        response.Status = 500;
                        response.Message = "Internal Server Error !!!";
                    }
                }
            }
            return Json(response,JsonRequestBehavior.AllowGet);
        }

        public ActionResult LinkedinLoginGetRedirectUri(string type)
        {
            var response = new ResponseModel<LoginResponse>();

            String AbsoluteUri = Request.Url.AbsoluteUri;
            string oauth_token = Request.QueryString["oauth_token"];
            string oauth_verifier = Request.QueryString["oauth_verifier"];
            String refKey = Request.QueryString["refKey"];
            string authLink = string.Empty;
            if (oauth_token == null || oauth_verifier == null)
            {
                authLink = CreateAuthorization();
                var linkedInApiData = new linkedinAuth
                {
                    oauth_Token = _oauth.Token,
                    oauth_TokenSecret = _oauth.TokenSecret,
                    oauth_verifier = ""
                };
                _db.linkedinAuths.Add(linkedInApiData);
                try
                {
                    _db.SaveChanges();
                    response.Status = 199;
                    response.Message = authLink;
                    //Response.Redirect(authLink);
                }
                catch (DbEntityValidationException e)
                {
                    DbContextException.LogDbContextException(e);
                    response.Status = 500;
                    response.Message = "Internal Server Error !!!";
                }
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LinkedinLogin(string type)
        {
            var response = new ResponseModel<LoginResponse>();

            String AbsoluteUri = Request.Url.AbsoluteUri;
            string oauth_token = Request.QueryString["oauth_token"];
            string oauth_verifier = Request.QueryString["oauth_verifier"];
            String refKey = Request.QueryString["refKey"];
            string authLink = string.Empty;
            if (oauth_token != null && oauth_verifier != null)
            {
                var linkedinApiDataResponse = _db.linkedinAuths.SingleOrDefault(x => x.oauth_Token == oauth_token);
                if (linkedinApiDataResponse != null)
                {
                    GetAccessToken(oauth_token, linkedinApiDataResponse.oauth_TokenSecret, oauth_verifier);
                    String UserDetailString = RequestProfile(_oauth.Token, _oauth.TokenSecret, oauth_verifier);
                    var linkedinUserDetails = JsonConvert.DeserializeObject<linkedinUserDataWrapper>(Convert.ToString(UserDetailString));
                    _db.linkedinAuths.Attach(linkedinApiDataResponse);
                    _db.linkedinAuths.Remove(linkedinApiDataResponse);
                    var ifUserAlreadyRegistered = _db.Users.SingleOrDefault(x => x.Username == linkedinUserDetails.emailAddress);
                    if (ifUserAlreadyRegistered != null)
                    {
                        string Authkey = ConfigurationManager.AppSettings["AuthKey"];
                        response.Payload = new LoginResponse();
                        response.Payload.UTMZK = EncryptionClass.GetEncryptionKey(ifUserAlreadyRegistered.Username, Authkey);
                        response.Payload.UTMZV = EncryptionClass.GetEncryptionKey(ifUserAlreadyRegistered.Password, Authkey);
                        response.Payload.TimeStamp = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        response.Payload.Code = "210";
                        response.Status = 210;
                        response.Message = "user Login via facebook";
                        try
                        {
                            ifUserAlreadyRegistered.KeepMeSignedIn = "true";//keepMeSignedIn.Equals("true", StringComparison.OrdinalIgnoreCase) ? "true" : "false";
                            _db.SaveChanges();

                            var session = new M2ESession(ifUserAlreadyRegistered.Username);
                            TokenManager.CreateSession(session);
                            response.Payload.UTMZT = session.SessionId;
                            ViewBag.umtzt = response.Payload.UTMZT;
                            ViewBag.umtzk = response.Payload.UTMZK;
                            ViewBag.umtzv = response.Payload.UTMZV;
                            return View();

                        }
                        catch (DbEntityValidationException e)
                        {
                            DbContextException.LogDbContextException(e);
                            response.Payload.Code = "500";

                            return Json(response, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        //add user to database.

                        var guid = Guid.NewGuid().ToString();
                        
                        if (linkedinUserDetails.pictureUrl == null || linkedinUserDetails.pictureUrl == "") linkedinUserDetails.pictureUrl = Constants.NA; // if picture is not available.
                        
                        var user = new User
                        {
                            Username = linkedinUserDetails.emailAddress,
                            Password = EncryptionClass.Md5Hash(Guid.NewGuid().ToString()),
                            Source = "linkedin",
                            isActive = "true",
                            Type = "user",
                            guid = Guid.NewGuid().ToString(),
                            FirstName = linkedinUserDetails.firstName,
                            LastName = linkedinUserDetails.lastName,
                            gender = Constants.NA,
                            ImageUrl = linkedinUserDetails.pictureUrl
                        };
                        _db.Users.Add(user);

                        if (!string.IsNullOrEmpty(refKey))
                        {
                            var dbRecommedBy = new RecommendedBy
                            {
                                RecommendedFrom = refKey,
                                RecommendedTo = user.Username
                            };
                            _db.RecommendedBies.Add(dbRecommedBy);
                        }

                        try
                        {                            
                            _db.SaveChanges();
                            string Authkey = ConfigurationManager.AppSettings["AuthKey"];
                            response.Payload = new LoginResponse();
                            response.Payload.UTMZK = EncryptionClass.GetEncryptionKey(user.Username, Authkey);
                            response.Payload.UTMZV = EncryptionClass.GetEncryptionKey(user.Password, Authkey);
                            response.Payload.TimeStamp = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                            response.Payload.Code = "210";
                            response.Status = 210;
                            response.Message = "user Login via linkedin";
                            try
                            {
                                var session = new M2ESession(user.Username);
                                TokenManager.CreateSession(session);
                                response.Payload.UTMZT = session.SessionId;

                                ViewBag.umtzt = response.Payload.UTMZT;
                                ViewBag.umtzk = response.Payload.UTMZK;
                                ViewBag.umtzv = response.Payload.UTMZV;
                                return View();
                            }
                            catch (DbEntityValidationException e)
                            {
                                DbContextException.LogDbContextException(e);
                                response.Status = 500;
                                response.Message = "Internal Server Error !!";
                            }
                            var signalRHub = new SignalRHub();
                            string totalProjects = "";
                            string successRate = "";
                            string totalUsers = _db.Users.Count().ToString(CultureInfo.InvariantCulture);
                            string projectCategories = "";
                            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>();
                            hubContext.Clients.All.updateBeforeLoginUserProjectDetails(totalProjects, successRate, totalUsers, projectCategories);
                        }
                        catch (DbEntityValidationException e)
                        {
                            DbContextException.LogDbContextException(e);
                            response.Status = 500;
                            response.Message = "Internal Server Error !!!";
                        }
                    }
                }
            }
            else
            {
                authLink = CreateAuthorization();
                var linkedInApiData = new linkedinAuth
                {
                    oauth_Token = _oauth.Token,
                    oauth_TokenSecret = _oauth.TokenSecret,
                    oauth_verifier = ""
                };
                _db.linkedinAuths.Add(linkedInApiData);
                try
                {
                    _db.SaveChanges();
                    Response.Redirect(authLink);
                }
                catch (DbEntityValidationException e)
                {
                    DbContextException.LogDbContextException(e);
                    response.Status = 500;
                    response.Message = "Internal Server Error !!!";
                }                
                                
            }
            ViewBag.code = response.Status;            
            return View();
        }

        public ActionResult LinkedinLoginCancelled(string type)
        {
            var response = new ResponseModel<LoginResponse>();

            String AbsoluteUri = Request.Url.AbsoluteUri;
            string oauth_token = Request.QueryString["oauth_token"];
            string oauth_verifier = Request.QueryString["oauth_verifier"];
            string authLink = string.Empty;
            if (oauth_token != null && oauth_verifier != null)
            {

            }
            else
            {
                authLink = CreateAuthorization();               
                Response.Redirect(authLink);
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FBLoginGetRedirectUri(string type)
        {
            var response = new ResponseModel<string>();

            String code = Request.QueryString["code"];
            string app_id = string.Empty;
            string app_secret = string.Empty;
            string returnUrl = "http://" + Request.Url.Authority + "/SocialAuth/FBLogin/facebook/";
            if (Request.Url.Authority.Contains("localhost"))
            {
                app_id = ConfigurationManager.AppSettings["FacebookAppID"].ToString();
                app_secret = ConfigurationManager.AppSettings["FacebookAppSecret"].ToString();
            }
            else
            {
                app_id = ConfigurationManager.AppSettings["FacebookAppIDCautom"].ToString();
                app_secret = ConfigurationManager.AppSettings["FacebookAppSecretCautom"].ToString();
            }
            string scope = "";
            if (code == null)
            {
                response.Status = 199;
                response.Message = (string.Format(
                    "https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri={1}&scope={2}",
                    app_id, returnUrl, scope));
                
                //Response.Redirect(response.Payload);
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
            if(Request.Url.Authority.Contains("localhost"))
            {
                app_id = ConfigurationManager.AppSettings["FacebookAppID"].ToString();
                app_secret = ConfigurationManager.AppSettings["FacebookAppSecret"].ToString();
            }
            else
            {
                app_id = ConfigurationManager.AppSettings["FacebookAppIDCautom"].ToString();
                app_secret = ConfigurationManager.AppSettings["FacebookAppSecretCautom"].ToString();
            }
            

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
                    FacebookAuthData.username = Constants.NA;
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

        protected string CreateAuthorization()
        {
            return _oauth.AuthorizationLinkGet();
        }

        protected void GetAccessToken(string Auth_token, string TokenSecret, string Auth_verifier)
        {
            _oauth.Token = Auth_token;
            _oauth.TokenSecret = TokenSecret;
            _oauth.Verifier = Auth_verifier;
            _oauth.AccessTokenGet(Auth_token);
        }

        protected void SendStatusUpdate(string AccessToken, string AccessTokenSecret, string Auth_verifier)
        {
            _oauth.Token = AccessToken;
            _oauth.TokenSecret = AccessTokenSecret;
            _oauth.Verifier = Auth_verifier;

            string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xml += "<current-status>It's really working .</current-status>";

            string response = _oauth.APIWebRequest("PUT", "http://api.linkedin.com/v1/people/~/current-status", xml);
            //if (response == "")
            //    txtApiResponse.Text = "Your new status updated";

        }

        protected string RequestProfile(string AccessToken, string AccessTokenSecret, string Auth_verifier)
        {
            _oauth.Token = AccessToken;
            _oauth.TokenSecret = AccessTokenSecret;
            _oauth.Verifier = Auth_verifier;
            return _oauth.APIWebRequest("GET", "https://api.linkedin.com/v1/people/~:(id,first-name,last-name,industry,email-address,picture-url)?format=json", null);
        }

        protected string RequestProfileImage(string AccessToken, string AccessTokenSecret, string Auth_verifier)
        {
            _oauth.Token = AccessToken;
            _oauth.TokenSecret = AccessTokenSecret;
            _oauth.Verifier = Auth_verifier;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(_oauth.APIWebRequest("GET", "http://api.linkedin.com/v1/people/~/picture-urls::(original)", null));
            return JsonConvert.SerializeXmlNode(doc).Replace(@"@", @"").Remove(1, 44);
        }

        private string getGoogleAuthToken(string returnUrl, string scope, string code, string app_id, string app_secret)
        {
            byte[] buffer = Encoding.ASCII.GetBytes("code=" + code + "&client_id=" + app_id + "&client_secret=" + app_secret + "&redirect_uri=" + returnUrl + "&grant_type=authorization_code");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://accounts.google.com/o/oauth2/token");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = buffer.Length;

            Stream strm = request.GetRequestStream();
            strm.Write(buffer, 0, buffer.Length);
            strm.Close();

            HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse();
            Stream responseStream = webResponse.GetResponseStream();
            StreamReader responseStreamReader = new StreamReader(responseStream);
            string result = responseStreamReader.ReadToEnd();//parse token from result
            var googleAccessTokenResponse = JsonConvert.DeserializeObject<googleAccessTokenWrapper>(Convert.ToString(result));
            return googleAccessTokenResponse.access_token;
        }

        
    }
}
