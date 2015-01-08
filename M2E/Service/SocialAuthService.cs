using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using Facebook;
using M2E.Common.Logger;
using M2E.CommonMethods;
using M2E.Encryption;
using M2E.Models;
using M2E.Models.Constants;
using M2E.Models.DataResponse;
using M2E.Service.Notifications;
using M2E.Service.Referral;
using M2E.Service.SocialNetwork.linkedin;
using M2E.Session;
using M2E.signalRPushNotifications;
using Microsoft.AspNet.SignalR;

namespace M2E.Service
{
    public class SocialAuthService
    {
        private static readonly ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        private DbContextException _dbContextException = new DbContextException();
        private readonly M2EContext _db = new M2EContext();
        private oAuthLinkedIn _oauth = new oAuthLinkedIn();

        public ResponseModel<LoginResponse> CheckAndSaveFacebookUserInfoIntoDatabase(string fid, string refKey, string access_token, bool isMobileApiCall)
        {
            var response = new ResponseModel<LoginResponse>();
            var ifFacebookUserAlreadyRegistered = _db.FacebookAuths.SingleOrDefault(x => x.facebookId == fid);

            if (isMobileApiCall)
            {
                var fb = new FacebookClient(access_token);

                dynamic fqlResponse = fb.Get("fql",
                            new { q = "SELECT uid, first_name, last_name, sex, pic_big_with_logo, username FROM user WHERE uid=me()" });
                var FacebookAuthData = new FacebookAuth();                
                FacebookAuthData.username = Constants.NA;
                FacebookAuthData.AuthToken = access_token;
                FacebookAuthData.datetime = DateTime.Now.ToString();
                FacebookAuthData.facebookId = Convert.ToString(fqlResponse.data[0].uid);
                FacebookAuthData.facebookUsername = fqlResponse.data[0].username;

                //var ifAlreadyExists = _db.FacebookAuths.SingleOrDefault(x => x.facebookId == fid);
                if (ifFacebookUserAlreadyRegistered == null)
                {
                    _db.FacebookAuths.Add(FacebookAuthData);
                    ifFacebookUserAlreadyRegistered = FacebookAuthData;
                }
                else
                {
                    // refresh the token
                    ifFacebookUserAlreadyRegistered.AuthToken = access_token;
                    ifFacebookUserAlreadyRegistered.datetime = DateTime.Now.ToString();
                }
            }

            //var ifFacebookUserAlreadyRegistered = _db.FacebookAuths.SingleOrDefault(x => x.facebookId == fid);
            if (ifFacebookUserAlreadyRegistered.username != Constants.NA)
            {
                if (_db.Users.Any(x => x.Username == ifFacebookUserAlreadyRegistered.username))
                {
                    var user = _db.Users.SingleOrDefault(x => x.Username == ifFacebookUserAlreadyRegistered.username);
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
                        response.Payload.Code = "210";
                        response.Status = 210;
                        response.Message = "user Login via facebook";
                        try
                        {
                            user.KeepMeSignedIn = "true";//keepMeSignedIn.Equals("true", StringComparison.OrdinalIgnoreCase) ? "true" : "false";
                            user.Locked = Constants.status_false;
                            _db.SaveChanges();

                            var session = new M2ESession(ifFacebookUserAlreadyRegistered.username);
                            TokenManager.CreateSession(session);
                            response.Payload.UTMZT = session.SessionId;
                            return response;

                        }
                        catch (DbEntityValidationException e)
                        {
                            DbContextException.LogDbContextException(e);
                            response.Payload.Code = "500";

                            return response;
                        }
                    }
                    else
                        response.Payload.Code = "403";
                }
            }
            else
            {
                //save user details in database ..

                var fb = new FacebookClient(ifFacebookUserAlreadyRegistered.AuthToken);
                dynamic result = fb.Get("fql",
                            new { q = "SELECT uid, first_name, last_name, sex, pic_big_with_logo, username FROM user WHERE uid=me()" });

                var guid = Guid.NewGuid().ToString();
                var user = new User
                {
                    Username = result.data[0].username + "@facebook.com",
                    Password = EncryptionClass.Md5Hash(Guid.NewGuid().ToString()),
                    Source = "facebook",
                    isActive = "true",
                    Type = "user",
                    guid = Guid.NewGuid().ToString(),
                    fixedGuid = Guid.NewGuid().ToString(),
                    FirstName = result.data[0].first_name,
                    LastName = result.data[0].last_name,
                    gender = result.data[0].sex,
                    ImageUrl = result.data[0].pic_big_with_logo
                };
                _db.Users.Add(user);

                if (!Constants.NA.Equals(refKey))
                {
                    new ReferralService().payReferralBonusAsync(refKey, user.Username, Constants.status_true);
                }

                try
                {
                    ifFacebookUserAlreadyRegistered.username = user.Username;
                    _db.SaveChanges();

                    var data = new Dictionary<string, string>();
                    data["Username"] = user.Username;
                    data["Password"] = user.Password;
                    data["userGuid"] = user.guid;

                    var encryptedData = EncryptionClass.encryptUserDetails(data);

                    response.Payload = new LoginResponse();
                    response.Payload.UTMZK = encryptedData["UTMZK"];
                    response.Payload.UTMZV = encryptedData["UTMZV"];
                    response.Payload.TimeStamp = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                    response.Payload.Code = "210";
                    response.Status = 210;
                    response.Message = "user Login via facebook";
                    try
                    {
                        var session = new M2ESession(ifFacebookUserAlreadyRegistered.username);
                        TokenManager.CreateSession(session);
                        response.Payload.UTMZT = session.SessionId;
                    }
                    catch (DbEntityValidationException e)
                    {
                        DbContextException.LogDbContextException(e);
                        response.Status = 500;
                        response.Message = "Internal Server Error !!";
                    }
                    var signalRHub = new SignalRHub();
                    string totalProjects = "";
                    string successRate = "";
                    string totalUsers = _db.Users.Count().ToString(CultureInfo.InvariantCulture);
                    string projectCategories = "";

                    new UserMessageService().SendUserNotificationForAccountVerificationSuccess(
                        user.Username, user.Type
                    );

                    var hubContext = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>();
                    hubContext.Clients.All.updateBeforeLoginUserProjectDetails(totalProjects, successRate, totalUsers, projectCategories);
                }
                catch (DbEntityValidationException e)
                {
                    DbContextException.LogDbContextException(e);
                    response.Status = 500;
                    response.Message = "Internal Server Error !!!";
                }

            }

            return response;
        }
    }
}
