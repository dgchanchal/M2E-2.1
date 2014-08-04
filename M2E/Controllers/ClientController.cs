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
            var username = "sumitchourasia91@gmail.com";            
            var headers = new HeaderManager(Request);           
            var clientTemplate = new ClientTemplateService();
            var isValidToken= TokenManager.IsValidSession(headers.AuthToken);
            return Json(clientTemplate.GetAllTemplateInformation(username));
        }

        [HttpPost]
        public JsonResult GetTemplateDetailById()
        {
            var username = Request.QueryString["username"].ToString(CultureInfo.InvariantCulture);
            var id = Convert.ToInt32(Request.QueryString["id"]);
            var clientTemplate = new ClientTemplateService();
            return Json(clientTemplate.GetTemplateDetailById(username, id));            
        }

        [HttpPost]
        public JsonResult GetTemplateImageDetailById()
        {
            var username = Request.QueryString["username"].ToString(CultureInfo.InvariantCulture);
            var id = Convert.ToInt32(Request.QueryString["id"]);
            var clientTemplate = new ClientTemplateService();
            return Json(clientTemplate.GetTemplateImageDetailById(username, id));
        }

        [HttpPost]
        public JsonResult CreateTemplate(CreateTemplateRequest req)
        {
            var username = Request.QueryString["username"].ToString(CultureInfo.InvariantCulture);
            var templateList = req.Data;
            var clientTemplate = new ClientTemplateService();
            var createTemplateResponse = clientTemplate.CreateTemplate(templateList, username,req.TemplateInfo);            
            var imgurImageList = req.ImgurList;
            if (createTemplateResponse.Status != 200) return Json(createTemplateResponse);
            if (imgurImageList != null)
                clientTemplate.ImgurImagesSaveToDatabaseWithTemplateId(imgurImageList, username, createTemplateResponse.Payload);

            return Json(createTemplateResponse);
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
            var username = Request.QueryString["username"].ToString(CultureInfo.InvariantCulture);
            var id = Request.QueryString["id"].ToString(CultureInfo.InvariantCulture);
            var templateList = req.Data;
            var imgurImageList = req.ImgurList;
            var clientTemplate = new ClientTemplateService();
            if(imgurImageList !=null)
            clientTemplate.ImgurImagesSaveToDatabaseWithTemplateId(imgurImageList,username,id);
            return Json(clientTemplate.CreateTemplateWithId(templateList, username, id));
        }

        [HttpPost]
        public JsonResult DeleteTemplateDetailById()
        {
            var username = Request.QueryString["username"].ToString(CultureInfo.InvariantCulture);
            var id = Convert.ToInt32(Request.QueryString["id"]);
            var clientTemplate = new ClientTemplateService();
            return Json(clientTemplate.DeleteTemplateDetailById(username, id));
        }

        [HttpPost]
        public JsonResult DeleteTemplateImgurImageById()
        {
            var username = Request.QueryString["username"].ToString(CultureInfo.InvariantCulture);
            var id = Convert.ToInt32(Request.QueryString["id"]);
            var clientTemplate = new ClientTemplateService();
            return Json(clientTemplate.DeleteTemplateImgurImageById(username, id));
        }

        [HttpPost]
        public JsonResult EditTemplateDetailById(CreateTemplateRequest req)
        {
            var username = Request.QueryString["username"].ToString(CultureInfo.InvariantCulture);
            var id = Convert.ToInt32(Request.QueryString["id"]);
            var templateList = req.Data;
            var clientTemplate = new ClientTemplateService();
            var createTemplateResponse = clientTemplate.EditTemplateDetailById(templateList,username, id);
            var imgurImageList = req.ImgurList;
            var refKey = username+id;
            if (createTemplateResponse.Status != 200) return Json(createTemplateResponse);
            if (imgurImageList != null)
                clientTemplate.ImgurImagesSaveToDatabaseWithTemplateId(imgurImageList, username, refKey);

            return Json(createTemplateResponse);
        }

        [HttpPost]
        public JsonResult GetClientDetails()
        {
            var username = Request.QueryString["username"].ToString(CultureInfo.InvariantCulture);
            var clientTemplate = new ClientDetailService();
            var clientDetailResponse = clientTemplate.GetClientDetails(username);
            return Json(clientDetailResponse);
        }
    }
}
