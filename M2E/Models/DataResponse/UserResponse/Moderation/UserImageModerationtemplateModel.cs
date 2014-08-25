using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataResponse.UserResponse.Moderation
{
    public class UserImageModerationtemplateModel
    {
        public string type { get; set; }
        public string subType { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string refKey { get; set; }
        public string question { get; set; }
        public string options { get; set; }
        public string imageUrl { get; set; }
        public string uniqueId { get; set; }
    }
}