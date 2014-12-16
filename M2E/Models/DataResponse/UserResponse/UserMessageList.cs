using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataResponse.UserResponse
{
    public class UserMessageList
    {
        public string link { get; set; }
        public string imageUrl { get; set; }
        public string messageTitle { get; set; }
        public string MessagePostedInTimeAgo { get; set; }
        public string messageContent { get; set; }
        public string MessageSeen { get; set; }
    }
}