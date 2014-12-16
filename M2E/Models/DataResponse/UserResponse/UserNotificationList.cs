using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataResponse.UserResponse
{
    public class UserNotificationList
    {
        public string link { get; set; }
        public string NotificationMessage { get; set; }
        public string NotificationClass { get; set; }
        public string NotificationPostedTimeAgo { get; set; }
    }
}