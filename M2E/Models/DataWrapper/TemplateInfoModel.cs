using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataWrapper
{
    public class TemplateInfoModel
    {
        public string type { get; set; }
        public string subType { get; set; }
        public string description { get; set; }
        public string totalThreads { get; set; }
        public string amountEachThread { get; set; }
    }
}