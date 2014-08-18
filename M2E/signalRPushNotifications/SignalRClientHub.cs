using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using M2E.Session;

namespace M2E.signalRPushNotifications
{
    public class SignalRClientHub : Hub
    {
        private static Dictionary<string, dynamic> connectedClients = new Dictionary<string, dynamic>();

        public void RegisterClient(string tokenId)
        {
            M2ESession session = TokenManager.getSessionInfo(tokenId);
            if (session != null)
            {
                SignalRManager.SignalRCreateManager(session.UserName, Clients.Caller);
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