using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataWrapper
{
    public class ResetPasswordRequest
    {
        public String Username { get; set; }
        public String Guid { get; set; }
        public String Password { get; set; }
    }
}