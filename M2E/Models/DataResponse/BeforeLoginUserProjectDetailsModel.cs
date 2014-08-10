using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataResponse
{
    public class BeforeLoginUserProjectDetailsModel
    {
        public string TotalProjects { get; set; }
        public string SuccessRate { get; set; }
        public string TotalUsers { get; set; }
        public string ProjectCategories { get; set; }
    }
}