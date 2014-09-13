using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M2E.Common.Logger;
using System.Reflection;
using M2E.CommonMethods;
using M2E.Models;
using System.Data.Entity.Validation;
using M2E.Models.Constants;

namespace M2E.Service.UserService
{
    public class UserReputationService
    {
        private static readonly ILogger Logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        private DbContextException _dbContextException = new DbContextException();
        private readonly M2EContext _db = new M2EContext();

        public bool UpdateUserReputation(string username, int reputationVal,string type,string subType)
        {
            var userReputation = _db.UserReputations.SingleOrDefault(x => x.username == username);            
            if (userReputation == null)
            {
                var UserReputationData = new UserReputation
                {
                    
                };
                
                _db.UserReputations.Add(UserReputationData);
            }
            else
            {
                
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

        public bool UpdateUserBalance(string username, double approved, double pending)
        {
            var userBalance = _db.UserEarnings.SingleOrDefault(x => x.username == username);
            if (userBalance == null)
            {
                var UserEarningData = new UserEarning
                {
                    username = username,
                    total = Convert.ToString(Convert.ToDouble(approved) + Convert.ToDouble(pending)),
                    approved = Convert.ToString(Convert.ToDouble(approved)),
                    pending = Convert.ToString(Convert.ToDouble(pending)),
                    currency = Constants.currency_INR
                };
                _db.UserEarnings.Add(UserEarningData);
            }
            else
            {
                userBalance.total = Convert.ToString(Convert.ToDouble(userBalance.total) + Convert.ToDouble(approved) + Convert.ToDouble(pending));
                userBalance.approved = Convert.ToString(Convert.ToDouble(userBalance.approved) + Convert.ToDouble(approved));
                userBalance.pending = Convert.ToString(Convert.ToDouble(userBalance.pending) + Convert.ToDouble(pending));                 
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