using System;
using System.Linq;
using System.Web;
using System.Data.Entity.Validation;
using System.Reflection;
using M2E.Common.Logger;
using M2E.CommonMethods;
using M2E.Encryption;
using M2E.Models;
using M2E.Models.DataWrapper;

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
                FirstName = req.FirstName,
                LastName = req.LastName,
                gender = "NA",
                ImageUrl = "NA"
            };
            _db.Users.Add(user);

            if (!string.IsNullOrEmpty(req.Referral))
            {
                var dbRecommedBy = new RecommendedBy
                {
                    RecommendedFrom = req.Referral,
                    RecommendedTo = req.Username
                };
                _db.RecommendedBies.Add(dbRecommedBy);
            }
            if (req.Type == "client")
            {
                var dbClientDetails = new ClientDetail
                {
                    Username = req.Username,
                    CompanyName = req.CompanyName
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