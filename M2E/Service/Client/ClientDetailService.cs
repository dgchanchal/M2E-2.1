using System;
using System.Linq;
using M2E.Models;
using M2E.Models.DataResponse;
using M2E.Common.Logger;
using System.Reflection;
using M2E.CommonMethods;
using M2E.Models.Constants;

namespace M2E.Service.Client
{
    public class ClientDetailService
    {
        private static readonly ILogger Logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        private DbContextException _dbContextException = new DbContextException();
        private readonly M2EContext _db = new M2EContext();

        public ResponseModel<ClientDetailsModel> GetClientDetails(string username)
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
                        createClientDetailResponse.gold = "0";
                        createClientDetailResponse.silver = "0";
                        createClientDetailResponse.bronze = "0";
                    }
                    else
                    {
                        createClientDetailResponse.totalReputation = userReputation.ReputationScore;
                        createClientDetailResponse.gold = userReputation.GoldEarned;
                        createClientDetailResponse.silver = userReputation.SilverEarned;
                        createClientDetailResponse.bronze = userReputation.BronzeEarned;
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
    }
}