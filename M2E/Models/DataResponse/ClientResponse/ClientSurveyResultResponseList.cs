using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataResponse.ClientResponse
{
    public class ClientSurveyResultResponseList
    {
        public string type { get; set; }
        public string subType { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public List<ClientSurveyResultResponse> resultList { get; set; }      
    }
}