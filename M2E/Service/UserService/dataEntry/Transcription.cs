using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M2E.Models.DataResponse.UserResponse.DataEntry.Transcription;
using M2E.Models;
using M2E.Common.Logger;
using M2E.CommonMethods;
using System.Reflection;
using M2E.Models.Constants;

namespace M2E.Service.UserService.dataEntry
{
    public class UserTranscriptionService
    {
        private static readonly ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        private DbContextException _dbContextException = new DbContextException();
        private readonly M2EContext _db = new M2EContext();

        public ResponseModel<UserTranscriptionTemplateModel> GetTranscriptionTemplateInformationByRefKey(string username, string refKey)
        {
            var response = new ResponseModel<UserTranscriptionTemplateModel>();            
            try
            {
                var TranscriptionJobInfo = _db.CreateTemplateQuestionInfoes.SingleOrDefault(x=>x.referenceId == refKey);
                var TranscriptionJobOptions = _db.CreateTemplateTextBoxQuestionsLists.SingleOrDefault(x=>x.referenceKey == refKey);
                var TranscriptionImage = _db.CreateTemplateImgurImagesLists.SingleOrDefault(x => x.referenceKey == refKey); // TODO: currently we assumed one transcription image per task. need to change.
                //var TranscriptionAllocatedThreadInfo = _db.UserMultipleJobMappings.SingleOrDefault(x => x.refKey == refKey && x.username == username && x.status != Constants.status_done);
                //if (TranscriptionAllocatedThreadInfo == null)
                //{
                //    response.Status = 405;
                //    response.Message = "This Thread is not assigned to you yet.";
                //    return response;
                //}
                if (TranscriptionJobInfo != null && TranscriptionJobOptions != null && TranscriptionImage != null)
                {                                       
                    var UserTranscriptionTemplateModel = new UserTranscriptionTemplateModel();
                    UserTranscriptionTemplateModel.type = TranscriptionJobInfo.type;
                    UserTranscriptionTemplateModel.subType = TranscriptionJobInfo.subType;
                    UserTranscriptionTemplateModel.description = TranscriptionJobInfo.description;
                    UserTranscriptionTemplateModel.options = TranscriptionJobOptions.Question; // currently using same question table for transcription.
                    UserTranscriptionTemplateModel.title = TranscriptionJobInfo.title;
                    UserTranscriptionTemplateModel.refKey = TranscriptionJobInfo.referenceId;
                    UserTranscriptionTemplateModel.imageUrl = TranscriptionImage.imgurLink;
                    //UserTranscriptionTemplateModel.uniqueId = Convert.ToString(TranscriptionAllocatedThreadInfo.Id);
                    response.Status = 200;
                    response.Message = "success";
                    response.Payload = UserTranscriptionTemplateModel;
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