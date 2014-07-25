using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataResponse
{
    
        public class imgurUploadImageResponse
        {
            public data data { get; set; }
        }
        public class data
        {
            public string id { get; set; }
            public string deletehash { get; set; }
            public string link { get; set; }
        }
    
}