using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using M2E.Session;
using M2E.Service.UserService.Survey;
using System.Globalization;
using M2E.Models.DataWrapper.UserSurvey;

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

        [HttpPost]
        public JsonResult SubmitTemplateSurveyResultByRefKey(UserSurveyResultRequest surveyResult)
        {
            var username = "sumitchourasia91@gmail.com";
            var refKey = Request.QueryString["refKey"].ToString(CultureInfo.InvariantCulture);
            var headers = new HeaderManager(Request);
            var userTemplateList = new UserProductSurveyTemplateService();
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            return Json(userTemplateList.SubmitTemplateSurveyResultByRefKey(surveyResult, refKey, username));

        }

        [HttpPost]
        public JsonResult AllocateThreadToUserByRefKey()
        {
            var username = "sumitchourasia91@gmail.com";
            var refKey = Request.QueryString["refKey"].ToString(CultureInfo.InvariantCulture);
            var headers = new HeaderManager(Request);
            var userTemplateList = new UserProductSurveyTemplateService();
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            return Json(userTemplateList.AllocateThreadToUserByRefKey(refKey, username));

        }

        [HttpPost]
        public JsonResult GetUserActiveThreads()
        {
            var username = "sumitchourasia91@gmail.com";
            var status = Request.QueryString["status"].ToString(CultureInfo.InvariantCulture);
            var headers = new HeaderManager(Request);
            var userTemplateList = new UserProductSurveyTemplateService();
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            return Json(userTemplateList.GetUserActiveThreads(username, status));

        }
    }
}
