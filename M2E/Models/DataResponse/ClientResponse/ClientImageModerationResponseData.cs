using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataResponse.ClientResponse
{
    public class ClientImageModerationResponseData
    {
        public string imageUrl { get; set; }
        public string userResponse { get; set; }
        public string userResponseValue { get; set; }
    }
}