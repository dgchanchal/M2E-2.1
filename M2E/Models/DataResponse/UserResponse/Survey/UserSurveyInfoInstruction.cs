using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataResponse.UserResponse.Survey
{
    public class UserSurveyInfoInstruction
    {
        string type { get; set; }
        string subType { get; set; }
        List<UserSurveyInfoInnerInstructionListData> data { get; set; }
    }
}