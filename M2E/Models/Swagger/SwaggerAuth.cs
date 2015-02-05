using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace M2E.Models.Swagger
{
    public class SwaggerAuth
    {
        public string apiVersion { get; set; }
        public string swaggerVersion { get; set; }
        public string basePath { get; set; }
        public string resourcePath { get; set; }
        public string[] produces { get; set; }
        public List<SwaggerAuthApis> apis { get; set; }
        public Dictionary<string, SwaggerAuthModelsLoyaltyProfileBean> models { get; set; }
    }

    //public class SwaggerAuthModels
    //{
    //    //public Dictionary<string, SwaggerAuthModelsLoyaltyProfileBean> test { get; set; } 
    //    //public SwaggerAuthModelsLoyaltyProfileBean LoyaltyProfileBean { get; set; }

    //}

    public class SwaggerAuthModelsLoyaltyProfileBean
    {
        public string id { get; set; }
        public string[] required { get; set; }
        //public SwaggerAuthModelsLoyaltyProfileBeanProperties properties { get; set; }
        public Dictionary<string, SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName> properties { get; set; }
    }

    public class SwaggerAuthModelsLoyaltyProfileBeanProperties
    {
        public SwaggerAuthModelsLoyaltyProfileBeanPropertiesBrand brand { get; set; }
        public SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName accountName { get; set; }
    }

    public class SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName
    {
        public string type { get; set; }
    }

    public class SwaggerAuthModelsLoyaltyProfileBeanPropertiesBrand
    {
        public string type { get; set; }
    }
    public class SwaggerAuthApis
    {
        public string path { get; set; }
        public List<SwaggerAuthApisOperations> operations { get; set; }
    }

    public class SwaggerAuthApisOperations
    {
        public string method { get; set; }
        public string summary { get; set; }
        public string notes { get; set; }
        public SwaggerAuthApisOperationsItems items { get; set; }

        public string nickname { get; set; }
        public SwaggerAuthApisOperationsAutorizations authorizations { get; set; }

        public List<SwaggerAuthApisOperationsParameters> parameters { get; set; }
        public List<SwaggerAuthApisOperationsResponseMessages> responseMessages { get; set; }
        public string deprecated { get; set; }
    }

    public class SwaggerAuthApisOperationsItems
    {
        
    }

    public class SwaggerAuthApisOperationsAutorizations
    {

    }

    public class SwaggerAuthApisOperationsResponseMessages
    {
        public string code { get; set; }
        public string message { get; set; }
    }

    public class SwaggerAuthApisOperationsParameters
    {
        public string name { get; set; }
        public string description { get; set; }
        public bool required { get; set; }
        public string type { get; set; }
        public string paramType { get; set; }
        public bool allowMultiple { get; set; }
        public string defaultValue { get; set; }
    }
}
