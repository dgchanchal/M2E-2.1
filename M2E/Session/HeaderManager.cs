using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Session
{
    public class HeaderManager
    {
        public HeaderManager(HttpRequestBase requestHeader)
        {
            IEnumerable<string> headerValues = requestHeader.Headers.GetValues("UTMZT");
            if(headerValues != null)
                this.AuthToken = headerValues.FirstOrDefault();            
            
            headerValues = requestHeader.Headers.GetValues("UTMZK");            
            if (headerValues != null)
                this.AuthKey = headerValues.FirstOrDefault();

            headerValues = requestHeader.Headers.GetValues("UTMZV");
            if (headerValues != null)
                this.AuthValue = headerValues.FirstOrDefault();
        }

        public string AuthToken { get; set; }
        public string AuthKey { get; set; }
        public string AuthValue { get; set; }
    }
}