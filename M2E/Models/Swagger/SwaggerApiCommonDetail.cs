using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace M2E.Models.Swagger
{
    public class SwaggerApiCommonDetail
    {
        public string basePath { get; set; }
        public string resourcePath { get; set; }
        public string[] produces { get; set; }        
        public List<operationsModel> operations { get; set; }

        public List<SwaggerAuthModelsLoyaltyProfileBean> dataType { get; set; }
    }

    public class operationsModel
    {
        public string apiPath { get; set; }
        public string method { get; set; }
        public string summary { get; set; }
        public string notes { get; set; }
        public string nickname { get; set; }
        public List<parametersModel> parameters { get; set; }
        public List<ResponseMessageModel> ResponseMessage { get; set; } 
    }

    public class ResponseMessageModel
    {
        public string code { get; set; }
        public string message { get; set; }
    }
    public class parametersModel
    {
        public string name { get; set; }
        public string description { get; set; }
        public bool required { get; set; }
        public string paramType { get; set; }
        public string type { get; set; }
        public bool allowMultiple { get; set; }
        public string defaultValue { get; set; }
        
    }
}
