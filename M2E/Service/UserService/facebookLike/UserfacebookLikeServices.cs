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
using Facebook;
using System.Data.Entity.Validation;

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
            else
            {
                //checkIfUserConnectedWithFacebook.AuthToken;
            }
            var facebookLikeTemplateDataList = _db.CreateTemplateFacebookLikes.OrderByDescending(x => x.creationTime).ToList();
            foreach (var facebookLikeTemplateData in facebookLikeTemplateDataList)
            {
                var fb = new FacebookClient(checkIfUserConnectedWithFacebook.AuthToken);
                bool alreadyLikedByUser = false;
                try
                {
                    dynamic result = fb.Get("fql",
                                new { q = "SELECT page_id FROM page_fan WHERE uid=" + checkIfUserConnectedWithFacebook.facebookId + " AND page_id=" + facebookLikeTemplateData.pageId });
                    foreach (var item in result.data)
                    {
                        alreadyLikedByUser = true; // exists   
                    }
                }
                catch (Exception)
                {
                    response.Status = 206;
                    response.Message = "Facebook Auth Token Expired";
                    return response; ;
                }
                                               
                if (alreadyLikedByUser) continue; // do not add in list if user already liked the page.

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

        public ResponseModel<String> ValidateFacebookLike(string username, string refKey)
        {
            var response = new ResponseModel<String>();            
            var checkIfUserConnectedWithFacebook = _db.FacebookAuths.SingleOrDefault(x => x.username == username);
            if (checkIfUserConnectedWithFacebook == null)
            {
                response.Status = 205;
                response.Message = "User is not connected with facebook";
                return response;
            }
            else
            {
                //checkIfUserConnectedWithFacebook.AuthToken;
            }
            var facebookLikeTemplateData = _db.CreateTemplateFacebookLikes.SingleOrDefault(x => x.referenceId == refKey);
            
            var fb = new FacebookClient(checkIfUserConnectedWithFacebook.AuthToken);
            bool alreadyLikedByUser = false;
            try
            {
                dynamic result = fb.Get("fql",
                            new { q = "SELECT page_id FROM page_fan WHERE uid=" + checkIfUserConnectedWithFacebook.facebookId + " AND page_id=" + facebookLikeTemplateData.pageId });
                foreach (var item in result.data)
                {
                    alreadyLikedByUser = true; // exists   
                }
            }
            catch (Exception)
            {
                response.Status = 206;
                response.Message = "Facebook Auth Token Expired";
                return response; ;
            }
            if (alreadyLikedByUser)
            {
                var facebookLikeListMap = _db.facebookPageLikeMappings.SingleOrDefault(x => x.username == username && x.refKey == refKey);
                if (facebookLikeListMap != null)
                {
                    response.Status = 208;
                    response.Message = "You have already earned for this page like.";
                }
                else
                {
                    var facebookPageLikeMapData = new facebookPageLikeMapping
                    {
                        PageId = facebookLikeTemplateData.pageId,
                        refKey = facebookLikeTemplateData.referenceId,
                        UserFacebookId = checkIfUserConnectedWithFacebook.facebookId,
                        username = checkIfUserConnectedWithFacebook.username,
                        DateTime = DateTime.Now
                    };

                    _db.facebookPageLikeMappings.Add(facebookPageLikeMapData);

                    try
                    {
                        _db.SaveChanges();
                    }
                    catch (DbEntityValidationException e)
                    {
                        DbContextException.LogDbContextException(e);                        
                    }
                    response.Status = 200;
                    response.Message = "User liked the page";
                }                
                
            }
            else
            {
                response.Status = 207;
                response.Message = "User didn't like the page yet";
            }
            return response;
        }
    }
}