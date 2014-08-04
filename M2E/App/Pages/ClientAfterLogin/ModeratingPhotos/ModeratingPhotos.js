'use strict';
define([appLocation.postLogin], function (app) {

    app.controller('ClientAfterLoginModeratingPhotos', function ($scope, $http, $rootScope, $routeParams, CookieUtil) {
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
        userSession.imgurImageTemplateModeratingPhotos = [];
        $scope.imgurImageTemplateModeratingPhotos = userSession.imgurImageTemplateModeratingPhotos;
        $scope.jobTemplate = [
                { type: "AddInstructions", title: "", visible: false, buttonText: "Add Instructions", editableInstructionsList: [{ Number: totalEditableInstruction, Text: "Instruction 1"}] },
                { type: "AddSingleQuestionsList", title: "", visible: false, buttonText: "Add Query", singleQuestionsList: [{ Number: totalSingleQuestionList, Question: "Is this Image Obscene?", Options: "Yes;No"}] }
        ];

        loadTemplate();

        function loadTemplate() {
            $('#createTemplateTitleTextModeratingPhotos').val($scope.jobTemplate[0].title);
            $.each($scope.jobTemplate[0].editableInstructionsList, function () {
                editableInstructions += "<li>";
                editableInstructions += this.Text + "&nbsp;&nbsp<a style='cursor:pointer' class='addInstructionClass' id='" + this.Number + "'><i class='fa fa-times'></i></a>";
                editableInstructions += "</li>";
            });

            var quesCount = 1;
            $.each($scope.jobTemplate[1].singleQuestionsList, function () {

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



            $('#editableInstructionsListIDModeratingPhotos').html(editableInstructions);
            $('#addSingleAnswerQuestionIDModeratingPhotos').html(totalQuestionSingleAnswerHtmlData);

            initAddInstructionClass();
            initAddQuestionSingleAnswerClass();

        }

        $scope.refreshModeratingPhotosListDiv = function () {
            $scope.imgurImageTemplateModeratingPhotos = userSession.imgurImageTemplateModeratingPhotos;
            $('.fancybox').fancybox();
        }

        $scope.DeleteEditImgurImageByIdFunction = function (id) {
            var i;
            for (i = 0; i < userSession.imgurImageTemplateModeratingPhotos.length; i++) {
                if (userSession.imgurImageTemplateModeratingPhotos[i].data.id == id) {
                    break;
                }
            }
            //console.log(userSession.imgurImageTemplateModeratingPhotos);
            
            userSession.imgurImageTemplateModeratingPhotos.splice(i, 1);
            
            //console.log(userSession.imgurImageTemplateModeratingPhotos);
            $scope.imgurImageTemplateModeratingPhotos = userSession.imgurImageTemplateModeratingPhotos;
            $('.fancybox').fancybox();
        }

        $scope.addEditableInstructions = function () {
            if (($('#AddInstructionsTextAreaModeratingPhotos').val() != "") && ($('#AddInstructionsTextAreaModeratingPhotos').val() != null)) {
                var addEditableInstructionsFancyBoxInImages = $('#AddInstructionsTextAreaModeratingPhotos').val();
                $('#AddInstructionsTextAreaModeratingPhotos').val('test');
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

                $('#AddInstructionsTextAreaModeratingPhotos').data("wysihtml5").editor.clear();
                refreshInstructionList();
                //userSession.wysiHtml5UploadedInstructionsImageUrlLink = [];                
                //$('#AddInstructionsTextArea').val(''); // TODO: clearing the text area not working
            } else {
                showToastMessage("Warning", "Instruction Text Box cann't be empty");
            }

        }

        // single questions..
        $scope.InsertSingleQuestionRow = function () {

            var addSingleQuestionQuestionsFancyBoxInImages = $('#SingleQuestionTextBoxQuestionDataModeratingPhotos').val();
            var addSingleQuestionOptionsFancyBoxInImages = $('#SingleQuestionTextBoxAnswerDataModeratingPhotos').val();
            //console.log(addFancyBoxInImages);
            var i = 0;
            $.each(userSession.wysiHtml5UploadedInstructionsImageUrlLink, function () {

                addSingleQuestionQuestionsFancyBoxInImages = replaceImageWithFancyBoxImage(addSingleQuestionQuestionsFancyBoxInImages, userSession.wysiHtml5UploadedInstructionsImageUrlLink[i].link_s, userSession.wysiHtml5UploadedInstructionsImageUrlLink[i].link);
                addSingleQuestionOptionsFancyBoxInImages = replaceImageWithFancyBoxImage(addSingleQuestionOptionsFancyBoxInImages, userSession.wysiHtml5UploadedInstructionsImageUrlLink[i].link_s, userSession.wysiHtml5UploadedInstructionsImageUrlLink[i].link);
                i++;
            });
            totalSingleQuestionList = totalSingleQuestionList + 1;
            var singleQuestionsList = { Number: totalSingleQuestionList, Question: addSingleQuestionQuestionsFancyBoxInImages, Options: addSingleQuestionOptionsFancyBoxInImages };
            $scope.jobTemplate[1].singleQuestionsList[0] = (singleQuestionsList);
            $('#SingleQuestionTextBoxQuestionDataModeratingPhotos').data("wysihtml5").editor.clear();
            $('#SingleQuestionTextBoxAnswerDataModeratingPhotos').data("wysihtml5").editor.clear();
            refreshSingleQuestionsList();
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

        $scope.addSingleAnswer = function () {
            if ($scope.jobTemplate[1].visible == true) {
                $scope.jobTemplate[1].buttonText = "Add Query";
                $scope.jobTemplate[1].visible = false;

            } else {
                $scope.jobTemplate[1].visible = true;
                $scope.jobTemplate[1].buttonText = "Remove Query";
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

        function initAddQuestionSingleAnswerClass() {
            $('.addQuestionSingleAnswerClass').click(function () {
                var i;
                for (i = 0; i < $scope.jobTemplate[1].singleQuestionsList.length; i++) {
                    if ($scope.jobTemplate[1].singleQuestionsList[i].Number == this.id) {
                        break;
                    }
                }
                $scope.jobTemplate[1].singleQuestionsList.splice(i, 1);
                refreshSingleQuestionsList();
            });
        }

        function refreshInstructionList() {
            editableInstructions = "";
            $.each($scope.jobTemplate[0].editableInstructionsList, function () {
                editableInstructions += "<li>";
                editableInstructions += this.Text + "&nbsp;&nbsp<a style='cursor:pointer' class='addInstructionClass' id='" + this.Number + "'><i class='fa fa-times'></i></a>";
                editableInstructions += "</li>";
            });
            $('#editableInstructionsListIDModeratingPhotos').html(editableInstructions);
            initAddInstructionClass();
            $('#addInstructionCloseButtonModeratingPhotos').click();
            $('.fancybox').fancybox();
        }

        function refreshSingleQuestionsList() {
            totalQuestionSingleAnswerHtmlData = "";
            var innerQuesCount = 1;
            $.each($scope.jobTemplate[1].singleQuestionsList, function () {
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
            $('#addSingleAnswerQuestionIDModeratingPhotos').html(totalQuestionSingleAnswerHtmlData);
            initAddQuestionSingleAnswerClass();
            $('#addQuestionSingleAnswerCloseButtonModeratingPhotos').click();
            $('.fancybox').fancybox();
        }


        $scope.ClientCreateModeratingPhotosFunction = function () {
            $scope.jobTemplate[0].title = $('#createTemplateTitleTextModeratingPhotos').val();
            var clientCreateModeratingPhotosData = { Data: $scope.jobTemplate, ImgurList: userSession.imgurImageTemplateModeratingPhotos };
            //var currentTemplateId = new Date().getTime();

            var url = ServerContextPah + '/Client/CreateTemplateModeratingPhotos?username=' + userSession.username;
            if (($('#createTemplateTitleTextModeratingPhotos').val() != "") && ($('#createTemplateTitleTextModeratingPhotos').val() != null)) {
                startBlockUI('wait..', 3);
                $http({
                    url: url,
                    method: "POST",
                    data: clientCreateModeratingPhotosData,
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data, status, headers, config) {
                    //$scope.persons = data; // assign  $scope.persons here as promise is resolved here
                    stopBlockUI();
                    userSession.imgurImageTemplateModeratingPhotos = [];
                    //var id = data.Message.split('-')[1];
                    //location.href = "#/editTemplate/edit/" + id;
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
