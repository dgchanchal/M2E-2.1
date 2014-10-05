using System;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Data.Entity.Validation;
using System.Reflection;
using M2E.Common.Logger;
using M2E.CommonMethods;
using M2E.Encryption;
using M2E.Models;
using M2E.Models.DataWrapper;
using M2E.Service.UserService;
using M2E.signalRPushNotifications;
using Microsoft.AspNet.SignalR;
using M2E.Models.Constants;

namespace M2E.Service.Register
{
    public class WebRegister
    {
        private static readonly ILogger Logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        private DbContextException _dbContextException = new DbContextException();
        private readonly M2EContext _db = new M2EContext();

        public ResponseModel<String> WebRegisterService(RegisterationRequest req, HttpRequestBase request)
        {            
            var response = new ResponseModel<String>();
            if (_db.Users.Any(x => x.Username == req.Username))
            {
                response.Status = 409;
                response.Message = "conflict";
                return response;
            }

            var guid = Guid.NewGuid().ToString();
            var user = new User
            {
                Username = req.Username,
                Password = EncryptionClass.Md5Hash(req.Password),
                Source = req.Source,
                isActive = "false",
                Type = req.Type,
                guid = Guid.NewGuid().ToString(),
                fixedGuid= Guid.NewGuid().ToString(),
                FirstName = req.FirstName,
                LastName = req.LastName,
                gender = "NA",
                ImageUrl = "NA"
            };
            _db.Users.Add(user);
            
            if (!Constants.NA.Equals(req.Referral))
            {
                var referralInfo = _db.Users.SingleOrDefault(x => x.fixedGuid == req.Referral);
                var ReferralUsername = "";
                if (referralInfo != null)
                {
                    ReferralUsername = referralInfo.Username;
                }
                else
                {
                    ReferralUsername = Constants.NA;
                }

                var dbRecommedBy = new RecommendedBy
                {
                    RecommendedFrom = req.Referral,
                    RecommendedTo = req.Username,
                    DateTime = DateTime.Now,
                    isValid = Constants.status_false,
                    RecommendedFromUsername = ReferralUsername
                };
                _db.RecommendedBies.Add(dbRecommedBy);
                
            }
            if (req.Type == "client")
            {
                var dbClientDetails = new ClientDetail
                {
                    Username = req.Username,
                    CompanyName =  string.IsNullOrEmpty(req.CompanyName)?"NA":req.CompanyName
                };
                _db.ClientDetails.Add(dbClientDetails);
            }
            var dbValidateUserKey = new ValidateUserKey
            {
                Username = req.Username,
                guid = guid
            };

            _db.ValidateUserKeys.Add(dbValidateUserKey);

            try
            {
                _db.SaveChanges();
                var signalRHub = new SignalRHub();
                string totalProjects = "";
                string successRate = "";
                string totalUsers = _db.Users.Count().ToString(CultureInfo.InvariantCulture);
                string projectCategories = "";
                var hubContext = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>();
                hubContext.Clients.All.updateBeforeLoginUserProjectDetails(totalProjects, successRate, totalUsers, projectCategories);
                SendAccountCreationValidationEmail.SendAccountCreationValidationEmailMessage(req.Username, guid, request);
            }
            catch (DbEntityValidationException e)
            {
                DbContextException.LogDbContextException(e);
                response.Status = 500;
                response.Message = "Internal Server Error !!!";
                return response;
            }

            response.Status = 200;
            response.Message = "success";
            response.Payload = "Account Created";

            return response;
        }
    }
}