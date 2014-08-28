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
        public string getFacebookAuthToken(string returnUrl, string scope, string code, string app_id, string app_secret)
        {

            Dictionary<string, string> tokens = new Dictionary<string, string>();
            string url = string.Format(
                "https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}&scope={2}&code={3}&client_secret={4}",
                app_id, returnUrl, scope, code, app_secret);

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {

                StreamReader reader = new StreamReader(response.GetResponseStream());

                string vals = reader.ReadToEnd();

                foreach (string token in vals.Split('&'))
                {
                    tokens.Add(token.Substring(0, token.IndexOf("=")),
                        token.Substring(token.IndexOf("=") + 1, token.Length - token.IndexOf("=") - 1));
                }

            }

            string access_token = tokens["access_token"];
            return access_token;
        }

        public static string GetPictureUrl(string faceBookId)
        {
            WebResponse response = null;
            string pictureUrl = string.Empty;
            try
            {
                WebRequest request = WebRequest.Create(string.Format("http://graph.facebook.com/{0}/picture?type=large", faceBookId));
                response = request.GetResponse();
                pictureUrl = response.ResponseUri.ToString();
            }
            catch (Exception ex)
            {
                //? handle
            }
            finally
            {
                if (response != null) response.Close();
            }
            return pictureUrl;
        }

    }
}