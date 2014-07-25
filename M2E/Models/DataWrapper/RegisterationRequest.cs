using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataWrapper
{
    public class RegisterationRequest
    {    
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
        public string Referral { get; set; }
    }
}