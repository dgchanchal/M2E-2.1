using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataResponse.UserResponse.Survey
{
    public class UserSurveyInfoInstruction
    {
        public string type { get; set; }
        public string subType { get; set; }
        public List<UserSurveyInfoInnerInstructionListData> data { get; set; }
    }
}