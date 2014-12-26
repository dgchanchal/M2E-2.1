using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace M2E.Models.DataWrapper
{
    public class AdminPostMessageWrapper
    {
        public string userType { get; set; }
        public string messageType { get; set; }
        public string sendTo { get; set; }
        public string message { get; set; }        
    }
}
