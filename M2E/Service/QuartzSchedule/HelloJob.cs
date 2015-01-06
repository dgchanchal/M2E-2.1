using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using M2E.Common.Logger;
using M2E.CommonMethods;
using M2E.Models;
using M2E.Models.Constants;
using M2E.Service.Notifications;
using M2E.Service.UserService;
using Quartz;

namespace M2E.Service.QuartzSchedule
{
    public class HelloJob : IJob
    {
        private static readonly ILogger Logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        private DbContextException _dbContextException = new DbContextException();
        private readonly M2EContext _db = new M2EContext();
        
        public delegate void QuartzSchedulerCheckUserJob_Delegate(string username, string refKey);
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("HelloJob is executing.");

            JobKey key = context.JobDetail.Key;
            JobDataMap dataMap = context.JobDetail.JobDataMap;

            string username = dataMap.GetString("username");
            string refKey = dataMap.GetString("refKey");

            QuartzSchedulerCheckUserJob_Delegate QuartzSchedulerCheckUserServiceJobDelegate = null;
            QuartzSchedulerCheckUserServiceJobDelegate = new QuartzSchedulerCheckUserJob_Delegate(QuartzSchedulerUserCheckAndUpdate);
            IAsyncResult CallAsynchMethod = null;
            CallAsynchMethod = QuartzSchedulerCheckUserServiceJobDelegate.BeginInvoke(username,refKey, null, null); //invoking the method
            
            }

        public void QuartzSchedulerUserCheckAndUpdate(string username, string refKey)
        {
            var userMappingTaskInfo =
                _db.UserJobMappings.SingleOrDefault(x => x.refKey == refKey && x.username == username && x.status == Constants.status_assigned);
            var taskInfo = _db.CreateTemplateQuestionInfoes.SingleOrDefault(x => x.referenceId == refKey);
            if (userMappingTaskInfo != null && taskInfo != null)
            {
                userMappingTaskInfo.status = Constants.status_timelineMissed;
                try
                {
                    _db.SaveChanges();
                    new UserReputationService().UpdateUserReputation(username,
                        -(Convert.ToDouble(taskInfo.payPerUser)*10), taskInfo.type, taskInfo.subType);
                    new UserNotificationService().SendUserSurveyAcceptanceMessage(username,
                        "Your Timeline missed for a job. <br/>" + taskInfo.title + "<br/> reputation deducted : -" + Convert.ToString(Convert.ToDouble(taskInfo.payPerUser) * 10));
                }
                catch (DbEntityValidationException e)
                {
                    DbContextException.LogDbContextException(e);
                }
                catch (Exception ex)
                {
                    Logger.Error("Hello Job schduler",ex);
                }
            }
        }

        
    }
}
