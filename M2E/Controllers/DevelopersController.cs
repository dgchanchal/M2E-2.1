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

            //swagger.models = new SwaggerAuthModels
            //{
            //    LoyaltyProfileBean = new SwaggerAuthModelsLoyaltyProfileBean
            //    {
            //        id = "LoyaltyProfileBean",
            //        required = new string[2]
            //    }
            //};
            ////swagger.models.LoyaltyProfileBean.required = new string[2];
            //swagger.models.LoyaltyProfileBean.required[0] = "brand";
            //swagger.models.LoyaltyProfileBean.required[1] = "accountName";
            //swagger.models.LoyaltyProfileBean.properties = new SwaggerAuthModelsLoyaltyProfileBeanProperties
            //{
            //    brand = new SwaggerAuthModelsLoyaltyProfileBeanPropertiesBrand { type = "testing" },
            //    accountName = new SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName { type = "value" }
            //};

            return Json(swagger, JsonRequestBehavior.AllowGet);
        }

        public JsonResult showJsonConfigAuth()
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
            swagger.apis[0] = new SwaggerAuthApis
            {
                path = "Authentication",
                operations = new List<SwaggerAuthApisOperations>(),
            };



            return Json(swagger, JsonRequestBehavior.AllowGet);
        }

        public JsonResult showJsonConfigAuthAuthentication()
        {
            var apiDetailsList = new List<SwaggerApiCommonDetail>();
            var apiDetails = new SwaggerApiCommonDetail
            {
                basePath = "http://www.cautom.com/Auth",
                resourcePath = "/Loyalty",
                produces = new string[2] {"application/json", "application/xml"}
            };

            //apiDetails.apiPath = "/Login";

            var operationModel = new operationsModel
            {
                method = "POST",
                apiPath = "/Login",
                summary = "Cautom Auth API",
                notes = "Cautom Authentication",
                nickname = "authentication"
            };

            var parameters = new parametersModel
            {
                name = "authentication",
                description = "Cautom Auth API",
                required = true,
                paramType = "body",
                allowMultiple = false,
                defaultValue = "",
                type = "authModel"
            };

            var responseMessage = new ResponseMessageModel();
            responseMessage.code = "200";
            responseMessage.message = "success api";

            var LoyaltyProfileBean = new SwaggerAuthModelsLoyaltyProfileBean
            {
                id = parameters.type,
                required = new string[2]
            };
            LoyaltyProfileBean.required = new string[2];
            LoyaltyProfileBean.required[0] = "UserName";
            LoyaltyProfileBean.required[1] = "Password";
            LoyaltyProfileBean.properties = new Dictionary<string, SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName>();
            LoyaltyProfileBean.properties["brand"] = new SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName() { type = "testing" };
            LoyaltyProfileBean.properties["accountName"] = new SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName() { type = "string" };
            //LoyaltyProfileBean["Brand"] = new SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName() {type = "testing"};
            //{
            //    brand = new SwaggerAuthModelsLoyaltyProfileBeanPropertiesBrand { type = "testing" },
            //    accountName = new SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName { type = "value" }
            //};
            
            //swagger.models["LoyaltyProfileBean"] = LoyaltyProfileBean;

            operationModel.parameters = new List<parametersModel>();
            operationModel.parameters.Add(parameters);

            operationModel.ResponseMessage = new List<ResponseMessageModel>();
            operationModel.ResponseMessage.Add(responseMessage);

            apiDetails.operations = new List<operationsModel>();
            apiDetails.operations.Add(operationModel);
            apiDetails.dataType = new List<SwaggerAuthModelsLoyaltyProfileBean>();
            apiDetails.dataType.Add(LoyaltyProfileBean);
            apiDetailsList.Add(apiDetails);

            var swagger = showJsonConfigCommon(apiDetailsList);   
            return Json(swagger, JsonRequestBehavior.AllowGet);
        }

        public SwaggerAuth showJsonConfigCommon(List<SwaggerApiCommonDetail> apiDetailsList)
        {
            
            var swagger = new SwaggerAuth
            {
                apiVersion = "1.0.0",
                swaggerVersion = "1.2",
                basePath = apiDetailsList[0].basePath,
                resourcePath = apiDetailsList[0].resourcePath
                //produces = new string[2]
            };
            int upperCounter = 0;
            foreach (var apiDetails in apiDetailsList)
            {
                
                int producesCounter = 0;
                swagger.produces = new string[apiDetails.produces.Length];
                foreach (var produces in apiDetails.produces)
                {
                    
                    swagger.produces[producesCounter] = produces;
                    producesCounter++;
                }


                swagger.apis = new List<SwaggerAuthApis>();
                var swaggerApis = new SwaggerAuthApis();
                swagger.apis.Add(swaggerApis);

                int operationCounter = 0; // initializing i to 0
                foreach (var operations in apiDetails.operations)
                {
                    swagger.apis[upperCounter] = new SwaggerAuthApis
                    {
                        path = operations.apiPath,
                        operations = new List<SwaggerAuthApisOperations>(),
                        //operations = new List<SwaggerAuthApisOperations>(),
                    };

                    var swaggarApisOperations = new SwaggerAuthApisOperations();
                    swagger.apis[upperCounter].operations.Add(swaggarApisOperations);

                    swagger.apis[upperCounter].operations[operationCounter] = new SwaggerAuthApisOperations
                    {
                        method = operations.method,
                        summary = operations.summary,
                        notes = operations.notes,
                        items = new SwaggerAuthApisOperationsItems(),
                        nickname = operations.nickname,
                        parameters = new List<SwaggerAuthApisOperationsParameters>(),
                        authorizations = new SwaggerAuthApisOperationsAutorizations(),

                    };

                    var swaggerApisOperationsParameters = new SwaggerAuthApisOperationsParameters();
                    swagger.apis[upperCounter].operations[operationCounter].parameters.Add(swaggerApisOperationsParameters);

                    int parametersCounter = 0;
                    foreach (var parameters in operations.parameters)
                    {
                        swagger.apis[upperCounter].operations[operationCounter].parameters[parametersCounter] = new SwaggerAuthApisOperationsParameters
                        {
                            name = parameters.name,
                            description = parameters.description,
                            required = parameters.required,
                            type = parameters.type,
                            paramType = parameters.paramType,
                            allowMultiple = parameters.allowMultiple,
                            defaultValue = parameters.defaultValue
                        };
                        parametersCounter++;
                    }
                    

                    swagger.apis[upperCounter].operations[operationCounter].responseMessages = new List<SwaggerAuthApisOperationsResponseMessages>();
                    var swaggerApisOperationsResponseMessage = new SwaggerAuthApisOperationsResponseMessages();
                    swagger.apis[upperCounter].operations[operationCounter].responseMessages.Add(swaggerApisOperationsResponseMessage);

                    int ResponseMessageCounter = 0;
                    foreach (var ResponseMessage in operations.ResponseMessage)
                    {
                        swagger.apis[upperCounter].operations[operationCounter].responseMessages[ResponseMessageCounter] = new SwaggerAuthApisOperationsResponseMessages
                        {
                            code = ResponseMessage.code,
                            message = ResponseMessage.message
                        };
                        ResponseMessageCounter++;
                    }
                    
                    swagger.apis[upperCounter].operations[operationCounter].deprecated = "false";
                    operationCounter++;
                }


                foreach (var dataType in apiDetails.dataType)
                {
                    
                    swagger.models = new Dictionary<string, SwaggerAuthModelsLoyaltyProfileBean>();
                    swagger.models[dataType.id] = dataType;
                }
                

                upperCounter++;
            }

            return swagger;
        }

        
    }
}
