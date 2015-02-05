using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using M2E.CommonMethods;
using M2E.Models;
using M2E.Models.Constants;
using M2E.Models.Swagger;

namespace M2E.Controllers
{
    public class DevelopersController : Controller
    {
        //
        // GET: /Developers/

        public ActionResult Index()
        {
            return View();
        }

        
        public JsonResult showJsonConfig()
        {
            var deviceId = Request.QueryString["deviceId"];
            var swagger = new SwaggerAuth
            {
                apiVersion = "1.0.0",
                swaggerVersion = "1.2",
               
            };
            

            swagger.apis = new List<SwaggerAuthApis>();
            var swaggerApis = new SwaggerAuthApis();
            swagger.apis.Add(swaggerApis);
            swagger.apis[0]= new SwaggerAuthApis
            {
                path = "Detail",
                operations = new List<SwaggerAuthApisOperations>(),
            };

            

            return Json(swagger, JsonRequestBehavior.AllowGet);
        }

        public JsonResult showJsonConfigDetail()
        {
            var deviceId = Request.QueryString["deviceId"];
            var swagger = new SwaggerAuth
            {
                apiVersion = "1.0.0",
                swaggerVersion = "1.2",
                basePath = "http://www.cautom.com",
                resourcePath = "/Loyalty",
                produces = new string[2]
            };
            swagger.produces[0] = "application/json";
            swagger.produces[1] = "application/xml";

            swagger.apis = new List<SwaggerAuthApis>();
            var swaggerApis = new SwaggerAuthApis();
            swagger.apis.Add(swaggerApis);
            swagger.apis[0] = new SwaggerAuthApis
            {
                path = "/loyalty/v1/profile/",
                operations = new List<SwaggerAuthApisOperations>(),
                //operations = new List<SwaggerAuthApisOperations>(),
            };

            var swaggarApisOperations = new SwaggerAuthApisOperations();
            swagger.apis[0].operations.Add(swaggarApisOperations);
            swagger.apis[0].operations[0] = new SwaggerAuthApisOperations
            {
                method = "GET",
                summary = "summaryss",
                notes = "notesss",
                items = new SwaggerAuthApisOperationsItems(),
                nickname = "loyaltyss",
                parameters = new List<SwaggerAuthApisOperationsParameters>(),
                authorizations = new SwaggerAuthApisOperationsAutorizations(),
                
            };

            var swaggerApisOperationsParameters = new SwaggerAuthApisOperationsParameters();
            swagger.apis[0].operations[0].parameters.Add(swaggerApisOperationsParameters);
            swagger.apis[0].operations[0].parameters[0] = new SwaggerAuthApisOperationsParameters
            {
                name = "loyalty",
                description = "description",
                required = true,
                type = "LoyaltyProfileBean",
                paramType = "body",
                allowMultiple = false,
                defaultValue = ""
            };

            swagger.apis[0].operations[0].responseMessages = new List<SwaggerAuthApisOperationsResponseMessages>();
            var swaggerApisOperationsResponseMessage = new SwaggerAuthApisOperationsResponseMessages();
            swagger.apis[0].operations[0].responseMessages.Add(swaggerApisOperationsResponseMessage);
            swagger.apis[0].operations[0].responseMessages[0] = new SwaggerAuthApisOperationsResponseMessages
            {
                code = "200",
                message = "The request succeeded."
            };
            swagger.apis[0].operations[0].deprecated = "false";

            swagger.models = new SwaggerAuthModels
            {
                LoyaltyProfileBean = new SwaggerAuthModelsLoyaltyProfileBean
                {
                    id = "LoyaltyProfileBean",
                    required = new string[2]
                }
            };
            //swagger.models.LoyaltyProfileBean.required = new string[2];
            swagger.models.LoyaltyProfileBean.required[0] = "brand";
            swagger.models.LoyaltyProfileBean.required[1] = "accountName";
            swagger.models.LoyaltyProfileBean.properties = new SwaggerAuthModelsLoyaltyProfileBeanProperties
            {
                brand = new SwaggerAuthModelsLoyaltyProfileBeanPropertiesBrand { type = "testing" },
                accountName = new SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName { type = "value" }
            };

            return Json(swagger, JsonRequestBehavior.AllowGet);
        }
    }
}
