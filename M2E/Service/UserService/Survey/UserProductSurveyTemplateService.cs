using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using M2E.Common.Logger;
using M2E.CommonMethods;
using M2E.Models;
using M2E.Models.DataResponse.UserResponse;
using M2E.Models.DataResponse.UserResponse.Survey;

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

        public ResponseModel<UserSurveyResponse> GetTemplateSurveyQuestionsByRefKey(string username, string refKey)
        {
            var response = new ResponseModel<UserSurveyResponse>();
            var surveyJobInfo = _db.CreateTemplateQuestionInfoes.SingleOrDefault(x => x.username == username && x.referenceId == refKey);
            var surveyTemplateInstructionsList = _db.CreateTemplateeditableInstructionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == refKey && x.username == username).ToList();
            var surveyTemplateSingleQuestionsList = _db.CreateTemplateSingleQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == refKey && x.username == username).ToList();
            var surveyTemplateMultipleQuestionsList = _db.CreateTemplateMultipleQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == refKey && x.username == username).ToList();
            var surveyTemplateTextBoxQuestionsList = _db.CreateTemplateTextBoxQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == refKey && x.username == username).ToList();
            var surveyTemplateListBoxQuestionsList = _db.CreateTemplateListBoxQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == refKey && x.username == username).ToList();


            response.Status = 200;
            response.Message = "success";
            response.Payload = new UserSurveyResponse();
            response.Payload.type = surveyJobInfo.type;
            response.Payload.subType = surveyJobInfo.subType;
            response.Payload.surveyTitle = surveyJobInfo.title;
            try
            {
                var UserSurveyInfoInstructionData = new UserSurveyInfoInstruction();
                UserSurveyInfoInstructionData.data = new List<UserSurveyInfoInnerInstructionListData>();
                foreach (var surveyInstruction in surveyTemplateInstructionsList)
                {
                    var UserSurveyInfoInnerInstructionListData = new UserSurveyInfoInnerInstructionListData();
                    UserSurveyInfoInnerInstructionListData.instruction = surveyInstruction.Text;
                    UserSurveyInfoInstructionData.data.Add(UserSurveyInfoInnerInstructionListData);
                }

                var UserSurveyInfoSingleAnswerQueston = new UserSurveyInfoSingleAnswerQueston();
                UserSurveyInfoSingleAnswerQueston.data = new List<UserSurveyInfoInnerListData>();
                foreach (var surveySingleAnswerQuestion in surveyTemplateSingleQuestionsList)
                {
                    var UserSurveyInfoInnerListData = new UserSurveyInfoInnerListData();
                    UserSurveyInfoInnerListData.id = surveySingleAnswerQuestion.Id.ToString();
                    UserSurveyInfoInnerListData.question = surveySingleAnswerQuestion.Question;
                    UserSurveyInfoInnerListData.options = surveySingleAnswerQuestion.Options;
                    UserSurveyInfoSingleAnswerQueston.data.Add(UserSurveyInfoInnerListData);
                }

                var UserSurveyInfoMultipleAnswerQuestion = new UserSurveyInfoMultipleAnswerQuestion();
                UserSurveyInfoMultipleAnswerQuestion.data = new List<UserSurveyInfoInnerListData>();
                foreach (var surveyMultipleAnswerQuestion in surveyTemplateMultipleQuestionsList)
                {
                    var UserSurveyInfoInnerListData = new UserSurveyInfoInnerListData();
                    UserSurveyInfoInnerListData.id = surveyMultipleAnswerQuestion.Id.ToString();
                    UserSurveyInfoInnerListData.question = surveyMultipleAnswerQuestion.Question;
                    UserSurveyInfoInnerListData.options = surveyMultipleAnswerQuestion.Options;
                    UserSurveyInfoMultipleAnswerQuestion.data.Add(UserSurveyInfoInnerListData);
                }

                var UserSurveyInfoListBoxAnswerQuestion = new UserSurveyInfoListBoxAnswerQuestion();
                UserSurveyInfoListBoxAnswerQuestion.data = new List<UserSurveyInfoInnerListData>();
                foreach (var surveyListBoxAnswerQuestion in surveyTemplateListBoxQuestionsList)
                {
                    var UserSurveyInfoInnerListData = new UserSurveyInfoInnerListData();
                    UserSurveyInfoInnerListData.id = surveyListBoxAnswerQuestion.Id.ToString();
                    UserSurveyInfoInnerListData.question = surveyListBoxAnswerQuestion.Question;
                    UserSurveyInfoInnerListData.options = surveyListBoxAnswerQuestion.Options;
                    UserSurveyInfoListBoxAnswerQuestion.data.Add(UserSurveyInfoInnerListData);
                }

                var UserSurveyInfoTextBoxAnswerQuestion = new UserSurveyInfoTextBoxAnswerQuestion();
                UserSurveyInfoTextBoxAnswerQuestion.data = new List<UserSurveyInfoInnerListData>();
                foreach (var surveyTextBoxAnswerQuestion in surveyTemplateTextBoxQuestionsList)
                {
                    var UserSurveyInfoInnerListData = new UserSurveyInfoInnerListData();
                    UserSurveyInfoInnerListData.id = surveyTextBoxAnswerQuestion.Id.ToString();
                    UserSurveyInfoInnerListData.question = surveyTextBoxAnswerQuestion.Question;
                    UserSurveyInfoInnerListData.options = surveyTextBoxAnswerQuestion.Options;
                    UserSurveyInfoTextBoxAnswerQuestion.data.Add(UserSurveyInfoInnerListData);
                }
                response.Payload.Instructions = new UserSurveyInfoInstruction();
                response.Payload.Instructions = UserSurveyInfoInstructionData;

                response.Payload.SingleAnswerQuestion = new UserSurveyInfoSingleAnswerQueston();
                response.Payload.SingleAnswerQuestion = UserSurveyInfoSingleAnswerQueston;

                response.Payload.MultipleAnswerQuestion = new UserSurveyInfoMultipleAnswerQuestion();
                response.Payload.MultipleAnswerQuestion = UserSurveyInfoMultipleAnswerQuestion;

                response.Payload.ListBoxAnswerQuestion = new UserSurveyInfoListBoxAnswerQuestion();
                response.Payload.ListBoxAnswerQuestion = UserSurveyInfoListBoxAnswerQuestion;

                response.Payload.TextBoxAnswerQuestion = new UserSurveyInfoTextBoxAnswerQuestion();
                response.Payload.TextBoxAnswerQuestion = UserSurveyInfoTextBoxAnswerQuestion;
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