using System.Globalization;
using System.Web;
using M2E.Common.Logger;
using M2E.Models;
using System;
using System.Linq;
using M2E.Encryption;
using M2E.Models.DataWrapper;
using M2E.CommonMethods;
using System.Data.Entity.Validation;
using System.Reflection;
using M2E.Session;
using M2E.Models.DataResponse;
using System.Collections.Generic;
using M2E.Models.Constants;

namespace M2E.Service.Auth
{
    public class AuthService
    {
        private static readonly ILogger Logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        private DbContextException _dbContextException = new DbContextException();
        private readonly M2EContext _db = new M2EContext();

        public ResponseModel<String> ValidateAccountService(ValidateAccountRequest req)
        {
            var response = new ResponseModel<string>();
            if (_db.ValidateUserKeys.Any(x => x.Username == req.userName && x.guid == req.guid))
            {
                var user = _db.Users.SingleOrDefault(x => x.Username == req.userName);
                if (user == null)
                {
                    response.Status = 500;
                    response.Message = "Internal Server Error";
                    Logger.Info("Validate Account : " + req.userName);
                    return response;
                }
                if (user.isActive == "true")
                {
                    response.Status = 405;
                    response.Message = "already active user";
                    return response;
                }
                user.isActive = "true";
                var Recommendations = _db.RecommendedBies.SingleOrDefault(x => x.RecommendedTo == user.Username);
                if (Recommendations != null)
                    Recommendations.isValid = Constants.status_true;
                try
                {
                    _db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    DbContextException.LogDbContextException(e);
                    response.Status = 500;
                    response.Message = "Internal Server Error";
                    return response;
                }
                response.Status = 200;
                response.Message = "validated";
                return response;
            }
            response.Status = 402;
            response.Message = "link expired";
            return response;
        }

        public ResponseModel<LoginResponse> LockAccountService(HeaderManager headers, M2ESession session)
        {
            var response = new ResponseModel<LoginResponse>();
            if (session.UserName != null)
            {
                bool logoutStatus = new TokenManager().Logout(headers.AuthToken);
                var user = _db.Users.SingleOrDefault(x => x.Username == session.UserName);
                if (user != null)
                {                                        
                    var data = new Dictionary<string, string>();
                    data["Username"] = user.Username;
                    data["Password"] = user.Password;
                    data["userGuid"] = user.guid;

                    var encryptedData = EncryptionClass.encryptUserDetails(data);

                    response.Payload = new LoginResponse();
                    response.Payload.UTMZK = encryptedData["UTMZK"];
                    response.Payload.UTMZV = encryptedData["UTMZV"];
                    response.Payload.TimeStamp = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                    response.Payload.Code = "200";
                    response.Status = 200;
                    response.Message = "Account Locked";

                    var newUserSession = new M2ESession(user.Username);
                    TokenManager.CreateSession(newUserSession);
                    response.Payload.UTMZT = newUserSession.SessionId;
                    user.Locked = Constants.status_true;

                    try
                    {
                        _db.SaveChanges();
                    }
                    catch (DbEntityValidationException e)
                    {
                        DbContextException.LogDbContextException(e);                        
                    }
                }
                else
                {
                    response.Status = 424;
                    response.Message = "user detail not available";
                }
            }
                        
            return response;
        }

        public ResponseModel<LoginResponse> unlockAccountService(HeaderManager headers, M2ESession session,string password)
        {
            var response = new ResponseModel<LoginResponse>();
            if (session == null)
            {
                response.Status = 201;
                response.Message = "user session not available";
            }
            else if (session.UserName != null)
            {
                var user = _db.Users.SingleOrDefault(x => x.Username == session.UserName && x.Password == password);
                if (user != null)
                {
                    user.Locked = Constants.status_false;
                    try
                    {
                        _db.SaveChanges();
                        response.Status = 200;
                        response.Message = "successfully unlocked";
                    }
                    catch (DbEntityValidationException e)
                    {
                        DbContextException.LogDbContextException(e);
                        response.Status = 500;
                        response.Message = "Exception occured";
                    }
                }
                else
                {
                    response.Status = 424;
                    response.Message = "user detail not available";
                }
            }
            else
            {
                response.Status = 201;
                response.Message = "user session not available";
            }
            return response;
        }

        public ResponseModel<String> ResendValidationCodeService(ValidateAccountRequest req, HttpRequestBase request)
        {
            var response = new ResponseModel<string>();
            if (_db.Users.Any(x => x.Username == req.userName))
            {
                var user = _db.Users.SingleOrDefault(x => x.Username == req.userName);
                if (user != null && (user.isActive == "true"))
                {
                    // Account has been already validated.
                    response.Status = 402;
                    response.Message = "warning";
                    return response;
                }
                var guidAlreadyExist = _db.ValidateUserKeys.SingleOrDefault(x => x.Username == req.userName);
                if (guidAlreadyExist != null)
                {
                    _db.ValidateUserKeys.Remove(guidAlreadyExist);
                }
                var dbValidateUserKey = new ValidateUserKey
                {
                    guid = Guid.NewGuid().ToString(),
                    Username = req.userName
                };
                _db.ValidateUserKeys.Add(dbValidateUserKey);
                try
                {
                    _db.SaveChanges();
                    SendAccountCreationValidationEmail.SendAccountCreationValidationEmailMessage(req.userName, dbValidateUserKey.guid, request);
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
                return response;
            }
            // User Doesn't Exist
            response.Status = 404;
            response.Message = "warning";
            return response;
        }

        public ResponseModel<String> ForgetPasswordService(string id, HttpRequestBase request)
        {
            var response = new ResponseModel<string>();
            if (_db.Users.Any(x => x.Username == id))
            {
                var user = _db.Users.SingleOrDefault(x => x.Username == id);
                if (user != null && (user.isActive.Equals("false",StringComparison.InvariantCulture)))
                {
                    // User account has not validated yet
                    response.Status = 402;
                    response.Message = "warning";
                    return response;
                }
                var forgetPasswordDataAlreadyExists = _db.ForgetPasswords.SingleOrDefault(x => x.Username == id);
                if (forgetPasswordDataAlreadyExists != null)
                    _db.ForgetPasswords.Remove(forgetPasswordDataAlreadyExists);
                var guid = Guid.NewGuid().ToString();
                var forgetPasswordData = new ForgetPassword
                {
                    Username = id,
                    guid = guid
                };
                _db.ForgetPasswords.Add(forgetPasswordData);
                try
                {
                    _db.SaveChanges();
                    var forgetPasswordValidationEmail = new ForgetPasswordValidationEmail();
                    forgetPasswordValidationEmail.SendForgetPasswordValidationEmailMessage(id, guid, request, DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture));
                }
                catch (DbEntityValidationException e)
                {
                    DbContextException.LogDbContextException(e);
                    response.Status = 500;
                    response.Message = "Internal Server Error";
                    return response;
                }
            }
            else
            {
                // User doesn't exist
                response.Status = 404;
                response.Message = "warning";
                return response;
            }
                response.Status = 200;
                response.Message = "Success";
                return response;
        }

        public ResponseModel<String> ResetPasswordService(ResetPasswordRequest req)
        {
            var response = new ResponseModel<string>();
            //EncryptionClass.GetDecryptionValue(req.Username, ConfigurationManager.AppSettings["AuthKey"]);
            if (_db.ForgetPasswords.Any(x => x.guid == req.Guid))
            {
                var removeForgetPasswordData = _db.ForgetPasswords.SingleOrDefault(x => x.guid == req.Guid);
                _db.ForgetPasswords.Remove(removeForgetPasswordData);

                var userData = _db.Users.SingleOrDefault(x => x.Username == removeForgetPasswordData.Username);
                if (userData != null)
                {
                    var password = EncryptionClass.Md5Hash(req.Password);
                    userData.Password = password;
                    userData.Locked = "false";
                }
                try
                {
                    _db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    DbContextException.LogDbContextException(e);
                    response.Status = 500;
                    response.Message = "Internal Server Error.";
                    Logger.Info("Save new Reseted Password : " + req.Username);
                    return response;
                }
                response.Status = 200;
                response.Message = "Success";
                return response;
            }
            response.Status = 402;
            response.Message = "link expired";
            return response;
        }
    }
}