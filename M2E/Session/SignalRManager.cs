using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;

namespace M2E.Session
{
    public class SignalRManager
    {
        public static void SignalRCreateManager(string username,dynamic clientAddr)
        {            
            const int hours = 1; // TODO: currently hard coded hour value;
            //MemoryCache.Default.Set(sessionId, session, new CacheItemPolicy() { SlidingExpiration = new TimeSpan(hours, 0, 0) });
            if (!MemoryCache.Default.Contains(username))
            {
                setMemoryCacheValue(username, clientAddr, hours, 0, 0);
            }            
        }
        private static void setMemoryCacheValue(string username, dynamic clientAddr, int hours, int minutes, int seconds)
        {
            MemoryCache.Default.Set(username, clientAddr, new CacheItemPolicy() { SlidingExpiration = new TimeSpan(hours, 0, 0) });
        }
        public static void RemoveSession(string username)
        {
            MemoryCache.Default.Remove(username);
        }

        public static dynamic getSignalRDetail(string username)
        {            
            if (username == null)
                return null;
            if (MemoryCache.Default.Contains(username))
            {
                return (dynamic)MemoryCache.Default.Get(username);
            }
            else
                return null;
        }
    }
}