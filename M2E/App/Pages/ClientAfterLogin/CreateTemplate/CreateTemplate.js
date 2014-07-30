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
            initAddInstructionClass();
            initAddQuestionSingleAnswerClass();
            initAddQuestionMultipleAnswerClass();
            initAddQuestionTextBoxAnswerClass();

        }


        $scope.addEditableInstructions = function () {
            if (($('#AddInstructionsTextArea').val() != "") && ($('#AddInstructionsTextArea').val() != null)) {
                totalEditableInstruction = totalEditableInstruction + 1;
                var editableInstructionDataToBeAdded = { Number: totalEditableInstruction, Text: $('#AddInstructionsTextArea').val() };
                $rootScope.jobTemplate[0].editableInstructionsList.push(editableInstructionDataToBeAdded);
                refreshInstructionList();
                //$('#AddInstructionsTextArea').val(''); // TODO: clearing the text area not working
            } else {
                showToastMessage("Warning", "Instruction Text Box cann't be empty");
            }

        }

        // single questions..
        $scope.InsertSingleQuestionRow = function () {
            totalSingleQuestionList = totalSingleQuestionList + 1;
            var singleQuestionsList = { Number: totalSingleQuestionList, Question: $('#SingleQuestionTextBoxQuestionData').val(), Options: $('#SingleQuestionTextBoxAnswerData').val() };
            $rootScope.jobTemplate[1].singleQuestionsList.push(singleQuestionsList);
            refreshSingleQuestionsList();
        }

        // multiple questions..
        $scope.InsertMultipleQuestionRow = function () {
            totalMultipleQuestionList = totalMultipleQuestionList + 1;
            var multipleQuestionsList = { Number: totalMultipleQuestionList, Question: $('#MultipleQuestionTextBoxQuestionData').val(), Options: $('#MultipleQuestionTextBoxAnswerData').val() };
            $rootScope.jobTemplate[2].multipleQuestionsList.push(multipleQuestionsList);
            refreshMultipleQuestionsList();
        }

        // listBox questions..
        $scope.InsertListBoxQuestionRow = function () {
            totalListBoxQuestionList = totalListBoxQuestionList + 1;
            var listBoxQuestionsList = { Number: totalListBoxQuestionList, Question: $('#ListBoxQuestionTextBoxQuestionData').val(), Options: $('#ListBoxQuestionTextBoxAnswerData').val() };
            $rootScope.jobTemplate[4].listBoxQuestionsList.push(listBoxQuestionsList);
            refreshListBoxQuestionsList();
        }

        // textbox questions..
        $scope.InsertTextBoxQuestionRow = function () {
            totalTextBoxQuestionList = totalTextBoxQuestionList + 1;
            var textBoxQuestionsList = { Number: totalTextBoxQuestionList, Question: $('#TextBoxQuestionTextBoxQuestionData').val(), Options: "text" };
            $rootScope.jobTemplate[3].textBoxQuestionsList.push(textBoxQuestionsList);
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
            initAddInstructionClass();
            $('#addInstructionCloseButton').click();
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
            initAddQuestionSingleAnswerClass();
            $('#addQuestionSingleAnswerCloseButton').click();
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
            initAddQuestionMultipleAnswerClass();
            $('#addQuestionMultipleAnswerCloseButton').click();
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
            initAddQuestionListBoxAnswerClass();
            $('#addQuestionListBoxAnswerCloseButton').click();
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
            initAddQuestionTextBoxAnswerClass();
            $('#addQuestionTextBoxAnswerCloseButton').click();
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
            var clientCreateTemplateData = { Data: $rootScope.jobTemplate, ImgurList: userSession.listOfImgurImages };
            //var currentTemplateId = new Date().getTime();

            var url = ServerContextPah + '/Client/CreateTemplate?username=' + userSession.username;
            if (($('#createTemplateTitleText').val() != "") && ($('#createTemplateTitleText').val() != null)) {
                startBlockUI('wait..', 3);
                $http({
                    url: url,
                    method: "POST",
                    data: clientCreateTemplateData,
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data, status, headers, config) {
                    //$scope.persons = data; // assign  $scope.persons here as promise is resolved here
                    stopBlockUI();
                    userSession.listOfImgurImages = [];
                    var id = data.Message.split('-')[1];
                    location.href = "#/editTemplate/edit/" + id;
                    showToastMessage("Success", "Successfully Created");
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

