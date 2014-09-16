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
using Newtonsoft.Json;
using M2E.Service.UserService.Moderation;
using M2E.Service.UserService.facebookLike;
using M2E.Models.Constants;

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
        public JsonResult GetAllFacebookLikeTemplateInformation()
        {
            //var username = "sumitchourasia91@gmail.com";
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            var UserfacebookLikesList = new UserfacebookLikeServices();
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {
                var response = UserfacebookLikesList.GetAllFacebookLikeTemplateInformation(session.UserName);
                return Json(response);
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
        public JsonResult ValidateFacebookLike(string refKey)
        {
            //var username = "sumitchourasia91@gmail.com";
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            var UserfacebookLikesList = new UserfacebookLikeServices();
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {
                var response = UserfacebookLikesList.ValidateFacebookLike(session.UserName, refKey);
                return Json(response);
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
        public JsonResult GetImageModerationTemplateInformationByRefKey()
        {
            //var username = "sumitchourasia91@gmail.com";
            var refKey = Request.QueryString["refKey"].ToString(CultureInfo.InvariantCulture);
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            var userTemplateList = new UserTranscriptionService();
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {
                return Json(userTemplateList.GetMultipleImageModerationTemplateInformationByRefKey(session.UserName, refKey));
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
        public JsonResult SubmitTranscriptionInputTableDataByRefKey(List<string[]> data)
        {
            //var username = "sumitchourasia91@gmail.com";
            var refKey = Request.QueryString["refKey"].ToString(CultureInfo.InvariantCulture);
            var UserResponse = new List<string[]>();
            foreach (var row in data)
            {
                bool useful = false;
                foreach (var inputBoxData in row)
                {
                    if (inputBoxData != null && inputBoxData != "")
                    {
                        useful = true;
                        break;
                    }
                }
                if (useful)
                    UserResponse.Add(row);
            }
            var serializeData = JsonConvert.SerializeObject(UserResponse);
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            var userTemplateList = new UserTranscriptionService();
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {
                return Json(userTemplateList.SubmitTranscriptionInputTableDataByRefKey(session.UserName, refKey, serializeData));
                return null;
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
        public JsonResult SubmitImageModerationInputTableDataByRefKey(List<UserSubmitImageModerationResult> res)
        {
            //var username = "sumitchourasia91@gmail.com";
            var refKey = Request.QueryString["refKey"].ToString(CultureInfo.InvariantCulture);                   
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            var UserImageModeration = new UserImageModeration();
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {
                return Json(UserImageModeration.SubmitMultipleImageModerationInputTableDataByRefKey(session.UserName, refKey, res));                
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
                return Json(userTemplateList.GetTemplateSurveyQuestionsByRefKey(refKey));
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
