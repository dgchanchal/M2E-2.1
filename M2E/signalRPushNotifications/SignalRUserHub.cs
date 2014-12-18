using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M2E.Models.Constants;
using Microsoft.AspNet.SignalR;
using M2E.Session;

namespace M2E.signalRPushNotifications
{
    public class SignalRUserHub : Hub
    {
        private static Dictionary<string, dynamic> connectedUsers = new Dictionary<string, dynamic>();

        public void RegisterUser(string tokenId)
        {
            M2ESession session = TokenManager.getSessionInfo(tokenId);
            if (session != null)
            {
                SignalRManager.SignalRCreateManager(session.UserName+Constants.userType_user, Clients.Caller);
                Clients.Caller.addMessage("'" + session.UserName + "'registered.");
            }            
        }

        public void AddNotification(string notificationMessage, string toUser)
        {
            dynamic client = SignalRManager.getSignalRDetail(toUser);
            if(client != null)
                client.addMessage(notificationMessage);
            
        }
    }
}