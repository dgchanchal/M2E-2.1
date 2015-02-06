using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Http.Description;
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
            apiDetailsList.Add(showJsonConfigAuthResendValidationCodeAPIInfo());
            apiDetailsList.Add(showJsonConfigAuthForgetPasswordAPIInfo());
            apiDetailsList.Add(showJsonConfigAuthResetPasswordAPIInfo());
            apiDetailsList.Add(showJsonConfigAuthContactUsAPIInfo());
            var swagger = showJsonConfigCommon(apiDetailsList);   
            return Json(swagger, JsonRequestBehavior.AllowGet);
        }

        public SwaggerApiCommonDetail showJsonConfigAuthAuthenticationAPIInfo()
        {
            var apiDetails = new SwaggerApiCommonDetail
            {
                basePath = "http://" + Request.Url.Authority + "/Auth",
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
                name = "body",
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
                basePath = "http://www.cautom.com/Auth/CreateAccount",
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
                name = "body",
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
                basePath = "http://www.cautom.com/Auth/ValidateAccount",
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
                name = "body",
                description = "Cautom Validate Account API",
                required = true,
                paramType = "body",
                allowMultiple = false,
                defaultValue = "",
                type = "validateAccountModel"
            };
            
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

            var responseMessage200 = new ResponseMessageModel { code = "200", message = "Email Id Successfully validated" };
            var responseMessage500 = new ResponseMessageModel { code = "500", message = "Internal Server Error." };
            var responseMessage405 = new ResponseMessageModel { code = "405", message = "Account Already Validated." };
            var responseMessage402 = new ResponseMessageModel { code = "402", message = "Link Expired." };

            operationModel.ResponseMessage = new List<ResponseMessageModel>
            {
                responseMessage200,
                responseMessage500,
                responseMessage405,
                responseMessage402
            };

            apiDetails.operations = new List<operationsModel> { operationModel };
            apiDetails.dataType = new List<SwaggerAuthModelsLoyaltyProfileBean> { LoyaltyProfileBean };

            return apiDetails;
        }

        public SwaggerApiCommonDetail showJsonConfigAuthResendValidationCodeAPIInfo()
        {
            var apiDetails = new SwaggerApiCommonDetail
            {
                basePath = "http://www.cautom.com/Auth/ValidateAccount",
                resourcePath = "/ResendValidationCode",
                produces = new string[2] { "application/json", "application/xml" }
            };

            //apiDetails.apiPath = "/Login";

            var operationModel = new operationsModel
            {
                method = "POST",
                apiPath = "/ResendValidationCode",
                summary = "Cautom ResendValidationCode Account API",
                notes = "Cautom ResendValidationCode Account Creation",
                nickname = "resendValidationCode"
            };

            var parameters = new parametersModel
            {
                name = "body",
                description = "Cautom ResendValidationCode Account API",
                required = true,
                paramType = "body",
                allowMultiple = false,
                defaultValue = "",
                type = "resendValidationCode"
            };

            

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

            var responseMessage200 = new ResponseMessageModel { code = "200", message = "Validation link successfully sent to registered Email ID." };
            var responseMessage402 = new ResponseMessageModel { code = "402", message = "Email ID already Validated." };
            var responseMessage500 = new ResponseMessageModel { code = "500", message = "Internal Server Error." };
            var responseMessage404 = new ResponseMessageModel { code = "404", message = "Email Id Doesn't exists." };

            operationModel.ResponseMessage = new List<ResponseMessageModel>
            {
                responseMessage200,
                responseMessage500,
                responseMessage404,
                responseMessage402
            };

            apiDetails.operations = new List<operationsModel> { operationModel };
            apiDetails.dataType = new List<SwaggerAuthModelsLoyaltyProfileBean> { LoyaltyProfileBean };

            return apiDetails;
        }

        public SwaggerApiCommonDetail showJsonConfigAuthForgetPasswordAPIInfo()
        {
            var apiDetails = new SwaggerApiCommonDetail
            {
                basePath = "http://www.cautom.com/Auth/ForgetPassword",
                resourcePath = "/ForgetPassword",
                produces = new string[2] { "application/json", "application/xml" }
            };

            //apiDetails.apiPath = "/Login";

            var operationModel = new operationsModel
            {
                method = "GET",
                apiPath = "/ForgetPassword/{emailId}",
                summary = "Cautom ForgetPassword Account API",
                notes = "Cautom ForgetPassword Account Creation",
                nickname = "forgetPassword"
            };

            var parameters = new parametersModel
            {
                name = "emailId",
                description = "Cautom ForgetPassword Account API",
                required = true,
                paramType = "path",
                allowMultiple = false,
                defaultValue = "",
                type = "forgetPassword"
            };


            operationModel.parameters = new List<parametersModel> { parameters };

            var responseMessage200 = new ResponseMessageModel { code = "200", message = "Password reset link sent to registered Email ID." };
            var responseMessage402 = new ResponseMessageModel { code = "402", message = "Account not Validated yet." };
            var responseMessage500 = new ResponseMessageModel { code = "500", message = "Internal Server Error." };
            var responseMessage404 = new ResponseMessageModel { code = "404", message = "User Doesn't exists." };

            operationModel.ResponseMessage = new List<ResponseMessageModel>
            {
                responseMessage200,
                responseMessage500,
                responseMessage404,
                responseMessage402
            };

            apiDetails.operations = new List<operationsModel> { operationModel };            

            return apiDetails;
        }

        public SwaggerApiCommonDetail showJsonConfigAuthResetPasswordAPIInfo()
        {
            var apiDetails = new SwaggerApiCommonDetail
            {
                basePath = "http://www.cautom.com/Auth/ResetPassword",
                resourcePath = "/ResetPassword",
                produces = new string[2] { "application/json", "application/xml" }
            };

            //apiDetails.apiPath = "/Login";

            var operationModel = new operationsModel
            {
                method = "POST",
                apiPath = "/ResetPassword",
                summary = "Cautom ResetPassword Account API",
                notes = "Cautom ResetPassword Account Creation - You need to have UserKey and Guid sent to your registered email id to reset password.",
                nickname = "resetPassword"
            };

            var parameters = new parametersModel
            {
                name = "resetPassword",
                description = "Cautom ResetPassword Account API",
                required = true,
                paramType = "body",
                allowMultiple = false,
                defaultValue = "",
                type = "resetPassword"
            };


            operationModel.parameters = new List<parametersModel> { parameters };

            var responseMessage200 = new ResponseMessageModel { code = "200", message = "Password reset link sent to registered Email ID." };
            var responseMessage402 = new ResponseMessageModel { code = "402", message = "Link Expired." };
            var responseMessage500 = new ResponseMessageModel { code = "500", message = "Internal Server Error." };
            var responseMessage404 = new ResponseMessageModel { code = "404", message = "User Doesn't exists." };

            var LoyaltyProfileBean = new SwaggerAuthModelsLoyaltyProfileBean
            {
                id = parameters.type,
                //required = new string[4]
            };
            //{"Username":"635588348252269728","Guid":"1f267a9f-863f-488c-8bd1-6fcd65b99c09","Password":"password"}
            LoyaltyProfileBean.required = new string[8];
            LoyaltyProfileBean.required[0] = "Username";
            LoyaltyProfileBean.required[1] = "Guid";
            LoyaltyProfileBean.required[2] = "Password";
            

            LoyaltyProfileBean.properties = new Dictionary<string, SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName>();
            LoyaltyProfileBean.properties["Username"] = new SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName() { type = "635588348252269728" };
            LoyaltyProfileBean.properties["Guid"] = new SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName() { type = "1f267a9f-863f-488c-8bd1-6fcd65b99c09" };
            LoyaltyProfileBean.properties["Password"] = new SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName() { type = "NewPassword" };
            

            operationModel.ResponseMessage = new List<ResponseMessageModel>
            {
                responseMessage200,
                responseMessage500,
                responseMessage404,
                responseMessage402
            };

            apiDetails.operations = new List<operationsModel> { operationModel };
            apiDetails.dataType = new List<SwaggerAuthModelsLoyaltyProfileBean> { LoyaltyProfileBean };


            return apiDetails;
        }

        public SwaggerApiCommonDetail showJsonConfigAuthContactUsAPIInfo()
        {
            var apiDetails = new SwaggerApiCommonDetail
            {
                basePath = "http://www.cautom.com/Auth/ContactUs",
                resourcePath = "/ContactUs",
                produces = new string[2] { "application/json", "application/xml" }
            };

            //apiDetails.apiPath = "/Login";

            var operationModel = new operationsModel
            {
                method = "POST",
                apiPath = "/ContactUs",
                summary = "Cautom ContactUs API",
                notes = "Cautom ContactUs API.",
                nickname = "ContactUs"
            };

            var parameters = new parametersModel
            {
                name = "contactUs",
                description = "Cautom ContactUs API",
                required = true,
                paramType = "body",
                allowMultiple = false,
                defaultValue = "",
                type = "contactUs"
            };


            operationModel.parameters = new List<parametersModel> { parameters };

            var responseMessage200 = new ResponseMessageModel { code = "200", message = "Password reset link sent to registered Email ID." };
            
            var responseMessage500 = new ResponseMessageModel { code = "500", message = "Internal Server Error." };
            var responseMessage404 = new ResponseMessageModel { code = "404", message = "User Doesn't exists." };

            var LoyaltyProfileBean = new SwaggerAuthModelsLoyaltyProfileBean
            {
                id = parameters.type,
                //required = new string[4]
            };
            //{"Name":"sumit chourasia","Email":"sumitchourasia91@gmail.com","Phone":"9538700019",
            //"Type":"Support related","Message":"A testing contact us message","SendMeACopy":true}
            LoyaltyProfileBean.required = new string[8];
            LoyaltyProfileBean.required[0] = "Name";
            LoyaltyProfileBean.required[1] = "Email";
            LoyaltyProfileBean.required[2] = "Phone";
            LoyaltyProfileBean.required[0] = "Type";
            LoyaltyProfileBean.required[1] = "Message";
            LoyaltyProfileBean.required[2] = "SendMeACopy";


            LoyaltyProfileBean.properties = new Dictionary<string, SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName>();
            LoyaltyProfileBean.properties["Name"] = new SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName() { type = "YourName" };
            LoyaltyProfileBean.properties["Email"] = new SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName() { type = "youremail@domain.com" };
            LoyaltyProfileBean.properties["Phone"] = new SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName() { type = "9538700019" };
            LoyaltyProfileBean.properties["Type"] = new SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName() { type = "Support related" };
            LoyaltyProfileBean.properties["Message"] = new SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName() { type = "A testing contact us message" };
            LoyaltyProfileBean.properties["SendMeACopy"] = new SwaggerAuthModelsLoyaltyProfileBeanPropertiesAccountName() { type = "true" };


            operationModel.ResponseMessage = new List<ResponseMessageModel>
            {
                responseMessage200,
                responseMessage500,
                responseMessage404
               
            };

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

                if (apiDetails.dataType != null)
                {
                    foreach (var dataType in apiDetails.dataType)
                    {

                        swagger.models[dataType.id] = dataType;
                    }
                }
                
                

                upperCounter++;
            }

            return swagger;
        }

        
    }
}
