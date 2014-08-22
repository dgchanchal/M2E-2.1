using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using M2E.Session;
using M2E.Service.UserService.Survey;
using System.Globalization;
using M2E.Models.DataWrapper.UserSurvey;
using M2E.Models;
using M2E.Service.UserService.dataEntry;

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
            //var username = "sumitchourasia91@gmail.com";
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            var userTemplateList = new UserProductSurveyTemplateService();
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {
                return Json(userTemplateList.GetAllTemplateInformation(session.UserName));
            }
            else
            {
                ResponseModel<string> response = new ResponseModel<string>();
                response.Status = 401;
                response.Message = "Unauthorized";
                return Json(response);
            }
            
           
        }

        [HttpPost]
        public JsonResult GetTemplateInformationByRefKey()
        {
            //var username = "sumitchourasia91@gmail.com";
            var refKey = Request.QueryString["refKey"].ToString(CultureInfo.InvariantCulture);            
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            var userTemplateList = new UserProductSurveyTemplateService();            
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {
                return Json(userTemplateList.GetTemplateInformationByRefKey(session.UserName, refKey));
            }
            else
            {
                ResponseModel<string> response = new ResponseModel<string>();
                response.Status = 401;
                response.Message = "Unauthorized";
                return Json(response);
            }
            

        }

        [HttpPost]
        public JsonResult GetTranscriptionTemplateInformationByRefKey()
        {
            //var username = "sumitchourasia91@gmail.com";
            var refKey = Request.QueryString["refKey"].ToString(CultureInfo.InvariantCulture);            
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            var userTemplateList = new UserTranscriptionService();   
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {
                return Json(userTemplateList.GetTranscriptionTemplateInformationByRefKey(session.UserName, refKey));
            }
            else
            {
                ResponseModel<string> response = new ResponseModel<string>();
                response.Status = 401;
                response.Message = "Unauthorized";
                return Json(response);
            }
            

        }

        [HttpPost]
        public JsonResult GetTemplateSurveyQuestionsByRefKey()
        {
            //var username = "sumitchourasia91@gmail.com";
            var refKey = Request.QueryString["refKey"].ToString(CultureInfo.InvariantCulture);
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            var userTemplateList = new UserProductSurveyTemplateService();            
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {
                return Json(userTemplateList.GetTemplateSurveyQuestionsByRefKey(session.UserName, refKey));
            }
            else
            {
                ResponseModel<string> response = new ResponseModel<string>();
                response.Status = 401;
                response.Message = "Unauthorized";
                return Json(response);
            }
            

        }

        [HttpPost]
        public JsonResult SubmitTemplateSurveyResultByRefKey(UserSurveyResultRequest surveyResult)
        {
            //var username = "sumitchourasia91@gmail.com";
            var refKey = Request.QueryString["refKey"].ToString(CultureInfo.InvariantCulture);
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            var userTemplateList = new UserProductSurveyTemplateService();            
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {
                return Json(userTemplateList.SubmitTemplateSurveyResultByRefKey(surveyResult, refKey, session.UserName));
            }
            else
            {
                ResponseModel<string> response = new ResponseModel<string>();
                response.Status = 401;
                response.Message = "Unauthorized";
                return Json(response);
            }
           

        }

        [HttpPost]
        public JsonResult AllocateThreadToUserByRefKey()
        {
            //var username = "sumitchourasia91@gmail.com";
            ResponseModel<string> response = new ResponseModel<string>();
            var refKey = Request.QueryString["refKey"].ToString(CultureInfo.InvariantCulture);
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            var userTemplateList = new UserProductSurveyTemplateService();            
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {
                response = userTemplateList.AllocateThreadToUserByRefKey(refKey, session.UserName);
            }
            else
            {                
                response.Status = 401;
                response.Message = "Unauthorized";                
            }
            return Json(response);

        }

        [HttpPost]
        public JsonResult GetUserActiveThreads()
        {
            //var username = "sumitchourasia91@gmail.com";
            var status = Request.QueryString["status"].ToString(CultureInfo.InvariantCulture);
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            var userTemplateList = new UserProductSurveyTemplateService();            
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {
                return Json(userTemplateList.GetUserActiveThreads(session.UserName, status));
            }
            else
            {
                ResponseModel<string> response = new ResponseModel<string>();
                response.Status = 401;
                response.Message = "Unauthorized";
                return Json(response);
            }
            

        }
    }
}
