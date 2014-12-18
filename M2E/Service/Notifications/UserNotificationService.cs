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
    public class UserNotificationService
    {
        private readonly M2EContext _db = new M2EContext();
        private static readonly ILogger Logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        public delegate void SendUserNotification_Delegate(string fromUsername, string toUsername, string userType, string messageTitle, DateTime messagePostedTime, string imageUrlCSSClass);

        public void SendUserNotificationAsync(string fromUsername, string toUsername, string userType, string messageTitle, DateTime messagePostedTime, string imageUrlCssClass)
        {
            try
            {
                SendUserNotification_Delegate sendUserNotificationServiceDelegate = null;
                sendUserNotificationServiceDelegate = new SendUserNotification_Delegate(SendUserNotification);
                IAsyncResult CallAsynchMethod = null;
                CallAsynchMethod = sendUserNotificationServiceDelegate.BeginInvoke( fromUsername, toUsername, userType, messageTitle, messagePostedTime, imageUrlCssClass, null, null); //invoking the method

            }
            catch (Exception ex)
            {
                Logger.Error("SendUserNotificationMessageAsync ", ex);
            }
        }

        public void SendUserNotification(string fromUsername, string toUsername, string userType, string messageTitle, DateTime messagePostedTime, string imageUrlCssClass)
        {
            var userNotification = new UserAlerts
            {
                messageFrom = fromUsername,
                messageTo = toUsername,
                titleText = messageTitle,                
                dateTime = DateTime.Now,
                iconUrl = imageUrlCssClass,
                AlertSeen = Constants.status_false,
                priority = Constants.NA,
                userType = userType,
                username = fromUsername               
            };

            _db.UserAlerts.Add(userNotification);
            try
            {
                _db.SaveChanges();
                SendRealTimeUserNotification(fromUsername, toUsername, userType, messageTitle, messagePostedTime, imageUrlCssClass);
            }
            catch (DbEntityValidationException ex)
            {
                DbContextException.LogDbContextException(ex);
            }
        }

        public void SendRealTimeUserNotification(string fromUsername, string toUsername, string userType, string messageTitle, DateTime messagePostedTime, string imageUrlCss)
        {
            try
            {
                var hubContext = GlobalHost.ConnectionManager.GetHubContext<SignalRUserHub>();
                dynamic client = SignalRManager.getSignalRDetail(toUsername + Constants.userType_user);
                client.updateUserNotification(userType, "#", imageUrlCss, messageTitle, messagePostedTime);
            }
            catch (DbEntityValidationException ex)
            {
                DbContextException.LogDbContextException(ex);
            }
        }
    }
}