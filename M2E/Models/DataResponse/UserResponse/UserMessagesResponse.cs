using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataResponse.UserResponse
{
    public class UserMessagesResponse
    {
        public string UnreadMessages { get; set; }
        public string CountLabelType { get; set; }
        public List<UserMessageList> MessageList { get; set; }
    }
}