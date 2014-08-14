using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataResponse.UserResponse.Survey
{
    public class UserSurveyInfoInnerListData
    {
        public string id { get; set; }
        public string question { get; set; }
        public string options { get; set; }
        public string answer { get; set; }
    }
}