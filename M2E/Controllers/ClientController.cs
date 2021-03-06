﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using M2E.Common.Logger;
using System.Reflection;
using M2E.Models.Constants;
using M2E.Models.DataResponse;
using M2E.Models.DataWrapper;
using M2E.Models;
using M2E.CommonMethods;
using M2E.Service.JobTemplate;
using M2E.Service.Client;
using M2E.Service.Notifications;
using M2E.Service.QuartzSchedulerService;
using M2E.Session;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using M2E.Encryption;
using M2E.signalRPushNotifications;
using Microsoft.AspNet.SignalR;
using Quartz;
using Quartz.Impl;

namespace M2E.Controllers
{
    public class ClientController : Controller
    {
        //
        // GET: /Client/        
        private static readonly ILogger Logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        private DbContextException _dbContextException = new DbContextException();
        private readonly M2EContext _db = new M2EContext();
        public ActionResult Index()
        {
            //Logger.Info("Client Controller index page");
            //var hubContext = GlobalHost.ConnectionManager.GetHubContext<SignalRClientHub>();
            //hubContext.Clients.All.updateUserNotificationMessage("user", "#", "../../Template/AdminLTE-master/img/avatar5.png", "signalR Title", "5 mins ago", "signalR Content..");
            //new UserMessageService().SendUserNotificationMessageAsync("admin","sumitchourasia91@gmail.com",Constants.userType_user,"message title","message body",DateTime.Now,Constants.avatar3);
            //new UserNotificationService().SendUserNotificationAsync("admin","sumitchourasia91@gmail.com",Constants.userType_user,"this is notification title",DateTime.Now,Constants.CSSImage_info);
            //new QuartzSchedulerService("sumitchourasia91@gmail.com","ref1234567890 "+DateTime.Now.Ticks);
            return View();
        }

        [HttpPost]
        public JsonResult GetAllTemplateInformation()
        {
            //var username = "sumitchourasia91@gmail.com";            
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            var clientTemplate = new ClientTemplateService();
            var isValidToken= TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {
                return Json(clientTemplate.GetAllTemplateInformation(session.UserName));
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
        public JsonResult MobileApiDecryptData()
        {
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            var clientTemplate = new ClientTemplateService();
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {
                string Authkey = ConfigurationManager.AppSettings["AuthKey"];

                string username = EncryptionClass.GetDecryptionValue(headers.AuthKey, Authkey);
                var dbUserInfo = _db.Users.SingleOrDefault(x=>x.Username == username);                               
                var data = new Dictionary<string, string>();                    
                data["Password"] = headers.AuthValue;
                data["userGuid"] = dbUserInfo.guid;
                var decryptedData = EncryptionClass.decryptUserDetails(data);
                string password = decryptedData["UTMZV"];
                var usernamePasswordResponse = new usernamePasswordDeserialize
                {
                    username = username,
                    password = password
                };
                return Json(usernamePasswordResponse);
            }
            else
            {
                ResponseModel<string> response = new ResponseModel<string>();
                response.Status = 401;
                response.Message = "Unauthorized";
                return Json(response);
            }
            
        }

        public JsonResult GetAllMessages()
        {
            //var username = "sumitchourasia91@gmail.com";            
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);            
            var userType = Convert.ToString(Request.QueryString["userType"]);
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {
                return Json(new UserMessageService().GetAllNotificationMessage(session.UserName, userType),JsonRequestBehavior.AllowGet);
            }
            else
            {
                ResponseModel<string> response = new ResponseModel<string>();
                response.Status = 401;
                response.Message = "Unauthorized";
                return Json(response,JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetAllNotifications()
        {
            //var username = "sumitchourasia91@gmail.com";            
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            var userType = Convert.ToString(Request.QueryString["userType"]);
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {
                return Json(new UserNotificationService().GetAllNotification(session.UserName, userType), JsonRequestBehavior.AllowGet);
            }
            else
            {
                ResponseModel<string> response = new ResponseModel<string>();
                response.Status = 401;
                response.Message = "Unauthorized";
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult GetTemplateInformationByRefKey()
        {
            //var username = "sumitchourasia91@gmail.com";            
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            var clientTemplate = new ClientTemplateService();
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            var id = Convert.ToInt32(Request.QueryString["id"]);
            var type = Convert.ToString(Request.QueryString["type"]);
            var subType = Convert.ToString(Request.QueryString["subType"]);
            if (isValidToken)
            {
                return Json(clientTemplate.GetTemplateInformationByRefKey(session.UserName,id,type,subType));
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
        public JsonResult GetTemplateDetailById()
        {
            //var username = Request.QueryString["username"].ToString(CultureInfo.InvariantCulture);
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            var id = Convert.ToInt32(Request.QueryString["id"]);
            var clientTemplate = new ClientTemplateService();
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {
                return Json(clientTemplate.GetTemplateDetailById(session.UserName, id));
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
        public JsonResult GetTemplateSurveyResponseResultById()
        {
            //var username = Request.QueryString["username"].ToString(CultureInfo.InvariantCulture);
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            var id = Convert.ToInt32(Request.QueryString["id"]);
            var clientTemplate = new ClientTemplateService();
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {
                return Json(clientTemplate.GetTemplateSurveyResponseResultById(session.UserName, id));
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
        public JsonResult GetTranscriptionResponseResultById()
        {
            //var username = Request.QueryString["username"].ToString(CultureInfo.InvariantCulture);
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            var id = Convert.ToInt32(Request.QueryString["id"]);
            var clientTemplate = new ClientTemplateService();
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {
                return Json(clientTemplate.GetTranscriptionResponseResultById(session.UserName, id));
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
        public JsonResult GetAllCompletedTranscriptionInformation()
        {
            //var username = Request.QueryString["username"].ToString(CultureInfo.InvariantCulture);
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            var id = Convert.ToInt32(Request.QueryString["id"]);
            var clientTemplate = new ClientTemplateService();
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {
                return Json(clientTemplate.GetAllCompletedTranscriptionInformation(session.UserName, id));
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
        public JsonResult GetAllCompletedImageModerationInformation()
        {
            //var username = Request.QueryString["username"].ToString(CultureInfo.InvariantCulture);
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            var id = Convert.ToInt32(Request.QueryString["id"]);
            var clientTemplate = new ClientTemplateService();
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {
                return Json(clientTemplate.GetAllCompletedImageModerationInformation(session.UserName, id));
            }
            else
            {
                ResponseModel<string> response = new ResponseModel<string>();
                response.Status = 401;
                response.Message = "Unauthorized";
                return Json(response);
            }
        }


        public ActionResult DownloadAllCompletedTranscriptionInformation()
        {
            //var username = Request.QueryString["username"].ToString(CultureInfo.InvariantCulture);
            var headers = new HeaderManager(Request);            
            var id = Convert.ToInt32(Request.QueryString["id"]);
            var guid = Convert.ToString(Request.QueryString["guid"]);
            M2ESession session = TokenManager.getSessionInfo(guid);
            var clientTemplate = new ClientTemplateService();
            var isValidToken = TokenManager.IsValidSession(guid);
            var fileName = "Transcription_" + session.UserName + "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
            if (isValidToken)
            {
                var CompletedTranscriptions = clientTemplate.GetAllCompletedTranscriptionInformation(session.UserName, id);
                var products = new System.Data.DataTable("teste");
                var columnName = CompletedTranscriptions.Payload.options.Split(';');

                foreach (var Column in columnName)
                {
                    products.Columns.Add(Column, typeof(string));
                }

                foreach (var userResponse in CompletedTranscriptions.Payload.data)
                {                    
                                        
                    products.Rows.Add();                    
                    int count = 1;
                    foreach (var item in userResponse.userResponseData)
                    {                        
                        products.Rows.Add(item);                                              
                    }

                    products.Rows.Add();
                    products.Rows.Add("Transcription Image", userResponse.imageUrl);
                    products.Rows.Add();
                    products.Rows.Add();
                    products.Rows.Add();
                }
                


                var grid = new GridView();
                grid.DataSource = products;
                grid.DataBind();

                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=" + fileName + ".xls");
                Response.ContentType = "application/ms-excel";

                Response.Charset = "";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                grid.RenderControl(htw);

                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();

                return View("MyView");                
            }
            else
            {                
                return null;
            }
        }

        [HttpPost]
        public JsonResult GetTemplateImageDetailById()
        {
            //var username = Request.QueryString["username"].ToString(CultureInfo.InvariantCulture);
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            var id = Convert.ToInt32(Request.QueryString["id"]);
            var clientTemplate = new ClientTemplateService();
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {
                return Json(clientTemplate.GetTemplateImageDetailById(session.UserName, id));
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
        public JsonResult CreateTemplate(CreateTemplateRequest req)
        {
            //var username = Request.QueryString["username"].ToString(CultureInfo.InvariantCulture);
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            var templateList = req.Data;

            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {
                var clientTemplate = new ClientTemplateService();
                var createTemplateResponse = clientTemplate.CreateTemplate(templateList, session.UserName, req.TemplateInfo);
                var imgurImageList = req.ImgurList;
                if (createTemplateResponse.Status != 200) return Json(createTemplateResponse);
                if (imgurImageList != null)
                    clientTemplate.ImgurImagesSaveToDatabaseWithTemplateId(imgurImageList, session.UserName, createTemplateResponse.Payload);

                return Json(createTemplateResponse);
            }
            else
            {
                ResponseModel<string> response = new ResponseModel<string>();
                response.Status = 401;
                response.Message = "Unauthorized";
                return Json(response);
            }
            
        }
       
        public JsonResult getReferralKey()
        {            
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);            

            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {                
                var response = new ClientDetailService().getReferralKey(session.UserName);
                if(response != null)
                    response.Payload.myReferralLink = "http://" + Request.Url.Authority + "/#/signup/user/"+response.Payload.myReferralLink;
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ResponseModel<string> response = new ResponseModel<string>();
                response.Status = 401;
                response.Message = "Unauthorized";
                return Json(response);
            }

        }

        public JsonResult GetEarningHistory()
        {
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);

            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {
                var response = new ClientDetailService().GetEarningHistory(session.UserName);                
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ResponseModel<string> response = new ResponseModel<string>();
                response.Status = 401;
                response.Message = "Unauthorized";
                return Json(response);
            }

        }

        public JsonResult GetReputationHistory()
        {
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);

            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {
                var response = new ClientDetailService().GetReputationHistory(session.UserName);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ResponseModel<string> response = new ResponseModel<string>();
                response.Status = 401;
                response.Message = "Unauthorized";
                return Json(response);
            }

        }

        //[HttpPost]
        //public JsonResult CreateTemplateModeratingPhotos(CreateTemplateRequest req)
        //{
        //    var username = Request.QueryString["username"].ToString(CultureInfo.InvariantCulture);
        //    var templateList = req.Data;
        //    var clientTemplate = new ClientTemplateService();
        //    var createTemplateResponse = clientTemplate.CreateTemplate(templateList, username,req.TemplateInfo);
        //    var imgurImageList = req.ImgurList;
        //    if (createTemplateResponse.Status != 200) return Json(createTemplateResponse);
        //    if (imgurImageList != null)
        //        clientTemplate.ImgurImagesSaveToDatabaseWithTemplateId(imgurImageList, username, createTemplateResponse.Payload);

        //    return Json("200");
        //} 

        [HttpPost]
        public JsonResult CreateTemplateWithId(CreateTemplateRequest req)
        {
            //var username = Request.QueryString["username"].ToString(CultureInfo.InvariantCulture);
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            var id = Request.QueryString["id"].ToString(CultureInfo.InvariantCulture);
            var templateList = req.Data;
            var imgurImageList = req.ImgurList;
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {
                var clientTemplate = new ClientTemplateService();
                if (imgurImageList != null)
                    clientTemplate.ImgurImagesSaveToDatabaseWithTemplateId(imgurImageList, session.UserName, id);
                return Json(clientTemplate.CreateTemplateWithId(templateList, session.UserName, id));
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
        public JsonResult DeleteTemplateDetailById()
        {
            //var username = Request.QueryString["username"].ToString(CultureInfo.InvariantCulture);
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            var id = Convert.ToInt32(Request.QueryString["id"]);
            var type = Convert.ToString(Request.QueryString["type"]);
            var subType = Convert.ToString(Request.QueryString["subType"]);
            var clientTemplate = new ClientTemplateService();
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {
                return Json(clientTemplate.DeleteTemplateDetailById(session.UserName, id,type, subType));
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
        public JsonResult AcceptCompleteTemplateDetailById()
        {
            //var username = Request.QueryString["username"].ToString(CultureInfo.InvariantCulture);
            ResponseModel<string> response = new ResponseModel<string>();

            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            var id = Convert.ToInt32(Request.QueryString["id"]);
            var type = Convert.ToString(Request.QueryString["type"]);
            var subType = Convert.ToString(Request.QueryString["subType"]);
            var userResponse = Convert.ToString(Request.QueryString["userResponse"]);
            var clientTemplate = new ClientTemplateService();
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {
                return Json(clientTemplate.AcceptCompleteTemplateDetailById(session.UserName, id, type, subType,userResponse));                
            }
            else
            {
                
                response.Status = 401;
                response.Message = "Unauthorized";
                return Json(response);
            }

        }

        [HttpPost]
        public JsonResult DeleteTemplateImgurImageById()
        {
            //var username = Request.QueryString["username"].ToString(CultureInfo.InvariantCulture);
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            var id = Convert.ToInt32(Request.QueryString["id"]);
            var clientTemplate = new ClientTemplateService();
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {
                return Json(clientTemplate.DeleteTemplateImgurImageById(session.UserName, id));
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
        public JsonResult EditTemplateDetailById(CreateTemplateRequest req)
        {
            //var username = Request.QueryString["username"].ToString(CultureInfo.InvariantCulture);
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            var id = Convert.ToInt32(Request.QueryString["id"]);
            var templateList = req.Data;
            var clientTemplate = new ClientTemplateService();
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {
                var createTemplateResponse = clientTemplate.EditTemplateDetailById(templateList, session.UserName, id);
                var imgurImageList = req.ImgurList;
                var refKey = session.UserName + id;
                if (createTemplateResponse.Status != 200) return Json(createTemplateResponse);
                if (imgurImageList != null)
                    clientTemplate.ImgurImagesSaveToDatabaseWithTemplateId(imgurImageList, session.UserName, refKey);

                return Json(createTemplateResponse);
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
        public JsonResult GetClientDetails()
        {
            var userType = Request.QueryString["userType"].ToString(CultureInfo.InvariantCulture);
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            var clientTemplate = new ClientDetailService();
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {
                var clientDetailResponse = clientTemplate.GetClientDetails(session.UserName, userType);
                //clientDetailResponse.Payload.RequestUrlAuthority = "\"http://"+Request.Url.Authority+"/SocialAuth/FBLogin/facebook/\"";
                return Json(clientDetailResponse);
            }
            else
            {
                ResponseModel<string> response = new ResponseModel<string>();
                response.Status = 401;
                response.Message = "Unauthorized";
                return Json(response);
            }
            
        }

        private Stream GenerateStreamFromString(string s)
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(s);
                writer.Flush();
                stream.Position = 0;
                return stream;
            }
            catch (Exception ex)
            {
                Logger.Error("GenerateStreamFromString", ex);

                throw;
            }
        }
    }
}
