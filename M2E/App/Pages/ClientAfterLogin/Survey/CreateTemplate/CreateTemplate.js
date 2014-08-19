'use strict';
define([appLocation.postLogin], function (app) {

    //getting user info..
    app.controller('createTemplateController', function ($scope, $http, $rootScope, $routeParams, CookieUtil) {
        //alert("create product controller");    
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

        $rootScope.jobTemplate = [
                { type: "AddInstructions", title: "", visible: false, buttonText: "Add Instructions", editableInstructionsList: [{ Number: totalEditableInstruction, Text: "Instruction 1"}] },
                { type: "AddSingleQuestionsList", title: "", visible: false, buttonText: "Add Ques. (single Ans.)", singleQuestionsList: [{ Number: totalSingleQuestionList, Question: "What is your gender ?", Options: "Male1;Female2"}] },
                { type: "AddMultipleQuestionsList", title: "", visible: false, buttonText: "Add Ques. (Multiple Ans.)", multipleQuestionsList: [{ Number: totalMultipleQuestionList, Question: "What is your multiple gender ?", Options: "Malem1;Femalem2"}] },
                { type: "AddTextBoxQuestionsList", title: "", visible: false, buttonText: "Add Ques. (TextBox Ans.)", textBoxQuestionsList: [{ Number: totalTextBoxQuestionList, Question: "Who won 2014 FIFA World cup ?", Options: "text"}] },
                { type: "AddListBoxQuestionsList", title: "", visible: false, buttonText: "Add Ques. (ListBox Ans.)", listBoxQuestionsList: [{ Number: totalListBoxQuestionList, Question: "What is your multiple gender ?", Options: "Malem1;Femalem2"}] }
        ];
        userSession.wysiHtml5UploadedInstructionsImageUrlLink = [];
        loadTemplate();


        function loadTemplate() {
            $('#createTemplateTitleText').val($rootScope.jobTemplate[0].title);
            $.each($rootScope.jobTemplate[0].editableInstructionsList, function () {
                editableInstructions += "<li>";
                editableInstructions += this.Text + "&nbsp;&nbsp<a style='cursor:pointer' class='addInstructionClass' id='" + this.Number + "'><i class='fa fa-times'></i></a>";
                editableInstructions += "</li>";
            });

            var quesCount = 1;
            $.each($rootScope.jobTemplate[1].singleQuestionsList, function () {

                totalQuestionSingleAnswerHtmlData += "<fieldset>";

                totalQuestionSingleAnswerHtmlData += "<label>";
                totalQuestionSingleAnswerHtmlData += "<b>" + quesCount + ". " + this.Question + "</b><a style='cursor:pointer' class='addQuestionSingleAnswerClass' id='" + this.Number + "'><i class='fa fa-times'></i></a>";
                totalQuestionSingleAnswerHtmlData += "</label>";

                var singleQuestionsOptionList = this.Options.split(';');
                for (var j = 0; j < singleQuestionsOptionList.length; j++) {
                    totalQuestionSingleAnswerHtmlData += "<div class='radio'>";
                    totalQuestionSingleAnswerHtmlData += "<label>";
                    totalQuestionSingleAnswerHtmlData += "<input type='radio' value='" + quesCount + "' name='" + quesCount + "'>" + singleQuestionsOptionList[j] + "";
                    totalQuestionSingleAnswerHtmlData += "</label>";
                    totalQuestionSingleAnswerHtmlData += "</div>";
                }

                totalQuestionSingleAnswerHtmlData += "</fieldset>";
                quesCount++;
            });

            quesCount = 1;
            $.each($rootScope.jobTemplate[2].multipleQuestionsList, function () {

                totalQuestionMultipleAnswerHtmlData += "<fieldset>";

                totalQuestionMultipleAnswerHtmlData += "<label>";
                totalQuestionMultipleAnswerHtmlData += "<b>" + quesCount + ". " + this.Question + "</b><a style='cursor:pointer' class='addQuestionMultipleAnswerClass' id='" + this.Number + "'><i class='fa fa-times'></i></a>";
                totalQuestionMultipleAnswerHtmlData += "</label>";

                var multipleQuestionsOptionList = this.Options.split(';');
                for (var j = 0; j < multipleQuestionsOptionList.length; j++) {
                    totalQuestionMultipleAnswerHtmlData += "<div class='radio'>";
                    totalQuestionMultipleAnswerHtmlData += "<label>";
                    totalQuestionMultipleAnswerHtmlData += "<input type='checkbox' value='" + quesCount + "' name='" + quesCount + "'>" + multipleQuestionsOptionList[j] + "";
                    totalQuestionMultipleAnswerHtmlData += "</label>";
                    totalQuestionMultipleAnswerHtmlData += "</div>";
                }

                totalQuestionMultipleAnswerHtmlData += "</fieldset>";
                quesCount++;
            });

            quesCount = 1;
            $.each($rootScope.jobTemplate[4].listBoxQuestionsList, function () {

                totalQuestionListBoxAnswerHtmlData += "<fieldset>";

                totalQuestionListBoxAnswerHtmlData += "<label>";
                totalQuestionListBoxAnswerHtmlData += "<b>" + quesCount + ". " + this.Question + "</b><a style='cursor:pointer' class='addQuestionListBoxAnswerClass' id='" + this.Number + "'><i class='fa fa-times'></i></a>";
                totalQuestionListBoxAnswerHtmlData += "</label>";

                var listBoxQuestionsOptionList = this.Options.split(';');
                totalQuestionListBoxAnswerHtmlData += "<select name='Education' class='form-control'>";
                for (var j = 0; j < listBoxQuestionsOptionList.length; j++) {
                    totalQuestionListBoxAnswerHtmlData += "<option value='" + j + 1 + "'>" + listBoxQuestionsOptionList[j] + "</option>";
                }
                totalQuestionListBoxAnswerHtmlData += "</select>";
                totalQuestionListBoxAnswerHtmlData += "</fieldset>";
                quesCount++;
            });

            quesCount = 1;
            $.each($rootScope.jobTemplate[3].textBoxQuestionsList, function () {

                totalQuestionTextBoxAnswerHtmlData += "<fieldset>";
                totalQuestionTextBoxAnswerHtmlData += "<div class='input-group'>";
                totalQuestionTextBoxAnswerHtmlData += "<label>";
                totalQuestionTextBoxAnswerHtmlData += "<b>" + quesCount + ". " + this.Question + "</b><a style='cursor:pointer' class='addQuestionTextBoxAnswerClass' id='" + this.Number + "'><i class='fa fa-times'></i></a>";
                totalQuestionTextBoxAnswerHtmlData += "</label>";
                totalQuestionTextBoxAnswerHtmlData += "</div>";
                totalQuestionTextBoxAnswerHtmlData += "<input type='text' class='form-control' value='' placeholder='Enter your answer' name='" + quesCount + " id='" + this.Number + "'/>";

                totalQuestionTextBoxAnswerHtmlData += "</fieldset>";
                quesCount++;
            });

            $('#editableInstructionsListID').html(editableInstructions);
            $('#addSingleAnswerQuestionID').html(totalQuestionSingleAnswerHtmlData);
            $('#addMultipleAnswerQuestionID').html(totalQuestionMultipleAnswerHtmlData);
            $('#addTextBoxAnswerQuestionID').html(totalQuestionTextBoxAnswerHtmlData);
            $('#addListBoxAnswerQuestionID').html(totalQuestionListBoxAnswerHtmlData);

            $('#editableInstructionsListIDPreview').html(editableInstructions);
            $('#addSingleAnswerQuestionIDPreview').html(totalQuestionSingleAnswerHtmlData);
            $('#addMultipleAnswerQuestionIDPreview').html(totalQuestionMultipleAnswerHtmlData);
            $('#addTextBoxAnswerQuestionIDPreview').html(totalQuestionTextBoxAnswerHtmlData);
            $('#addListBoxAnswerQuestionIDPreview').html(totalQuestionListBoxAnswerHtmlData);

            initAddInstructionClass();
            initAddQuestionSingleAnswerClass();
            initAddQuestionMultipleAnswerClass();
            initAddQuestionTextBoxAnswerClass();
            initAddQuestionListBoxAnswerClass();
            refreshComponentsAfterEdit();
        }


        $scope.addEditableInstructions = function () {
            if (($('#AddInstructionsTextArea').val() != "") && ($('#AddInstructionsTextArea').val() != null)) {
                var addEditableInstructionsFancyBoxInImages = $('#AddInstructionsTextArea').val();
                $('#AddInstructionsTextArea').val('test');
                //console.log(addFancyBoxInImages);
                var i = 0;
                $.each(userSession.wysiHtml5UploadedInstructionsImageUrlLink, function () {

                    addEditableInstructionsFancyBoxInImages = replaceImageWithFancyBoxImage(addEditableInstructionsFancyBoxInImages, userSession.wysiHtml5UploadedInstructionsImageUrlLink[i].link_s, userSession.wysiHtml5UploadedInstructionsImageUrlLink[i].link);
                    i++;
                });

                //console.log(addFancyBoxInImages);
                totalEditableInstruction = totalEditableInstruction + 1;
                var editableInstructionDataToBeAdded = { Number: totalEditableInstruction, Text: addEditableInstructionsFancyBoxInImages };
                $rootScope.jobTemplate[0].editableInstructionsList.push(editableInstructionDataToBeAdded);

                $('#AddInstructionsTextArea').data("wysihtml5").editor.clear();
                refreshInstructionList();
                //userSession.wysiHtml5UploadedInstructionsImageUrlLink = [];                
                //$('#AddInstructionsTextArea').val(''); // TODO: clearing the text area not working
            } else {
                showToastMessage("Warning", "Instruction Text Box cann't be empty");
            }

        }

        // single questions..
        $scope.InsertSingleQuestionRow = function () {

            var addSingleQuestionQuestionsFancyBoxInImages = $('#SingleQuestionTextBoxQuestionData').val();
            var addSingleQuestionOptionsFancyBoxInImages = $('#SingleQuestionTextBoxAnswerData').val();
            //console.log(addFancyBoxInImages);
            var i = 0;
            $.each(userSession.wysiHtml5UploadedInstructionsImageUrlLink, function () {

                addSingleQuestionQuestionsFancyBoxInImages = replaceImageWithFancyBoxImage(addSingleQuestionQuestionsFancyBoxInImages, userSession.wysiHtml5UploadedInstructionsImageUrlLink[i].link_s, userSession.wysiHtml5UploadedInstructionsImageUrlLink[i].link);
                addSingleQuestionOptionsFancyBoxInImages = replaceImageWithFancyBoxImage(addSingleQuestionOptionsFancyBoxInImages, userSession.wysiHtml5UploadedInstructionsImageUrlLink[i].link_s, userSession.wysiHtml5UploadedInstructionsImageUrlLink[i].link);
                i++;
            });
            totalSingleQuestionList = totalSingleQuestionList + 1;
            var singleQuestionsList = { Number: totalSingleQuestionList, Question: addSingleQuestionQuestionsFancyBoxInImages, Options: addSingleQuestionOptionsFancyBoxInImages };
            $rootScope.jobTemplate[1].singleQuestionsList.push(singleQuestionsList);
            $('#SingleQuestionTextBoxQuestionData').data("wysihtml5").editor.clear();
            $('#SingleQuestionTextBoxAnswerData').data("wysihtml5").editor.clear();
            refreshSingleQuestionsList();
        }

        // multiple questions..
        $scope.InsertMultipleQuestionRow = function () {
            var addMultipleQuestionQuestionsFancyBoxInImages = $('#MultipleQuestionTextBoxQuestionData').val();
            var addMultipleQuestionOptionsFancyBoxInImages = $('#MultipleQuestionTextBoxAnswerData').val();
            //console.log(addFancyBoxInImages);
            var i = 0;
            $.each(userSession.wysiHtml5UploadedInstructionsImageUrlLink, function () {

                addMultipleQuestionQuestionsFancyBoxInImages = replaceImageWithFancyBoxImage(addMultipleQuestionQuestionsFancyBoxInImages, userSession.wysiHtml5UploadedInstructionsImageUrlLink[i].link_s, userSession.wysiHtml5UploadedInstructionsImageUrlLink[i].link);
                addMultipleQuestionOptionsFancyBoxInImages = replaceImageWithFancyBoxImage(addMultipleQuestionOptionsFancyBoxInImages, userSession.wysiHtml5UploadedInstructionsImageUrlLink[i].link_s, userSession.wysiHtml5UploadedInstructionsImageUrlLink[i].link);
                i++;
            });

            totalMultipleQuestionList = totalMultipleQuestionList + 1;
            var multipleQuestionsList = { Number: totalMultipleQuestionList, Question: addMultipleQuestionQuestionsFancyBoxInImages, Options: addMultipleQuestionOptionsFancyBoxInImages };
            $rootScope.jobTemplate[2].multipleQuestionsList.push(multipleQuestionsList);
            $('#MultipleQuestionTextBoxQuestionData').data("wysihtml5").editor.clear();
            $('#MultipleQuestionTextBoxAnswerData').data("wysihtml5").editor.clear();
            refreshMultipleQuestionsList();
        }

        // listBox questions..
        $scope.InsertListBoxQuestionRow = function () {
            var addListBoxQuestionQuestionsFancyBoxInImages = $('#ListBoxQuestionTextBoxQuestionData').val();
            var addListBoxQuestionOptionsFancyBoxInImages = $('#ListBoxQuestionTextBoxAnswerData').val();
            //console.log(addFancyBoxInImages);
            var i = 0;
            $.each(userSession.wysiHtml5UploadedInstructionsImageUrlLink, function () {

                addListBoxQuestionQuestionsFancyBoxInImages = replaceImageWithFancyBoxImage(addListBoxQuestionQuestionsFancyBoxInImages, userSession.wysiHtml5UploadedInstructionsImageUrlLink[i].link_s, userSession.wysiHtml5UploadedInstructionsImageUrlLink[i].link);
                addListBoxQuestionOptionsFancyBoxInImages = replaceImageWithFancyBoxImage(addListBoxQuestionOptionsFancyBoxInImages, userSession.wysiHtml5UploadedInstructionsImageUrlLink[i].link_s, userSession.wysiHtml5UploadedInstructionsImageUrlLink[i].link);
                i++;
            });

            totalListBoxQuestionList = totalListBoxQuestionList + 1;
            var listBoxQuestionsList = { Number: totalListBoxQuestionList, Question: addListBoxQuestionQuestionsFancyBoxInImages, Options: addListBoxQuestionOptionsFancyBoxInImages };
            $rootScope.jobTemplate[4].listBoxQuestionsList.push(listBoxQuestionsList);
            $('#ListBoxQuestionTextBoxQuestionData').data("wysihtml5").editor.clear();
            $('#ListBoxQuestionTextBoxAnswerData').data("wysihtml5").editor.clear();
            refreshListBoxQuestionsList();
        }

        // textbox questions..
        $scope.InsertTextBoxQuestionRow = function () {

            var addTextBoxQuestionFancyBoxInImages = $('#TextBoxQuestionTextBoxQuestionData').val();
            //console.log(addFancyBoxInImages);
            var i = 0;
            $.each(userSession.wysiHtml5UploadedInstructionsImageUrlLink, function () {

                addTextBoxQuestionFancyBoxInImages = replaceImageWithFancyBoxImage(addTextBoxQuestionFancyBoxInImages, userSession.wysiHtml5UploadedInstructionsImageUrlLink[i].link_s, userSession.wysiHtml5UploadedInstructionsImageUrlLink[i].link);
                i++;
            });

            totalTextBoxQuestionList = totalTextBoxQuestionList + 1;
            var textBoxQuestionsList = { Number: totalTextBoxQuestionList, Question: addTextBoxQuestionFancyBoxInImages, Options: "text" };
            $rootScope.jobTemplate[3].textBoxQuestionsList.push(textBoxQuestionsList);
            $('#TextBoxQuestionTextBoxQuestionData').data("wysihtml5").editor.clear();
            refreshTextBoxQuestionsList();
        }

        $scope.addSingleAnswer = function () {
            if ($rootScope.jobTemplate[1].visible == true) {
                $rootScope.jobTemplate[1].buttonText = "Add Ques. (single Ans.)";
                $rootScope.jobTemplate[1].visible = false;

            } else {
                $rootScope.jobTemplate[1].visible = true;
                $rootScope.jobTemplate[1].buttonText = "Remove Ques. (single Ans.)";
            }
        }

        $scope.addMultipleAnswer = function () {
            if ($rootScope.jobTemplate[2].visible == true) {
                $rootScope.jobTemplate[2].buttonText = "Add Ques. (Multiple Ans.)";
                $rootScope.jobTemplate[2].visible = false;
            } else {
                $rootScope.jobTemplate[2].visible = true;
                $rootScope.jobTemplate[2].buttonText = "Remove Ques. (Multiple Ans.)";
            }
        }

        $scope.addListBoxAnswer = function () {
            if ($rootScope.jobTemplate[4].visible == true) {
                $rootScope.jobTemplate[4].buttonText = "Add Ques. (ListBox Ans.)";
                $rootScope.jobTemplate[4].visible = false;
            } else {
                $rootScope.jobTemplate[4].visible = true;
                $rootScope.jobTemplate[4].buttonText = "Remove Ques. (ListBox Ans.)";
            }
        }

        $scope.addInstructionsRow = function () {
            if ($rootScope.jobTemplate[0].visible == true) {
                $rootScope.jobTemplate[0].buttonText = "Add Instructions";
                $rootScope.jobTemplate[0].visible = false;
            } else {
                $rootScope.jobTemplate[0].visible = true;
                $rootScope.jobTemplate[0].buttonText = "Remove Instructions";
            }
        }
        $scope.addTextBoxAnswer = function () {
            if ($rootScope.jobTemplate[3].visible == true) {
                $rootScope.jobTemplate[3].buttonText = "Add Ques. (TextBox Ans.)";
                $rootScope.jobTemplate[3].visible = false;
            } else {
                $rootScope.jobTemplate[3].visible = true;
                $rootScope.jobTemplate[3].buttonText = "Remove Ques. (TextBox Ans.)";
            }
        }

        function initAddInstructionClass() {
            $('.addInstructionClass').click(function () {
                var i;
                for (i = 0; i < $rootScope.jobTemplate[0].editableInstructionsList.length; i++) {
                    if ($rootScope.jobTemplate[0].editableInstructionsList[i].Number == this.id) {
                        break;
                    }
                }
                $rootScope.jobTemplate[0].editableInstructionsList.splice(i, 1);
                refreshInstructionList();
            });
        }

        function initAddQuestionSingleAnswerClass() {
            $('.addQuestionSingleAnswerClass').click(function () {
                var i;
                for (i = 0; i < $rootScope.jobTemplate[1].singleQuestionsList.length; i++) {
                    if ($rootScope.jobTemplate[1].singleQuestionsList[i].Number == this.id) {
                        break;
                    }
                }
                $rootScope.jobTemplate[1].singleQuestionsList.splice(i, 1);
                refreshSingleQuestionsList();
            });
        }

        function initAddQuestionMultipleAnswerClass() {
            $('.addQuestionMultipleAnswerClass').click(function () {
                var i;
                for (i = 0; i < $rootScope.jobTemplate[2].multipleQuestionsList.length; i++) {
                    if ($rootScope.jobTemplate[2].multipleQuestionsList[i].Number == this.id) {
                        break;
                    }
                }
                $rootScope.jobTemplate[2].multipleQuestionsList.splice(i, 1);
                refreshMultipleQuestionsList();
            });
        }

        function initAddQuestionListBoxAnswerClass() {
            $('.addQuestionListBoxAnswerClass').click(function () {
                var i;
                for (i = 0; i < $rootScope.jobTemplate[4].listBoxQuestionsList.length; i++) {
                    if ($rootScope.jobTemplate[4].listBoxQuestionsList[i].Number == this.id) {
                        break;
                    }
                }
                $rootScope.jobTemplate[4].listBoxQuestionsList.splice(i, 1);
                refreshListBoxQuestionsList();
            });
        }

        function initAddQuestionTextBoxAnswerClass() {
            $('.addQuestionTextBoxAnswerClass').click(function () {
                var i;
                for (i = 0; i < $rootScope.jobTemplate[3].textBoxQuestionsList.length; i++) {
                    if ($rootScope.jobTemplate[3].textBoxQuestionsList[i].Number == this.id) {
                        break;
                    }
                }
                $rootScope.jobTemplate[3].textBoxQuestionsList.splice(i, 1);
                refreshTextBoxQuestionsList();
            });
        }

        function refreshInstructionList() {
            editableInstructions = "";
            $.each($rootScope.jobTemplate[0].editableInstructionsList, function () {
                editableInstructions += "<li>";
                editableInstructions += this.Text + "&nbsp;&nbsp<a style='cursor:pointer' class='addInstructionClass' id='" + this.Number + "'><i class='fa fa-times'></i></a>";
                editableInstructions += "</li>";
            });
            $('#editableInstructionsListID').html(editableInstructions);
            $('#editableInstructionsListIDPreview').html(editableInstructions);
            initAddInstructionClass();
            //$('#addInstructionCloseButton').click();
            refreshComponentsAfterEdit();
        }

        function refreshSingleQuestionsList() {
            totalQuestionSingleAnswerHtmlData = "";
            var innerQuesCount = 1;
            $.each($rootScope.jobTemplate[1].singleQuestionsList, function () {
                totalQuestionSingleAnswerHtmlData += "<fieldset>";

                totalQuestionSingleAnswerHtmlData += "<label>";
                totalQuestionSingleAnswerHtmlData += "<b>" + innerQuesCount + ". " + this.Question + "</b> <a style='cursor:pointer' class='addQuestionSingleAnswerClass' id='" + this.Number + "'><i class='fa fa-times'></i></a>";
                totalQuestionSingleAnswerHtmlData += "</label>";

                var singleQuestionsOptionList = this.Options.split(';');
                for (var j = 0; j < singleQuestionsOptionList.length; j++) {
                    totalQuestionSingleAnswerHtmlData += "<div class='radio'>";
                    totalQuestionSingleAnswerHtmlData += "<label>";
                    totalQuestionSingleAnswerHtmlData += "<input type='radio' value='" + innerQuesCount + "' name='" + innerQuesCount + "'>" + singleQuestionsOptionList[j] + "";
                    totalQuestionSingleAnswerHtmlData += "</label>";
                    totalQuestionSingleAnswerHtmlData += "</div>";
                }

                totalQuestionSingleAnswerHtmlData += "</fieldset>";
                innerQuesCount++;
            });
            $('#addSingleAnswerQuestionID').html(totalQuestionSingleAnswerHtmlData);
            $('#addSingleAnswerQuestionIDPreview').html(totalQuestionSingleAnswerHtmlData);
            initAddQuestionSingleAnswerClass();
            //$('#addQuestionSingleAnswerCloseButton').click();
            refreshComponentsAfterEdit();
        }

        function refreshMultipleQuestionsList() {
            totalQuestionMultipleAnswerHtmlData = "";
            var innerQuesCount = 1;
            $.each($rootScope.jobTemplate[2].multipleQuestionsList, function () {
                totalQuestionMultipleAnswerHtmlData += "<fieldset>";

                totalQuestionMultipleAnswerHtmlData += "<label>";
                totalQuestionMultipleAnswerHtmlData += "<b>" + innerQuesCount + ". " + this.Question + "</b> <a style='cursor:pointer' class='addQuestionMultipleAnswerClass' id='" + this.Number + "'><i class='fa fa-times'></i></a>";
                totalQuestionMultipleAnswerHtmlData += "</label>";

                var multipleQuestionsOptionList = this.Options.split(';');
                for (var j = 0; j < multipleQuestionsOptionList.length; j++) {
                    totalQuestionMultipleAnswerHtmlData += "<div class='radio'>";
                    totalQuestionMultipleAnswerHtmlData += "<label>";
                    totalQuestionMultipleAnswerHtmlData += "<input type='checkbox' value='" + innerQuesCount + "' name='" + innerQuesCount + "'>" + multipleQuestionsOptionList[j] + "";
                    totalQuestionMultipleAnswerHtmlData += "</label>";
                    totalQuestionMultipleAnswerHtmlData += "</div>";
                }

                totalQuestionMultipleAnswerHtmlData += "</fieldset>";
                innerQuesCount++;
            });
            $('#addMultipleAnswerQuestionID').html(totalQuestionMultipleAnswerHtmlData);
            $('#addMultipleAnswerQuestionIDPreview').html(totalQuestionMultipleAnswerHtmlData);
            initAddQuestionMultipleAnswerClass();
            //$('#addQuestionMultipleAnswerCloseButton').click();
            refreshComponentsAfterEdit();
        }

        function refreshListBoxQuestionsList() {
            totalQuestionListBoxAnswerHtmlData = "";
            var innerQuesCount = 1;
            $.each($rootScope.jobTemplate[4].listBoxQuestionsList, function () {
                totalQuestionListBoxAnswerHtmlData += "<fieldset>";

                totalQuestionListBoxAnswerHtmlData += "<label>";
                totalQuestionListBoxAnswerHtmlData += "<b>" + innerQuesCount + ". " + this.Question + "</b> <a style='cursor:pointer' class='addQuestionListBoxAnswerClass' id='" + this.Number + "'><i class='fa fa-times'></i></a>";
                totalQuestionListBoxAnswerHtmlData += "</label>";

                var listBoxQuestionsOptionList = this.Options.split(';');
                totalQuestionListBoxAnswerHtmlData += "<select name='Education' class='form-control'>";
                for (var j = 0; j < listBoxQuestionsOptionList.length; j++) {
                    totalQuestionListBoxAnswerHtmlData += "<option value='" + j + 1 + "'>" + listBoxQuestionsOptionList[j] + "</option>";
                }
                totalQuestionListBoxAnswerHtmlData += "</select>";

                totalQuestionListBoxAnswerHtmlData += "</fieldset>";
                innerQuesCount++;
            });
            $('#addListBoxAnswerQuestionID').html(totalQuestionListBoxAnswerHtmlData);
            $('#addListBoxAnswerQuestionIDPreview').html(totalQuestionListBoxAnswerHtmlData);
            initAddQuestionListBoxAnswerClass();
            //$('#addQuestionListBoxAnswerCloseButton').click();
            refreshComponentsAfterEdit();
        }

        function refreshTextBoxQuestionsList() {
            totalQuestionTextBoxAnswerHtmlData = "";
            var innerQuesCount = 1;
            $.each($rootScope.jobTemplate[3].textBoxQuestionsList, function () {

                totalQuestionTextBoxAnswerHtmlData += "<fieldset>";
                totalQuestionTextBoxAnswerHtmlData += "<div class='input-group'>";
                totalQuestionTextBoxAnswerHtmlData += "<label>";
                totalQuestionTextBoxAnswerHtmlData += "<b>" + innerQuesCount + ". " + this.Question + "</b><a style='cursor:pointer' class='addQuestionTextBoxAnswerClass' id='" + this.Number + "'><i class='fa fa-times'></i></a>";
                totalQuestionTextBoxAnswerHtmlData += "</label>";
                totalQuestionTextBoxAnswerHtmlData += "</div>";
                totalQuestionTextBoxAnswerHtmlData += "<input type='text' class='form-control' value='' placeholder='Enter your answer' name='" + innerQuesCount + " id='" + this.Number + "'/>";

                totalQuestionTextBoxAnswerHtmlData += "</fieldset>";

                innerQuesCount++;
            });
            $('#addTextBoxAnswerQuestionID').html(totalQuestionTextBoxAnswerHtmlData);
            $('#addTextBoxAnswerQuestionIDPreview').html(totalQuestionTextBoxAnswerHtmlData);
            initAddQuestionTextBoxAnswerClass();
            //$('#addQuestionTextBoxAnswerCloseButton').click();
            refreshComponentsAfterEdit();
        }

        $scope.enableFileDrop = function () {
            if ($rootScope.jobTemplate[1].visible == true) {
                $rootScope.jobTemplate[1].buttonText = "Add Ques. (single Ans.)";
                $rootScope.jobTemplate[1].visible = false;
            } else {
                $rootScope.jobTemplate[1].visible = true;
                $rootScope.jobTemplate[1].buttonText = "Remove Ques. (single Ans.)";
            }
        }


        $scope.ClientCreateTemplateFunction = function () {
            $rootScope.jobTemplate[0].title = $('#createTemplateTitleText').val();
            var clientCreateTemplateData = { Data: $rootScope.jobTemplate, ImgurList: userSession.listOfImgurImages, TemplateInfo: { type: TemplateInfoModel.surveyType, subType: TemplateInfoModel.surveySubTypeProductSurvey, description: $('#createTemplateDescriptionText').val(), totalThreads: $("#totalNumberOfThreads").val(), amountEachThread: $("#amountPerThreadTextBoxInput").val()} };
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
            else {
                if (($('#createTemplateTitleText').val() != "") && ($('#createTemplateTitleText').val() != null)) {
                    startBlockUI('wait..', 3);
                    $http({
                        url: url,
                        method: "POST",
                        data: clientCreateTemplateData,
                        headers: headers
                    }).success(function (data, status, headers, config) {
                        //$scope.persons = data; // assign  $scope.persons here as promise is resolved here
                        stopBlockUI();
                        userSession.listOfImgurImages = [];
                        var id = data.Message.split('-')[1];
                        location.href = "#/";
                        showToastMessage("Success", "Successfully Created");
                    }).error(function (data, status, headers, config) {

                    });
                }
                else {
                    showToastMessage("Error", "Title of the Template cann't be empty");
                }
            }
            

        }

        $scope.convertAllWysiHtml5ImagesToFancyBoxLink = function () {
            textToReplace = "src=\"../../Upload/Images/Tulips.jpg\" title=\"Image: ../../Upload/Images/Tulips.jpg\"";

        }

        function refreshComponentsAfterEdit() {
            refreshCheckboxAndRadioButton();
            $('.fancybox').fancybox();
        }
        function refreshCheckboxAndRadioButton() {
            $("input[type='checkbox'], input[type='radio']").iCheck({
                checkboxClass: 'icheckbox_minimal',
                radioClass: 'iradio_minimal'
            });
        }
    });




});

