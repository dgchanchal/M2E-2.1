using System;
using System.Globalization;
using System.Web.Mvc;
using M2E.Common.Logger;
using System.Reflection;
using M2E.Models.DataWrapper;
using M2E.Models;
using M2E.CommonMethods;
using M2E.Service.JobTemplate;
using M2E.Service.Client;
using M2E.Session;

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
            Logger.Info("Client Controller index page");  
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
            var clientTemplate = new ClientTemplateService();
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {
                return Json(clientTemplate.DeleteTemplateDetailById(session.UserName, id));
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
            //var username = Request.QueryString["username"].ToString(CultureInfo.InvariantCulture);
            var headers = new HeaderManager(Request);
            M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            var clientTemplate = new ClientDetailService();
            var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            if (isValidToken)
            {
                var clientDetailResponse = clientTemplate.GetClientDetails(session.UserName);
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
    }
}
