using System.Data.Entity.Validation;
using System.Globalization;
using System.Reflection;
using M2E.Common.Logger;
using M2E.CommonMethods;
using System;
using System.Linq;
using M2E.Models;
using M2E.Models.DataResponse;
using M2E.Encryption;
using System.Configuration;
using System.Collections.Generic;

namespace M2E.Service.Login
{
    public class WebLogin
    {
        private static readonly ILogger Logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        private DbContextException _dbContextException = new DbContextException();
        private readonly M2EContext _db = new M2EContext();

        public LoginResponse Login(string userName, string passwrod, string returnUrl, string keepMeSignedIn)
        {            
            var userData = new LoginResponse();
            if (_db.Users.Any(x => x.Username == userName && x.Password == passwrod))
            {
                var user = _db.Users.SingleOrDefault(x => x.Username == userName && x.isActive == "true");
                if (user != null)
                {
                    var data = new Dictionary<string, string>();                    
                    data["Username"] = user.Username;
                    data["Password"] = user.Password;
                    data["userGuid"] = user.guid;

                    var encryptedData = EncryptionClass.encryptUserDetails(data);
                    userData.UTMZK = encryptedData["UTMZK"];
                    userData.UTMZV = encryptedData["UTMZV"];
                    userData.TimeStamp = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                    userData.Code = "200";
                    try
                    {
                        user.KeepMeSignedIn = keepMeSignedIn.Equals("true",StringComparison.OrdinalIgnoreCase)? "true" : "false";
                        _db.SaveChanges();
                    }
                    catch (DbEntityValidationException e)
                    {
                        DbContextException.LogDbContextException(e);
                        userData.Code = "500";
                        return userData;
                    }
                }
                else
                    userData.Code = "403";
            }
            else
                userData.Code = "401";            
            return userData;
        }
    }
}