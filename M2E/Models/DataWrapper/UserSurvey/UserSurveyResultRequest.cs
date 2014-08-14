using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataWrapper.UserSurvey
{
    public class UserSurveyResultRequest
    {
        public List<UserSurveyAnswerKeyValuePairRequest> surveySingleAnswerQuestion { get; set; }
        public List<UserSurveyAnswerKeyValuePairRequest> surveyMultipleAnswerQuestion { get; set; }
        public List<UserSurveyAnswerKeyValuePairRequest> surveyListBoxAnswerQuestion { get; set; }
        public List<UserSurveyAnswerKeyValuePairRequest> surveyTextBoxAnswerQuestion { get; set; }
        
    }
}