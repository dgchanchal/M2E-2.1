﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M2E.Common.Logger;
using System.Reflection;
using M2E.CommonMethods;
using M2E.Models;
using System.Data.Entity.Validation;
using M2E.Models.Constants;
using System.Configuration;
using M2E.Service.Notifications;

namespace M2E.Service.UserService
{
    public class UserReputationService
    {
        private static readonly ILogger Logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        private DbContextException _dbContextException = new DbContextException();
        private readonly M2EContext _db = new M2EContext();

        public bool UpdateUserReputation(string username, double reputationVal,string type,string subType)
        {
            var userReputation = _db.UserReputations.SingleOrDefault(x => x.username == username);            
            if (userReputation == null)
            {
                var userReputationData = new UserReputation
                {
                    username = username,
                    ReputationScore = Convert.ToString(reputationVal),
                    UserBadge = Constants.NA
                };
                _db.UserReputations.Add(userReputationData);
            }
            else
            {
                userReputation.ReputationScore = Convert.ToString(Convert.ToDouble(userReputation.ReputationScore) + reputationVal);
            }

            String descriptionString = Constants.NA;
            if (reputationVal < 0)
                descriptionString = Constants.reputationDeducted;
            var UserReputationMappingData = new UserReputationMapping
            {
                DateTime = DateTime.Now,
                description = descriptionString,
                type = type,
                subType = subType,
                username = username,
                reputation = Convert.ToString(reputationVal)
            };
            _db.UserReputationMappings.Add(UserReputationMappingData);
            
            try
            {
                _db.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException e)
            {
                DbContextException.LogDbContextException(e);
                return false;
            }
        }

        public bool UpdateUserBalance(string userType,string username, double approved, double pending,double averageReputationScore, string paymentMode, string title, string type, string subType, bool isDollar)
        {
            var userBalance = _db.UserEarnings.SingleOrDefault(x => x.username == username && x.userType == userType);
            string currency = Constants.currency_INR;
            if (userType == Constants.userType_client)
            {
                 currency = Constants.currency_Dollar;
            }               

            bool addToUserBalanceHistory = approved > 0;
            if (isDollar)
            {
                approved *= (Convert.ToDouble(Convert.ToString(ConfigurationManager.AppSettings["dollarToRupeesValue"])));
                pending *= (Convert.ToDouble(Convert.ToString(ConfigurationManager.AppSettings["dollarToRupeesValue"])));
            }            
            if (userBalance == null)
            {
                var UserEarningData = new UserEarning
                {
                    username = username,
                    total = Convert.ToString(Convert.ToDouble(approved) + Convert.ToDouble(pending)),
                    approved = Convert.ToString(Convert.ToDouble(approved)),
                    pending = Convert.ToString(Convert.ToDouble(pending)),
                    currency = currency,
                    userType = userType
                };
                _db.UserEarnings.Add(UserEarningData);
            }
            else
            {
                userBalance.total = Convert.ToString(Convert.ToDouble(userBalance.total) + Convert.ToDouble(approved) + Convert.ToDouble(pending));
                userBalance.approved = Convert.ToString(Convert.ToDouble(userBalance.approved) + Convert.ToDouble(approved));
                userBalance.pending = Convert.ToString(Convert.ToDouble(userBalance.pending) + Convert.ToDouble(pending));                 
            }

            if (addToUserBalanceHistory)
            {
                var userEarningHistoryUpdate = new UserEarningHistory
                {
                    amount = Convert.ToString(approved),
                    dateTime = DateTime.Now,
                    paymentMode = paymentMode,
                    subtype = subType,
                    type = type,
                    title = title,
                    username = username,
                    userType = userType,
                    currency = currency
 
                };

                _db.UserEarningHistories.Add(userEarningHistoryUpdate);

                if (type == Constants.type_survey)
                {
                    var userReputation = _db.UserReputations.SingleOrDefault(x => x.username == username);
                    var reputationScore = Math.Round(((approved * averageReputationScore)/5),2);
                    if (userReputation == null)
                    {
                        var userReputationData = new UserReputation
                        {
                            username = username,
                            ReputationScore = Convert.ToString(reputationScore),
                            UserBadge = Constants.NA
                        };
                        _db.UserReputations.Add(userReputationData);
                    }
                    else
                    {
                        userReputation.ReputationScore = Convert.ToString(Convert.ToDouble(userReputation.ReputationScore)+reputationScore);
                    }
                    var UserReputationMappingData = new UserReputationMapping
                    {
                        DateTime = DateTime.Now,
                        description = Constants.NA,
                        type = type,
                        subType = subType,
                        username = username,
                        reputation = Convert.ToString(reputationScore)
                    };
                    _db.UserReputationMappings.Add(UserReputationMappingData);

                    //synchronous call in thread.
                    new UserNotificationService().SendUserSurveyAcceptanceMessage(username, "You Have Earned Reputation:" + Convert.ToString(reputationScore) + " <br/> Money: " + Convert.ToString(approved) + "<br/> From Survey - " + title);
                }                
            }

            
            try
            {
                _db.SaveChanges();                        
                return true;
            }
            catch (DbEntityValidationException e)
            {
                DbContextException.LogDbContextException(e);
                return false;                  
            }
        }
    }
}