using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataResponse.ClientResponse
{
    public class ClientAllTranscriptionResultResponse
    {
        public string type { get; set; }
        public string subType { get; set; }
        public string title { get; set; }
        public string options { get; set; }
        public List<ClientTranscriptionTableResponseData> data { get; set; }
    }
}