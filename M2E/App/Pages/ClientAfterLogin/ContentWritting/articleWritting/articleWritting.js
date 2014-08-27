'use strict';
define([appLocation.postLogin], function (app) {

    app.controller('ClientAfterLoginArticleWrittingTemplate', function ($scope, $http, $rootScope, $routeParams, CookieUtil) {
        $('title').html("sample page"); //TODO: change the title so cann't be tracked in log

        var editableInstructions = "";
        var totalQuestionSingleAnswerHtmlData = "";
        var totalQuestionMultipleAnswerHtmlData = "";
        var totalQuestionTextBoxAnswerHtmlData = "";
        var totalQuestionListBoxAnswerHtmlData = "";
        var totalEditableInstruction = 0;
        var totalSingleQuestionList = 0;
        var totalMultipleQuestionList = 0;
        var totalTextBoxQuestionList = 0;
        var totalListBoxQuestionList = 0;
        userSession.wysiHtml5UploadedInstructionsImageUrlLink = [];
        userSession.imgurImageTranscriptionTemplate = [];
        $scope.imgurImageTranscriptionTemplate = userSession.imgurImageTranscriptionTemplate;
        $scope.jobTemplate = [
                { type: "AddInstructions", title: "", visible: true, buttonText: "Add Instructions", editableInstructionsList: [{ Number: totalEditableInstruction, Text: "Instruction 1"}] },
                { type: "AddSingleQuestionsList", title: "", visible: false, buttonText: "Add Query", singleQuestionsList: [{ Number: totalSingleQuestionList, Question: "Is this Image Obscene?", Options: "Yes;No"}] },
                { type: "AddMultipleQuestionsList", title: "", visible: false, buttonText: "Add Ques. (Multiple Ans.)", multipleQuestionsList: [{ Number: totalMultipleQuestionList, Question: "What is your multiple gender ?", Options: "Malem1;Femalem2"}] },
                { type: "AddTextBoxQuestionsList", title: "", visible: true, buttonText: "Add Ques. (TextBox Ans.)", textBoxQuestionsList: [{ Number: totalTextBoxQuestionList, Question: "This is the description of the question", Options: "text"}] },
                { type: "AddListBoxQuestionsList", title: "", visible: false, buttonText: "Add Ques. (ListBox Ans.)", listBoxQuestionsList: [{ Number: totalListBoxQuestionList, Question: "What is your multiple gender ?", Options: "Malem1;Femalem2"}] }
        ];

        loadTemplate();

        function loadTemplate() {
            $('#createTemplateTitleTextTranscriptionTemplate').val($scope.jobTemplate[0].title);
            $.each($scope.jobTemplate[0].editableInstructionsList, function () {
                editableInstructions += "<li>";
                editableInstructions += this.Text + "&nbsp;&nbsp<a style='cursor:pointer' class='addInstructionClass' id='" + this.Number + "'><i class='fa fa-times'></i></a>";
                editableInstructions += "</li>";
            });



            $('#editableInstructionsListIDTranscriptionTemplate').html(editableInstructions);
            $('#addTextBoxAnswerQuestionIDArticleWritingTemplate').html($scope.jobTemplate[3].textBoxQuestionsList[0].Question);

            $('#editableInstructionsListIDTranscriptionTemplatePreview').html(editableInstructions);
            $('#TextBoxQuestionTextBoxQuestionDataArticleWritingTemplatePreviewDescription').html($scope.jobTemplate[3].textBoxQuestionsList[0].Question);
            initAddInstructionClass();
            initAddQuestionTextBoxAnswerClass();
        }

        $scope.refreshTranscriptionTemplateListDiv = function () {
            //alert("inside function");
            $scope.imgurImageTranscriptionTemplate = userSession.imgurImageTranscriptionTemplate;
            updateTotalPayableAmountDiv();
            $('.fancybox').fancybox();
        }

        $scope.DeleteEditImgurImageByIdFunction = function (id) {
            var i;
            for (i = 0; i < userSession.imgurImageTranscriptionTemplate.length; i++) {
                if (userSession.imgurImageTranscriptionTemplate[i].data.id == id) {
                    break;
                }
            }
            //console.log(userSession.imgurImageTemplateModeratingPhotos);

            userSession.imgurImageTranscriptionTemplate.splice(i, 1);

            //console.log(userSession.imgurImageTemplateModeratingPhotos);
            $scope.imgurImageTranscriptionTemplate = userSession.imgurImageTranscriptionTemplate;
            updateTotalPayableAmountDiv();
            $('.fancybox').fancybox();
        }

        function updateTotalPayableAmountDiv() {
            $('#totalNumberOfThreads').val(userSession.imgurImageTranscriptionTemplate.length);
            $("#TotalAmountPayableCreateTemplate").html($("#amountPerThreadTextBoxInput").val() * $('#totalNumberOfThreads').val());
        }
        $scope.addEditableInstructions = function () {
            if (($('#AddInstructionsTextAreaTranscriptionTemplate').val() != "") && ($('#AddInstructionsTextAreaTranscriptionTemplate').val() != null)) {
                var addEditableInstructionsFancyBoxInImages = $('#AddInstructionsTextAreaTranscriptionTemplate').val();
                //$('#AddInstructionsTextAreaModeratingPhotos').val('test');
                //console.log(addFancyBoxInImages);
                var i = 0;
                $.each(userSession.wysiHtml5UploadedInstructionsImageUrlLink, function () {

                    addEditableInstructionsFancyBoxInImages = replaceImageWithFancyBoxImage(addEditableInstructionsFancyBoxInImages, userSession.wysiHtml5UploadedInstructionsImageUrlLink[i].link_s, userSession.wysiHtml5UploadedInstructionsImageUrlLink[i].link);
                    i++;
                });

                //console.log(addFancyBoxInImages);
                totalEditableInstruction = totalEditableInstruction + 1;
                var editableInstructionDataToBeAdded = { Number: totalEditableInstruction, Text: addEditableInstructionsFancyBoxInImages };
                $scope.jobTemplate[0].editableInstructionsList.push(editableInstructionDataToBeAdded);

                $('#AddInstructionsTextAreaTranscriptionTemplate').data("wysihtml5").editor.clear();
                refreshInstructionList();
                //userSession.wysiHtml5UploadedInstructionsImageUrlLink = [];                
                //$('#AddInstructionsTextArea').val(''); // TODO: clearing the text area not working
            } else {
                showToastMessage("Warning", "Instruction Text Box cann't be empty");
            }

        }

        // textbox questions..
        $scope.InsertTextBoxQuestionRow = function () {
            if (($('#TextBoxQuestionTextBoxQuestionDataArticleWritingTemplate').val() != "") && ($('#TextBoxQuestionTextBoxQuestionDataArticleWritingTemplate').val() != null)) {
                var addTextBoxQuestionFancyBoxInImages = $('#TextBoxQuestionTextBoxQuestionDataArticleWritingTemplate').val();

                var textBoxQuestionsList = { Number: 0, Question: addTextBoxQuestionFancyBoxInImages, Options: "text" };
                $scope.jobTemplate[3].textBoxQuestionsList[0] = (textBoxQuestionsList);
                console.log($scope.jobTemplate[3].textBoxQuestionsList[0]);
                //            $('#TextBoxQuestionTextBoxQuestionData').data("wysihtml5").editor.clear();
                refreshTextBoxQuestionsList();
            } else {
                showToastMessage("Warning", "Instruction Text Box cann't be empty");
            }
        }

        $scope.addInstructionsRow = function () {
            if ($scope.jobTemplate[0].visible == true) {
                $scope.jobTemplate[0].buttonText = "Add Instructions";
                $scope.jobTemplate[0].visible = false;
            } else {
                $scope.jobTemplate[0].visible = true;
                $scope.jobTemplate[0].buttonText = "Remove Instructions";
            }
        }


        function initAddInstructionClass() {
            $('.addInstructionClass').click(function () {
                var i;
                for (i = 0; i < $scope.jobTemplate[0].editableInstructionsList.length; i++) {
                    if ($scope.jobTemplate[0].editableInstructionsList[i].Number == this.id) {
                        break;
                    }
                }
                $scope.jobTemplate[0].editableInstructionsList.splice(i, 1);
                refreshInstructionList();
            });
        }

        function initAddQuestionTextBoxAnswerClass() {
            $('.addQuestionTextBoxAnswerClass').click(function () {
                var i;
                for (i = 0; i < $scope.jobTemplate[3].textBoxQuestionsList.length; i++) {
                    if ($scope.jobTemplate[3].textBoxQuestionsList[i].Number == this.id) {
                        break;
                    }
                }
                $scope.jobTemplate[3].textBoxQuestionsList.splice(i, 1);
                refreshTextBoxQuestionsList();
            });
        }

        function refreshInstructionList() {
            editableInstructions = "";
            $.each($scope.jobTemplate[0].editableInstructionsList, function () {
                editableInstructions += "<li>";
                editableInstructions += this.Text + "&nbsp;&nbsp<a style='cursor:pointer' class='addInstructionClass' id='" + this.Number + "'><i class='fa fa-times'></i></a>";
                editableInstructions += "</li>";
            });
            $('#editableInstructionsListIDTranscriptionTemplate').html(editableInstructions);
            $('#editableInstructionsListIDTranscriptionTemplatePreview').html(editableInstructions);
            initAddInstructionClass();
            //$('#addInstructionCloseButtonModeratingPhotos').click();
            $('.fancybox').fancybox();
        }

        function refreshTextBoxQuestionsList() {

            $('#addTextBoxAnswerQuestionIDArticleWritingTemplate').html($scope.jobTemplate[3].textBoxQuestionsList[0].Question);
            $('#TextBoxQuestionTextBoxQuestionDataArticleWritingTemplatePreviewDescription').html($scope.jobTemplate[3].textBoxQuestionsList[0].Question);
            initAddQuestionTextBoxAnswerClass();
            //$('#addQuestionTextBoxAnswerCloseButton').click();
            //$('.fancybox').fancybox();

        }


        $scope.ClientCreateArticleWritingTemplateFunction = function () {
            $scope.jobTemplate[0].title = $('#createTemplateTitleTextArticleWritingTemplate').val();
            //console.log(userSession.imgurImageTranscriptionTemplate);
            var clientCreateTranscriptionTemplateData = { Data: $scope.jobTemplate, TemplateInfo: { type: TemplateInfoModel.type_contentWritting, subType: TemplateInfoModel.subType_articleWritting, description: $('#createTemplateDescriptionText').val(), totalThreads: $("#totalNumberOfThreads").val(), amountEachThread: $("#amountPerThreadTextBoxInput").val()} };
            //var currentTemplateId = new Date().getTime();

            var url = ServerContextPah + '/Client/CreateTemplate';
            var headers = {
                'Content-Type': 'application/json',
                'UTMZT': CookieUtil.getUTMZT(),
                'UTMZK': CookieUtil.getUTMZK(),
                'UTMZV': CookieUtil.getUTMZV()
            };

            var isValidAmountPerThreadTextBoxInput = ($('#amountPerThreadTextBoxInput').val() != "") && $('#amountPerThreadTextBoxInput').val() >= 0.03;
            var isValidTotalNumberOfThreads = ($('#totalNumberOfThreads').val() != "") && $('#totalNumberOfThreads').val() >= 1;
            if (!isValidAmountPerThreadTextBoxInput || !isValidTotalNumberOfThreads) {
                showToastMessage("Error", "Some Fields are invalid !!! Min Amount Payable to each workforce is $0.03 <br/> Min Thread Allowed is 1");
            }
            else if (($('#createTemplateTitleTextArticleWritingTemplate').val() != "") && ($('#createTemplateTitleTextArticleWritingTemplate').val() != null)) {
                startBlockUI('wait..', 3);
                $http({
                    url: url,
                    method: "POST",
                    data: clientCreateTranscriptionTemplateData,
                    headers: headers
                }).success(function (data, status, headers, config) {
                    //$scope.persons = data; // assign  $scope.persons here as promise is resolved here
                    stopBlockUI();
                    userSession.imgurImageTranscriptionTemplate = [];
                    //var id = data.Message.split('-')[1];
                    //location.href = "#/editTemplate/edit/" + id;
                    showToastMessage("Success", "Successfully Created");
                    location.href = "/client#/";
                }).error(function (data, status, headers, config) {

                });
            }
            else {
                showToastMessage("Error", "Title of the Template cann't be empty");
            }

        }

        $scope.convertAllWysiHtml5ImagesToFancyBoxLink = function () {
            textToReplace = "src=\"../../Upload/Images/Tulips.jpg\" title=\"Image: ../../Upload/Images/Tulips.jpg\"";

        }

    });

});
