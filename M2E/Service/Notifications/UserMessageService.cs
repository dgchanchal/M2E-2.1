using System.Data.Entity.Validation;
using M2E.Common.Logger;
using M2E.CommonMethods;
using M2E.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using M2E.Models.Constants;
using M2E.Session;
using M2E.signalRPushNotifications;
using Microsoft.AspNet.SignalR;

namespace M2E.Service.Notifications
{
    public class UserMessageService
    {
        private readonly M2EContext _db = new M2EContext();
        private static readonly ILogger Logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        public delegate void SendUserNotificationMessage_Delegate(string fromUsername, string toUsername,string userType, string messageTitle, string messageBody, DateTime messagePostedTime, string imageUrl);

        public void SendUserNotificationForAccountVerificationSuccess(string sendTo, string userType)
        {
            new UserMessageService().SendUserNotificationMessageAsync(
                    "admin", sendTo, userType, "Account Verified", "Contratulations !! Your Account Verification done.", DateTime.Now, Constants.avatar
                    );
        }

        public void SendUserNotificationMessageAsync(string fromUsername, string toUsername,string userType, string messageTitle, string messageBody, DateTime messagePostedTime, string imageUrl)
        {            
            try
            {
                SendUserNotificationMessage_Delegate sendUserNotificationMessageServiceDelegate = null;
                sendUserNotificationMessageServiceDelegate = new SendUserNotificationMessage_Delegate(SendUserNotificationMessage);
                IAsyncResult CallAsynchMethod = null;
                CallAsynchMethod = sendUserNotificationMessageServiceDelegate.BeginInvoke(fromUsername, toUsername, userType, messageTitle, messageBody, messagePostedTime, imageUrl, null, null); //invoking the method

            }
            catch (Exception ex)
            {
                Logger.Error("SendUserNotificationMessageAsync ", ex);
            }
        }

        public void SendUserNotificationMessage(string fromUsername,string toUsername,string userType,string messageTitle,string messageBody,DateTime messagePostedTime,string imageUrl)
        {
            var userMessage = new UserMessages
            {
                messageFrom = fromUsername, 
                messageTo = toUsername,
                titleText = messageTitle,
                bodyText = messageBody,
                dateTime = messagePostedTime,
                iconUrl = imageUrl,
                messageSeen = Constants.status_false,
                priority = Constants.NA,
                userType = userType,
                username = fromUsername
            };

            _db.UserMessages.Add(userMessage);
            try
            {
                _db.SaveChanges();
                SendRealTimeUserNotificationMessage(fromUsername, toUsername, userType, messageTitle, messageBody, messagePostedTime, imageUrl);
            }
            catch (DbEntityValidationException ex)
            {
                DbContextException.LogDbContextException(ex);
            }
        }

        public void SendRealTimeUserNotificationMessage(string fromUsername, string toUsername, string userType, string messageTitle, string messageBody, DateTime messagePostedTime, string imageUrl)
        {            
            try
            {                
                var hubContext = GlobalHost.ConnectionManager.GetHubContext<SignalRClientHub>();
                dynamic client = SignalRManager.getSignalRDetail(toUsername);
                client.updateUserNotificationMessage(Constants.userType_user, "#", imageUrl, messageTitle, messagePostedTime, messageBody);
            }
            catch (DbEntityValidationException ex)
            {
                DbContextException.LogDbContextException(ex);
            }
        }
    }
}