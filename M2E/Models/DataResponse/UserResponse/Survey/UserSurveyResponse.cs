using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataResponse.UserResponse.Survey
{
    public class UserSurveyResponse
    {
        public string surveyTitle { get; set; }
        public string type { get; set; }
        public string subType { get; set; }
        public UserSurveyInfoInstruction Instructions { get; set; }
        public UserSurveyInfoSingleAnswerQueston SingleAnswerQuestion { get; set; }
        public UserSurveyInfoMultipleAnswerQuestion MultipleAnswerQuestion { get; set; }
        public UserSurveyInfoListBoxAnswerQuestion ListBoxAnswerQuestion { get; set; }
        public UserSurveyInfoTextBoxAnswerQuestion TextBoxAnswerQuestion { get; set; }
    }
}