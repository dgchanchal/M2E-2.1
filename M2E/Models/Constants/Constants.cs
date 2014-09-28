using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.Constants
{
    public static class Constants
    {
        public const string status_done = "done";
        public const string status_open = "open";
        public const string status_active = "active";
        public const string status_assigned = "assigned";
        public const string status_reviewed = "reviewed";

        public const string payment_credit = "credit";
        public const string payment_debit = "debit";

        public const string type_dataEntry = "dataEntry";
        public const string subType_Transcription = "Transcription";
        public const string subType_dataCollection = "dataCollection";
        public const string subType_taggingImage = "taggingImage";
        public const string subType_transcribeAV = "transcribeAV";

        public const string type_moderation = "moderation";
        public const string subType_moderatingPhotos = "moderatingPhotos";        
        
        public const string type_survey = "survey";
        public const string subType_productSurvey = "productSurvey";
        public const string subType_surveyLink = "surveyLink";
                                  
        public const string type_contentWritting = "contentWriting";      
        public const string subType_articleWritting = "articleWriting";
        public const string subType_blogWriting = "blogWriting";
        public const string subType_copyTyping = "copyTyping";
        public const string subType_powerpoint = "powerpoint";
        public const string subType_shortStories = "shortStories";
        public const string subType_travelWriting = "travelWriting";
        public const string subType_reviews = "reviews";
        public const string subType_productDescriptions = "productDescriptions";
        
        public const string type_Ads = "Ads";        
        public const string subType_facebookLike = "facebookLike"; 


        public const string NA = "NA";

        public const string status_true = "true";
        public const string status_false = "false";

        public const string clientImageUrl = "http://i.imgur.com/Y5DauNCm.jpg";

        public const string currency_INR = "INR";
        public const string currency_Dollar = "Dollar";

        public const string userType_user = "user";
        public const string userType_client = "client";

        public const string questionTypeSAQ = "SAQ";
        public const string questionTypeMAQ = "MAQ";
        public const string questionTypeLAQ = "LAQ";
        public const string questionTypeTAQ = "TAQ";

        public const string demoUserEmail = "demo@demo.com";        

        public const int picPerUserImageModeration = 5;
    }
}