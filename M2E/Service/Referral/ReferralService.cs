using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;
using M2E.Common.Logger;
using M2E.CommonMethods;
using M2E.Models;
using M2E.Models.Constants;
using M2E.Service.Notifications;
using M2E.Service.UserService;

namespace M2E.Service.Referral
{
    public class ReferralService
    {
        private static readonly ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        private DbContextException _dbContextException = new DbContextException();
        private readonly M2EContext _db = new M2EContext();
        public delegate void payReferralBonus_Delegate(String refKey, String recommendedToUsername,String isValidated);

        public void payReferralBonusAsync(String refKey,String recommendedToUsername,String isValidated)
        {
            payReferralBonus_Delegate payReferralBonusDelegate_delegate = null;
            payReferralBonusDelegate_delegate = new payReferralBonus_Delegate(payReferralBonus);
            IAsyncResult CallAsynchMethod = null;
            CallAsynchMethod = payReferralBonusDelegate_delegate.BeginInvoke(refKey,recommendedToUsername,isValidated, null, null); //invoking the method
        }

        public void payReferralBonus(String refKey,String recommendedToUsername,String isValidated)
        {
            var referralInfo = _db.Users.SingleOrDefault(x => x.fixedGuid == refKey);
            var ReferralUsername = "";
            if (referralInfo != null)
            {
                ReferralUsername = referralInfo.Username;
            }
            else
            {
                ReferralUsername = Constants.NA;
            }

            var dbRecommedBy = new RecommendedBy
            {
                RecommendedFrom = refKey,
                RecommendedTo = recommendedToUsername,
                DateTime = DateTime.Now,
                isValid = isValidated,
                RecommendedFromUsername = ReferralUsername
            };
            _db.RecommendedBies.Add(dbRecommedBy);

            try
            {
                _db.SaveChanges();               
            }
            catch (DbEntityValidationException e)
            {
                DbContextException.LogDbContextException(e);                
            }

            if (isValidated == Constants.status_true)
            {
                var result_recommendation = new UserReputationService().UpdateUserBalance(Constants.userType_user, ReferralUsername,
               Constants.newAccountCreationReferralBalanceAmount, 0, 0, Constants.payment_credit, recommendedToUsername + " Joined Cautom", "New Account",
               "Referral Bonus", false);
            }
            
            new UserNotificationService().SendUserReferralAcceptanceNotification(ReferralUsername,recommendedToUsername);
        }
    }
}
