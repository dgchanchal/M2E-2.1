using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataWrapper
{
    public class ContactUsRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string SendMeACopy { get; set; }
    }
}