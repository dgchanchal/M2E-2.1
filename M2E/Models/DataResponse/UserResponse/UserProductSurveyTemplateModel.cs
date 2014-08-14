using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataResponse.UserResponse
{
    public class UserProductSurveyTemplateModel
    {
        public string type { get; set; }
        public string userStatus { get; set; }
        public string userDeadline { get; set; }
        public string subType { get; set; }
        public string title { get; set; }
        public string refKey { get; set; }
        public string creationTime { get; set; }
        public string totalThreads { get; set; }
        public string earningPerThreads { get; set; }
        public string currency { get; set; }
        public string remainingThreads { get; set; }
    }
}