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
using M2E.Models.Constants;

namespace M2E.Service.UserService.Survey
{
    public class UserProductSurveyTemplateService
    {
        private static readonly ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        private DbContextException _dbContextException = new DbContextException();
        private readonly M2EContext _db = new M2EContext();

        public ResponseModel<List<UserProductSurveyTemplateModel>> GetAllTemplateInformation(string username)
        {
            return GetAllTemplateInformationIncludingDoneAssigned(username);
            //return GetAllTemplateInformationExcludingDoneAssigned(username);
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
                string remainingThreadsTemp = string.Empty;
                if ((job.type == Constants.type_dataEntry && job.subType == Constants.subType_Transcription)||
                        (job.type == Constants.type_moderation && job.subType == Constants.subType_moderatingPhotos))
                    remainingThreadsTemp = Convert.ToString(Convert.ToInt32(job.totalThreads) - _db.UserMultipleJobMappings.Where(x => x.refKey == job.referenceId).Count());
                else
                    remainingThreadsTemp = Convert.ToString(Convert.ToInt32(job.totalThreads) - _db.UserJobMappings.Where(x => x.refKey == job.referenceId).Count());
                    
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
            const string status_done = "done";
            const string status_assigned = "assigned";
            const string status_reviewed = "reviewed";

            if (EntireSurveyResult == null)
            {
                response.Status = 403;
                response.Message = "No data to save to database.";
                return response;
            }
            var UserAlreadySubmittedForThisSurvey = _db.UserJobMappings.SingleOrDefault(x => x.refKey == refKey && x.username == username && x.status == status_done);
            if(UserAlreadySubmittedForThisSurvey != null)
            {
                response.Status = 405;
                response.Message = "You cann't submit same survey Twice.";
                return response;
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
            
            var clientJobInfo = _db.CreateTemplateQuestionInfoes.SingleOrDefault(x => x.referenceId == refKey);
            var response = new ResponseModel<string>();
            if ((clientJobInfo.type == Constants.type_dataEntry && clientJobInfo.subType == Constants.subType_Transcription) ||
                (clientJobInfo.type == Constants.type_moderation && clientJobInfo.subType == Constants.subType_moderatingPhotos))
            {
                response = AllocateMultipleAssignTypeThreadToUserByRefKey(clientJobInfo,refKey,username);
            }
            else
            {
                response = AllocateSingleAssignTypeThreadToUserByRefKey(clientJobInfo,refKey, username);
            }
            return response;
        }

        public ResponseModel<List<UserActiveThreadsResponse>> GetUserActiveThreads(string username, string status)
        {
            var response = new ResponseModel<List<UserActiveThreadsResponse>>();
            response.Payload = new List<UserActiveThreadsResponse>();
            List<UserJobMapping> UserThreads=new List<UserJobMapping>();
            List<UserMultipleJobMapping> UserMultipleJobMapping = new List<UserMultipleJobMapping>(); ;
            if (status == Constants.status_active)
            {
                UserThreads = _db.UserJobMappings.OrderBy(x => x.Id).Where(x => x.username == username && x.status != Constants.status_done).ToList();
                UserMultipleJobMapping = _db.UserMultipleJobMappings.OrderBy(x => x.Id).Where(x => x.username == username && x.status != Constants.status_done).ToList();
            }
            else
            {
                UserThreads = _db.UserJobMappings.OrderBy(x => x.Id).Where(x => x.username == username && x.status == Constants.status_done).ToList();
                UserMultipleJobMapping = _db.UserMultipleJobMappings.OrderBy(x => x.Id).Where(x => x.username == username && x.status == Constants.status_done).ToList();
            }
            if (UserThreads == null && UserMultipleJobMapping== null)
            {
                response.Status = 404;
                response.Message = "No Threads Available for you.";
                return response;
            }
                        
            foreach (var thread in UserThreads)
            {                
                var ThreadInfo = _db.CreateTemplateQuestionInfoes.SingleOrDefault(x=> x.referenceId == thread.refKey);
                if (ThreadInfo != null)
                {
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
                
            }

            foreach (var thread in UserMultipleJobMapping)
            {
                
                    var ThreadInfo = _db.CreateTemplateQuestionInfoes.SingleOrDefault(x => x.referenceId == thread.refKey);
                    if (ThreadInfo != null)
                    {
                        var UserActiveThreadsResponse = new UserActiveThreadsResponse();
                        UserActiveThreadsResponse.startTime = thread.startTime;
                        UserActiveThreadsResponse.endTime = thread.endTime;
                        UserActiveThreadsResponse.expectedDeliveryTime = thread.expectedDelivery;
                        UserActiveThreadsResponse.status = thread.status;
                        UserActiveThreadsResponse.refKey = thread.refKey;
                        UserActiveThreadsResponse.title = ThreadInfo.title;
                        UserActiveThreadsResponse.type = ThreadInfo.type;
                        UserActiveThreadsResponse.subType = ThreadInfo.subType;
                        response.Payload.Add(UserActiveThreadsResponse);  
                    }                                  
                
            }

                response.Status = 200;
                response.Message = "success";
            
            return response;
        }

        private ResponseModel<string> AllocateMultipleAssignTypeThreadToUserByRefKey(CreateTemplateQuestionInfo clientJobInfo, string refKey, string username)
        {
            var response = new ResponseModel<string>();
            var ifAlreadyAllocated = _db.UserMultipleJobMappings.SingleOrDefault(x => x.refKey == refKey && x.username == username);
            if (ifAlreadyAllocated != null)
            {
                response.Status = 403;
                response.Message = "You have already applied for this job";
                return response;
            }
            const int expectedDeliveryTimeInMinutes = 15;
                        
            var UserMultipleJobMapping = new UserMultipleJobMapping();
            UserMultipleJobMapping.endTime = Constants.NA;
            UserMultipleJobMapping.expectedDelivery = DateTime.Now.AddMinutes(expectedDeliveryTimeInMinutes).ToString();
            UserMultipleJobMapping.refKey = refKey;
            UserMultipleJobMapping.startTime = DateTime.Now.ToString();
            UserMultipleJobMapping.status = Constants.status_assigned;

            if (clientJobInfo.type == Constants.type_moderation && clientJobInfo.subType == Constants.subType_moderatingPhotos)
            {
                UserMultipleJobMapping.subType = Constants.subType_moderatingPhotos;
                UserMultipleJobMapping.type = Constants.type_moderation;
            }
            else if (clientJobInfo.type == Constants.type_dataEntry && clientJobInfo.subType == Constants.subType_Transcription)
            {
                UserMultipleJobMapping.subType = Constants.subType_Transcription;
                UserMultipleJobMapping.type = Constants.type_dataEntry;
            }
            UserMultipleJobMapping.surveyResult = Constants.NA;
            UserMultipleJobMapping.username = username;

            var availableJobLists = _db.CreateTemplateImgurImagesLists.Where(x => x.referenceKey == refKey && x.status == Constants.status_open).ToList();
            if (availableJobLists != null)
            {
                if (availableJobLists.Count == 0)
                {
                    response.Status = 406;
                    response.Message = "All Threads of this job is already assigned.";
                    return response;
                }
                var transcriptionTask = availableJobLists.First();
                try
                {
                    //to be inclucded in lock
                    lock (this)
                    {
                        UserMultipleJobMapping.imageKey = transcriptionTask.imgurLink;
                        var updateImgurImageMapAfterAssigning = _db.CreateTemplateImgurImagesLists.SingleOrDefault(x => x.Id == transcriptionTask.Id);
                        updateImgurImageMapAfterAssigning.status = Constants.status_assigned;
                        _db.UserMultipleJobMappings.Add(UserMultipleJobMapping);
                        _db.SaveChanges();
                    }                    
                    //to be inclucded in lock
                    

                    long JobId = clientJobInfo.Id;
                    long JobCompleted = _db.UserMultipleJobMappings.Where(x => x.refKey == refKey && x.status == Constants.status_done).Count();
                    long JobAssigned = _db.UserMultipleJobMappings.Where(x => x.refKey == refKey && x.status == Constants.status_assigned).Count();
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
            }
            //UserMultipleJobMapping.imageKey = 
            

            return response;
        }

        private ResponseModel<string> AllocateSingleAssignTypeThreadToUserByRefKey(CreateTemplateQuestionInfo clientJobInfo, string refKey, string username)
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
            UserJobMapping.status = Constants.status_assigned;

            _db.UserJobMappings.Add(UserJobMapping);
            try
            {
                _db.SaveChanges();

                long JobId = clientJobInfo.Id;
                long JobCompleted = _db.UserJobMappings.Where(x => x.refKey == refKey && x.status == Constants.status_done).Count();
                long JobAssigned = _db.UserJobMappings.Where(x => x.refKey == refKey && x.status == Constants.status_assigned).Count();
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

        private ResponseModel<List<UserProductSurveyTemplateModel>> GetAllTemplateInformationIncludingDoneAssigned(string username)
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
                    string remainingThreadsTemp = string.Empty;

                    var userTemplate = new UserProductSurveyTemplateModel
                    {
                        title = job.title,
                        type = job.type,
                        subType = job.subType,
                        refKey = job.referenceId,
                        creationTime = job.creationTime,
                        earningPerThreads = earningPerThreadTemp,
                        currency = "INR", // hard coded currency
                        totalThreads = job.totalThreads
                    };

                    if ((job.type == Constants.type_dataEntry && job.subType == Constants.subType_Transcription)||
                        (job.type == Constants.type_moderation && job.subType == Constants.subType_moderatingPhotos))
                    {
                        var AlreadyAppliedJobs = _db.UserMultipleJobMappings.SingleOrDefault(x => x.username == username && x.refKey == job.referenceId);
                        if (AlreadyAppliedJobs != null)
                        {
                            userTemplate.userStatus = AlreadyAppliedJobs.status;
                            userTemplate.userDeadline = AlreadyAppliedJobs.expectedDelivery;
                        }
                        else
                        {
                            userTemplate.userStatus = "new";
                            userTemplate.userDeadline = "NA";
                        }
                        remainingThreadsTemp = Convert.ToString(Convert.ToInt32(job.totalThreads) - _db.UserMultipleJobMappings.Where(x => x.refKey == job.referenceId).Count());
                    }
                    else
                    {
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
                        remainingThreadsTemp = Convert.ToString(Convert.ToInt32(job.totalThreads) - _db.UserJobMappings.Where(x => x.refKey == job.referenceId).Count());
                    }

                    userTemplate.remainingThreads = remainingThreadsTemp;


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

        private ResponseModel<List<UserProductSurveyTemplateModel>> GetAllTemplateInformationExcludingDoneAssigned(string username)
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
                    string remainingThreadsTemp = string.Empty;

                    var userTemplate = new UserProductSurveyTemplateModel
                    {
                        title = job.title,
                        type = job.type,
                        subType = job.subType,
                        refKey = job.referenceId,
                        creationTime = job.creationTime,
                        earningPerThreads = earningPerThreadTemp,
                        currency = "INR", // hard coded currency
                        totalThreads = job.totalThreads
                    };

                    if (job.type == Constants.type_dataEntry && job.subType == Constants.subType_Transcription)
                    {
                        var AlreadyAppliedJobs = _db.UserMultipleJobMappings.SingleOrDefault(x => x.username == username && x.refKey == job.referenceId && x.status != Constants.status_done);
                        if (AlreadyAppliedJobs != null)
                        {
                            continue;
                        }
                        else
                        {
                            userTemplate.userStatus = "new";
                            userTemplate.userDeadline = "NA";
                        }
                        remainingThreadsTemp = Convert.ToString(Convert.ToInt32(job.totalThreads) - _db.UserMultipleJobMappings.Where(x => x.refKey == job.referenceId).Count());
                    }
                    else
                    {
                        var AlreadyAppliedJobs = _db.UserJobMappings.SingleOrDefault(x => x.username == username && x.refKey == job.referenceId);
                        if (AlreadyAppliedJobs != null)
                        {
                            continue;
                        }
                        else
                        {
                            userTemplate.userStatus = "new";
                            userTemplate.userDeadline = "NA";
                        }
                        remainingThreadsTemp = Convert.ToString(Convert.ToInt32(job.totalThreads) - _db.UserJobMappings.Where(x => x.refKey == job.referenceId).Count());
                    }

                    userTemplate.remainingThreads = remainingThreadsTemp;


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
    }
}