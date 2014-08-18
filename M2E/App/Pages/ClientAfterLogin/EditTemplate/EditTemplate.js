'use strict';
define([appLocation.postLogin], function (app) {

    //getting user info..
    app.controller('editTemplateController', function ($scope, $http, $rootScope, $routeParams, CookieUtil) {
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
        $rootScope.jobTemplate = [];
        $rootScope.imgurImageTemplate = [];

        var url = ServerContextPah + '/Client/GetTemplateDetailById?id=' + $routeParams.templateid;
        var headers = {
            'Content-Type': 'application/json',
            'UTMZT': CookieUtil.getUTMZT(),
            'UTMZK': CookieUtil.getUTMZK(),
            'UTMZV': CookieUtil.getUTMZV()
        };
        startBlockUI('wait..', 3);
        $http({
            url: url,
            method: "POST",
            headers: headers
        }).success(function (data, status, headers, config) {
            //$scope.persons = data; // assign  $scope.persons here as promise is resolved here
            stopBlockUI();
            if (data.Status == "200") {
                
                $rootScope.jobTemplate = data.Payload.Data;
                loadTemplate();
            }
            else if (data.Status == "404") {
                
                alert("This template is not present in database");
            }
            else if (data.Status == "500") {
                
                alert("Internal Server Error Occured");
            }
        }).error(function (data, status, headers, config) {
            stopBlockUI();
        });
        loadImagesfromImgur();

        function loadImagesfromImgur() {
            var url = ServerContextPah + '/Client/GetTemplateImageDetailById?id=' + $routeParams.templateid;
            var headers = {
                'Content-Type': 'application/json',
                'UTMZT': CookieUtil.getUTMZT(),
                'UTMZK': CookieUtil.getUTMZK(),
                'UTMZV': CookieUtil.getUTMZV()
            };
            $http({
                url: url,
                method: "POST",
                headers: headers
            }).success(function (data, status, headers, config) {
                //$scope.persons = data; // assign  $scope.persons here as promise is resolved here

                if (data.Status == "200") {
                    $rootScope.imgurImageTemplate = data.Payload;

                    //console.log(data.Payload);
                    //console.log($rootScope.imgurImageTemplate);
                }
                else if (data.Status == "404") {
                    stopBlockUI();
                    alert("This template is not present in database");
                }
                else if (data.Status == "500") {
                    stopBlockUI();
                    alert("Internal Server Error Occured");
                }
            }).error(function (data, status, headers, config) {
                stopBlockUI();
            });
        }
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
                    totalQuestionListBoxAnswerHtmlData += "<option value='" + quesCount + "'>" + listBoxQuestionsOptionList[j] + "</option>";
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
                    totalQuestionListBoxAnswerHtmlData += "<option value='" + quesCount + "'>" + listBoxQuestionsOptionList[j] + "</option>";
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

        $scope.DeleteEditImgurImageByIdFunction = function (id) {

            var url = ServerContextPah + '/Client/DeleteTemplateImgurImageById?id=' + id;
            {
                startBlockUI('wait..', 3);
                $http({
                    url: url,
                    method: "POST",
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data, status, headers, config) {
                    //$scope.persons = data; // assign  $scope.persons here as promise is resolved here
                    stopBlockUI();
                    showToastMessage("Success", "Successfully deleted Image");
                    loadImagesfromImgur();
                }).error(function (data, status, headers, config) {
                    stopBlockUI();
                });
            }


        }


        $scope.ClientEditTemplateFunction = function () {
            $rootScope.jobTemplate[0].title = $('#createTemplateTitleText').val();
            var clientEditTemplateData = { Data: $rootScope.jobTemplate, ImgurList: userSession.listOfImgurImages };
            var url = ServerContextPah + '/Client/EditTemplateDetailById?id=' + $routeParams.templateid;
            var headersSubmit = {
                'Content-Type': 'application/json',
                'UTMZT': CookieUtil.getUTMZT(),
                'UTMZK': CookieUtil.getUTMZK(),
                'UTMZV': CookieUtil.getUTMZV()
            };
            if (($('#createTemplateTitleText').val() != "") && ($('#createTemplateTitleText').val() != null)) {
                startBlockUI('wait..', 3);
                $http({
                    url: url,
                    method: "POST",
                    data: clientEditTemplateData,
                    headers: headersSubmit
                }).success(function (data, status, headers, config) {
                    //$scope.persons = data; // assign  $scope.persons here as promise is resolved here
                    stopBlockUI();
                    showToastMessage("Success", "Successfully Edited");
                    loadImagesfromImgur();
                }).error(function (data, status, headers, config) {
                    stopBlockUI();
                });
            }
            else {
                showToastMessage("Error", "Title of the Template cann't be empty");
            }

        }

        $scope.copyToClipboard = function (event) {
            //alert("text");
            window.prompt("Copy to clipboard: Ctrl+C, Enter", event.target.id);
        }

    });

});
