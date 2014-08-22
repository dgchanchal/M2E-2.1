using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataResponse.ClientResponse
{
    public class ClientTranscriptionTableResponseData
    {
        public string imageUrl { get; set; }
        public string imageUrl_s { get; set; }
        public List<string[]> userResponseData { get; set; }
    }
}