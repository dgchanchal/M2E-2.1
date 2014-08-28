using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M2E.Common.Logger;
using M2E.CommonMethods;
using M2E.Models;
using System.Reflection;
using System.Globalization;

namespace M2E.DAO
{
    public class ProjectDAO
    {
        private static readonly ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        private DbContextException _dbContextException = new DbContextException();
        private readonly M2EContext _db = new M2EContext();

        public string totalAvailableProjects()
        {
            return (_db.CreateTemplateQuestionInfoes.Count() + _db.CreateTemplateFacebookLikes.Count()).ToString(CultureInfo.InvariantCulture);
        }
    }
}