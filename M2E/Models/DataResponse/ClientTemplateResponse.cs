using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataResponse
{    
    public class ClientTemplateResponse
    {
        public bool showEllipse { get; set; }
        public string title { get; set; }
        public string timeShowType { get; set; }
        public string showTime { get; set; }
        public string editId { get; set; }
        public string creationDate { get; set; }
        public string progressPercent { get; set; }
    }
}