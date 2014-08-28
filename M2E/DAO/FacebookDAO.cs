using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M2E.Common.Logger;
using M2E.CommonMethods;
using M2E.Models;
using System.Reflection;
using System.Globalization;
using M2E.Models.Constants;

namespace M2E.DAO
{
    public class FacebookDAO
    {
        private static readonly ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        private DbContextException _dbContextException = new DbContextException();
        private readonly M2EContext _db = new M2EContext();

        public long facebookLikeCompletedThreadsWithRefKey(string refKey)
        {
            return _db.UserFacebookLikeJobMappings.Where(x => x.refKey == refKey && x.status == Constants.status_done).Count();
        }
        public long facebookLikeRemainingThreadsWithRefKey(string refKey)
        {
            return ((Convert.ToInt32(_db.CreateTemplateFacebookLikes.SingleOrDefault(x=>x.referenceId == refKey).totalThreads)) - (_db.UserFacebookLikeJobMappings.Where(x => x.refKey == refKey && x.status == Constants.status_done).Count()));
        }
    }
}