using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataResponse.UserResponse
{
    public class UserActiveThreadsResponse
    {
        public string title { get; set; }
        public string refKey { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string expectedDeliveryTime { get; set; }
        public string status { get; set; }
        public string type { get; set; }
        public string subType { get; set; }
    }
}