using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M2E.Models.DataResponse;
using M2E.Models;
using M2E.Common.Logger;
using M2E.CommonMethods;
using System.Reflection;
using System.Globalization;
using M2E.Models.DataWrapper.CreateTemplate;
using System.Data.Entity.Validation;
using M2E.Service.JobTemplate.CommonMethods;
using M2E.Session;
using M2E.Models.DataWrapper;
using M2E.signalRPushNotifications;
using Microsoft.AspNet.SignalR;
using M2E.Models.DataResponse.ClientResponse;
using M2E.Models.Constants;
using Newtonsoft.Json;
using M2E.DAO;

namespace M2E.Service.JobTemplate
{
    public class ClientTemplateService
    {
        private static readonly ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        private DbContextException _dbContextException = new DbContextException();
        private readonly M2EContext _db = new M2EContext();

        public ResponseModel<List<ClientTemplateResponse>> GetAllTemplateInformation(string username)
        {            
            var response = new ResponseModel<List<ClientTemplateResponse>>();
            var templateData = _db.CreateTemplateQuestionInfoes.OrderByDescending(x => x.creationTime).Where(x=>x.username == username).ToList();

            response.Status = 200;
            response.Message = "success";
            response.Payload = new List<ClientTemplateResponse>();
            try
            {
                foreach (var job in templateData)
                {
                    if (job.type == Constants.type_dataEntry && job.subType == Constants.subType_Transcription)
                    {
                        long JobCompleted = _db.UserMultipleJobMappings.Where(x => x.refKey == job.referenceId && x.status == Constants.status_done).Count();
                        long JobAssigned = _db.UserMultipleJobMappings.Where(x => x.refKey == job.referenceId && x.status == Constants.status_assigned).Count();
                        long JobReviewed = (JobCompleted > 1) ? (JobCompleted) / 2 : 0;  // currently hard coded.
                        if (JobCompleted > Convert.ToInt32(job.totalThreads))
                            JobCompleted = Convert.ToInt32(job.totalThreads);
                        var clientTemplate = new ClientTemplateResponse
                        {
                            title = job.title,
                            creationDate = job.creationTime.Split(' ')[0],
                            showTime = " 4 hours",
                            editId = job.Id.ToString(CultureInfo.InvariantCulture),
                            showEllipse = true,
                            timeShowType = "success",
                            progressPercent = Convert.ToString(System.Math.Ceiling(((JobCompleted) * 100 / Convert.ToDouble(job.totalThreads)) * 100) / 100),
                            JobCompleted = Convert.ToString(JobCompleted),
                            JobAssigned = Convert.ToString(JobAssigned),
                            JobTotal = job.totalThreads,
                            JobReviewed = Convert.ToString(JobReviewed),
                            type = job.type,
                            subType = job.subType
                        };
                        response.Payload.Add(clientTemplate);
                    }
                    else
                    {
                        long JobCompleted = _db.UserJobMappings.Where(x => x.refKey == job.referenceId && x.status == Constants.status_done).Count();
                        long JobAssigned = _db.UserJobMappings.Where(x => x.refKey == job.referenceId && x.status == Constants.status_assigned).Count();
                        long JobReviewed = (JobCompleted > 1) ? (JobCompleted) / 2 : 0;  // currently hard coded.
                        if (JobCompleted > Convert.ToInt32(job.totalThreads))
                            JobCompleted = Convert.ToInt32(job.totalThreads);
                        var clientTemplate = new ClientTemplateResponse
                        {
                            title = job.title,
                            creationDate = job.creationTime.Split(' ')[0],
                            showTime = " 4 hours",
                            editId = job.Id.ToString(CultureInfo.InvariantCulture),
                            showEllipse = true,
                            timeShowType = "success",
                            progressPercent = Convert.ToString(System.Math.Ceiling(((JobCompleted) * 100 / Convert.ToDouble(job.totalThreads)) * 100) / 100),
                            JobCompleted = Convert.ToString(JobCompleted),
                            JobAssigned = Convert.ToString(JobAssigned),
                            JobTotal = job.totalThreads,
                            JobReviewed = Convert.ToString(JobReviewed),
                            type = job.type,
                            subType = job.subType
                        };
                        response.Payload.Add(clientTemplate);
                    }
                    
                }

                var FacebookLikeTemplateData = _db.CreateTemplateFacebookLikes.OrderByDescending(x => x.creationTime).Where(x => x.username == username).ToList();
                if (FacebookLikeTemplateData != null)
                {
                    foreach (var facebookJob in FacebookLikeTemplateData)
                    {
                        long JobCompleted = new FacebookDAO().facebookLikeCompletedThreadsWithRefKey(facebookJob.referenceId);                        
                        if (JobCompleted > Convert.ToInt32(facebookJob.totalThreads))
                            JobCompleted = Convert.ToInt32(facebookJob.totalThreads);
                        long JobAssigned = JobCompleted;
                        long JobReviewed = JobCompleted;
                        var clientTemplate = new ClientTemplateResponse
                        {
                            title = facebookJob.title,
                            creationDate = facebookJob.creationTime,
                            showTime = " 4 hours",
                            editId = facebookJob.Id.ToString(CultureInfo.InvariantCulture),
                            showEllipse = true,
                            timeShowType = "success",
                            progressPercent = Convert.ToString(System.Math.Ceiling(((JobCompleted) * 100 / Convert.ToDouble(facebookJob.totalThreads)) * 100) / 100),
                            JobCompleted = Convert.ToString(JobCompleted),
                            JobAssigned = Convert.ToString(JobAssigned),
                            JobTotal = facebookJob.totalThreads,
                            JobReviewed = Convert.ToString(JobReviewed),
                            type = facebookJob.type,
                            subType = facebookJob.subType
                        };
                        response.Payload.Add(clientTemplate);                 
                    }
                }
                response.Payload = response.Payload.OrderByDescending(x => x.creationDate).ToList();
                return response;
            }
            catch (Exception)
            {
                response.Status = 500;//some error occured
                response.Message = "failed";
                return response;
            }
            
        }

        public ResponseModel<ClientTemplateResponse> GetTemplateInformationByRefKey(string username,long id,string type,string subType)
        {
            var response = new ResponseModel<ClientTemplateResponse>();                              
            //var job = _db.CreateTemplateQuestionInfoes.SingleOrDefault(x => x.referenceId == clientJobInfo.referenceId && x.username == username);
            response.Status = 200;
            response.Message = "success";
            response.Payload = new ClientTemplateResponse();
            try
            {
                if (type == Constants.type_Ads && subType == Constants.subType_facebookLike)
                {
                    var clientFacebookLikeJobInfo = _db.CreateTemplateFacebookLikes.SingleOrDefault(x => x.Id == id && x.username == username);

                    long JobCompleted = new FacebookDAO().facebookLikeCompletedThreadsWithRefKey(clientFacebookLikeJobInfo.referenceId);
                    if (JobCompleted > Convert.ToInt32(clientFacebookLikeJobInfo.totalThreads))
                        JobCompleted = Convert.ToInt32(clientFacebookLikeJobInfo.totalThreads);

                    long JobAssigned = JobCompleted;
                    long JobReviewed = JobCompleted;
                    
                    var clientTemplate = new ClientTemplateResponse
                    {
                        title = clientFacebookLikeJobInfo.title,
                        creationDate = clientFacebookLikeJobInfo.creationTime,
                        showTime = " 4 hours",
                        editId = clientFacebookLikeJobInfo.Id.ToString(CultureInfo.InvariantCulture),
                        showEllipse = true,
                        timeShowType = "success",
                        progressPercent = Convert.ToString(System.Math.Ceiling(((JobCompleted) * 100 / Convert.ToDouble(clientFacebookLikeJobInfo.totalThreads)) * 100) / 100),
                        JobCompleted = Convert.ToString(JobCompleted),
                        JobAssigned = Convert.ToString(JobAssigned),
                        JobTotal = Convert.ToString(clientFacebookLikeJobInfo.totalThreads),
                        JobReviewed = Convert.ToString(JobReviewed),
                        type = clientFacebookLikeJobInfo.type,
                        subType = clientFacebookLikeJobInfo.subType,
                        refKey = clientFacebookLikeJobInfo.referenceId
                    };
                    response.Payload = clientTemplate;                
                }
                else
                {
                    var clientJobInfo = _db.CreateTemplateQuestionInfoes.SingleOrDefault(x => x.Id == id && x.username == username);  

                    if ((clientJobInfo.type == Constants.type_dataEntry && clientJobInfo.subType == Constants.subType_Transcription) ||
                    (clientJobInfo.type == Constants.type_moderation && clientJobInfo.subType == Constants.subType_moderatingPhotos))
                    {
                        long JobCompleted = _db.UserMultipleJobMappings.Where(x => x.refKey == clientJobInfo.referenceId && x.status == Constants.status_done).Count();
                        long JobAssigned = _db.UserMultipleJobMappings.Where(x => x.refKey == clientJobInfo.referenceId && x.status == Constants.status_assigned).Count();
                        long JobReviewed = (JobCompleted > 1) ? (JobCompleted) / 2 : 0;  // currently hard coded.

                        if (JobCompleted > Convert.ToInt32(clientJobInfo.totalThreads))
                            JobCompleted = Convert.ToInt32(clientJobInfo.totalThreads);

                        var clientTemplate = new ClientTemplateResponse
                        {
                            title = clientJobInfo.title,
                            creationDate = clientJobInfo.creationTime,
                            showTime = " 4 hours",
                            editId = clientJobInfo.Id.ToString(CultureInfo.InvariantCulture),
                            showEllipse = true,
                            timeShowType = "success",
                            progressPercent = Convert.ToString(System.Math.Ceiling(((JobCompleted) * 100 / Convert.ToDouble(clientJobInfo.totalThreads)) * 100) / 100),
                            JobCompleted = Convert.ToString(JobCompleted),
                            JobAssigned = Convert.ToString(JobAssigned),
                            JobTotal = Convert.ToString(clientJobInfo.totalThreads),
                            JobReviewed = Convert.ToString(JobReviewed),
                            type = clientJobInfo.type,
                            subType = clientJobInfo.subType,
                            refKey = clientJobInfo.referenceId
                        };
                        response.Payload = clientTemplate;

                    }
                    else
                    {
                        long JobCompleted = _db.UserJobMappings.Where(x => x.refKey == clientJobInfo.referenceId && x.status == Constants.status_done).Count();
                        long JobAssigned = _db.UserJobMappings.Where(x => x.refKey == clientJobInfo.referenceId && x.status == Constants.status_assigned).Count();
                        long JobReviewed = (JobCompleted > 1) ? (JobCompleted) / 2 : 0;  // currently hard coded.

                        if (JobCompleted > Convert.ToInt32(clientJobInfo.totalThreads))
                            JobCompleted = Convert.ToInt32(clientJobInfo.totalThreads);

                        var clientTemplate = new ClientTemplateResponse
                        {
                            title = clientJobInfo.title,
                            creationDate = clientJobInfo.creationTime,
                            showTime = " 4 hours",
                            editId = clientJobInfo.Id.ToString(CultureInfo.InvariantCulture),
                            showEllipse = true,
                            timeShowType = "success",
                            progressPercent = Convert.ToString(System.Math.Ceiling(((JobCompleted) * 100 / Convert.ToDouble(clientJobInfo.totalThreads)) * 100) / 100),
                            JobCompleted = Convert.ToString(JobCompleted),
                            JobAssigned = Convert.ToString(JobAssigned),
                            JobTotal = Convert.ToString(clientJobInfo.totalThreads),
                            JobReviewed = Convert.ToString(JobReviewed),
                            type = clientJobInfo.type,
                            subType = clientJobInfo.subType
                        };
                        response.Payload = clientTemplate;
                    }
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

        public ResponseModel<ClientSurveyResultResponseList> GetTemplateSurveyResponseResultById(string username, long id)
        {
            var response = new ResponseModel<ClientSurveyResultResponseList>();
            try
            {
                var ClientSurveyResultResponseListData = new ClientSurveyResultResponseList();
                var templateData = _db.CreateTemplateQuestionInfoes.SingleOrDefault(x => x.Id == id);                
                var createTemplateSingleQuestionsListsCreateResponse = _db.CreateTemplateSingleQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId).ToList();
                var createTemplateMultipleQuestionsListsCreateResponse = _db.CreateTemplateMultipleQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId).ToList();
                var createTemplateTextBoxQuestionsListsCreateResponse = _db.CreateTemplateTextBoxQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId).ToList();
                var createTemplateListBoxQuestionsListsCreateResponse = _db.CreateTemplateListBoxQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId).ToList();

                if (templateData != null)
                {                    
                    ClientSurveyResultResponseListData.type = templateData.type;
                    ClientSurveyResultResponseListData.subType = templateData.subType;
                    ClientSurveyResultResponseListData.title = templateData.title;
                    ClientSurveyResultResponseListData.description = templateData.description;
                    ClientSurveyResultResponseListData.resultList = new List<ClientSurveyResultResponse>();
                    //int index = 0;
                    foreach (var singleQuestionsLists in createTemplateSingleQuestionsListsCreateResponse)
                    {                        
                        var mapRes = new Dictionary<string, int>();

                        var ClientSurveyResultResponseData = new ClientSurveyResultResponse();
                        ClientSurveyResultResponseData.questionType = Constants.questionTypeSAQ;
                        ClientSurveyResultResponseData.question = singleQuestionsLists.Question;
                        ClientSurveyResultResponseData.options = singleQuestionsLists.Options;
                        ClientSurveyResultResponseData.UniqueId = Constants.questionTypeSAQ + singleQuestionsLists.Id;
                        //ClientSurveyResultResponseData.index = index;
                        ClientSurveyResultResponseData.resultMap = new Dictionary<string, int>();
                        var optionsList = singleQuestionsLists.Options.Split(';');
                        var questionListId = Convert.ToString(singleQuestionsLists.Id);
                        var map = _db.UserSurveyResultToBeRevieweds1
                                     .Where(x=>x.refKey == singleQuestionsLists.referenceKey &&
                                               x.type == Constants.questionTypeSAQ &&
                                               x.questionId == questionListId)
                                     .GroupBy(x=>x.answer)
                                     .ToDictionary(x=>x.Key, x=>x.Count());
                        for (int i = 0; i < optionsList.Length; i++)
                        {
                            if (!map.ContainsKey(Convert.ToString(i)))
                                mapRes[Convert.ToString(i)] = 0;
                            else
                                mapRes[Convert.ToString(i)] = map[Convert.ToString(i)];
                        }
                        
                        ClientSurveyResultResponseData.resultMap = mapRes;
                        ClientSurveyResultResponseListData.resultList.Add(ClientSurveyResultResponseData);
                        //index++;
                    }

                    foreach (var multipleQuestionsLists in createTemplateMultipleQuestionsListsCreateResponse)
                    {                        
                        var mapRes = new Dictionary<string, int>();

                        var ClientSurveyResultResponseData = new ClientSurveyResultResponse();
                        ClientSurveyResultResponseData.questionType = Constants.questionTypeMAQ;
                        ClientSurveyResultResponseData.question = multipleQuestionsLists.Question;
                        ClientSurveyResultResponseData.options = multipleQuestionsLists.Options;
                        ClientSurveyResultResponseData.UniqueId = Constants.questionTypeMAQ + multipleQuestionsLists.Id;
                        //ClientSurveyResultResponseData.index = index;
                        ClientSurveyResultResponseData.resultMap = new Dictionary<string, int>();
                        var optionsList = multipleQuestionsLists.Options.Split(';');
                        var questionListId = Convert.ToString(multipleQuestionsLists.Id);
                        var map = _db.UserSurveyResultToBeRevieweds1
                                     .Where(x => x.refKey == multipleQuestionsLists.referenceKey &&
                                               x.type == Constants.questionTypeMAQ &&
                                               x.questionId == questionListId)
                                     .GroupBy(x => x.answer)
                                     .ToDictionary(x => x.Key, x => x.Count());
                        for (int i = 0; i < optionsList.Length; i++)
                        {
                            if (!map.ContainsKey(Convert.ToString(i)))
                                mapRes[Convert.ToString(i)] = 0;
                            else
                                mapRes[Convert.ToString(i)] = map[Convert.ToString(i)];
                        }

                        ClientSurveyResultResponseData.resultMap = mapRes;
                        ClientSurveyResultResponseListData.resultList.Add(ClientSurveyResultResponseData);
                    }

                    foreach (var listBoxQuestionsLists in createTemplateListBoxQuestionsListsCreateResponse)
                    {                        
                        var mapRes = new Dictionary<string, int>();

                        var ClientSurveyResultResponseData = new ClientSurveyResultResponse();
                        ClientSurveyResultResponseData.questionType = Constants.questionTypeLAQ;
                        ClientSurveyResultResponseData.question = listBoxQuestionsLists.Question;
                        ClientSurveyResultResponseData.options = listBoxQuestionsLists.Options;
                        ClientSurveyResultResponseData.UniqueId = Constants.questionTypeLAQ + listBoxQuestionsLists.Id;
                        //ClientSurveyResultResponseData.index = index;
                        ClientSurveyResultResponseData.resultMap = new Dictionary<string, int>();
                        var optionsList = listBoxQuestionsLists.Options.Split(';');
                        var questionListId = Convert.ToString(listBoxQuestionsLists.Id);
                        var map = _db.UserSurveyResultToBeRevieweds1
                                     .Where(x => x.refKey == listBoxQuestionsLists.referenceKey &&
                                               x.type == Constants.questionTypeLAQ &&
                                               x.questionId == questionListId)
                                     .GroupBy(x => x.answer)
                                     .ToDictionary(x => x.Key, x => x.Count());
                        for (int i = 0; i < optionsList.Length; i++)
                        {
                            if (!map.ContainsKey(Convert.ToString(i)))
                                mapRes[Convert.ToString(i)] = 0;
                            else
                                mapRes[Convert.ToString(i)] = map[Convert.ToString(i)];
                        }

                        ClientSurveyResultResponseData.resultMap = mapRes;
                        ClientSurveyResultResponseListData.resultList.Add(ClientSurveyResultResponseData);
                    }

                    foreach (var textBoxQuestionsLists in createTemplateTextBoxQuestionsListsCreateResponse)
                    {                        
                        var mapRes = new Dictionary<string, string>();

                        var ClientSurveyResultResponseData = new ClientSurveyResultResponse();
                        ClientSurveyResultResponseData.questionType = Constants.questionTypeTAQ;
                        ClientSurveyResultResponseData.question = textBoxQuestionsLists.Question;
                        //ClientSurveyResultResponseData.options = textBoxQuestionsLists.Options;
                        ClientSurveyResultResponseData.UniqueId = Constants.questionTypeTAQ + textBoxQuestionsLists.Id;
                        //ClientSurveyResultResponseData.index = index;
                        ClientSurveyResultResponseData.textBoxResultMap = new Dictionary<string, string>();
                        var optionsList = textBoxQuestionsLists.Options.Split(';');
                        var questionListId = Convert.ToString(textBoxQuestionsLists.Id);
                        var map = _db.UserSurveyResultToBeRevieweds1
                                     .Where(x => x.refKey == textBoxQuestionsLists.referenceKey &&
                                               x.type == Constants.questionTypeTAQ &&
                                               x.questionId == questionListId)
                                     .GroupBy(x => x.answer).OrderByDescending(x=>x.Count())
                                     .ToDictionary(x => x.Key, x => x.Count());
                        //for (int i = 0; i < optionsList.Length; i++)
                        //{
                        //    if (!map.ContainsKey(Convert.ToString(i)))
                        //        mapRes[Convert.ToString(i)] = 0;
                        //    else
                        //        mapRes[Convert.ToString(i)] = map[Convert.ToString(i)];
                        //}
                        ClientSurveyResultResponseData.resultMap = map;
                        ClientSurveyResultResponseListData.resultList.Add(ClientSurveyResultResponseData);
                    }

                    response.Status = 200;
                    response.Message = "success";
                    response.Payload = ClientSurveyResultResponseListData;
                    
                }
                else
                {
                    response.Status = 404;
                    response.Message = "No Data found";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Exception";
                return response;
            }
        }

        public ResponseModel<ClientTranscriptionResultResponse> GetTranscriptionResponseResultById(string username, long id)
        {
            var response = new ResponseModel<ClientTranscriptionResultResponse>();
            try
            {
                var ClientSurveyResultResponseListData = new ClientTranscriptionResultResponse();
                var templateData = _db.CreateTemplateQuestionInfoes.SingleOrDefault(x => x.Id == id);
                
                if (templateData != null)
                {
                    //var transcriptionData = _db.UserMultipleJobMappings.SingleOrDefault(

                }
                else
                {
                    response.Status = 404;
                    response.Message = "No Data found";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Exception";
                return response;
            }
        }

        public ResponseModel<ClientAllTranscriptionResultResponse> GetAllCompletedTranscriptionInformation(string username, long id)
        {
            var response = new ResponseModel<ClientAllTranscriptionResultResponse>();
            try
            {
                var ClientTranscriptionResultResponseListData = new ClientAllTranscriptionResultResponse();
                ClientTranscriptionResultResponseListData.data = new List<ClientTranscriptionTableResponseData>();
                var templateData = _db.CreateTemplateQuestionInfoes.SingleOrDefault(x => x.Id == id);

                if (templateData != null)
                {
                    var TranscriptionResultList = _db.UserMultipleJobMappings.Where(x => x.refKey == templateData.referenceId && x.status == Constants.status_done).ToList();

                    ClientTranscriptionResultResponseListData.title = templateData.title;
                    ClientTranscriptionResultResponseListData.type = templateData.type;
                    ClientTranscriptionResultResponseListData.subType = templateData.subType;
                    ClientTranscriptionResultResponseListData.options = _db.CreateTemplateTextBoxQuestionsLists.SingleOrDefault(x => x.referenceKey == templateData.referenceId).Question;
                    foreach (var TranscriptionResult in TranscriptionResultList)
                    {
                        var ClientTranscriptionResultResponse = new ClientTranscriptionTableResponseData();
                        ClientTranscriptionResultResponse.imageUrl = TranscriptionResult.imageKey;
                        //ClientTranscriptionResultResponse.imageUrl_s = TranscriptionResult.imageKey.Split('/')[0] + "//" + TranscriptionResult.imageKey.Split('/')[2] + "/" + TranscriptionResult.imageKey.Split('/')[3].Split('.')[0] + 's' + "." + TranscriptionResult.imageKey.Split('/')[3].Split('.')[1];
                        ClientTranscriptionResultResponse.userResponseData = JsonConvert.DeserializeObject<List<string[]>>(TranscriptionResult.surveyResult);
                        ClientTranscriptionResultResponseListData.data.Add(ClientTranscriptionResultResponse);
                    }
                    response.Status = 200;
                    response.Message = "success";
                    response.Payload = ClientTranscriptionResultResponseListData;
                }
                else
                {
                    response.Status = 404;
                    response.Message = "No Data found";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Exception";
                return response;
            }
        }

        public ResponseModel<ClientAllImageModerationResultResponse> GetAllCompletedImageModerationInformation(string username, long id)
        {
            var response = new ResponseModel<ClientAllImageModerationResultResponse>();
            try
            {
                var ClientImageModerationResultResponseListData = new ClientAllImageModerationResultResponse();
                ClientImageModerationResultResponseListData.data = new List<ClientImageModerationResponseData>();
                var templateData = _db.CreateTemplateQuestionInfoes.SingleOrDefault(x => x.Id == id);

                if (templateData != null)
                {
                    var ImageModerationResultList = _db.UserMultipleJobMappings.Where(x => x.refKey == templateData.referenceId && x.status == Constants.status_done).ToList();

                    ClientImageModerationResultResponseListData.title = templateData.title;
                    ClientImageModerationResultResponseListData.type = templateData.type;
                    ClientImageModerationResultResponseListData.subType = templateData.subType;
                    var ImageModerationDescription = _db.CreateTemplateSingleQuestionsLists.SingleOrDefault(x => x.referenceKey == templateData.referenceId);
                    ClientImageModerationResultResponseListData.options = ImageModerationDescription.Options;
                    ClientImageModerationResultResponseListData.question = ImageModerationDescription.Question;
                    foreach (var ImageModerationResult in ImageModerationResultList)
                    {
                        var ClientImageModerationResponseDataResponse = new ClientImageModerationResponseData();
                        ClientImageModerationResponseDataResponse.imageUrl = ImageModerationResult.imageKey;
                        ClientImageModerationResponseDataResponse.userResponse = ImageModerationResult.surveyResult;
                        ClientImageModerationResponseDataResponse.userResponseValue = ImageModerationDescription.Options.Split(';')[Convert.ToInt32(ImageModerationResult.surveyResult)];
                        ClientImageModerationResultResponseListData.data.Add(ClientImageModerationResponseDataResponse);
                    }
                    response.Status = 200;
                    response.Message = "success";
                    response.Payload = ClientImageModerationResultResponseListData;
                }
                else
                {
                    response.Status = 404;
                    response.Message = "No Data found";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Exception";
                return response;
            }
        }

        public ResponseModel<ClientTemplateDetailById> GetTemplateDetailById(string username,long id)
        {
            var response = new ResponseModel<ClientTemplateDetailById>();
            try
            {
                var templateData = _db.CreateTemplateQuestionInfoes.SingleOrDefault(x => x.Id == id);
                var createTemplateeditableInstructionsListsCreateResponse = _db.CreateTemplateeditableInstructionsLists.OrderBy(x => x.Id).Where(x=>x.referenceKey==templateData.referenceId).ToList();
                var createTemplateSingleQuestionsListsCreateResponse = _db.CreateTemplateSingleQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId).ToList();
                var createTemplateMultipleQuestionsListsCreateResponse = _db.CreateTemplateMultipleQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId).ToList();
                var createTemplateTextBoxQuestionsListsCreateResponse = _db.CreateTemplateTextBoxQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId).ToList();
                var createTemplateListBoxQuestionsListsCreateResponse = _db.CreateTemplateListBoxQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId).ToList();

                if (templateData != null && createTemplateListBoxQuestionsListsCreateResponse != null && createTemplateTextBoxQuestionsListsCreateResponse != null && createTemplateMultipleQuestionsListsCreateResponse != null && createTemplateSingleQuestionsListsCreateResponse != null && createTemplateeditableInstructionsListsCreateResponse != null)
                {
                    var createTemplateQuestionInfoModelEditableInstructionsCreateResponse = new CreateTemplateQuestionInfoModel();

                    createTemplateQuestionInfoModelEditableInstructionsCreateResponse.type = "AddInstructions";
                    createTemplateQuestionInfoModelEditableInstructionsCreateResponse.title = templateData.title;
                    createTemplateQuestionInfoModelEditableInstructionsCreateResponse.visible = false;
                    createTemplateQuestionInfoModelEditableInstructionsCreateResponse.buttonText = "Add Instructions";

                    createTemplateQuestionInfoModelEditableInstructionsCreateResponse.editableInstructionsList = new List<CreateTemplateeditableInstructionsListModel>();
                    foreach (var editableInstructionsLists in createTemplateeditableInstructionsListsCreateResponse)
                    {
                        var createTemplateeditableInstructionsListModelCreateResponse = new CreateTemplateeditableInstructionsListModel
                        {
                            Number = editableInstructionsLists.Number,
                            Text = editableInstructionsLists.Text
                        };
                        createTemplateQuestionInfoModelEditableInstructionsCreateResponse.editableInstructionsList.Add(createTemplateeditableInstructionsListModelCreateResponse);
                        createTemplateQuestionInfoModelEditableInstructionsCreateResponse.buttonText = "Remove Instructions";
                        createTemplateQuestionInfoModelEditableInstructionsCreateResponse.visible = true;
                    }

                    var createTemplateQuestionInfoModelSingleQuestionsCreateResponse = new CreateTemplateQuestionInfoModel();

                    createTemplateQuestionInfoModelSingleQuestionsCreateResponse.type = "AddSingleQuestionsList";
                    createTemplateQuestionInfoModelSingleQuestionsCreateResponse.title = templateData.title;
                    createTemplateQuestionInfoModelSingleQuestionsCreateResponse.visible = false;
                    createTemplateQuestionInfoModelSingleQuestionsCreateResponse.buttonText = "Add Ques. (single Ans.)";

                    createTemplateQuestionInfoModelSingleQuestionsCreateResponse.singleQuestionsList = new List<CreateTemplateSingleQuestionsListModel>();
                    foreach (var singleQuestionsLists in createTemplateSingleQuestionsListsCreateResponse)
                    {
                        var createTemplateSingleQuestionsListModelCreateResponse = new CreateTemplateSingleQuestionsListModel
                        {
                            Number = singleQuestionsLists.Number,
                            Question = singleQuestionsLists.Question,
                            Options = singleQuestionsLists.Options
                        };
                        createTemplateQuestionInfoModelSingleQuestionsCreateResponse.singleQuestionsList.Add(createTemplateSingleQuestionsListModelCreateResponse);
                        createTemplateQuestionInfoModelSingleQuestionsCreateResponse.visible = true;
                        createTemplateQuestionInfoModelSingleQuestionsCreateResponse.buttonText = "Remove Ques. (single Ans.)";
                    }

                    var createTemplateQuestionInfoModelMultipleQuestionsCreateResponse = new CreateTemplateQuestionInfoModel();

                    createTemplateQuestionInfoModelMultipleQuestionsCreateResponse.type = "AddMultipleQuestionsList";
                    createTemplateQuestionInfoModelMultipleQuestionsCreateResponse.title = templateData.title;
                    createTemplateQuestionInfoModelMultipleQuestionsCreateResponse.visible = false;
                    createTemplateQuestionInfoModelMultipleQuestionsCreateResponse.buttonText = "Add Ques. (Multiple Ans.)";
                    createTemplateQuestionInfoModelMultipleQuestionsCreateResponse.multipleQuestionsList = new List<CreateTemplateMultipleQuestionsListModel>();
                    foreach (var multipleQuestionsLists in createTemplateMultipleQuestionsListsCreateResponse)
                    {
                        var createTemplateMultipleQuestionsListModelCreateResponse = new CreateTemplateMultipleQuestionsListModel
                        {
                            Number = multipleQuestionsLists.Number,
                            Question = multipleQuestionsLists.Question,
                            Options = multipleQuestionsLists.Options
                        };
                        createTemplateQuestionInfoModelMultipleQuestionsCreateResponse.multipleQuestionsList.Add(createTemplateMultipleQuestionsListModelCreateResponse);
                        createTemplateQuestionInfoModelMultipleQuestionsCreateResponse.visible = true;
                        createTemplateQuestionInfoModelMultipleQuestionsCreateResponse.buttonText = "Remove Ques. (Multiple Ans.)";
                    }

                    var createTemplateQuestionInfoModelTextBoxQuestionsCreateResponse = new CreateTemplateQuestionInfoModel();

                    createTemplateQuestionInfoModelTextBoxQuestionsCreateResponse.type = "AddTextBoxQuestionsList";
                    createTemplateQuestionInfoModelTextBoxQuestionsCreateResponse.title = templateData.title;
                    createTemplateQuestionInfoModelTextBoxQuestionsCreateResponse.visible = false;
                    createTemplateQuestionInfoModelTextBoxQuestionsCreateResponse.buttonText = "Add Ques. (TextBox Ans.)";

                    createTemplateQuestionInfoModelTextBoxQuestionsCreateResponse.textBoxQuestionsList = new List<CreateTemplateTextBoxQuestionsListModel>();
                    foreach (var textBoxQuestionsLists in createTemplateTextBoxQuestionsListsCreateResponse)
                    {
                        var createTemplateTextBoxQuestionsListModelCreateResponse = new CreateTemplateTextBoxQuestionsListModel
                        {
                            Number = textBoxQuestionsLists.Number,
                            Question = textBoxQuestionsLists.Question,
                            Options = textBoxQuestionsLists.Options
                        };
                        createTemplateQuestionInfoModelTextBoxQuestionsCreateResponse.textBoxQuestionsList.Add(createTemplateTextBoxQuestionsListModelCreateResponse);
                        createTemplateQuestionInfoModelTextBoxQuestionsCreateResponse.visible = true;
                        createTemplateQuestionInfoModelTextBoxQuestionsCreateResponse.buttonText = "Remove Ques. (TextBox Ans.)";
                    }

                    var createTemplateQuestionInfoModelListBoxQuestionsCreateResponse = new CreateTemplateQuestionInfoModel();

                    createTemplateQuestionInfoModelListBoxQuestionsCreateResponse.type = "AddListBoxQuestionsList";
                    createTemplateQuestionInfoModelListBoxQuestionsCreateResponse.title = templateData.title;
                    createTemplateQuestionInfoModelListBoxQuestionsCreateResponse.visible = false;
                    createTemplateQuestionInfoModelListBoxQuestionsCreateResponse.buttonText = "Add Ques. (ListBox Ans.)";

                    createTemplateQuestionInfoModelListBoxQuestionsCreateResponse.listBoxQuestionsList = new List<CreateTemplateListBoxQuestionsListModel>();
                    foreach (var listBoxQuestionsLists in createTemplateListBoxQuestionsListsCreateResponse)
                    {
                        var createTemplateListBoxQuestionsListModelCreateResponse = new CreateTemplateListBoxQuestionsListModel
                        {
                            Number = listBoxQuestionsLists.Number,
                            Question = listBoxQuestionsLists.Question,
                            Options = listBoxQuestionsLists.Options
                        };
                        createTemplateQuestionInfoModelListBoxQuestionsCreateResponse.listBoxQuestionsList.Add(createTemplateListBoxQuestionsListModelCreateResponse);
                        createTemplateQuestionInfoModelListBoxQuestionsCreateResponse.visible = true;
                        createTemplateQuestionInfoModelListBoxQuestionsCreateResponse.buttonText = "Remove Ques. (ListBox Ans.)";
                    }


                    response.Status = 200;
                    response.Message = "success";
                    response.Payload = new ClientTemplateDetailById
                    {
                        Data = new List<CreateTemplateQuestionInfoModel>
                        {
                            createTemplateQuestionInfoModelEditableInstructionsCreateResponse,
                            createTemplateQuestionInfoModelSingleQuestionsCreateResponse,
                            createTemplateQuestionInfoModelMultipleQuestionsCreateResponse,
                            createTemplateQuestionInfoModelTextBoxQuestionsCreateResponse,
                            createTemplateQuestionInfoModelListBoxQuestionsCreateResponse
                        }
                    };
                }
                else
                {
                    response.Status = 404;
                    response.Message = "No Data found";
                }
                return response;
            }
            catch (Exception)
            {
                response.Status = 500;
                response.Message = "Exception";
                return response;
            }
        }

        public ResponseModel<List<ImgurImageResponse>> GetTemplateImageDetailById(string username, long id)
        {
            var response = new ResponseModel<List<ImgurImageResponse>>();
            try
            {
                var refKey = username + id;
                var createTemplateImagesListsCreateResponse = _db.CreateTemplateImgurImagesLists.OrderBy(x => x.Id).Where(x => x.referenceKey == refKey).ToList();

                if (createTemplateImagesListsCreateResponse != null)
                {
                    var imgurImageList = new List<ImgurImageResponse>();
                    foreach (var createTemplateImageCreateResponse in createTemplateImagesListsCreateResponse)
                    {
                        var imgurImage = new ImgurImageResponse();
                        imgurImage.data = new imgurData();
                        imgurImage.data.id = createTemplateImageCreateResponse.Id.ToString();                        
                        imgurImage.data.deletehash = createTemplateImageCreateResponse.imgurDeleteHash;
                        imgurImage.data.link = createTemplateImageCreateResponse.imgurLink;
                        imgurImage.data.link_s = createTemplateImageCreateResponse.imgurLink.Split('/')[0] + "//" + createTemplateImageCreateResponse.imgurLink.Split('/')[2] + "/" + createTemplateImageCreateResponse.imgurLink.Split('/')[3].Split('.')[0] + 's' + "." + createTemplateImageCreateResponse.imgurLink.Split('/')[3].Split('.')[1];
                        imgurImage.data.link_m = createTemplateImageCreateResponse.imgurLink.Split('/')[0] + "//" + createTemplateImageCreateResponse.imgurLink.Split('/')[2] + "/" + createTemplateImageCreateResponse.imgurLink.Split('/')[3].Split('.')[0] + 'm' + "." + createTemplateImageCreateResponse.imgurLink.Split('/')[3].Split('.')[1];
                        imgurImage.data.link_l = createTemplateImageCreateResponse.imgurLink.Split('/')[0] + "//" + createTemplateImageCreateResponse.imgurLink.Split('/')[2] + "/" + createTemplateImageCreateResponse.imgurLink.Split('/')[3].Split('.')[0] + 'l' + "." + createTemplateImageCreateResponse.imgurLink.Split('/')[3].Split('.')[1];
                        imgurImage.data.copyText ="";
                        imgurImageList.Add(imgurImage);
                    }
                    response.Status = 200;
                    response.Message = "success";
                    response.Payload = imgurImageList;
                }
                else
                {
                    response.Status = 404;
                    response.Message = "No Data found";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Exception";
                return response;
            }
        }

        public ResponseModel<string> CreateTemplate(List<CreateTemplateQuestionInfoModel> req,string username,TemplateInfoModel TemplateInfo)
        {
            var response = new ResponseModel<string>();

            var keyInfo = _db.CreateTemplateQuestionInfoes.FirstOrDefault();
            var refKey = _db.Users.SingleOrDefault(x=>x.Username == username).guid;
            var digitKey = 0;

            Random rnd = new Random();
            int randomValue = rnd.Next(1, 10000);
            
            if (keyInfo != null)
            {
                digitKey = _db.CreateTemplateQuestionInfoes.Max(x => x.Id) + 1;                
            }
            else
            {
                digitKey = 1;                
            }
            refKey += digitKey + randomValue;

            if (TemplateInfo.type == Constants.type_Ads && TemplateInfo.subType == Constants.subType_facebookLike)
            {
                var createTemplateFacebookLikeInsert = new CreateTemplateFacebookLike
                {                    
                    username = username,
                    title = req[0].title,                    
                    type = TemplateInfo.type,
                    subType = TemplateInfo.subType,
                    creationTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    referenceId = refKey,
                    totalThreads = TemplateInfo.totalThreads,
                    completed = Constants.NA,
                    verified = Constants.NA,
                    payPerUser = TemplateInfo.amountEachThread,
                    DateTime = DateTime.Now,
                    description = (req[3].textBoxQuestionsList[0].Question) == null ? Constants.NA : req[3].textBoxQuestionsList[0].Question,
                    pageId = TemplateInfo.pageId,
                    pageUrl = TemplateInfo.pageUrl
                };
                _db.CreateTemplateFacebookLikes.Add(createTemplateFacebookLikeInsert);
                try
                {
                    _db.SaveChanges();
                    
                    var signalRHub = new SignalRHub();
                    string totalProjects = new ProjectDAO().totalAvailableProjects();
                    string successRate = "";
                    string totalUsers = "";
                    string projectCategories = "";
                    var hubContext = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>();
                    hubContext.Clients.All.updateBeforeLoginUserProjectDetails(totalProjects, successRate, totalUsers, projectCategories);

                    response.Status = 200;
                    response.Message = "success-" + digitKey;
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
            else
            {
                var createTemplateQuestionsInfoInsert = new CreateTemplateQuestionInfo
                {
                    description = TemplateInfo.description != null ? TemplateInfo.description : Constants.NA,
                    username = username,
                    title = req[0].title,
                    visible = Constants.NA,
                    type = TemplateInfo.type,
                    subType = TemplateInfo.subType,
                    creationTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    referenceId = refKey,
                    totalThreads = TemplateInfo.totalThreads,
                    completed = Constants.NA,
                    verified = Constants.NA,
                    payPerUser = TemplateInfo.amountEachThread
                };

                _db.CreateTemplateQuestionInfoes.Add(createTemplateQuestionsInfoInsert);

                try
                {
                    _db.SaveChanges();
                    CreateSubTemplateByRefKey CreateSubTemplateByRefKey = new CreateSubTemplateByRefKey();
                    CreateSubTemplateByRefKey.CreateSubTemplateByRefKeyService(req, username, refKey);

                    var signalRHub = new SignalRHub();
                    string totalProjects = new ProjectDAO().totalAvailableProjects();
                    string successRate = "";
                    string totalUsers = "";
                    string projectCategories = "";
                    var hubContext = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>();
                    hubContext.Clients.All.updateBeforeLoginUserProjectDetails(totalProjects, successRate, totalUsers, projectCategories);

                    response.Status = 200;
                    response.Message = "success-" + digitKey;
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
            
            return response;
        }

        public ResponseModel<string> CreateTemplateWithId(List<CreateTemplateQuestionInfoModel> req, string username,string id)
        {
            var response = new ResponseModel<string>();
            
            var refKey = username+id;
            
            var createTemplateQuestionsInfoInsert = new CreateTemplateQuestionInfo
            {
                description = Constants.NA,
                username = username,
                title = req[0].title,
                visible = Constants.NA,
                type = Constants.NA,
                subType = Constants.NA,
                creationTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                referenceId = refKey,
                totalThreads = Constants.NA,
                completed = Constants.NA,
                verified = Constants.NA,
                payPerUser = Constants.NA
            };

            _db.CreateTemplateQuestionInfoes.Add(createTemplateQuestionsInfoInsert);

            try
            {
                _db.SaveChanges();
                CreateSubTemplateByRefKey CreateSubTemplateByRefKey = new CreateSubTemplateByRefKey();
                CreateSubTemplateByRefKey.CreateSubTemplateByRefKeyService(req, username, refKey);
                response.Status = 200;
                response.Message = "Success";
                response.Payload = "Successfully Created";
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

        public ResponseModel<string> DeleteTemplateDetailById(string username, long id,string type, string subType)
        {
            var response = new ResponseModel<string>();
            try
            {
                if (type == Constants.type_Ads && subType == Constants.subType_facebookLike)
                {
                    response = DeleteFacebookLikeTemplateDetailById(username, id);
                }
                else
                {
                    response = DeleteSurveyTemplateDetailById(username, id);
                }
                

                return response;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Exception";
                return response;
            }
        }

        private ResponseModel<string> DeleteFacebookLikeTemplateDetailById(string username, long id)
        {
            var response = new ResponseModel<string>();
            var FacebookLikeTemplateData = _db.CreateTemplateFacebookLikes.SingleOrDefault(x => x.Id == id && x.username == username);
            var UserFacebookLikeJobMappingsList = _db.UserFacebookLikeJobMappings.Where(x => x.refKey == FacebookLikeTemplateData.referenceId);

            if (FacebookLikeTemplateData != null)
                _db.CreateTemplateFacebookLikes.Remove(FacebookLikeTemplateData);

            if (UserFacebookLikeJobMappingsList != null)
            {
                foreach (var UserFacebookLikeJobMapping in UserFacebookLikeJobMappingsList)
                {
                    _db.UserFacebookLikeJobMappings.Remove(UserFacebookLikeJobMapping);
                }
            }

            try
            {
                _db.SaveChanges();
                response.Status = 200;
                response.Message = "Success";
                response.Payload = "Successfully Deleted";
                return response;
            }
            catch (DbEntityValidationException e)
            {
                DbContextException.LogDbContextException(e);
                response.Status = 500;
                response.Message = "Failed";
                response.Payload = "Exception Occured";
                return response;
            }            
        }

        private ResponseModel<string> DeleteSurveyTemplateDetailById(string username, long id)
        {
            var response = new ResponseModel<string>();

            var templateData = _db.CreateTemplateQuestionInfoes.SingleOrDefault(x => x.Id == id && x.username == username);
            var UserJobMappingList = _db.UserJobMappings.Where(x => x.refKey == templateData.referenceId).ToList();
            var UserMultipleJobMappingList = _db.UserMultipleJobMappings.Where(x => x.refKey == templateData.referenceId).ToList();
            var UserSurveyResultList = _db.UserSurveyResultToBeRevieweds1.Where(x => x.refKey == templateData.referenceId).ToList();
            var createTemplateeditableInstructionsListsCreateResponse = _db.CreateTemplateeditableInstructionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId && x.username == username).ToList();
            var createTemplateSingleQuestionsListsCreateResponse = _db.CreateTemplateSingleQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId && x.username == username).ToList();
            var createTemplateMultipleQuestionsListsCreateResponse = _db.CreateTemplateMultipleQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId && x.username == username).ToList();
            var createTemplateTextBoxQuestionsListsCreateResponse = _db.CreateTemplateTextBoxQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId && x.username == username).ToList();
            var createTemplateListBoxQuestionsListsCreateResponse = _db.CreateTemplateListBoxQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId && x.username == username).ToList();
            var createTemplateImgurImagesListsCreateResponse = _db.CreateTemplateImgurImagesLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId && x.username == username).ToList();

            if (templateData != null)
                _db.CreateTemplateQuestionInfoes.Remove(templateData);

            if (UserJobMappingList != null)
            {
                foreach (var UserJobMapping in UserJobMappingList)
                {
                    _db.UserJobMappings.Remove(UserJobMapping);
                }
            }

            if (UserMultipleJobMappingList != null)
            {
                foreach (var UserMultipleJobMapping in UserMultipleJobMappingList)
                {
                    _db.UserMultipleJobMappings.Remove(UserMultipleJobMapping);
                }
            }

            if (UserSurveyResultList != null)
            {
                foreach (var UserSurveyResult in UserSurveyResultList)
                {
                    _db.UserSurveyResultToBeRevieweds1.Remove(UserSurveyResult);
                }
            }

            if (createTemplateImgurImagesListsCreateResponse != null)
            {
                foreach (var createTemplateImgurImageCreateResponse in createTemplateImgurImagesListsCreateResponse)
                {
                    _db.CreateTemplateImgurImagesLists.Remove(createTemplateImgurImageCreateResponse);
                }
            }

            if (createTemplateeditableInstructionsListsCreateResponse != null)
            {
                foreach (var createTemplateeditableInstructionCreateResponse in createTemplateeditableInstructionsListsCreateResponse)
                {
                    _db.CreateTemplateeditableInstructionsLists.Remove(createTemplateeditableInstructionCreateResponse);
                }
            }

            if (createTemplateSingleQuestionsListsCreateResponse != null)
            {
                foreach (var createTemplateSingleQuestionCreateResponse in createTemplateSingleQuestionsListsCreateResponse)
                {
                    _db.CreateTemplateSingleQuestionsLists.Remove(createTemplateSingleQuestionCreateResponse);
                }
            }

            if (createTemplateMultipleQuestionsListsCreateResponse != null)
            {
                foreach (var createTemplateMultipleQuestionCreateResponse in createTemplateMultipleQuestionsListsCreateResponse)
                {
                    _db.CreateTemplateMultipleQuestionsLists.Remove(createTemplateMultipleQuestionCreateResponse);
                }
            }

            if (createTemplateTextBoxQuestionsListsCreateResponse != null)
            {
                foreach (var createTemplateTextBoxQuestionCreateResponse in createTemplateTextBoxQuestionsListsCreateResponse)
                {
                    _db.CreateTemplateTextBoxQuestionsLists.Remove(createTemplateTextBoxQuestionCreateResponse);
                }
            }

            if (createTemplateListBoxQuestionsListsCreateResponse != null)
            {
                foreach (var createTemplateListBoxQuestionCreateResponse in createTemplateListBoxQuestionsListsCreateResponse)
                {
                    _db.CreateTemplateListBoxQuestionsLists.Remove(createTemplateListBoxQuestionCreateResponse);
                }
            }


            try
            {
                _db.SaveChanges();
                response.Status = 200;
                response.Message = "Success";
                response.Payload = "Successfully Deleted";
                return response;
            }
            catch (DbEntityValidationException e)
            {
                DbContextException.LogDbContextException(e);
                response.Status = 500;
                response.Message = "Failed";
                response.Payload = "Exception Occured";
                return response;
            }
        }
        public ResponseModel<string> DeleteTemplateImgurImageById(string username, long id)
        {
            var response = new ResponseModel<string>();
            try
            {                
                var createTemplateImgurImagesListsCreateResponse = _db.CreateTemplateImgurImagesLists.OrderBy(x => x.Id).Where(x => x.Id == id && x.username == username).ToList();
                
                if (createTemplateImgurImagesListsCreateResponse != null)
                {
                    foreach (var createTemplateImgurImageCreateResponse in createTemplateImgurImagesListsCreateResponse)
                    {
                        _db.CreateTemplateImgurImagesLists.Remove(createTemplateImgurImageCreateResponse);
                    }
                }
                
                try
                {
                    _db.SaveChanges();
                    response.Status = 200;
                    response.Message = "Success";
                    response.Payload = "Successfully Deleted";
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
            catch (Exception)
            {
                response.Status = 500;
                response.Message = "Exception";
                return response;
            }
        }

        public ResponseModel<string> EditTemplateDetailById(List<CreateTemplateQuestionInfoModel> req,string username, long id)
        {
            var response = new ResponseModel<string>();
            try
            {
                var templateData = _db.CreateTemplateQuestionInfoes.SingleOrDefault(x => x.Id == id && x.username == username);
                var createTemplateeditableInstructionsListsCreateResponse = _db.CreateTemplateeditableInstructionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId && x.username == username).ToList();
                var createTemplateSingleQuestionsListsCreateResponse = _db.CreateTemplateSingleQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId && x.username == username).ToList();
                var createTemplateMultipleQuestionsListsCreateResponse = _db.CreateTemplateMultipleQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId && x.username == username).ToList();
                var createTemplateTextBoxQuestionsListsCreateResponse = _db.CreateTemplateTextBoxQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId && x.username == username).ToList();
                var createTemplateListBoxQuestionsListsCreateResponse = _db.CreateTemplateListBoxQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId && x.username == username).ToList();
                
                if (createTemplateeditableInstructionsListsCreateResponse != null)
                {
                    foreach (var createTemplateeditableInstructionCreateResponse in createTemplateeditableInstructionsListsCreateResponse)
                    {
                        _db.CreateTemplateeditableInstructionsLists.Remove(createTemplateeditableInstructionCreateResponse);
                    }
                }

                if (createTemplateSingleQuestionsListsCreateResponse != null)
                {
                    foreach (var createTemplateSingleQuestionCreateResponse in createTemplateSingleQuestionsListsCreateResponse)
                    {
                        _db.CreateTemplateSingleQuestionsLists.Remove(createTemplateSingleQuestionCreateResponse);
                    }
                }

                if (createTemplateMultipleQuestionsListsCreateResponse != null)
                {
                    foreach (var createTemplateMultipleQuestionCreateResponse in createTemplateMultipleQuestionsListsCreateResponse)
                    {
                        _db.CreateTemplateMultipleQuestionsLists.Remove(createTemplateMultipleQuestionCreateResponse);
                    }
                }

                if (createTemplateTextBoxQuestionsListsCreateResponse != null)
                {
                    foreach (var createTemplateTextBoxQuestionCreateResponse in createTemplateTextBoxQuestionsListsCreateResponse)
                    {
                        _db.CreateTemplateTextBoxQuestionsLists.Remove(createTemplateTextBoxQuestionCreateResponse);
                    }
                }

                if (createTemplateListBoxQuestionsListsCreateResponse != null)
                {
                    foreach (var createTemplateListBoxQuestionCreateResponse in createTemplateListBoxQuestionsListsCreateResponse)
                    {
                        _db.CreateTemplateListBoxQuestionsLists.Remove(createTemplateListBoxQuestionCreateResponse);
                    }
                }

                
                try
                {
                    templateData.title = req[0].title;
                    _db.SaveChanges();
                    CreateSubTemplateByRefKey CreateSubTemplateByRefKey = new CreateSubTemplateByRefKey();
                    CreateSubTemplateByRefKey.CreateSubTemplateByRefKeyService(req, username, templateData.referenceId);

                    response.Status = 200;
                    response.Message = "Success";
                    response.Payload = "Successfully Edited";
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
            catch (Exception)
            {
                response.Status = 500;
                response.Message = "Exception";
                return response;
            }
        }

        public ResponseModel<string> ImgurImagesSaveToDatabaseWithTemplateId(List<imgurUploadImageResponse> ImgurList, string username, string id)
        {
            var response = new ResponseModel<string>();

            var refKey = id;
            foreach (var imageInfo in ImgurList)
            {
                var CreateTemplateImgurImagesListInsert = new CreateTemplateImgurImagesList
                {
                    assignedTo = Constants.NA,
                    assignTime = Constants.NA,
                    completedAt = Constants.NA,
                    referenceKey = refKey,
                    status = Constants.status_open,
                    username = username,
                    verified = "No",
                    imgurId = imageInfo.data.id == null ? Constants.NA : imageInfo.data.id,
                    imgurDeleteHash = imageInfo.data.deletehash,
                    imgurLink = imageInfo.data.link
                };

                _db.CreateTemplateImgurImagesLists.Add(CreateTemplateImgurImagesListInsert);
            }            
            try
            {
                _db.SaveChanges();                
                response.Status = 200;
                response.Message = "Success";
                response.Payload = "Successfully Added";
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
    }
}