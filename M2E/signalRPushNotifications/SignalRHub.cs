using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace M2E.signalRPushNotifications
{
    public class SignalRHub : Hub
    {
        private static Dictionary<string, dynamic> connectedClients = new Dictionary<string, dynamic>();

        public void RegisterClient(string userName)
        {
            lock (connectedClients)
            {
                if (connectedClients.ContainsKey(userName))
                {
                    connectedClients[userName] = Clients.Caller;
                }
                else
                {
                    connectedClients.Add(userName, Clients.Caller);
                }
            }
            Clients.Caller.addMessage("'" + userName + "'registered.");
        }

        public void AddNotification(string notificationMessage, string toUser)
        {
            lock (connectedClients)
            {
                if (connectedClients.ContainsKey(toUser))
                {
                    dynamic client = connectedClients[toUser];
                    client.addMessage(notificationMessage);
                }
            }
        }

        public void Send(string name, string message)
        {
            Clients.All.sendMessage(name, message);
        }
    }
}