using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataResponse.UserResponse
{
    public class UserNotificationsResponse
    {
        public string UnreadNotifications { get; set; }
        public string CountLabelType { get; set; }
        public List<UserNotificationList> NotificationList { get; set; } 
    }
}