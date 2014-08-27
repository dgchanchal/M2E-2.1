using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        [HttpPost]
        public JsonResult FBLogin(string type)
        {

            return Json("");
        }
    }
}
