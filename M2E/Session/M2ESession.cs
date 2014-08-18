using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Session
{
    public class M2ESession
    {
        public M2ESession(string userName)
        {
            var now = DateTime.Now;
            this.SessionId = Guid.NewGuid().ToString();
            this.UserName = userName;
        }
        public M2ESession(string userName, string Guid)
        {
            var now = DateTime.Now;
            this.SessionId = Guid;
            this.UserName = userName;
        }
        public string SessionId { get; set; }
        public string UserName { get; set; }
    }
}