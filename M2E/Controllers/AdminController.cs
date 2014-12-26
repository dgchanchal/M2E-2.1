using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using M2E.Models.Constants;
using M2E.Models.DataWrapper;
using M2E.Service.Notifications;

namespace M2E.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult AdminPostMessage(AdminPostMessageWrapper data)
        {
            //var username = "sumitchourasia91@gmail.com";            
            //var headers = new HeaderManager(Request);
            //M2ESession session = TokenManager.getSessionInfo(headers.AuthToken, headers);
            //var userType = Convert.ToString(Request.QueryString["userType"]);
            //var isValidToken = TokenManager.IsValidSession(headers.AuthToken);
            //if (isValidToken)
            //{
            //    return Json(new UserMessageService().GetAllNotificationMessage(session.UserName, userType), JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    ResponseModel<string> response = new ResponseModel<string>();
            //    response.Status = 401;
            //    response.Message = "Unauthorized";
            //    return Json(response, JsonRequestBehavior.AllowGet);
            //}
            if (data.sendTo == "all" || data.messageType == Constants.messageTypeTask)
            {
                if (data.sendTo == "all")
                {
                    if (data.userType == Constants.userType_user)
                    {
                        new SendNewTaskService().SendUserTaskNotificationToAllAsync(data.message, data.message, DateTime.Now);
                    }                        
                    else
                    {
                        new SendNewTaskService().SendClientTaskNotificationToAllAsync(data.message, data.message, DateTime.Now);
                    }
                }                    
                else
                {
                    new SendNewTaskService().SendUserTaskNotificationAsync("admin@cautom.com",data.sendTo,data.userType,"info",data.message,DateTime.Now);
                }
            }

            else if (data.messageType == Constants.messageTypeMessage)
            {
                new UserMessageService().SendUserNotificationMessageAsync("admin@cautom.com",data.sendTo,data.userType,"message",data.message,DateTime.Now,Constants.avatar2);
            }
            else if (data.messageType == Constants.messageTypeNotification)
            {
               new UserNotificationService().SendUserNotificationAsync("admin@cautom.com",data.sendTo,data.userType,"notification",DateTime.Now,Constants.CSSImage_info);   
            }            
            else
            {
                
            }
            return Json("done", JsonRequestBehavior.AllowGet);
        }

    }
}
