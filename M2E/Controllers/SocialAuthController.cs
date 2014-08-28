using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using M2E.Models;
using zestork.Service;
using Facebook;

namespace M2E.Controllers
{
    public class SocialAuthController : Controller
    {
        //
        // GET: /SocialAuth/

        public ActionResult Index()
        {
            return View();
        }
        
        public JsonResult Login(string type)
        {
            

            return Json("test",JsonRequestBehavior.AllowGet);
        }

        public JsonResult FBLogin(string type)
        {
            var response = new ResponseModel<string>();

            String code = Request.QueryString["code"];
            string app_id = string.Empty;
            string app_secret = string.Empty;
            string returnUrl = "http://localhost:8111/SocialAuth/FBLogin/facebook/";
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
                
                return Json(response,JsonRequestBehavior.AllowGet);
            }
            else
            {
                string access_token = new FacebookService().getFacebookAuthToken(returnUrl, scope, code, app_id, app_secret);
                var fb = new FacebookClient(access_token);
                //var fb = new FacebookClient();
                dynamic result = fb.Get("fql",
                    new { q = "SELECT page_id FROM page_fan WHERE uid=100001648098091 AND page_id=223215721036909" });

            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        
    }
}
