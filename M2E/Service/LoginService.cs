using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using M2E.Models.DataResponse;
using M2E.Service.Login;

namespace M2E.Service
{
    public class LoginService
    {
        public LoginResponse WebLogin(string userName, string passwrod, string returnUrl, string keepMeSignedIn)
        {
            var webLogin = new WebLogin();
            var model = webLogin.Login(userName, passwrod, returnUrl, keepMeSignedIn);
            return model;
        }
    }
}