using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M2E.Models.DataWrapper.CreateTemplate;
using M2E.Models.DataResponse;

namespace M2E.Models.DataWrapper
{
    public class CreateTemplateRequest
    {
        public List<CreateTemplateQuestionInfoModel> Data { get; set; }
        public List<imgurUploadImageResponse> ImgurList { get; set; }
        public TemplateInfoModel TemplateInfo { get; set; }
    }    
}