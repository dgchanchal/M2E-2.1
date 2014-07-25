using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using log4net.Config;
using GaDotNet.Common.Data;
using GaDotNet.Common.Helpers;
using GaDotNet.Common;
using System.Configuration;
using System.ComponentModel;

namespace M2E.Common.Logger
{
    public class Logger : ILogger
    {
        private string _currentClassName;        
        bool GALoggin;
        ILog logger = null;
        public Logger(string currentClassName)
        {
            this._currentClassName = currentClassName;
            GALoggin = Convert.ToBoolean(ConfigurationManager.AppSettings["GALogging"]);
            
            logger = LogManager.GetLogger(_currentClassName);
            BasicConfigurator.Configure();
            log4net.Config.XmlConfigurator.Configure();
              
                       
        }

        public void Info(string message)
        {
            if (GALoggin && Convert.ToBoolean(ConfigurationManager.AppSettings["GAInfoLogging"]))
            {                
                trackGoogleEvents("Logger-Info", "Info", message);               
            }
            else
            {
                logger.Info(message);
            }
        }

        public void Error(string message, Exception ex)
        {
            if (GALoggin)
            {
                trackGoogleEvents("Logger-Error", message, ex.Message.ToString());
            }
            else
            {
                logger.Error(message, ex);
            }            
        }

        public void Debug(string message, Exception ex)
        {
            if (GALoggin)
            {
                trackGoogleEvents("Logger-Debug", message, ex.Message.ToString());
            }
            else
            {
                logger.Debug(message, ex);
            }                  
        }

        public void Fatal(string message, Exception ex)
        {
            if (GALoggin)
            {
                trackGoogleEvents("Logger-Fatal", message, ex.Message.ToString());
            }
            else
            {
                logger.Fatal(message, ex);
            }              
        }

        private void trackGoogleEvents(string Category, string Action, string Label)
        {
            try
            {
                asyncTrackGoogleEvents(Category, Action, Label); // to make it async call if required..
            }
            catch (Exception ex)
            {
                logger.Fatal("Google Analytics Event Tracking Exception", ex);
            }
            
        }
        private void asyncTrackGoogleEvents(string Category, string Action, string Label)
        {
            GoogleEvent GoogleEvent = new GoogleEvent("MadeToEarn.com", Category, Action, Label, 1);
            TrackingRequest requestEvent = new RequestFactory().BuildRequest(GoogleEvent);
            GoogleTracking.FireTrackingEvent(requestEvent);
        }
    }
}