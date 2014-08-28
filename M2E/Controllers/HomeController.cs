using System;
using System.Globalization;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Reflection;
using M2E.Common.Logger;
using M2E.CommonMethods;
using M2E.Models;
using M2E.Models.DataResponse;
using M2E.signalRPushNotifications;
using Microsoft.AspNet.SignalR;
using M2E.DAO;

namespace M2E.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        private static readonly ILogger Logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        private DbContextException _dbContextException = new DbContextException();
        private readonly M2EContext _db = new M2EContext();
        private ILogger _logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));

        public ActionResult Index()
        {
            //var watch = Stopwatch.StartNew();
            //logger.Info("Home Controller index page");
            //watch.Stop();
            //logger.Info(Convert.ToString(watch.ElapsedMilliseconds) + " - time");       
            return View();
        }

        public ActionResult BeforeLoginUserProjectDetailsService()
        {            
            string totalProjects = "152";
            string successRate = "94.2";
            string totalUsers = "3854";
            string projectCategories = "37";
            var response = new ResponseModel<BeforeLoginUserProjectDetailsModel>();
            response.Status = 200;
            response.Message = "success";
            var beforeLoginUserProjectDetailsServiceData = new BeforeLoginUserProjectDetailsModel
            {
                TotalUsers = _db.Users.Count().ToString(CultureInfo.InvariantCulture),
                TotalProjects = new ProjectDAO().totalAvailableProjects(),
                SuccessRate = "94.3",
                ProjectCategories = "35"
            };
            response.Payload = beforeLoginUserProjectDetailsServiceData;
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult updateBeforeLoginUserProjectDetailsService()
        {
            var SignalRHub = new SignalRHub();
            string totalProjects = "152";
            string successRate = "94.2";
            string totalUsers = "3854";
            string projectCategories = "37";
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>();
            hubContext.Clients.All.updateBeforeLoginUserProjectDetails(totalProjects, successRate,totalUsers,projectCategories);
            //sendMessage
            //SignalRHub.updateBeforeLoginUserProjectDetails(totalProjects, successRate,totalUsers,projectCategories);
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}
