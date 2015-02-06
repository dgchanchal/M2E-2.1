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
            apiDetailsList.Add(showJsonConfigAuthAuthenticationAPIInfo());
            apiDetailsList.Add(showJsonConfigAuthCreateAccountAPIInfo());
            apiDetailsList.Add(showJsonConfigAuthValidateAccountAPIInfo());
            var swagger = showJsonConfigCommon(apiDetailsList);   
            return Json(swagger, JsonRequestBehavior.AllowGet);
        }

        public SwaggerApiCommonDetail showJsonConfigAuthAuthenticationAPIInfo()
        {
            var apiDetails = new SwaggerApiCommonDetail
            {
                basePath = "http://www.cautom.com/Auth",
                resourcePath = "/Auth",
                produces = new string[2] { "application/json", "application/xml" }
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

            
            var LoyaltyProfileBean = new SwaggerAuthModelsLoyaltyProfileBean
            {
                id = parameters.type,
                required = new string[4]
            };

            //{"Username":"sumitchourasia91@gmail.com","Password":"password","Type":"web","KeepMeSignedInCheckBox":true}

            LoyaltyProfileBean.required = new string[4];
            LoyaltyProfileBean.required[0] = "Username";
            LoyaltyProfileBean.required[1] = "Password";
            LoyaltyProfileBean.required[2] = "Type";
            LoyaltyProfileBean.required[3] = "KeepMeSignedInCheckBox";
            LoyaltyProfileBean.properties = new Dictionary<string, SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName>();
            LoyaltyProfileBean.properties["Username"] = new SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName() { type = "sumitchourasia91@gmail.com" };
            LoyaltyProfileBean.properties["Password"] = new SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName() { type = "password" };
            LoyaltyProfileBean.properties["Type"] = new SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName() { type = "web" };
            LoyaltyProfileBean.properties["KeepMeSignedInCheckBox"] = new SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName() { type = "true" };

            operationModel.parameters = new List<parametersModel> { parameters };

            var responseMessage200 = new ResponseMessageModel { code = "200", message = "Successfully login" };
            var responseMessage401 = new ResponseMessageModel { code = "401", message = "Unauthorized User" };
            var responseMessage500 = new ResponseMessageModel { code = "500", message = "Internal Server Error Occured" };
            var responseMessage403 = new ResponseMessageModel { code = "403", message = "Database Error Occured" };

            operationModel.ResponseMessage = new List<ResponseMessageModel>
            {
                responseMessage200,
                responseMessage401,
                responseMessage500,
                responseMessage403
            };

            apiDetails.operations = new List<operationsModel> { operationModel };
            apiDetails.dataType = new List<SwaggerAuthModelsLoyaltyProfileBean> { LoyaltyProfileBean };
            
            return apiDetails;
        }

        public SwaggerApiCommonDetail showJsonConfigAuthCreateAccountAPIInfo()
        {
            var apiDetails = new SwaggerApiCommonDetail
            {
                basePath = "http://www.cautom.com/CreateAccount",
                resourcePath = "/CreateAccount",
                produces = new string[2] { "application/json", "application/xml" }
            };

            //apiDetails.apiPath = "/Login";

            var operationModel = new operationsModel
            {
                method = "POST",
                apiPath = "/CreateAccount",
                summary = "Cautom Create Account API",
                notes = "Cautom Account Creation",
                nickname = "accountCreation"
            };

            var parameters = new parametersModel
            {
                name = "accountCreation",
                description = "Cautom Create Account API",
                required = true,
                paramType = "body",
                allowMultiple = false,
                defaultValue = "",
                type = "createAccountModel"
            };

            
            var LoyaltyProfileBean = new SwaggerAuthModelsLoyaltyProfileBean
            {
                id = parameters.type,
                //required = new string[4]
            };

            //{"FirstName":"sumit","LastName":"chourasia","Username":"useyouruniqueid@domain.com",
            //"Password":"password","CompanyName":"myCompany","Type":"client","Source":"web","Referral":"NA"}

            LoyaltyProfileBean.required = new string[8];
            LoyaltyProfileBean.required[0] = "FirstName";
            LoyaltyProfileBean.required[1] = "LastName";
            LoyaltyProfileBean.required[2] = "Username";
            LoyaltyProfileBean.required[3] = "Password";
            LoyaltyProfileBean.required[4] = "CompanyName";
            LoyaltyProfileBean.required[5] = "Type";
            LoyaltyProfileBean.required[6] = "Source";
            LoyaltyProfileBean.required[7] = "Referral";

            LoyaltyProfileBean.properties = new Dictionary<string, SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName>();
            LoyaltyProfileBean.properties["FirstName"] = new SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName() { type = "Sumit" };
            LoyaltyProfileBean.properties["LastName"] = new SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName() { type = "Chourasia" };
            LoyaltyProfileBean.properties["Username"] = new SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName() { type = "useyouruniqueid@domain.com" };
            LoyaltyProfileBean.properties["Password"] = new SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName() { type = "password" };
            LoyaltyProfileBean.properties["CompanyName"] = new SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName() { type = "myCompanyName" };
            LoyaltyProfileBean.properties["Type"] = new SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName() { type = "client" };
            LoyaltyProfileBean.properties["Source"] = new SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName() { type = "web" };
            LoyaltyProfileBean.properties["Referral"] = new SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName() { type = "NA" };


            operationModel.parameters = new List<parametersModel> { parameters };

            var responseMessage200 = new ResponseMessageModel { code = "200", message = "Successfully Created Account. Check Email id provided for guid." };
            var responseMessage409 = new ResponseMessageModel { code = "409", message = "Email ID provided is already registered." };
            var responseMessage500 = new ResponseMessageModel { code = "500", message = "Internal Server Error Occured." };


            operationModel.ResponseMessage = new List<ResponseMessageModel>
            {
                responseMessage200,
                responseMessage409,
                responseMessage500
            };

            apiDetails.operations = new List<operationsModel> { operationModel };
            apiDetails.dataType = new List<SwaggerAuthModelsLoyaltyProfileBean> { LoyaltyProfileBean };

            return apiDetails;
        }

        public SwaggerApiCommonDetail showJsonConfigAuthValidateAccountAPIInfo()
        {
            var apiDetails = new SwaggerApiCommonDetail
            {
                basePath = "http://www.cautom.com/ValidateAccount",
                resourcePath = "/ValidateAccount",
                produces = new string[2] { "application/json", "application/xml" }
            };

            //apiDetails.apiPath = "/Login";

            var operationModel = new operationsModel
            {
                method = "POST",
                apiPath = "/ValidateAccount",
                summary = "Cautom Validate Account API",
                notes = "Cautom Validate Account Creation",
                nickname = "validateAccountCreation"
            };

            var parameters = new parametersModel
            {
                name = "validateAccountCreation",
                description = "Cautom Validate Account API",
                required = true,
                paramType = "body",
                allowMultiple = false,
                defaultValue = "",
                type = "validateAccountModel"
            };

            var responseMessage = new ResponseMessageModel();
            responseMessage.code = "200";
            responseMessage.message = "success api";

            var LoyaltyProfileBean = new SwaggerAuthModelsLoyaltyProfileBean
            {
                id = parameters.type,
                //required = new string[4]
            };

            //{"userName":"sum_kumar12@yahoo.co.in","guid":"7e1befc7-8185-4f84-bbab-aa754c615eac"}
            LoyaltyProfileBean.required = new string[2];
            LoyaltyProfileBean.required[0] = "userName";
            LoyaltyProfileBean.required[1] = "guid";
            

            LoyaltyProfileBean.properties = new Dictionary<string, SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName>();
            LoyaltyProfileBean.properties["userName"] = new SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName() { type = "sum_kumar12@yahoo.co.in" };
            LoyaltyProfileBean.properties["guid"] = new SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName() { type = "7e1befc7-8185-4f84-bbab-aa754c615eac" };
            

            operationModel.parameters = new List<parametersModel> { parameters };

            operationModel.ResponseMessage = new List<ResponseMessageModel> { responseMessage };

            apiDetails.operations = new List<operationsModel> { operationModel };
            apiDetails.dataType = new List<SwaggerAuthModelsLoyaltyProfileBean> { LoyaltyProfileBean };

            return apiDetails;
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
            swagger.apis = new List<SwaggerAuthApis>();
            swagger.models = new Dictionary<string, SwaggerAuthModelsLoyaltyProfileBean>();
            swagger.produces = new string[apiDetailsList[0].produces.Length];
            foreach (var apiDetails in apiDetailsList)
            {
                
                int producesCounter = 0;
                
                foreach (var produces in apiDetails.produces)
                {
                    
                    swagger.produces[producesCounter] = produces;
                    producesCounter++;
                }
                
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
                    

                    int ResponseMessageCounter = 0;
                    foreach (var ResponseMessage in operations.ResponseMessage)
                    {
                        var swaggerApisOperationsResponseMessage = new SwaggerAuthApisOperationsResponseMessages();
                        swagger.apis[upperCounter].operations[operationCounter].responseMessages.Add(swaggerApisOperationsResponseMessage);
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
                                        
                    swagger.models[dataType.id] = dataType;
                }
                

                upperCounter++;
            }

            return swagger;
        }

        
    }
}
