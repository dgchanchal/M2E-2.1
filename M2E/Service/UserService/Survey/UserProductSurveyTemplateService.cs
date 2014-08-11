using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using M2E.Common.Logger;
using M2E.CommonMethods;
using M2E.Models;
using M2E.Models.DataResponse.UserResponse;

namespace M2E.Service.UserService.Survey
{
    public class UserProductSurveyTemplateService
    {
        private static readonly ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        private DbContextException _dbContextException = new DbContextException();
        private readonly M2EContext _db = new M2EContext();

        public ResponseModel<List<UserProductSurveyTemplateModel>> GetAllTemplateInformation(string username)
        {
            var response = new ResponseModel<List<UserProductSurveyTemplateModel>>();
            var templateData = _db.CreateTemplateQuestionInfoes.OrderByDescending(x => x.creationTime).ToList();
            response.Status = 200;
            response.Message = "success";
            response.Payload = new List<UserProductSurveyTemplateModel>();
            try
            {
                foreach (var job in templateData)
                {
                    var userTemplate = new UserProductSurveyTemplateModel
                    {
                        title = job.title,
                        type = job.type,
                        subType = job.subType,
                        refKey = job.referenceId,
                        creationTime = job.creationTime,
                        earningPerThreads = "50",
                        currency = "INR",
                        totalThreads = "5000", // currently hard coded.
                        remainingThreads = "500"// currently hard coded.                       
                    };
                    response.Payload.Add(userTemplate);
                }

                return response;
            }
            catch (Exception)
            {
                response.Status = 500;//some error occured
                response.Message = "failed";
                return response;
            }

        }

        public ResponseModel<UserProductSurveyTemplateModel> GetTemplateInformationByRefKey(string username,string refKey)
        {
            var response = new ResponseModel<UserProductSurveyTemplateModel>();
            var job = _db.CreateTemplateQuestionInfoes.SingleOrDefault(x=>x.username == username && x.referenceId == refKey);
            response.Status = 200;
            response.Message = "success";
            response.Payload = new UserProductSurveyTemplateModel();
            try
            {
                
                    var userTemplate = new UserProductSurveyTemplateModel
                    {
                        title = job.title,
                        type = job.type,
                        subType = job.subType,
                        refKey = job.referenceId,
                        creationTime = job.creationTime,
                        earningPerThreads = "50",
                        currency = "INR",
                        totalThreads = "5000", // currently hard coded.
                        remainingThreads = "500"// currently hard coded.                       
                    };
                    response.Payload =userTemplate;
                

                return response;
            }
            catch (Exception)
            {
                response.Status = 500;//some error occured
                response.Message = "failed";
                return response;
            }

        }

    }
}