using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Web;
using M2E.Common.Logger;
using M2E.CommonMethods;
using M2E.Models;
using M2E.Models.Constants;
using M2E.Session;
using M2E.signalRPushNotifications;
using Microsoft.AspNet.SignalR;

namespace M2E.Service.Notifications
{
    public class SendNewTaskService
    {
        private readonly M2EContext _db = new M2EContext();
        private static readonly ILogger Logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        public delegate void SendUserTaskNotificationMessage_Delegate(string toUsername, string userType, string messageTitle, string messageBody, DateTime messagePostedTime);
        public delegate void SendUserTaskNotificationToAllMessage_Delegate(string messageTitle, string messageBody, DateTime messagePostedTime);

        public void SendUserTaskNotificationAsync(string fromUsername, string toUsername, string userType, string messageTitle, string messageBody, DateTime messagePostedTime, string imageUrl)
        {
            try
            {
                SendUserTaskNotificationMessage_Delegate sendUserTaskNotificationServiceDelegate = null;
                sendUserTaskNotificationServiceDelegate = new SendUserTaskNotificationMessage_Delegate(SendRealTimeUserTaskNotification);
                IAsyncResult CallAsynchMethod = null;
                CallAsynchMethod = sendUserTaskNotificationServiceDelegate.BeginInvoke(toUsername, userType, messageTitle, messageBody, messagePostedTime, null, null); //invoking the method

            }
            catch (Exception ex)
            {
                Logger.Error("SendUserNotificationMessageAsync ", ex);
            }
        }

        public void SendUserTaskNotificationToAllAsync(string messageTitle, string messageBody, DateTime messagePostedTime)
        {
            try
            {
                SendUserTaskNotificationToAllMessage_Delegate sendUserTaskNotificationToAllServiceDelegate = null;
                sendUserTaskNotificationToAllServiceDelegate = new SendUserTaskNotificationToAllMessage_Delegate(SendRealTimeUserTaskNotificationToAllUser);
                IAsyncResult CallAsynchMethod = null;
                CallAsynchMethod = sendUserTaskNotificationToAllServiceDelegate.BeginInvoke(messageTitle, messageBody, messagePostedTime, null, null); //invoking the method

            }
            catch (Exception ex)
            {
                Logger.Error("SendUserNotificationMessageAsync ", ex);
            }
        }

        public void SendUserTaskNotification(string fromUsername, string toUsername, string userType, string messageTitle, string messageBody, DateTime messagePostedTime, string imageUrl)
        {
            try
            {                
                SendRealTimeUserTaskNotification(toUsername, userType, messageTitle, messageBody, messagePostedTime); //invoking the method
            }
            catch (Exception ex)
            {
                Logger.Error("SendUserNotificationMessageAsync ", ex);
            }
        }

        public void SendRealTimeUserTaskNotification(string toUsername, string userType, string messageTitle, string messageBody, DateTime messagePostedTime)
        {            
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SignalRUserHub>();
            dynamic client = SignalRManager.getSignalRDetail(toUsername + Constants.userType_user);
            client.updateUserTaskNotification(Constants.userType_user, "#", messageTitle, messagePostedTime, messageBody);                        
        }

        public void SendRealTimeUserTaskNotificationToAllUser(string messageTitle, string messageBody, DateTime messagePostedTime)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SignalRUserHub>();
            hubContext.Clients.All.updateAllUserTaskNotification("#", messageTitle, messagePostedTime, messageBody);
        }
    }
}