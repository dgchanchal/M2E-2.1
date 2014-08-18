using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;
using M2E.Models;
using System.Configuration;
using M2E.Encryption;

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

        public static string GetUsernameFromSessionId(HeaderManager headers)
        {
            var session = getSessionInfo(headers.AuthToken, headers);
            return session.UserName;
        }

        public static M2ESession getSessionInfo(string sessionId, HeaderManager headers)
        {
            M2ESession session = null;
            if (IsValidSession(sessionId, out session))
            {
                return session;
            }
            else
            {
                if (headers == null)
                    return null;
                if (sessionId == null)
                    return null;
                string Authkey = ConfigurationManager.AppSettings["AuthKey"];
                string username = EncryptionClass.GetDecryptionValue(headers.AuthKey, Authkey);
                M2EContext _db = new M2EContext();
                var dbUserInfo = _db.Users.SingleOrDefault(x=>x.Username == username);
                if (dbUserInfo != null)
                {
                    string password = EncryptionClass.GetDecryptionValue(headers.AuthValue, Authkey);
                    if (dbUserInfo.KeepMeSignedIn == "true" && dbUserInfo.Password == password)
                    {
                        var NewSession = new M2ESession(username, sessionId);
                        TokenManager.CreateSession(NewSession);
                        return getSessionInfo(sessionId, headers);
                    }
                    else
                    {
                        return null;
                    }

                }
                else
                {
                    return null;
                }                
            }
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

            if (sessionId == null)
                return false;
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