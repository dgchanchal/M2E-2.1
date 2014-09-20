using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataResponse
{
    public class EarningHistoryResponse
    {
        public string amount { get; set; }
        public string dateTime { get; set; }
        public string PaymentMode { get; set; }
        public string subType { get; set; }
        public string type { get; set; }
        public string title { get; set; }        
    }
}