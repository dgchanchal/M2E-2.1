﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M2E.Models;
using M2E.signalRPushNotifications;
using M2E.Session;
using M2E.Common.Logger;
using M2E.CommonMethods;
using System.Reflection;
using System.Data.Entity.Validation;
using Microsoft.AspNet.SignalR;
using M2E.Models.Constants;

namespace M2E.Service.UserService.Moderation
{
    public class UserImageModeration
    {
        private static readonly ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        private DbContextException _dbContextException = new DbContextException();
        private readonly M2EContext _db = new M2EContext();

        public ResponseModel<string> SubmitImageModerationInputTableDataByRefKey(string username, string refKey, string surveyResult)
        {
            var response = new ResponseModel<string>();
            var UserMultipleJobMapping = _db.UserMultipleJobMappings.SingleOrDefault(x => x.refKey == refKey && x.username == username);
            var clientJobInfo = _db.CreateTemplateQuestionInfoes.SingleOrDefault(x => x.referenceId == refKey);
            if (UserMultipleJobMapping == null)
            {
                response.Status = 403;
                response.Message = "You can't submit answer for this transcription.";
                return response;
            }
            else if (UserMultipleJobMapping.status == Constants.status_done)
            {
                response.Status = 403;
                response.Message = "You have already submitted your answer.";
                return response;
            }
            else
            {
                UserMultipleJobMapping.status = Constants.status_done;
                UserMultipleJobMapping.surveyResult = surveyResult;
                try
                {
                    _db.SaveChanges();

                    long JobId = clientJobInfo.Id;
                    long JobCompleted = _db.UserMultipleJobMappings.Where(x => x.refKey == refKey && x.status == Constants.status_done).Count();
                    long JobAssigned = _db.UserMultipleJobMappings.Where(x => x.refKey == refKey && x.status == Constants.status_assigned).Count();
                    long JobReviewed = (JobCompleted > 1) ? (JobCompleted) / 2 : 0;  // currently hard coded.

                    //var SignalRClientHub = new SignalRClientHub();
                    //var hubContext = GlobalHost.ConnectionManager.GetHubContext<SignalRClientHub>();
                    //dynamic client = SignalRManager.getSignalRDetail(clientJobInfo.username);
                    //if (client != null)
                    //{
                    //    client.updateClientProgressChart(Convert.ToString(JobId), clientJobInfo.totalThreads, Convert.ToString(JobCompleted), Convert.ToString(JobAssigned), Convert.ToString(JobReviewed));
                    //    //client.updateClientProgressChart("8", "20", "10", "8", "5");
                    //    //client.addMessage("add message signalR");
                    //}

                    bool status = new UserUpdatesClientRealTimeData().UpdateClientRealTimeData(JobId, JobCompleted, JobAssigned, JobReviewed, clientJobInfo.totalThreads, clientJobInfo.username);

                    response.Status = 200;
                    response.Message = "success-";
                    response.Payload = refKey;
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

        }
    }
}