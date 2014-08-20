using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataResponse.ClientResponse
{
    public class ClientSurveyResultResponse
    {
        public string questionType { get; set; }
        public string question { get; set; }
        public string options { get; set; }
        public string UniqueId { get; set; }
        public int index { get; set; }
        public Dictionary<string, int> resultMap { get; set; }
        public Dictionary<string, string> textBoxResultMap { get; set; }
    }
}