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
        $scope.imgurImageTemplateModeratingPhotosImageTitleOptionsList = "";
        $scope.jobTemplate = [
                { type: "AddInstructions", title: "", visible: false, buttonText: "Add Instructions", editableInstructionsList: [{ Number: totalEditableInstruction, Text: "Instruction 1"}] },
                { type: "AddSingleQuestionsList", title: "", visible: true, buttonText: "Add Query", singleQuestionsList: [{ Number: totalSingleQuestionList, Question: "Is this Image Obscene (dynamic)?", Options: "Yes;No"}] },
                { type: "AddMultipleQuestionsList", title: "", visible: false, buttonText: "Add Ques. (Multiple Ans.)", multipleQuestionsList: [{ Number: totalMultipleQuestionList, Question: "What is your multiple gender ?", Options: "Malem1;Femalem2"}] },
                { type: "AddTextBoxQuestionsList", title: "", visible: false, buttonText: "Add Ques. (TextBox Ans.)", textBoxQuestionsList: [{ Number: totalTextBoxQuestionList, Question: "Who won 2014 FIFA World cup ?", Options: "text"}] },
                { type: "AddListBoxQuestionsList", title: "", visible: false, buttonText: "Add Ques. (ListBox Ans.)", listBoxQuestionsList: [{ Number: totalListBoxQuestionList, Question: "What is your multiple gender ?", Options: "Malem1;Femalem2"}] }
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
                totalQuestionSingleAnswerHtmlData += "<b>" + this.Question + "</b>";
                totalQuestionSingleAnswerHtmlData += "</label>";

                var singleQuestionsOptionList = this.Options.split(';');
                
                for (var j = 0; j < singleQuestionsOptionList.length; j++) {
                    $scope.imgurImageTemplateModeratingPhotosImageTitleOptionsList += "&nbsp<a class='userInputOnModeratingPhotosWithId cursorPointer'>" + singleQuestionsOptionList[j] + "</a> &nbsp";
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
            updateTotalPayableAmountDiv();
            $('.fancybox').fancybox();
        }

        function updateTotalPayableAmountDiv() {
            $('#totalNumberOfThreads').val(userSession.imgurImageTemplateModeratingPhotos.length);
            $("#TotalAmountPayableCreateTemplate").html($("#amountPerThreadTextBoxInput").val() * $('#totalNumberOfThreads').val());
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
            updateTotalPayableAmountDiv();
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


            if (($('#SingleQuestionTextBoxQuestionDataModeratingPhotos').val() != "") && ($('#SingleQuestionTextBoxQuestionDataModeratingPhotos').val() != null)) {

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
            } else {
                showToastMessage("Warning", "Images Category list input box cann't be empty");
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
                totalQuestionSingleAnswerHtmlData += "<b>" + this.Question + "</b>";
                totalQuestionSingleAnswerHtmlData += "</label>";

                var singleQuestionsOptionList = this.Options.split(';');

                for (var j = 0; j < singleQuestionsOptionList.length; j++) {
                    $scope.imgurImageTemplateModeratingPhotosImageTitleOptionsList += "&nbsp<a class='userInputOnModeratingPhotosWithId cursorPointer'>" + singleQuestionsOptionList[j] + "</a>&nbsp";
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
            var clientCreateModeratingPhotosData = { Data: $scope.jobTemplate, ImgurList: userSession.imgurImageTemplateModeratingPhotos, TemplateInfo: { type: TemplateInfoModel.moderationType, subType: TemplateInfoModel.moderationSubTypeModeratingPhotos, description: $('#createTemplateDescriptionTextModeratingPhotos').val(), totalThreads: $("#totalNumberOfThreads").val(), amountEachThread: $("#amountPerThreadTextBoxInput").val()} };
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
            else if (($('#createTemplateTitleTextModeratingPhotos').val() != "") && ($('#createTemplateTitleTextModeratingPhotos').val() != null)) {
                startBlockUI('wait..', 3);
                $http({
                    url: url,
                    method: "POST",
                    data: clientCreateModeratingPhotosData,
                    headers: headers
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

    });

});

$(function () {
    $(".userInputOnModeratingPhotosWithId").live("click", function () {
        alert($(this).text());
    });
});