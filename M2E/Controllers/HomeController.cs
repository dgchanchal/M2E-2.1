using System;
using System.Web.Mvc;
using System.Reflection;
using M2E.Common.Logger;

namespace M2E.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        private ILogger _logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));

        public ActionResult Index()
        {
            //var watch = Stopwatch.StartNew();
            //logger.Info("Home Controller index page");
            //watch.Stop();
            //logger.Info(Convert.ToString(watch.ElapsedMilliseconds) + " - time");       
            return View();
        }

    }
}
