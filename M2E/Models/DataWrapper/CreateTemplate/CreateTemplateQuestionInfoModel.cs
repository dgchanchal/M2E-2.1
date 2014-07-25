using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataWrapper.CreateTemplate
{    
    public class CreateTemplateQuestionInfoModel
    {
        public string type { get; set; }
        public bool visible {get;set;}
        public string buttonText {get;set;}
        public string title { get; set; }
        public List<CreateTemplateeditableInstructionsListModel> editableInstructionsList { get; set; }
        public List<CreateTemplateTextBoxQuestionsListModel> textBoxQuestionsList { get; set; }
        public List<CreateTemplateSingleQuestionsListModel> singleQuestionsList { get; set; }
        public List<CreateTemplateMultipleQuestionsListModel> multipleQuestionsList { get; set; }
        public List<CreateTemplateListBoxQuestionsListModel> listBoxQuestionsList { get; set; }
        
    }
}