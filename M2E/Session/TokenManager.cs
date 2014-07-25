using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;

namespace M2E.Session
{
    public class TokenManager
    {
        public static void CreateSession(M2ESession session)
        {
            var sessionId = session.SessionId;
            const int hours = 1; // TODO: currently hard coded hour value;
            MemoryCache.Default.Set(sessionId, session, new CacheItemPolicy() { SlidingExpiration = new TimeSpan(hours, 0, 0) });
        }

        public static void RemoveSession(string sessionId)
        {
            MemoryCache.Default.Remove(sessionId);
        }

        public static bool IsValidSession(string sessionId)
        {
            if (sessionId == null)
                return false;

            M2ESession session = null;
            return IsValidSession(sessionId, out session);
        }

        private static bool IsValidSession(string sessionId, out  M2ESession session)
        {
            session = null;            
            if (MemoryCache.Default.Contains(sessionId))
            {
                session = (M2ESession)MemoryCache.Default.Get(sessionId);
            }
            return VerifySessionObject(session);
        }

        private static bool VerifySessionObject(M2ESession session)
        {
            return session != null;
        }
    }
}