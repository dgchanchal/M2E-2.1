using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using M2E.Common.Logger;
using M2E.Models.Constants;
using M2E.Service.QuartzSchedule;
using Quartz;
using Quartz.Impl;

namespace M2E.Service.QuartzSchedulerService
{
    public class QuartzSchedulerService
    {
        private static readonly ILogger Logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        public QuartzSchedulerService(string username,string refKey)
        {
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            // get a scheduler
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();

            IJobDetail job = JobBuilder.Create<HelloJob>()
        .WithIdentity("myJob", username+refKey) // name "myJob", group "group1"
        .UsingJobData("username", username)
        .UsingJobData("refKey", refKey)
        .Build();

            // Trigger the job to run now, and then every 40 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", username+refKey)
                .StartAt(DateTime.Now.AddMinutes(Constants.allowedTimeIntervalForCompletingJobInMinutes))
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(60)
                    .WithRepeatCount(1))
                .Build();

            // Tell quartz to schedule the job using our trigger            
            try
            {
                sched.ScheduleJob(job, trigger);
            }
            catch (Exception ex)
            {
                Logger.Error("QuartzschedulerService ",ex);                    
            }
        }
    }
}
