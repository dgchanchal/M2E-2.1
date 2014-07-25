using System;
using System.Collections.Generic;
using M2E.Models;
using M2E.Models.DataWrapper.CreateTemplate;
using M2E.Common.Logger;
using M2E.CommonMethods;
using System.Reflection;
using System.Globalization;
using System.Data.Entity.Validation;

namespace M2E.Service.JobTemplate.CommonMethods
{
    public class CreateSubTemplateByRefKey
    {
        private static readonly ILogger Logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        private DbContextException _dbContextException = new DbContextException();
        private readonly M2EContext _db = new M2EContext();

        public ResponseModel<string> CreateSubTemplateByRefKeyService(List<CreateTemplateQuestionInfoModel> req, string username,string refKey)
        {
            var response = new ResponseModel<string>();

            foreach (var templateQuestions in req)
            {
                if (templateQuestions.visible == false)
                    continue;
                switch (templateQuestions.type)
                {
                    case "AddInstructions":
                        foreach (var instructionsList in templateQuestions.editableInstructionsList)
                        {
                            var createTemplateeditableInstructionsListInsert = new CreateTemplateeditableInstructionsList
                            {
                                username = username,
                                Number = instructionsList.Number,
                                Text = instructionsList.Text,
                                assignTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                                assignedTo = "NA",
                                completedAt = "NA",
                                referenceKey = refKey,
                                status = "Open",
                                verified = "NA"
                            };
                            _db.CreateTemplateeditableInstructionsLists.Add(createTemplateeditableInstructionsListInsert);
                        }
                        break;
                    case "AddSingleQuestionsList":
                        foreach (var singleQuestionList in templateQuestions.singleQuestionsList)
                        {
                            var createTemplateSingleQuestionsListInsert = new CreateTemplateSingleQuestionsList
                            {
                                username = username,
                                Number = singleQuestionList.Number,
                                Question = singleQuestionList.Question,
                                Options = singleQuestionList.Options,
                                assignTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                                assignedTo = "NA",
                                completedAt = "NA",
                                referenceKey = refKey,
                                status = "Open",
                                verified = "NA"
                            };
                            _db.CreateTemplateSingleQuestionsLists.Add(createTemplateSingleQuestionsListInsert);
                        }
                        break;
                    case "AddMultipleQuestionsList":
                        foreach (var multipleQuestionsList in templateQuestions.multipleQuestionsList)
                        {
                            var createTemplateMultipleQuestionsListInsert = new CreateTemplateMultipleQuestionsList
                            {
                                username = username,
                                Number = multipleQuestionsList.Number,
                                Question = multipleQuestionsList.Question,
                                Options = multipleQuestionsList.Options,
                                assignTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                                assignedTo = "NA",
                                completedAt = "NA",
                                referenceKey = refKey,
                                status = "Open",
                                verified = "NA"
                            };
                            _db.CreateTemplateMultipleQuestionsLists.Add(createTemplateMultipleQuestionsListInsert);
                        }
                        break;
                    case "AddTextBoxQuestionsList":
                        foreach (var textBoxQuestionsList in templateQuestions.textBoxQuestionsList)
                        {
                            var createTemplateTextBoxQuestionsListInsert = new CreateTemplateTextBoxQuestionsList
                            {
                                username = username,
                                Number = textBoxQuestionsList.Number,
                                Question = textBoxQuestionsList.Question,
                                Options = textBoxQuestionsList.Options,
                                assignTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                                assignedTo = "NA",
                                completedAt = "NA",
                                referenceKey = refKey,
                                status = "Open",
                                verified = "NA"
                            };
                            _db.CreateTemplateTextBoxQuestionsLists.Add(createTemplateTextBoxQuestionsListInsert);
                        }
                        break;
                    case "AddListBoxQuestionsList":
                        foreach (var listBoxQuestionsList in templateQuestions.listBoxQuestionsList)
                        {
                            var createTemplateListBoxQuestionsListInsert = new CreateTemplateListBoxQuestionsList
                            {
                                username = username,
                                Number = listBoxQuestionsList.Number,
                                Question = listBoxQuestionsList.Question,
                                Options = listBoxQuestionsList.Options,
                                assignTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                                assignedTo = "NA",
                                completedAt = "NA",
                                referenceKey = refKey,
                                status = "Open",
                                verified = "NA"
                            };
                            _db.CreateTemplateListBoxQuestionsLists.Add(createTemplateListBoxQuestionsListInsert);
                        }
                        break;
                }
            }
            
            try
            {
                _db.SaveChanges();
                response.Status = 200;
                response.Message = "Success";
                response.Payload = "Successfully Created";
            }
            catch (DbEntityValidationException e)
            {
                DbContextException.LogDbContextException(e);
                response.Status = 500;
                response.Message = "Failed";
                response.Payload = "Exception Occured";
            }
            return response;
        }
    }
}