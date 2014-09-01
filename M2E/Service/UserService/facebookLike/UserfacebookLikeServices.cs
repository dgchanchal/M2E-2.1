using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M2E.Models.DataResponse.UserResponse.FacebookLike;
using M2E.Models;
using M2E.Common.Logger;
using System.Reflection;
using M2E.CommonMethods;
using M2E.Models.Constants;
using M2E.DAO;

namespace M2E.Service.UserService.facebookLike
{
    public class UserfacebookLikeServices
    {
        private static readonly ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        private DbContextException _dbContextException = new DbContextException();
        private readonly M2EContext _db = new M2EContext();

        public ResponseModel<List<UserFacebookLikeTemplateModel>> GetAllFacebookLikeTemplateInformation(string username)
        {
            var response = new ResponseModel<List<UserFacebookLikeTemplateModel>>();
            response.Payload = new List<UserFacebookLikeTemplateModel>();
            var checkIfUserConnectedWithFacebook = _db.FacebookAuths.SingleOrDefault(x => x.username == username);
            if (checkIfUserConnectedWithFacebook == null)
            {
                response.Status = 205;
                response.Message = "User is not connected with facebook";
                return response;
            }
            var facebookLikeTemplateDataList = _db.CreateTemplateFacebookLikes.OrderByDescending(x => x.creationTime).ToList();
            foreach (var facebookLikeTemplateData in facebookLikeTemplateDataList)
            {
                var UserFacebookLikeTemplateModelData = new UserFacebookLikeTemplateModel();
                var ifAlreadyLiked = _db.UserFacebookLikeJobMappings.SingleOrDefault(x => x.refKey == facebookLikeTemplateData.referenceId && x.username == username);
                if (ifAlreadyLiked == null)
                {
                    UserFacebookLikeTemplateModelData.creationTime = facebookLikeTemplateData.creationTime;
                    UserFacebookLikeTemplateModelData.currency = Constants.currency_INR;
                    UserFacebookLikeTemplateModelData.earningPerThreads = facebookLikeTemplateData.payPerUser;
                    UserFacebookLikeTemplateModelData.pageId = facebookLikeTemplateData.pageId;
                    UserFacebookLikeTemplateModelData.pageUrl = facebookLikeTemplateData.pageUrl;
                    UserFacebookLikeTemplateModelData.refKey = facebookLikeTemplateData.referenceId;
                    UserFacebookLikeTemplateModelData.remainingThreads = Convert.ToString(new FacebookDAO().facebookLikeRemainingThreadsWithRefKey(facebookLikeTemplateData.referenceId));
                    UserFacebookLikeTemplateModelData.subType = Constants.subType_facebookLike;
                    UserFacebookLikeTemplateModelData.title = facebookLikeTemplateData.title;
                    UserFacebookLikeTemplateModelData.totalThreads = facebookLikeTemplateData.totalThreads;
                    UserFacebookLikeTemplateModelData.type = Constants.type_Ads;
                    UserFacebookLikeTemplateModelData.userDeadline = Constants.NA;
                    UserFacebookLikeTemplateModelData.userStatus = Constants.status_open;
                    UserFacebookLikeTemplateModelData.description = facebookLikeTemplateData.description;
                    response.Payload.Add(UserFacebookLikeTemplateModelData);
                }                
            }

            try
            {
                response.Status = 200;
                response.Message = "Success";
                return response;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                return response;
            }            
        }
    }
}