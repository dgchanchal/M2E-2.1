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
using M2E.Models.DataWrapper.UserSurvey;
using System.Data.Entity.Validation;
using M2E.signalRPushNotifications;
using Microsoft.AspNet.SignalR;
using M2E.Session;
using System.Configuration;

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
                    string earningPerThreadTemp = Convert.ToString(Convert.ToDouble(job.payPerUser) * (Convert.ToDouble(Convert.ToString(ConfigurationManager.AppSettings["dollarToRupeesValue"]))));
                    string remainingThreadsTemp = Convert.ToString(Convert.ToInt32(job.totalThreads) - _db.UserJobMappings.Where(x => x.refKey == job.referenceId).Count());
                    var userTemplate = new UserProductSurveyTemplateModel
                    {
                        title = job.title,
                        type = job.type,
                        subType = job.subType,
                        refKey = job.referenceId,
                        creationTime = job.creationTime,
                        earningPerThreads = earningPerThreadTemp,
                        currency = "INR", // hard coded currency
                        totalThreads = job.totalThreads, 
                        remainingThreads =  remainingThreadsTemp                 
                    };
                    var AlreadyAppliedJobs = _db.UserJobMappings.SingleOrDefault(x => x.username == username && x.refKey == job.referenceId);
                    if (AlreadyAppliedJobs != null)
                    {
                        userTemplate.userStatus = AlreadyAppliedJobs.status;
                        userTemplate.userDeadline = AlreadyAppliedJobs.expectedDeliveryTime;
                    }
                    else
                    {
                        userTemplate.userStatus = "new";
                        userTemplate.userDeadline = "NA";
                    }
                    response.Payload.Add(userTemplate);
                }

                return response;
            }
            catch (Exception ex)
            {
                response.Status = 500;//some error occured
                response.Message = "failed";
                return response;
            }

        }

        public ResponseModel<UserProductSurveyTemplateModel> GetTemplateInformationByRefKey(string username,string refKey)
        {
            var response = new ResponseModel<UserProductSurveyTemplateModel>();
            var job = _db.CreateTemplateQuestionInfoes.SingleOrDefault(x=>x.referenceId == refKey);
            response.Status = 200;
            response.Message = "success";
            response.Payload = new UserProductSurveyTemplateModel();
            try
            {
                string earningPerThreadTemp = Convert.ToString(Convert.ToDouble(job.payPerUser) *(Convert.ToDouble(ConfigurationManager.AppSettings["dollarToRupeesValue"])));
                string remainingThreadsTemp = Convert.ToString(Convert.ToInt32(job.totalThreads) - _db.UserJobMappings.Where(x => x.refKey == job.referenceId).Count());
                    var userTemplate = new UserProductSurveyTemplateModel
                    {
                        title = job.title,
                        type = job.type,
                        subType = job.subType,
                        refKey = job.referenceId,
                        creationTime = job.creationTime,
                        earningPerThreads = earningPerThreadTemp,
                        currency = "INR", // hard coded currency
                        totalThreads = job.totalThreads, // currently hard coded.
                        remainingThreads = remainingThreadsTemp
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
            var surveyJobInfo = _db.CreateTemplateQuestionInfoes.SingleOrDefault(x => x.referenceId == refKey);
            var surveyTemplateInstructionsList = _db.CreateTemplateeditableInstructionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == refKey).ToList();
            var surveyTemplateSingleQuestionsList = _db.CreateTemplateSingleQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == refKey).ToList();
            var surveyTemplateMultipleQuestionsList = _db.CreateTemplateMultipleQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == refKey).ToList();
            var surveyTemplateTextBoxQuestionsList = _db.CreateTemplateTextBoxQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == refKey).ToList();
            var surveyTemplateListBoxQuestionsList = _db.CreateTemplateListBoxQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == refKey).ToList();


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
                    UserSurveyInfoInnerListData.id = "SAQ-"+surveySingleAnswerQuestion.Id.ToString();
                    UserSurveyInfoInnerListData.question = surveySingleAnswerQuestion.Question;
                    UserSurveyInfoInnerListData.options = surveySingleAnswerQuestion.Options;
                    UserSurveyInfoSingleAnswerQueston.data.Add(UserSurveyInfoInnerListData);
                }

                var UserSurveyInfoMultipleAnswerQuestion = new UserSurveyInfoMultipleAnswerQuestion();
                UserSurveyInfoMultipleAnswerQuestion.data = new List<UserSurveyInfoInnerListData>();
                foreach (var surveyMultipleAnswerQuestion in surveyTemplateMultipleQuestionsList)
                {
                    var UserSurveyInfoInnerListData = new UserSurveyInfoInnerListData();
                    UserSurveyInfoInnerListData.id = "MAQ-" + surveyMultipleAnswerQuestion.Id.ToString();
                    UserSurveyInfoInnerListData.question = surveyMultipleAnswerQuestion.Question;
                    UserSurveyInfoInnerListData.options = surveyMultipleAnswerQuestion.Options;
                    UserSurveyInfoMultipleAnswerQuestion.data.Add(UserSurveyInfoInnerListData);
                }

                var UserSurveyInfoListBoxAnswerQuestion = new UserSurveyInfoListBoxAnswerQuestion();
                UserSurveyInfoListBoxAnswerQuestion.data = new List<UserSurveyInfoInnerListData>();
                foreach (var surveyListBoxAnswerQuestion in surveyTemplateListBoxQuestionsList)
                {
                    var UserSurveyInfoInnerListData = new UserSurveyInfoInnerListData();
                    UserSurveyInfoInnerListData.id = "LAQ-" + surveyListBoxAnswerQuestion.Id.ToString();
                    UserSurveyInfoInnerListData.question = surveyListBoxAnswerQuestion.Question;
                    UserSurveyInfoInnerListData.options = surveyListBoxAnswerQuestion.Options;
                    UserSurveyInfoListBoxAnswerQuestion.data.Add(UserSurveyInfoInnerListData);
                }

                var UserSurveyInfoTextBoxAnswerQuestion = new UserSurveyInfoTextBoxAnswerQuestion();
                UserSurveyInfoTextBoxAnswerQuestion.data = new List<UserSurveyInfoInnerListData>();
                foreach (var surveyTextBoxAnswerQuestion in surveyTemplateTextBoxQuestionsList)
                {
                    var UserSurveyInfoInnerListData = new UserSurveyInfoInnerListData();
                    UserSurveyInfoInnerListData.id = "TAQ-" + surveyTextBoxAnswerQuestion.Id.ToString();
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

        public ResponseModel<string> SubmitTemplateSurveyResultByRefKey(UserSurveyResultRequest EntireSurveyResult, string refKey,string username)
        {
            var response = new ResponseModel<string>();

            if (EntireSurveyResult == null)
            {
                response.Status = 403;
                response.Message = "No data to save to database.";
            }

            if (EntireSurveyResult.surveySingleAnswerQuestion != null)
            {
                foreach (var userSurveyResult in EntireSurveyResult.surveySingleAnswerQuestion)
                {
                    var surveyResponse = new UserSurveyResultToBeReviewed();
                    surveyResponse.questionId = userSurveyResult.key.Split('-')[1];
                    surveyResponse.type = userSurveyResult.key.Split('-')[0];
                    surveyResponse.answer = userSurveyResult.value;
                    surveyResponse.refKey = refKey;
                    surveyResponse.username = username;
                    _db.UserSurveyResultToBeRevieweds1.Add(surveyResponse);
                }
            }

            if (EntireSurveyResult.surveyMultipleAnswerQuestion != null)
            {
                foreach (var userSurveyResult in EntireSurveyResult.surveyMultipleAnswerQuestion)
                {
                    string[] optionValues = userSurveyResult.value.Split(';');
                    for (int i = 0; i < optionValues.Length - 1; i++)
                    {
                        var surveyResponse = new UserSurveyResultToBeReviewed();
                        surveyResponse.questionId = userSurveyResult.key.Split('-')[1];
                        surveyResponse.type = userSurveyResult.key.Split('-')[0];
                        surveyResponse.answer = optionValues[i];
                        surveyResponse.refKey = refKey;
                        surveyResponse.username = username;
                        _db.UserSurveyResultToBeRevieweds1.Add(surveyResponse);
                    }

                }
            }

            if (EntireSurveyResult.surveyListBoxAnswerQuestion != null)
            {
                foreach (var userSurveyResult in EntireSurveyResult.surveyListBoxAnswerQuestion)
                {
                    var surveyResponse = new UserSurveyResultToBeReviewed();
                    surveyResponse.questionId = userSurveyResult.key.Split('-')[1];
                    surveyResponse.type = userSurveyResult.key.Split('-')[0];
                    surveyResponse.answer = userSurveyResult.value;
                    surveyResponse.refKey = refKey;
                    surveyResponse.username = username;
                    _db.UserSurveyResultToBeRevieweds1.Add(surveyResponse);
                }
            }

            if (EntireSurveyResult.surveyTextBoxAnswerQuestion != null)
            {
                foreach (var userSurveyResult in EntireSurveyResult.surveyTextBoxAnswerQuestion)
                {
                    var surveyResponse = new UserSurveyResultToBeReviewed();
                    surveyResponse.questionId = userSurveyResult.key.Split('-')[1];
                    surveyResponse.type = userSurveyResult.key.Split('-')[0];
                    surveyResponse.answer = userSurveyResult.value;
                    surveyResponse.refKey = refKey;
                    surveyResponse.username = username;
                    _db.UserSurveyResultToBeRevieweds1.Add(surveyResponse);
                }
            }
            
            var surveyThreadUserJobMapping = _db.UserJobMappings.SingleOrDefault(x => x.username == username && x.refKey == refKey);
            const string status_done = "done";
            const string status_assigned = "assigned";
            const string status_reviewed = "reviewed";
            surveyThreadUserJobMapping.status = status_done;
            surveyThreadUserJobMapping.endTime = DateTime.Now.ToString();
            try
            {
                _db.SaveChanges();
                var clientJobInfo = _db.CreateTemplateQuestionInfoes.SingleOrDefault(x => x.referenceId == refKey);
                long JobId = clientJobInfo.Id;
                long JobCompleted = _db.UserJobMappings.Where(x => x.refKey == refKey && x.status == status_done).Count();
                long JobAssigned = _db.UserJobMappings.Where(x => x.refKey == refKey && x.status == status_assigned).Count();                
                long JobReviewed = (JobCompleted > 1) ? (JobCompleted) / 2 : 0;  // currently hard coded.

                var SignalRClientHub = new SignalRClientHub();                
                var hubContext = GlobalHost.ConnectionManager.GetHubContext<SignalRClientHub>();
                dynamic client = SignalRManager.getSignalRDetail(clientJobInfo.username);
                if (client != null)
                {
                    client.updateClientProgressChart(Convert.ToString(JobId), clientJobInfo.totalThreads, Convert.ToString(JobCompleted), Convert.ToString(JobAssigned), Convert.ToString(JobReviewed));
                    //client.updateClientProgressChart("8", "20", "10", "8", "5");
                    //client.addMessage("add message signalR");
                }

                response.Status = 200;
                response.Message = "success-";
                response.Payload = refKey;
            }
            catch (DbEntityValidationException e)
            {
                DbContextException.LogDbContextException(e);
                response.Status = 500;
                response.Message = "Failed";
                response.Payload = "Exception Occured";
            }

            return response;
        }

        public ResponseModel<string> AllocateThreadToUserByRefKey(string refKey, string username)
        {
            var response = new ResponseModel<string>();

            var ifAlreadyAllocated = _db.UserJobMappings.SingleOrDefault(x => x.refKey == refKey && x.username == username);
            if (ifAlreadyAllocated != null)
            {
                response.Status = 403;
                response.Message = "already applied";
                return response;
            }
            int expectedDeliveryTimeInMinutes = 15;
            var UserJobMapping = new UserJobMapping();
            UserJobMapping.refKey = refKey;
            UserJobMapping.username = username;
            UserJobMapping.startTime = DateTime.Now.ToString();
            UserJobMapping.expectedDeliveryTime = DateTime.Now.AddMinutes(expectedDeliveryTimeInMinutes).ToString();
            UserJobMapping.endTime = "NA";
            UserJobMapping.status = "assigned";

            _db.UserJobMappings.Add(UserJobMapping);
            try
            {
                _db.SaveChanges();
                const string status_done = "done";
                const string status_assigned = "assigned";
                const string status_reviewed = "reviewed";

                var clientJobInfo = _db.CreateTemplateQuestionInfoes.SingleOrDefault(x => x.referenceId == refKey);
                long JobId = clientJobInfo.Id;
                long JobCompleted = _db.UserJobMappings.Where(x => x.refKey == refKey && x.status == status_done).Count();
                long JobAssigned = _db.UserJobMappings.Where(x => x.refKey == refKey && x.status == status_assigned).Count();                
                long JobReviewed = (JobCompleted > 1) ? (JobCompleted) / 2 : 0;  // currently hard coded.

                var SignalRClientHub = new SignalRClientHub();
                var hubContext = GlobalHost.ConnectionManager.GetHubContext<SignalRClientHub>();
                dynamic client = SignalRManager.getSignalRDetail(clientJobInfo.username);
                if (client != null)
                {
                    client.updateClientProgressChart(Convert.ToString(JobId), clientJobInfo.totalThreads, Convert.ToString(JobCompleted), Convert.ToString(JobAssigned), Convert.ToString(JobReviewed));
                    //client.updateClientProgressChart("8", "20", "10", "8", "5");
                    //client.addMessage("add message signalR");
                }

                response.Status = 200;
                response.Message = "success-";
                response.Payload = refKey;
            }
            catch (DbEntityValidationException e)
            {
                DbContextException.LogDbContextException(e);
                response.Status = 500;
                response.Message = "Failed";
                response.Payload = "Exception Occured";
            }

            return response;
        }

        public ResponseModel<List<UserActiveThreadsResponse>> GetUserActiveThreads(string username, string status)
        {
            var response = new ResponseModel<List<UserActiveThreadsResponse>>();
            response.Payload = new List<UserActiveThreadsResponse>();
            List<UserJobMapping> UserThreads=new List<UserJobMapping>();;
            string statusDone = "done";
            if(status == "active")
                UserThreads = _db.UserJobMappings.OrderBy(x => x.Id).Where(x => x.username == username && x.status != statusDone).ToList();
            else
                UserThreads = _db.UserJobMappings.OrderBy(x => x.Id).Where(x => x.username == username && x.status == statusDone).ToList();
            if (UserThreads == null)
            {
                response.Status = 404;
                response.Message = "No Threads Available for you.";
                return response;
            }
            
            foreach (var thread in UserThreads)
            {                
                var ThreadInfo = _db.CreateTemplateQuestionInfoes.SingleOrDefault(x=> x.referenceId == thread.refKey);
                var UserActiveThreadsResponse = new UserActiveThreadsResponse();
                UserActiveThreadsResponse.startTime = thread.startTime;
                UserActiveThreadsResponse.endTime = thread.endTime;
                UserActiveThreadsResponse.expectedDeliveryTime = thread.expectedDeliveryTime;
                UserActiveThreadsResponse.status = thread.status;
                UserActiveThreadsResponse.refKey = thread.refKey;
                UserActiveThreadsResponse.title = ThreadInfo.title;
                UserActiveThreadsResponse.type = ThreadInfo.type;
                UserActiveThreadsResponse.subType = ThreadInfo.subType;
                response.Payload.Add(UserActiveThreadsResponse);
            }
                response.Status = 200;
                response.Message = "success";
                
            

            return response;
        }
    }
}