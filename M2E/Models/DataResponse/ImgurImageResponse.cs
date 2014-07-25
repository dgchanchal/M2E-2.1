using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataResponse
{
    public class ImgurImageResponse
    {
        public imgurData data { get; set; }
    }
    public class imgurData
    {
        public string id { get; set; }
        public string copyText { get; set; }
        public string deletehash { get; set; }
        public string link { get; set; }
        public string link_s { get; set; }
        public string link_m { get; set; }
        public string link_l { get; set; }
    }
}