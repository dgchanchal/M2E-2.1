using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataResponse.ClientResponse
{
    public class ClientTranscriptionResultResponse
    {
        public string type { get; set; }
        public string subType { get; set; }
        public string title { get; set; }
        public string imageUrl { get; set; }
        public List<string[]> userResponseData { get; set; }
    }
}