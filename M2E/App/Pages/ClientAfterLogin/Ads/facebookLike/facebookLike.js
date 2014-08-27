'use strict';
define([appLocation.postLogin], function (app) {

    app.controller('ClientAfterLoginFacebookLikeTemplate', function ($scope, $http, $rootScope, Restangular, $routeParams, CookieUtil) {
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

        $scope.param_show_faces = true;
        $scope.param_header = true;
        $scope.param_stream = false;
        $scope.param_show_border = false;

        $scope.facebookLikeIframe = "";
        $scope.facebookLikePageUrl = "";
        $scope.facebookLikePageId = "";

        $scope.showFacebookDetailDiv = false;
        $scope.facebookData = {};

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
            $('#addTextBoxAnswerQuestionIDArticleWritingTemplate').html($scope.jobTemplate[3].textBoxQuestionsList[0].Question);
            $('#TextBoxQuestionTextBoxQuestionDataArticleWritingTemplatePreviewDescription').html($scope.jobTemplate[3].textBoxQuestionsList[0].Question);
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


        // textbox questions..
        $scope.InsertTextBoxQuestionRow = function () {

            var addTextBoxQuestionFancyBoxInImages = $('#TextBoxQuestionTextBoxQuestionDataArticleWritingTemplate').val();
            //console.log(addFancyBoxInImages);
            var i = 0;
            $.each(userSession.wysiHtml5UploadedInstructionsImageUrlLink, function () {

                addTextBoxQuestionFancyBoxInImages = replaceImageWithFancyBoxImage(addTextBoxQuestionFancyBoxInImages, userSession.wysiHtml5UploadedInstructionsImageUrlLink[i].link_s, userSession.wysiHtml5UploadedInstructionsImageUrlLink[i].link);
                i++;
            });

            
            var textBoxQuestionsList = { Number: totalTextBoxQuestionList, Question: addTextBoxQuestionFancyBoxInImages, Options: "text" };
            $scope.jobTemplate[3].textBoxQuestionsList[0]=(textBoxQuestionsList);
            $('#TextBoxQuestionTextBoxQuestionDataArticleWritingTemplate').data("wysihtml5").editor.clear();
            refreshTextBoxQuestionsList();
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


        function refreshTextBoxQuestionsList() {

            $('#addTextBoxAnswerQuestionIDArticleWritingTemplate').html($scope.jobTemplate[3].textBoxQuestionsList[0].Question);
            $('#TextBoxQuestionTextBoxQuestionDataArticleWritingTemplatePreviewDescription').html($scope.jobTemplate[3].textBoxQuestionsList[0].Question);
            initAddQuestionTextBoxAnswerClass();
            //$('#addQuestionTextBoxAnswerCloseButton').click();
            //$('.fancybox').fancybox();

        }

        $scope.ClientRenderTemplateView = function () {
            //var url = "https://www.facebook.com/beststatuslines";
            var url = $('#FacebookLikePageUrl').val();
            $scope.facebookLikePageUrl = url;
            $scope.facebookLikePageUrl = url.replace('//www.', '//graph.');
            getFacebookPageId();

            $scope.facebookLikeIframe = "<iframe src='//www.facebook.com/plugins/likebox.php?href=" + url + "&amp;width&amp;height=290&amp;colorscheme=" + $("#facebookSelectTheme option:selected").text() + "&amp;show_faces=" + $scope.param_show_faces + "&amp;header=" + $scope.param_header + "&amp;stream=" + $scope.param_stream + "&amp;show_border=" + $scope.param_show_border + "&amp;appId=" + facebookAppId + "' scrolling='no' frameborder='0' style='border:none; overflow:hidden; height:290px;' allowTransparency='true'></iframe>";

            $('#showFacebookIframeForGivenPageUrl').html($scope.facebookLikeIframe);

        }

        function getFacebookPageId() {

            var url = $scope.facebookLikePageUrl
            var userSurveyResultData = $scope.userSurveyResult;
            var headers = {
                'Content-Type': 'application/json'
            };

            $http({
                url: url,
                method: "GET",
                data: userSurveyResultData,
                headers: headers
            }).success(function (data, status, headers, config) {

                $scope.facebookData = data;
                $scope.showFacebookDetailDiv = true;

            }).error(function (data, status, headers, config) {

            });

        };

        $scope.ClientCreateFacebookLikeTemplateFunction = function () {
            $scope.jobTemplate[0].title = $('#createTemplateTitleTextArticleWritingTemplate').val();
            //console.log(userSession.imgurImageTranscriptionTemplate);
            if ($scope.facebookLikePageUrl == "") {
                showToastMessage("Error", "First Try to render the page, if you are able to see your page below then only submit your job. Please contact us if you have any issue.");
            }
            else {
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


        }

        $scope.convertAllWysiHtml5ImagesToFancyBoxLink = function () {
            textToReplace = "src=\"../../Upload/Images/Tulips.jpg\" title=\"Image: ../../Upload/Images/Tulips.jpg\"";

        }

    });

});
