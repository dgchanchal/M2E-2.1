using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataResponse
{
    public class UserReferenceDetailResponse
    {
        public string myReferralLink { get; set; }
        public List<UserReferenceDetails> myReferenceList { get; set; }
    }
}