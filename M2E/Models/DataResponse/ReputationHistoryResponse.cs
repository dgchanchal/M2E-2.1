using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataResponse
{
    public class ReputationHistoryResponse
    {
        public string dateTime { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public string subType { get; set; }
        public string username { get; set; }
        public string reputation { get; set; }        
    }
}