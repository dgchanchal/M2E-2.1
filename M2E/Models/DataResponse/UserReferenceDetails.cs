using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataResponse
{
    public class UserReferenceDetails
    {
        public string username { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string AccountCreationDate { get; set; }
        public string imageUrl { get; set; }
        public string isValid { get; set; }
        public string source { get; set; }
        public string earning { get; set; }
        public string facebookLink { get; set; }
        public string googleLink { get; set; }
        public string linkedinLink { get; set; }
    }
}