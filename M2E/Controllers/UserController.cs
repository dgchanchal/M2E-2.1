using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using M2E.Session;
using M2E.Service.UserService.Survey;
using System.Globalization;

namespace M2E.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetAllTemplateInformation()
        {
            var username = "sumitchourasia91@gmail.com";
            var headers = new HeaderManager(Request);
            var userTemplateList = new UserProductSurveyTemplateService();
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            return Json(userTemplateList.GetAllTemplateInformation(username));
           
        }

        [HttpPost]
        public JsonResult GetTemplateInformationByRefKey()
        {
            var username = "sumitchourasia91@gmail.com";
            var refKey = Request.QueryString["refKey"].ToString(CultureInfo.InvariantCulture);
            var headers = new HeaderManager(Request);
            var userTemplateList = new UserProductSurveyTemplateService();
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            return Json(userTemplateList.GetTemplateInformationByRefKey(username, refKey));

        }

        [HttpPost]
        public JsonResult GetTemplateSurveyQuestionsByRefKey()
        {
            var username = "sumitchourasia91@gmail.com";
            var refKey = Request.QueryString["refKey"].ToString(CultureInfo.InvariantCulture);
            var headers = new HeaderManager(Request);
            var userTemplateList = new UserProductSurveyTemplateService();
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            return Json(userTemplateList.GetTemplateSurveyQuestionsByRefKey(username, refKey));

        }
    }
}
