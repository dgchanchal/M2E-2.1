using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M2E.Models.Constants;
using M2E.signalRPushNotifications;
using Microsoft.AspNet.SignalR;
using M2E.Session;

namespace M2E.Service.UserService
{
    public class UserUpdatesClientRealTimeData
    {
        public bool UpdateClientRealTimeData(long JobId,long JobCompleted,long JobAssigned, long JobReviewed,string totalThreads,string username)
        {           
            var SignalRClientHub = new SignalRClientHub();
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SignalRClientHub>();
            dynamic client = SignalRManager.getSignalRDetail(username+Constants.userType_client);
            if (client != null)
            {
                client.updateClientProgressChart(Convert.ToString(JobId), totalThreads, Convert.ToString(JobCompleted), Convert.ToString(JobAssigned), Convert.ToString(JobReviewed));
                return true;
                //client.updateClientProgressChart("8", "20", "10", "8", "5");
                //client.addMessage("add message signalR");
            }
            else
                return false;
        }
    }
}