using System;
using System.Globalization;
using System.Linq;
using M2E.Models;
using M2E.Models.DataResponse;
using M2E.Common.Logger;
using System.Reflection;
using M2E.CommonMethods;
using M2E.Models.Constants;
using M2E.Encryption;
using System.Configuration;
using System.Collections.Generic;
using M2E.Models.DataResponse.UserResponse;

namespace M2E.Service.Client
{
    public class ClientDetailService
    {
        private static readonly ILogger Logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        private DbContextException _dbContextException = new DbContextException();
        private readonly M2EContext _db = new M2EContext();

        public ResponseModel<ClientDetailsModel> GetClientDetails(string username, string userType)
        {
            var response = new ResponseModel<ClientDetailsModel>();
                        
            try
            {
                var clientDetailDbResult = _db.Users.SingleOrDefault(x => x.Username == username);
                if (clientDetailDbResult != null)
                {
                    var createClientDetailResponse = new ClientDetailsModel
                    {
                        FirstName = clientDetailDbResult.FirstName,
                        LastName = clientDetailDbResult.LastName,
                        Username = clientDetailDbResult.Username,
                        imageUrl = clientDetailDbResult.ImageUrl == Constants.NA ? Constants.clientImageUrl : clientDetailDbResult.ImageUrl,
                        gender = clientDetailDbResult.gender,
                        isLocked = clientDetailDbResult.Locked
                    };
                    response.Status = 200;
                    response.Message = "success";
                    response.Payload = createClientDetailResponse;

                    var userReputation = _db.UserReputations.SingleOrDefault(x => x.username == clientDetailDbResult.Username);
                    if (userReputation == null)
                    {
                        createClientDetailResponse.totalReputation = "0";                       
                    }
                    else
                    {
                        createClientDetailResponse.totalReputation = userReputation.ReputationScore;                        
                    }

                    var userBalance = _db.UserEarnings.SingleOrDefault(x => x.username == username && x.userType == userType);
                    if (userBalance == null)
                    {
                        createClientDetailResponse.availableBalance = "0";
                        createClientDetailResponse.pendingBalance = "0";
                        createClientDetailResponse.currency = userType == Constants.userType_client ? Constants.currency_Dollar : Constants.currency_INR;
                    }
                    else
                    {
                        createClientDetailResponse.availableBalance = userBalance.approved;
                        createClientDetailResponse.pendingBalance = userBalance.pending;
                        createClientDetailResponse.currency = userBalance.currency;
                    }

                    var userMessages =
                        _db.UserMessages.Where(x => x.messageTo == username && x.userType == userType).OrderByDescending(x => x.dateTime).ToList();
                    createClientDetailResponse.Messages = new UserMessagesResponse
                    {
                        CountLabelType = "success",
                        UnreadMessages = userMessages.Count(x => x.messageSeen == Constants.status_false).ToString(),
                        MessageList = new List<UserMessageList>()
                    };
                    foreach (var message in userMessages)
                    {
                        var userMessage = new UserMessageList
                        {
                            link = "#",
                            MessagePostedInTimeAgo = "5 mins",
                            MessageSeen = message.messageSeen,
                            imageUrl = message.iconUrl,
                            messageTitle = message.titleText,
                            messageContent = message.bodyText
                        };
                        createClientDetailResponse.Messages.MessageList.Add(userMessage);
                    }
                }
                else
                {
                    response.Status = 404;
                    response.Message = "username not found";
                }
            }
            catch (Exception)
            {
                response.Status = 500;
                response.Message = "exception occured !!!";
            }
            return response;
        }

        public ResponseModel<UserReferenceDetailResponse> getReferralKey(string username)
        {
            var response = new ResponseModel<UserReferenceDetailResponse>();
            response.Payload = new UserReferenceDetailResponse();
            response.Payload.myReferenceList = new List<UserReferenceDetails>();            
            try
            {
                response.Status = 200;
                response.Message = "success !!!";
                var user = _db.Users.SingleOrDefault(x => x.Username == username);
                response.Payload.myReferralLink = user.fixedGuid;
                var referredUserList = _db.RecommendedBies.Where(x => x.RecommendedFrom == user.fixedGuid).ToList();
                foreach (var referredUser in referredUserList)
                {
                    var UserReferenceData = new UserReferenceDetails();
                    UserReferenceData.username = referredUser.RecommendedTo;
                    UserReferenceData.AccountCreationDate = referredUser.DateTime.ToString();
                    UserReferenceData.isValid = referredUser.isValid;
                    UserReferenceData.earning = (referredUser.isValid == Constants.status_true)?"INR 1":"NIL"; // currently hard coded.
                    response.Payload.myReferenceList.Add(UserReferenceData);
                }                                            
            }
            catch (Exception)
            {
                response.Status = 500;
                response.Message = "exception occured !!!";
            }
            return response;
        }

        public ResponseModel<List<EarningHistoryResponse>> GetEarningHistory(string username)
        {
            var response = new ResponseModel<List<EarningHistoryResponse>>();
            response.Payload = new List<EarningHistoryResponse>();
            
            try
            {
                response.Status = 200;
                response.Message = "success !!!";
                
                var earningHistoryList = _db.UserEarningHistories.OrderByDescending(x=>x.dateTime).Where(x => x.username == username).ToList();
                foreach (var earningHistory in earningHistoryList)
                {
                    var earningHistoryData = new EarningHistoryResponse
                    {
                        amount = earningHistory.amount,
                        PaymentMode = earningHistory.paymentMode,
                        dateTime = Convert.ToString(earningHistory.dateTime),
                        subType = earningHistory.subtype,
                        title = earningHistory.title,
                        type = earningHistory.type
                    };

                    response.Payload.Add(earningHistoryData);
                }
            }
            catch (Exception)
            {
                response.Status = 500;
                response.Message = "exception occured !!!";
            }
            return response;
        }

        public ResponseModel<List<ReputationHistoryResponse>> GetReputationHistory(string username)
        {
            var response = new ResponseModel<List<ReputationHistoryResponse>>();
            response.Payload = new List<ReputationHistoryResponse>();

            try
            {
                response.Status = 200;
                response.Message = "success !!!";

                var earningReputationList = _db.UserReputationMappings.OrderByDescending(x => x.DateTime).Where(x => x.username == username).ToList();
                foreach (var earningReputation in earningReputationList)
                {
                    var earningReputationData = new ReputationHistoryResponse
                    {
                        username = username,
                        dateTime = earningReputation.DateTime.ToString(),
                        description = Constants.NA,
                        type = earningReputation.type,
                        subType = earningReputation.subType,
                        reputation = earningReputation.reputation

                    };

                    response.Payload.Add(earningReputationData);
                }
            }
            catch (Exception)
            {
                response.Status = 500;
                response.Message = "exception occured !!!";
            }
            return response;
        }
    }
}