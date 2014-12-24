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

        public void SendUserSurveyAcceptanceMessageAsync(string toUsername, string messageTitle)
        {
            SendUserNotificationAsync("Admin", toUsername, Constants.userType_user, messageTitle, DateTime.Now, Constants.CSSImage_success);
        }

        public void SendUserSurveyAcceptanceMessage(string toUsername, string messageTitle)
        {
            SendUserNotification("Admin", toUsername, Constants.userType_user, messageTitle, DateTime.Now, Constants.CSSImage_success);
        }

        public void SendUserCommonNotification(String toUsername,String message)
        {
            SendUserNotification("Admin", toUsername, Constants.userType_user, message, DateTime.Now, Constants.CSSImage_success);
        }

        public void SendUserCommonNotificationAsync(String toUsername, String message)
        {
            SendUserNotificationAsync("Admin", toUsername, Constants.userType_user, message, DateTime.Now, Constants.CSSImage_success);
        }
        public void SendUserReferralAcceptanceNotification(String toUsername,String referreralUsername)
        {
            SendUserNotification("Admin", toUsername, Constants.userType_user, "Your Referral "+referreralUsername+" Joined Cautom", DateTime.Now, Constants.CSSImage_success);
        }
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

        public ResponseModel<List<UserAlerts>> GetAllNotification(string username, string userType)
        {
            var response = new ResponseModel<List<UserAlerts>>();
            try
            {
                response.Payload =
                    _db.UserAlerts.Where(x => x.messageTo == username && x.userType == userType)
                        .OrderByDescending(x => x.dateTime)
                        .ToList();
                response.Message = "success";
                response.Status = 200;
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error("GetAllNotificationMessage", ex);
                response.Message = "Internal server Error !!";
                response.Status = 500;
                return response;
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