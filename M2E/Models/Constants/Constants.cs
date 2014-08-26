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
        public const string type_dataEntry = "dataEntry";
        public const string subType_Transcription = "Transcription";
        public const string type_moderation = "moderation";
        public const string subType_moderatingPhotos = "moderatingPhotos";
        public const string NA = "NA";

        public const string questionTypeSAQ = "SAQ";
        public const string questionTypeMAQ = "MAQ";
        public const string questionTypeLAQ = "LAQ";
        public const string questionTypeTAQ = "TAQ";
    }
}