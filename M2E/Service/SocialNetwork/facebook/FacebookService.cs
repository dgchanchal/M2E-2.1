using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Net;
using System.IO;
using Facebook;
using System.Configuration;
using System.Data.Entity.Validation;

namespace zestork.Service
{
    public class FacebookService
    {
        //private ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));

        //public LogOnModel Login(string returnUrl, string code, string referral,string userType)
        //{
        //    var userData = new LogOnModel();
        //    userData = checkFacebookAuthorization(returnUrl, code, userType);
        //    return userData;
        //}

        //private LogOnModel checkFacebookAuthorization(string returnUrl, string code, string userType)
        //{
        //    var userData = new LogOnModel();
        //    try
        //    {
        //        var _db = new ZestorkContainer();

        //        string app_id = string.Empty;
        //        string app_secret = string.Empty;

        //        app_id = ConfigurationManager.AppSettings["FacebookAppID"].ToString();
        //        app_secret = ConfigurationManager.AppSettings["FacebookAppSecret"].ToString();
                                              
        //        string scope = "";
        //        if (code == null)
        //        {
        //            userData.ReturnUrl = (string.Format(
        //                "https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri={1}&scope={2}",
        //                app_id, returnUrl, scope));

        //            return userData;
        //        }
        //        else
        //        {

        //            string access_token = getFacebookAuthToken(returnUrl, scope, code,app_id,app_secret);
        //            var client = new FacebookClient(access_token);
        //            dynamic me = client.Get("me");
        //            String userName = Convert.ToString(me.username);
        //            userData.User = new User();
        //            if (_db.Users.Any(x => x.Username == userName + "@facebook.com"))
        //            {
        //                Users user = _db.Users.SingleOrDefault(x => x.Username == userName + "@facebook.com");
        //                //user is already registered
        //                userData.User.FirstName = user.FirstName;
        //                userData.User.LastName = user.LastName;
        //                userData.User.Username = user.Username;
        //                userData.User.Gender = user.gender;
        //                userData.User.ImageUrl = user.ImageUrl;
        //                userData.statusCode = "200";
        //            }
        //            else
        //            {
        //                // add user in database
        //                String ID = Guid.NewGuid().ToString();
        //                String ImageUrl = FacebookService.GetPictureUrl(userName);                        
        //                userData.User.FirstName = me.first_name;
        //                userData.User.LastName = me.last_name;
        //                userData.User.Username = me.username;                        
        //                userData.User.ImageUrl = ImageUrl;
        //                userData.User.Username = userName + "@facebook.com";
        //                var user = new Users
        //                {
        //                    Username = userName + "@facebook.com",
        //                    Password = Guid.NewGuid().ToString(),
        //                    Source = "facebook",
        //                    isActive = "true",
        //                    Type = userType!=null?userType:"NA",
        //                    guid = Guid.NewGuid().ToString(),
        //                    FirstName = me.first_name,
        //                    LastName = me.last_name,
        //                    gender = "NA",
        //                    ImageUrl = ImageUrl
        //                };

        //                _db.Users.Add(user);

        //                try
        //                {
        //                    _db.SaveChanges();
        //                    userData.statusCode = "200";
        //                }
        //                catch (DbEntityValidationException e)
        //                {
        //                    dbContextException dbContextException = new CommonMethods.dbContextException();
        //                    dbContextException.logDbContextException(e);
        //                    throw;
        //                }
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("facebook login error", ex);
        //        throw;
        //    }
            

        //    return userData;
        //}

        //private string getFacebookAuthToken(string returnUrl, string scope, string code, string app_id, string app_secret)
        //{
            
        //    Dictionary<string, string> tokens = new Dictionary<string, string>();
        //    string url = string.Format(
        //        "https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}&scope={2}&code={3}&client_secret={4}",
        //        app_id, returnUrl, scope, code, app_secret);

        //    HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

        //    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
        //    {

        //        StreamReader reader = new StreamReader(response.GetResponseStream());

        //        string vals = reader.ReadToEnd();

        //        foreach (string token in vals.Split('&'))
        //        {
        //            tokens.Add(token.Substring(0, token.IndexOf("=")),
        //                token.Substring(token.IndexOf("=") + 1, token.Length - token.IndexOf("=") - 1));
        //        }

        //    }

        //    string access_token = tokens["access_token"];
        //    return access_token;
        //}
        //public static string GetPictureUrl(string faceBookId)
        //{
        //    WebResponse response = null;
        //    string pictureUrl = string.Empty;
        //    try
        //    {
        //        WebRequest request = WebRequest.Create(string.Format("http://graph.facebook.com/{0}/picture?type=large", faceBookId));
        //        response = request.GetResponse();
        //        pictureUrl = response.ResponseUri.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        //? handle
        //    }
        //    finally
        //    {
        //        if (response != null) response.Close();
        //    }
        //    return pictureUrl;
        //}

    }
}