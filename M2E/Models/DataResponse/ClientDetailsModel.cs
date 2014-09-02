using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataResponse
{
    public class ClientDetailsModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string RequestUrlAuthority { get; set; }
        public string imageUrl { get; set; }
        public string gender { get; set; }        
    }
}