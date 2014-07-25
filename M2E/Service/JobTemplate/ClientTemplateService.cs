using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M2E.Models.DataResponse;
using M2E.Models;
using M2E.Common.Logger;
using M2E.CommonMethods;
using System.Reflection;
using System.Globalization;
using M2E.Models.DataWrapper.CreateTemplate;
using System.Data.Entity.Validation;
using M2E.Service.JobTemplate.CommonMethods;
using M2E.Session;

namespace M2E.Service.JobTemplate
{
    public class ClientTemplateService
    {
        private static readonly ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        private DbContextException _dbContextException = new DbContextException();
        private readonly M2EContext _db = new M2EContext();

        public ResponseModel<List<ClientTemplateResponse>> GetAllTemplateInformation(string username)
        {            
            var response = new ResponseModel<List<ClientTemplateResponse>>();
            var templateData = _db.CreateTemplateQuestionInfoes.OrderByDescending(x => x.creationTime).ToList();
            response.Status = 200;
            response.Message = "success";
            response.Payload = new List<ClientTemplateResponse>();
            try
            {
                foreach (var job in templateData)
                {
                    var clientTemplate = new ClientTemplateResponse
                    {
                        title = job.title,
                        creationDate = job.creationTime.Split(' ')[0],
                        showTime = " 4 hours",
                        editId = job.Id.ToString(CultureInfo.InvariantCulture),
                        showEllipse = true,
                        timeShowType = "success"
                    };
                    response.Payload.Add(clientTemplate);
                }
                
                return response;
            }
            catch (Exception)
            {
                response.Status = 500;//some error occured
                response.Message = "failed";
                return response;
            }
            
        }

        public ResponseModel<ClientTemplateDetailById> GetTemplateDetailById(string username,long id)
        {
            var response = new ResponseModel<ClientTemplateDetailById>();
            try
            {
                var templateData = _db.CreateTemplateQuestionInfoes.SingleOrDefault(x => x.Id == id && x.username == username);
                var createTemplateeditableInstructionsListsCreateResponse = _db.CreateTemplateeditableInstructionsLists.OrderBy(x => x.Id).Where(x=>x.referenceKey==templateData.referenceId && x.username == username).ToList();
                var createTemplateSingleQuestionsListsCreateResponse = _db.CreateTemplateSingleQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId && x.username == username).ToList();
                var createTemplateMultipleQuestionsListsCreateResponse = _db.CreateTemplateMultipleQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId && x.username == username).ToList();
                var createTemplateTextBoxQuestionsListsCreateResponse = _db.CreateTemplateTextBoxQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId && x.username == username).ToList();
                var createTemplateListBoxQuestionsListsCreateResponse = _db.CreateTemplateListBoxQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId && x.username == username).ToList();

                if (templateData != null && createTemplateListBoxQuestionsListsCreateResponse != null && createTemplateTextBoxQuestionsListsCreateResponse != null && createTemplateMultipleQuestionsListsCreateResponse != null && createTemplateSingleQuestionsListsCreateResponse != null && createTemplateeditableInstructionsListsCreateResponse != null)
                {
                    var createTemplateQuestionInfoModelEditableInstructionsCreateResponse = new CreateTemplateQuestionInfoModel();

                    createTemplateQuestionInfoModelEditableInstructionsCreateResponse.type = "AddInstructions";
                    createTemplateQuestionInfoModelEditableInstructionsCreateResponse.title = templateData.title;
                    createTemplateQuestionInfoModelEditableInstructionsCreateResponse.visible = false;
                    createTemplateQuestionInfoModelEditableInstructionsCreateResponse.buttonText = "Add Instructions";

                    createTemplateQuestionInfoModelEditableInstructionsCreateResponse.editableInstructionsList = new List<CreateTemplateeditableInstructionsListModel>();
                    foreach (var editableInstructionsLists in createTemplateeditableInstructionsListsCreateResponse)
                    {
                        var createTemplateeditableInstructionsListModelCreateResponse = new CreateTemplateeditableInstructionsListModel
                        {
                            Number = editableInstructionsLists.Number,
                            Text = editableInstructionsLists.Text
                        };
                        createTemplateQuestionInfoModelEditableInstructionsCreateResponse.editableInstructionsList.Add(createTemplateeditableInstructionsListModelCreateResponse);
                        createTemplateQuestionInfoModelEditableInstructionsCreateResponse.buttonText = "Remove Instructions";
                        createTemplateQuestionInfoModelEditableInstructionsCreateResponse.visible = true;
                    }

                    var createTemplateQuestionInfoModelSingleQuestionsCreateResponse = new CreateTemplateQuestionInfoModel();

                    createTemplateQuestionInfoModelSingleQuestionsCreateResponse.type = "AddSingleQuestionsList";
                    createTemplateQuestionInfoModelSingleQuestionsCreateResponse.title = templateData.title;
                    createTemplateQuestionInfoModelSingleQuestionsCreateResponse.visible = false;
                    createTemplateQuestionInfoModelSingleQuestionsCreateResponse.buttonText = "Add Ques. (single Ans.)";

                    createTemplateQuestionInfoModelSingleQuestionsCreateResponse.singleQuestionsList = new List<CreateTemplateSingleQuestionsListModel>();
                    foreach (var singleQuestionsLists in createTemplateSingleQuestionsListsCreateResponse)
                    {
                        var createTemplateSingleQuestionsListModelCreateResponse = new CreateTemplateSingleQuestionsListModel
                        {
                            Number = singleQuestionsLists.Number,
                            Question = singleQuestionsLists.Question,
                            Options = singleQuestionsLists.Options
                        };
                        createTemplateQuestionInfoModelSingleQuestionsCreateResponse.singleQuestionsList.Add(createTemplateSingleQuestionsListModelCreateResponse);
                        createTemplateQuestionInfoModelSingleQuestionsCreateResponse.visible = true;
                        createTemplateQuestionInfoModelSingleQuestionsCreateResponse.buttonText = "Remove Ques. (single Ans.)";
                    }

                    var createTemplateQuestionInfoModelMultipleQuestionsCreateResponse = new CreateTemplateQuestionInfoModel();

                    createTemplateQuestionInfoModelMultipleQuestionsCreateResponse.type = "AddMultipleQuestionsList";
                    createTemplateQuestionInfoModelMultipleQuestionsCreateResponse.title = templateData.title;
                    createTemplateQuestionInfoModelMultipleQuestionsCreateResponse.visible = false;
                    createTemplateQuestionInfoModelMultipleQuestionsCreateResponse.buttonText = "Add Ques. (Multiple Ans.)";
                    createTemplateQuestionInfoModelMultipleQuestionsCreateResponse.multipleQuestionsList = new List<CreateTemplateMultipleQuestionsListModel>();
                    foreach (var multipleQuestionsLists in createTemplateMultipleQuestionsListsCreateResponse)
                    {
                        var createTemplateMultipleQuestionsListModelCreateResponse = new CreateTemplateMultipleQuestionsListModel
                        {
                            Number = multipleQuestionsLists.Number,
                            Question = multipleQuestionsLists.Question,
                            Options = multipleQuestionsLists.Options
                        };
                        createTemplateQuestionInfoModelMultipleQuestionsCreateResponse.multipleQuestionsList.Add(createTemplateMultipleQuestionsListModelCreateResponse);
                        createTemplateQuestionInfoModelMultipleQuestionsCreateResponse.visible = true;
                        createTemplateQuestionInfoModelMultipleQuestionsCreateResponse.buttonText = "Remove Ques. (Multiple Ans.)";
                    }

                    var createTemplateQuestionInfoModelTextBoxQuestionsCreateResponse = new CreateTemplateQuestionInfoModel();

                    createTemplateQuestionInfoModelTextBoxQuestionsCreateResponse.type = "AddTextBoxQuestionsList";
                    createTemplateQuestionInfoModelTextBoxQuestionsCreateResponse.title = templateData.title;
                    createTemplateQuestionInfoModelTextBoxQuestionsCreateResponse.visible = false;
                    createTemplateQuestionInfoModelTextBoxQuestionsCreateResponse.buttonText = "Add Ques. (TextBox Ans.)";

                    createTemplateQuestionInfoModelTextBoxQuestionsCreateResponse.textBoxQuestionsList = new List<CreateTemplateTextBoxQuestionsListModel>();
                    foreach (var textBoxQuestionsLists in createTemplateTextBoxQuestionsListsCreateResponse)
                    {
                        var createTemplateTextBoxQuestionsListModelCreateResponse = new CreateTemplateTextBoxQuestionsListModel
                        {
                            Number = textBoxQuestionsLists.Number,
                            Question = textBoxQuestionsLists.Question,
                            Options = textBoxQuestionsLists.Options
                        };
                        createTemplateQuestionInfoModelTextBoxQuestionsCreateResponse.textBoxQuestionsList.Add(createTemplateTextBoxQuestionsListModelCreateResponse);
                        createTemplateQuestionInfoModelTextBoxQuestionsCreateResponse.visible = true;
                        createTemplateQuestionInfoModelTextBoxQuestionsCreateResponse.buttonText = "Remove Ques. (TextBox Ans.)";
                    }

                    var createTemplateQuestionInfoModelListBoxQuestionsCreateResponse = new CreateTemplateQuestionInfoModel();

                    createTemplateQuestionInfoModelListBoxQuestionsCreateResponse.type = "AddListBoxQuestionsList";
                    createTemplateQuestionInfoModelListBoxQuestionsCreateResponse.title = templateData.title;
                    createTemplateQuestionInfoModelListBoxQuestionsCreateResponse.visible = false;
                    createTemplateQuestionInfoModelListBoxQuestionsCreateResponse.buttonText = "Add Ques. (ListBox Ans.)";

                    createTemplateQuestionInfoModelListBoxQuestionsCreateResponse.listBoxQuestionsList = new List<CreateTemplateListBoxQuestionsListModel>();
                    foreach (var listBoxQuestionsLists in createTemplateListBoxQuestionsListsCreateResponse)
                    {
                        var createTemplateListBoxQuestionsListModelCreateResponse = new CreateTemplateListBoxQuestionsListModel
                        {
                            Number = listBoxQuestionsLists.Number,
                            Question = listBoxQuestionsLists.Question,
                            Options = listBoxQuestionsLists.Options
                        };
                        createTemplateQuestionInfoModelListBoxQuestionsCreateResponse.listBoxQuestionsList.Add(createTemplateListBoxQuestionsListModelCreateResponse);
                        createTemplateQuestionInfoModelListBoxQuestionsCreateResponse.visible = true;
                        createTemplateQuestionInfoModelListBoxQuestionsCreateResponse.buttonText = "Remove Ques. (ListBox Ans.)";
                    }


                    response.Status = 200;
                    response.Message = "success";
                    response.Payload = new ClientTemplateDetailById
                    {
                        Data = new List<CreateTemplateQuestionInfoModel>
                        {
                            createTemplateQuestionInfoModelEditableInstructionsCreateResponse,
                            createTemplateQuestionInfoModelSingleQuestionsCreateResponse,
                            createTemplateQuestionInfoModelMultipleQuestionsCreateResponse,
                            createTemplateQuestionInfoModelTextBoxQuestionsCreateResponse,
                            createTemplateQuestionInfoModelListBoxQuestionsCreateResponse
                        }
                    };
                }
                else
                {
                    response.Status = 404;
                    response.Message = "No Data found";
                }
                return response;
            }
            catch (Exception)
            {
                response.Status = 500;
                response.Message = "Exception";
                return response;
            }
        }

        public ResponseModel<List<ImgurImageResponse>> GetTemplateImageDetailById(string username, long id)
        {
            var response = new ResponseModel<List<ImgurImageResponse>>();
            try
            {
                var refKey = username + id;
                var createTemplateImagesListsCreateResponse = _db.CreateTemplateImgurImagesLists.OrderBy(x => x.Id).Where(x => x.referenceKey == refKey && x.username == username).ToList();

                if (createTemplateImagesListsCreateResponse != null)
                {
                    var imgurImageList = new List<ImgurImageResponse>();
                    foreach (var createTemplateImageCreateResponse in createTemplateImagesListsCreateResponse)
                    {
                        var imgurImage = new ImgurImageResponse();
                        imgurImage.data = new imgurData();
                        imgurImage.data.id = createTemplateImageCreateResponse.Id.ToString();                        
                        imgurImage.data.deletehash = createTemplateImageCreateResponse.imgurDeleteHash;
                        imgurImage.data.link = createTemplateImageCreateResponse.imgurLink;
                        imgurImage.data.link_s = createTemplateImageCreateResponse.imgurLink.Split('/')[0] + "//" + createTemplateImageCreateResponse.imgurLink.Split('/')[2] + "/" + createTemplateImageCreateResponse.imgurLink.Split('/')[3].Split('.')[0] + 's' + "." + createTemplateImageCreateResponse.imgurLink.Split('/')[3].Split('.')[1];
                        imgurImage.data.link_m = createTemplateImageCreateResponse.imgurLink.Split('/')[0] + "//" + createTemplateImageCreateResponse.imgurLink.Split('/')[2] + "/" + createTemplateImageCreateResponse.imgurLink.Split('/')[3].Split('.')[0] + 'm' + "." + createTemplateImageCreateResponse.imgurLink.Split('/')[3].Split('.')[1];
                        imgurImage.data.link_l = createTemplateImageCreateResponse.imgurLink.Split('/')[0] + "//" + createTemplateImageCreateResponse.imgurLink.Split('/')[2] + "/" + createTemplateImageCreateResponse.imgurLink.Split('/')[3].Split('.')[0] + 'l' + "." + createTemplateImageCreateResponse.imgurLink.Split('/')[3].Split('.')[1];
                        imgurImage.data.copyText ="";
                        imgurImageList.Add(imgurImage);
                    }
                    response.Status = 200;
                    response.Message = "success";
                    response.Payload = imgurImageList;
                }
                else
                {
                    response.Status = 404;
                    response.Message = "No Data found";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Exception";
                return response;
            }
        }

        public ResponseModel<string> CreateTemplate(List<CreateTemplateQuestionInfoModel> req,string username)
        {
            var response = new ResponseModel<string>();

            var keyInfo = _db.CreateTemplateQuestionInfoes.FirstOrDefault();
            var refKey = username;
            var digitKey = 0;
            if (keyInfo != null)
            {
                digitKey = _db.CreateTemplateQuestionInfoes.Max(x => x.Id) + 1;                
            }
            else
            {
                digitKey = 1;                
            }
            refKey += digitKey;
            var createTemplateQuestionsInfoInsert = new CreateTemplateQuestionInfo
            {
                buttonText = "NA",
                username = username,
                title = req[0].title,
                visible = "NA",
                type = "NA",
                creationTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                referenceId = refKey,
                total = "NA",
                completed = "NA",
                verified = "NA"
            };

            _db.CreateTemplateQuestionInfoes.Add(createTemplateQuestionsInfoInsert);
            
            try
            {
                _db.SaveChanges();
                CreateSubTemplateByRefKey CreateSubTemplateByRefKey = new CreateSubTemplateByRefKey();
                CreateSubTemplateByRefKey.CreateSubTemplateByRefKeyService(req, username, refKey);                
                response.Status = 200;
                response.Message = "success-"+digitKey;
                response.Payload = refKey;
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

        public ResponseModel<string> CreateTemplateWithId(List<CreateTemplateQuestionInfoModel> req, string username,string id)
        {
            var response = new ResponseModel<string>();
            
            var refKey = username+id;
            
            var createTemplateQuestionsInfoInsert = new CreateTemplateQuestionInfo
            {
                buttonText = "NA",
                username = username,
                title = req[0].title,
                visible = "NA",
                type = "NA",
                creationTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                referenceId = refKey,
                total = "NA",
                completed = "NA",
                verified = "NA"
            };

            _db.CreateTemplateQuestionInfoes.Add(createTemplateQuestionsInfoInsert);

            try
            {
                _db.SaveChanges();
                CreateSubTemplateByRefKey CreateSubTemplateByRefKey = new CreateSubTemplateByRefKey();
                CreateSubTemplateByRefKey.CreateSubTemplateByRefKeyService(req, username, refKey);
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

        public ResponseModel<string> DeleteTemplateDetailById(string username, long id)
        {
            var response = new ResponseModel<string>();
            try
            {
                var templateData = _db.CreateTemplateQuestionInfoes.SingleOrDefault(x => x.Id == id && x.username == username);
                var createTemplateeditableInstructionsListsCreateResponse = _db.CreateTemplateeditableInstructionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId && x.username == username).ToList();
                var createTemplateSingleQuestionsListsCreateResponse = _db.CreateTemplateSingleQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId && x.username == username).ToList();
                var createTemplateMultipleQuestionsListsCreateResponse = _db.CreateTemplateMultipleQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId && x.username == username).ToList();
                var createTemplateTextBoxQuestionsListsCreateResponse = _db.CreateTemplateTextBoxQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId && x.username == username).ToList();
                var createTemplateListBoxQuestionsListsCreateResponse = _db.CreateTemplateListBoxQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId && x.username == username).ToList();
                var createTemplateImgurImagesListsCreateResponse = _db.CreateTemplateImgurImagesLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId && x.username == username).ToList();

                if (templateData != null)
                    _db.CreateTemplateQuestionInfoes.Remove(templateData);
                if (createTemplateImgurImagesListsCreateResponse != null)
                {
                    foreach (var createTemplateImgurImageCreateResponse in createTemplateImgurImagesListsCreateResponse)
                    {
                        _db.CreateTemplateImgurImagesLists.Remove(createTemplateImgurImageCreateResponse);
                    }
                }

                if (createTemplateeditableInstructionsListsCreateResponse != null)
                {
                    foreach (var createTemplateeditableInstructionCreateResponse in createTemplateeditableInstructionsListsCreateResponse)
                    {
                        _db.CreateTemplateeditableInstructionsLists.Remove(createTemplateeditableInstructionCreateResponse);
                    }                    
                }

                if (createTemplateSingleQuestionsListsCreateResponse != null)
                {
                    foreach (var createTemplateSingleQuestionCreateResponse in createTemplateSingleQuestionsListsCreateResponse)
                    {
                        _db.CreateTemplateSingleQuestionsLists.Remove(createTemplateSingleQuestionCreateResponse);
                    }
                }

                if (createTemplateMultipleQuestionsListsCreateResponse != null)
                {
                    foreach (var createTemplateMultipleQuestionCreateResponse in createTemplateMultipleQuestionsListsCreateResponse)
                    {
                        _db.CreateTemplateMultipleQuestionsLists.Remove(createTemplateMultipleQuestionCreateResponse);
                    }
                }

                if (createTemplateTextBoxQuestionsListsCreateResponse != null)
                {
                    foreach (var createTemplateTextBoxQuestionCreateResponse in createTemplateTextBoxQuestionsListsCreateResponse)
                    {
                        _db.CreateTemplateTextBoxQuestionsLists.Remove(createTemplateTextBoxQuestionCreateResponse);
                    }
                }

                if (createTemplateListBoxQuestionsListsCreateResponse != null)
                {
                    foreach (var createTemplateListBoxQuestionCreateResponse in createTemplateListBoxQuestionsListsCreateResponse)
                    {
                        _db.CreateTemplateListBoxQuestionsLists.Remove(createTemplateListBoxQuestionCreateResponse);
                    }
                }
                   

                try
                {
                    _db.SaveChanges();
                    response.Status = 200;
                    response.Message = "Success";
                    response.Payload = "Successfully Deleted";
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
            catch (Exception)
            {
                response.Status = 500;
                response.Message = "Exception";
                return response;
            }
        }

        public ResponseModel<string> DeleteTemplateImgurImageById(string username, long id)
        {
            var response = new ResponseModel<string>();
            try
            {                
                var createTemplateImgurImagesListsCreateResponse = _db.CreateTemplateImgurImagesLists.OrderBy(x => x.Id).Where(x => x.Id == id && x.username == username).ToList();
                
                if (createTemplateImgurImagesListsCreateResponse != null)
                {
                    foreach (var createTemplateImgurImageCreateResponse in createTemplateImgurImagesListsCreateResponse)
                    {
                        _db.CreateTemplateImgurImagesLists.Remove(createTemplateImgurImageCreateResponse);
                    }
                }
                
                try
                {
                    _db.SaveChanges();
                    response.Status = 200;
                    response.Message = "Success";
                    response.Payload = "Successfully Deleted";
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
            catch (Exception)
            {
                response.Status = 500;
                response.Message = "Exception";
                return response;
            }
        }

        public ResponseModel<string> EditTemplateDetailById(List<CreateTemplateQuestionInfoModel> req,string username, long id)
        {
            var response = new ResponseModel<string>();
            try
            {
                var templateData = _db.CreateTemplateQuestionInfoes.SingleOrDefault(x => x.Id == id && x.username == username);
                var createTemplateeditableInstructionsListsCreateResponse = _db.CreateTemplateeditableInstructionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId && x.username == username).ToList();
                var createTemplateSingleQuestionsListsCreateResponse = _db.CreateTemplateSingleQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId && x.username == username).ToList();
                var createTemplateMultipleQuestionsListsCreateResponse = _db.CreateTemplateMultipleQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId && x.username == username).ToList();
                var createTemplateTextBoxQuestionsListsCreateResponse = _db.CreateTemplateTextBoxQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId && x.username == username).ToList();
                var createTemplateListBoxQuestionsListsCreateResponse = _db.CreateTemplateListBoxQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId && x.username == username).ToList();
                
                if (createTemplateeditableInstructionsListsCreateResponse != null)
                {
                    foreach (var createTemplateeditableInstructionCreateResponse in createTemplateeditableInstructionsListsCreateResponse)
                    {
                        _db.CreateTemplateeditableInstructionsLists.Remove(createTemplateeditableInstructionCreateResponse);
                    }
                }

                if (createTemplateSingleQuestionsListsCreateResponse != null)
                {
                    foreach (var createTemplateSingleQuestionCreateResponse in createTemplateSingleQuestionsListsCreateResponse)
                    {
                        _db.CreateTemplateSingleQuestionsLists.Remove(createTemplateSingleQuestionCreateResponse);
                    }
                }

                if (createTemplateMultipleQuestionsListsCreateResponse != null)
                {
                    foreach (var createTemplateMultipleQuestionCreateResponse in createTemplateMultipleQuestionsListsCreateResponse)
                    {
                        _db.CreateTemplateMultipleQuestionsLists.Remove(createTemplateMultipleQuestionCreateResponse);
                    }
                }

                if (createTemplateTextBoxQuestionsListsCreateResponse != null)
                {
                    foreach (var createTemplateTextBoxQuestionCreateResponse in createTemplateTextBoxQuestionsListsCreateResponse)
                    {
                        _db.CreateTemplateTextBoxQuestionsLists.Remove(createTemplateTextBoxQuestionCreateResponse);
                    }
                }

                if (createTemplateListBoxQuestionsListsCreateResponse != null)
                {
                    foreach (var createTemplateListBoxQuestionCreateResponse in createTemplateListBoxQuestionsListsCreateResponse)
                    {
                        _db.CreateTemplateListBoxQuestionsLists.Remove(createTemplateListBoxQuestionCreateResponse);
                    }
                }

                
                try
                {
                    templateData.title = req[0].title;
                    _db.SaveChanges();
                    CreateSubTemplateByRefKey CreateSubTemplateByRefKey = new CreateSubTemplateByRefKey();
                    CreateSubTemplateByRefKey.CreateSubTemplateByRefKeyService(req, username, templateData.referenceId);

                    response.Status = 200;
                    response.Message = "Success";
                    response.Payload = "Successfully Edited";
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
            catch (Exception)
            {
                response.Status = 500;
                response.Message = "Exception";
                return response;
            }
        }

        public ResponseModel<string> ImgurImagesSaveToDatabaseWithTemplateId(List<imgurUploadImageResponse> ImgurList, string username, string id)
        {
            var response = new ResponseModel<string>();

            var refKey = id;
            foreach (var imageInfo in ImgurList)
            {
                var CreateTemplateImgurImagesListInsert = new CreateTemplateImgurImagesList
                {
                    assignedTo = "NA",
                    assignTime = "NA",
                    completedAt = "NA",
                    referenceKey = refKey,
                    status = "open",
                    username = username,
                    verified = "No",
                    imgurId = imageInfo.data.id,
                    imgurDeleteHash = imageInfo.data.deletehash,
                    imgurLink = imageInfo.data.link
                };

                _db.CreateTemplateImgurImagesLists.Add(CreateTemplateImgurImagesListInsert);
            }            
            try
            {
                _db.SaveChanges();                
                response.Status = 200;
                response.Message = "Success";
                response.Payload = "Successfully Added";
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