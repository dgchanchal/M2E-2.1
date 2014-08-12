using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataResponse.UserResponse.Survey
{
    public class UserSurveyResponse
    {
        UserSurveyInfoInstruction Instructions { get; set; }
        UserSurveyInfoSingleAnswerQueston SingleAnswerQuestion { get; set; }
        UserSurveyInfoMultipleAnswerQuestion MultipleAnswerQuestion { get; set; }
        UserSurveyInfoListBoxAnswerQuestion ListBoxAnswerQuestion { get; set; }
        UserSurveyInfoTextBoxAnswerQuestion TextBoxAnswerQuestion { get; set; }
    }
}