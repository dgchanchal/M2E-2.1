using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M2E.Models.DataWrapper.CreateTemplate;

namespace M2E.Models.DataResponse
{
    public class ClientTemplateDetailById
    {
        public List<CreateTemplateQuestionInfoModel> Data { get; set; }
    }
}