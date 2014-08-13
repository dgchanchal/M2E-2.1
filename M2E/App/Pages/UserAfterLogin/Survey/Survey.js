'use strict';
define([appLocation.userPostLogin], function (app) {

    app.controller('UserAfterLoginSurvey', function ($scope, $http, $route, $rootScope, CookieUtil) {
        $scope.logoImage = { url: logoImage };
        $('title').html("index"); //TODO: change the title so cann't be tracked in log
        $scope.userSurveyResult = {
            surveySingleAnswerQuestion: [],
            surveyMultipleAnswerQuestion: [],
            surveyListBoxAnswerQuestion: [],
            surveyTextBoxAnswerQuestion: []
        };

        $scope.surveyInfoInstruction = {
            type: "",
            subType: "",
            data: [
                { instruction: "The Question relates to your life , so they are simple and can be easily answered." },
                { instruction: "This survey is only to know the basics of human mentality, so you need not to worry , just choose the best option according to you." }
            ]
        };

        $scope.surveyInfoSingleAnswerQuestion = {
            type: "",
            subType: "",
            data: [
            //                                    { id: "SAQ-1", question: "Where do you live?", options: splitOptionsToList("Mumbai;Delhi;Kolkata;Chennai"), answer: "-" },
            //                                    { id: "SAQ-2", question: "What is your favorite passtime?", options: splitOptionsToList("Studying;Playing;Dancing;Coding"), answer: "-" },
            //                                    { id: "SAQ-3", question: "Which among these is animal?", options: splitOptionsToList("<a title=\"Personalized Title\" data-fancybox-group=\"gallery\" href=\"http://i.imgur.com/wom619S.jpg\" class=\"fancybox\"><img alt=\"\" src=\"http://i.imgur.com/wom619Ss.jpg\" class=\"MaxUploadedSmallSized\"></a>;<a title=\"Personalized Title\" data-fancybox-group=\"gallery\" href=\"http://i.imgur.com/FhD2x5H.jpg\" class=\"fancybox\"><img alt=\"\" src=\"http://i.imgur.com/FhD2x5Hs.jpg\" class=\"MaxUploadedSmallSized\"></a>;<a title=\"Personalized Title\" data-fancybox-group=\"gallery\" href=\"http://i.imgur.com/TvI9dOg.jpg\" class=\"fancybox\"><img alt=\"\" src=\"http://i.imgur.com/TvI9dOgs.jpg\" class=\"MaxUploadedSmallSized\"></a>;<a title=\"Personalized Title\" data-fancybox-group=\"gallery\" href=\"http://i.imgur.com/6oXVy0a.jpg\" class=\"fancybox\"><img alt=\"\" src=\"http://i.imgur.com/6oXVy0as.jpg\" class=\"MaxUploadedSmallSized\"></a>"), answer: "-" },
            //                                    { id: "SAQ-4", question: "is the following image obscene?</b></p><a title=\"Personalized Title\" data-fancybox-group=\"gallery\" href=\"http://i.imgur.com/z8oQdAh.png\" class=\"fancybox\"><img alt=\"\" src=\"http://i.imgur.com/z8oQdAhs.png\" class=\"MaxUploadedSmallSized\"></a>", options: splitOptionsToList("Yes;No"), answer: "-" },
            //                                    { id: "SAQ-4", question: "Can you Name this famous Personality?</b></p> <a title=\"Personalized Title\" data-fancybox-group=\"gallery\" href=\"http://upload.wikimedia.org/wikipedia/commons/d/d7/Bundesarchiv_Bild_183-61849-0001%2C_Indien%2C_Otto_Grotewohl_bei_Ministerpr%C3%A4sident_Nehru_cropped.jpg\" class=\"fancybox\"><img alt=\"\" src=\"http://upload.wikimedia.org/wikipedia/commons/d/d7/Bundesarchiv_Bild_183-61849-0001%2C_Indien%2C_Otto_Grotewohl_bei_Ministerpr%C3%A4sident_Nehru_cropped.jpg\" class=\"MaxUploadedSmallSized\"></a>", options: splitOptionsToList("Indra Gandi;Jawahar Lal Nehru;Amitabh Bacchan;Abdul Kalam"), answer: "-" }

            ]
        };

        $scope.surveyInfoMultipleAnswerQuestion = {
            type: "",
            subType: "",
            data: [
                { id: "MAQ-1", question: "Which among these is perfect buy Phone in the Market?", options: splitOptionsToList("Iphone 5s;Samsung Galaxy S5;Moto X;Moto E"), answer: "-" },
                { id: "MAQ-2", question: "Which among follower cricket deserves Bharat Ratna Award?", options: splitOptionsToList("Sachin Tendulkar;Rahul Dravid;Kapil Dev;Narayana Kartiken;Dhayn Chnadra;Rahul Gandhi;Narendra Modi;Shahrukh Khan"), answer: "-" },
                { id: "MAQ-3", question: "News Channel which are Transparent in india?", options: splitOptionsToList("Aaj Tak;NDTV;ZEE News;DD News"), answer: "-" },

            ]
        };

        $scope.surveyInfoListBoxAnswerQuestion = {
            type: "",
            subType: "",
            data: [
                { id: "LAQ-1", question: "Which among these is perfect buy Phone in the Market?", options: splitOptionsToList("Iphone 5s;Samsung Galaxy S5;Moto X;Moto E"), answer: "-" },
                { id: "LAQ-2", question: "Which among follower cricket deserves Bharat Ratna Award?", options: splitOptionsToList("Sachin Tendulkar;Rahul Dravid;Kapil Dev;Narayana Kartiken;Dhayn Chnadra;Rahul Gandhi;Narendra Modi;Shahrukh Khan"), answer: "-" },
                { id: "LAQ-3", question: "News Channel which are Transparent in india?", options: splitOptionsToList("Aaj Tak;NDTV;ZEE News;DD News"), answer: "-" },

            ]
        };

        $scope.surveyInfoTextBoxAnswerQuestion = {
            type: "",
            subType: "",
            data: [
                { id: "TAQ-1", question: "Enter your City Pin Code?", options: splitOptionsToList("Iphone 5s;Samsung Galaxy S5;Moto X;Moto E"), answer: "-" }

            ]
        };

        var renderSurveyQuestion = "";

        // instruction list
        if ($scope.surveyInfoInstruction.data.Length != 0) {
            renderSurveyQuestion += "<div class='swiper-slide gray-slide box-body maxHeight800px'>";
            renderSurveyQuestion += "<div class='box box-color box-bordered blue'>";
            renderSurveyQuestion += "<div class='box-title'>";
            renderSurveyQuestion += "<h3>";
            renderSurveyQuestion += "<i class='icon-file'></i>";
            renderSurveyQuestion += "Instructions";
            renderSurveyQuestion += "</h3>";
            renderSurveyQuestion += "</div>";
            renderSurveyQuestion += "<div class='box-content'>";
            $.each($scope.surveyInfoInstruction.data, function () {
                renderSurveyQuestion += "<li>" + this.instruction + "</li>";
            });
            renderSurveyQuestion += "</div>";
            renderSurveyQuestion += "</div>";
            renderSurveyQuestion += "</div>";
        }

        // single answer question..
        if ($scope.surveyInfoSingleAnswerQuestion.data.Length != 0) {
            $.each($scope.surveyInfoSingleAnswerQuestion.data, function () {
                renderSurveyQuestion += "<div class='swiper-slide gray-slide box-body maxHeight800px'>";
                renderSurveyQuestion += "<div class='box box-color box-bordered blue'>";
                renderSurveyQuestion += "<div class='box-title'>";
                renderSurveyQuestion += "<h3>";
                renderSurveyQuestion += "<i class='icon-file'></i>";
                renderSurveyQuestion += "Single Answer Question";
                renderSurveyQuestion += "</h3>";
                renderSurveyQuestion += "</div>";
                renderSurveyQuestion += "<div class='box-content'>";
                renderSurveyQuestion += "<p><b>";
                renderSurveyQuestion += this.question;
                renderSurveyQuestion += "</b></p>";
                var id = this.id;
                $.each(this.options, function () {
                    renderSurveyQuestion += "<input type='radio' class='userSurveyRadioButton' name='" + id + "' value='" + id + "_" + this + "'/> " + this + "<br/>";

                });
                renderSurveyQuestion += "</div>";
                renderSurveyQuestion += "</div>";
                renderSurveyQuestion += "</div>";
            });
        }

        //multiple answer question
        if ($scope.surveyInfoMultipleAnswerQuestion.data.Length != 0) {
            $.each($scope.surveyInfoMultipleAnswerQuestion.data, function () {
                renderSurveyQuestion += "<div class='swiper-slide gray-slide box-body maxHeight800px'>";
                renderSurveyQuestion += "<div class='box box-color box-bordered blue'>";
                renderSurveyQuestion += "<div class='box-title'>";
                renderSurveyQuestion += "<h3>";
                renderSurveyQuestion += "<i class='icon-file'></i>";
                renderSurveyQuestion += "Multiple Answer Question";
                renderSurveyQuestion += "</h3>";
                renderSurveyQuestion += "</div>";
                renderSurveyQuestion += "<div class='box-content'>";
                renderSurveyQuestion += "<p><b>";
                renderSurveyQuestion += this.question;
                renderSurveyQuestion += "</b></p>";
                var id = this.id;
                $.each(this.options, function () {
                    renderSurveyQuestion += "<input type='checkbox' class='userSurveyCheckBoxButton' name='" + id + "' value='" + id + "_" + this + "'/> " + this + "<br/>";

                });
                renderSurveyQuestion += "</div>";
                renderSurveyQuestion += "</div>";
                renderSurveyQuestion += "</div>";
            });
        }

        //listbox answer question
        if ($scope.surveyInfoListBoxAnswerQuestion.data.Length != 0) {
            $.each($scope.surveyInfoListBoxAnswerQuestion.data, function () {
                renderSurveyQuestion += "<div class='swiper-slide gray-slide box-body maxHeight800px'>";
                renderSurveyQuestion += "<div class='box box-color box-bordered blue'>";
                renderSurveyQuestion += "<div class='box-title'>";
                renderSurveyQuestion += "<h3>";
                renderSurveyQuestion += "<i class='icon-file'></i>";
                renderSurveyQuestion += "ListBox Answer Question";
                renderSurveyQuestion += "</h3>";
                renderSurveyQuestion += "</div>";
                renderSurveyQuestion += "<div class='box-content'>";
                renderSurveyQuestion += "<p><b>";
                renderSurveyQuestion += this.question;
                renderSurveyQuestion += "</b></p>";
                renderSurveyQuestion += "<select name='" + this.id + "' id='" + this.id + "'>";
                var id = this.id;
                $.each(this.options, function () {
                    renderSurveyQuestion += "<option value='" + this + "'>" + this + "</option><br/>";

                });
                renderSurveyQuestion += "</select>";
                renderSurveyQuestion += "</div>";
                renderSurveyQuestion += "</div>";
                renderSurveyQuestion += "</div>";
            });
        }
        //textBox answer question
        if ($scope.surveyInfoTextBoxAnswerQuestion.data.Length != 0) {
            $.each($scope.surveyInfoTextBoxAnswerQuestion.data, function () {
                renderSurveyQuestion += "<div class='swiper-slide gray-slide box-body maxHeight800px'>";
                renderSurveyQuestion += "<div class='box box-color box-bordered blue'>";
                renderSurveyQuestion += "<div class='box-title'>";
                renderSurveyQuestion += "<h3>";
                renderSurveyQuestion += "<i class='icon-file'></i>";
                renderSurveyQuestion += "TextBox Answer Question";
                renderSurveyQuestion += "</h3>";
                renderSurveyQuestion += "</div>";
                renderSurveyQuestion += "<div class='box-content'>";
                renderSurveyQuestion += "<p><b>";
                renderSurveyQuestion += this.question;
                renderSurveyQuestion += "</b></p>";

                renderSurveyQuestion += "<input type='text' name='" + this.id + "' placeholder='Enter Your Answer'/><br/>";

                renderSurveyQuestion += "</div>";
                renderSurveyQuestion += "</div>";
                renderSurveyQuestion += "</div>";
            });
        }

        // adding button at last
        renderSurveyQuestion += "<div class='swiper-slide gray-slide box-body maxHeight800px'>";
        renderSurveyQuestion += "<div class='box box-color box-bordered blue'>";
        renderSurveyQuestion += "<div class='box-title'>";
        renderSurveyQuestion += "<h3>";
        renderSurveyQuestion += "<i class='icon-file'></i>";
        renderSurveyQuestion += "Submit Your Survey";
        renderSurveyQuestion += "</h3>";
        renderSurveyQuestion += "</div>";
        renderSurveyQuestion += "<div class='box-content'>";
        renderSurveyQuestion += "<button  class=\"btn btn-success btn-sm\">submit</button>";
        renderSurveyQuestion += "</div>";
        renderSurveyQuestion += "</div>";
        renderSurveyQuestion += "</div>";
        $('#swiperWrapperId').html(renderSurveyQuestion);
        initializeSwiperFunction();

        $scope.newValue = function (value) {
            console.log(value);
        }

        $scope.showCurrentData = function () {
            console.log(surveyInfoSingleAnswerQuestion.data);
        }

        function initializeSwiperFunction() {
            var mySwiper = new Swiper('.swiper-container', {
                pagination: '.pagination',
                paginationClickable: true
            })
            $('.fancybox').fancybox();


            //radiobutton
            $('.userSurveyRadioButton').on('change', function () {
                //var a = "change " + $(this).val();
                //console.log(a + "---" + this.value);
                var data = this.value.split('_');
                $scope.userSurveyResult.surveySingleAnswerQuestion.push(commonUserSurveyRadioButtonFunction(data));
                console.log($scope.userSurveyResult.surveySingleAnswerQuestion);
            });

            $('.userSurveyRadioButton').on('ifChecked', function (event) {
                //var a = "ifChecked "+ $(this).val();
                //console.log(a + "---" + this.value);
                var data = this.value.split('_');
                $scope.userSurveyResult.surveySingleAnswerQuestion.push(commonUserSurveyRadioButtonFunction(data));
                console.log($scope.userSurveyResult.surveySingleAnswerQuestion);
            });

            function commonUserSurveyRadioButtonFunction(data) {
                var radioButtonAnswer = { key: data[0], value: data[1] };

                var i, flag = false;
                for (i = 0; i < $scope.userSurveyResult.surveySingleAnswerQuestion.length; i++) {
                    if ($scope.userSurveyResult.surveySingleAnswerQuestion[i].key == data[0]) {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                    $scope.userSurveyResult.surveySingleAnswerQuestion.splice(i, 1);

                return radioButtonAnswer;
            }

            //checkbox
            $('.userSurveyCheckBoxButton').on('change', function () {
                var a;
                var data = this.value.split('_');
                var checked = true;
                var i;
                if ($(this).is(':checked')) {
                    checked = true;
                }
                else {
                    checked = false;
                }

                commonUserSurveyCheckBoxButtonFunction(data, checked);
                //console.log($scope.userSurveyResult.surveyMultipleAnswerQuestion);

            });

            $('.userSurveyCheckBoxButton').on('ifChecked', function (event) {
                var data = this.value.split('_');
                commonUserSurveyCheckBoxButtonFunction(data, true);
                //console.log($scope.userSurveyResult.surveyMultipleAnswerQuestion);
            });
            $('.userSurveyCheckBoxButton').on('ifUnchecked', function (event) {
                var data = this.value.split('_');
                commonUserSurveyCheckBoxButtonFunction(data, false);
                //console.log($scope.userSurveyResult.surveyMultipleAnswerQuestion);
            });

            function commonUserSurveyCheckBoxButtonFunction(data, checked) {
                var i, flag = false;
                for (i = 0; i < $scope.userSurveyResult.surveyMultipleAnswerQuestion.length; i++) {
                    if ($scope.userSurveyResult.surveyMultipleAnswerQuestion[i].key == data[0]) {
                        flag = true;
                        break;
                    }
                }

                if (flag) {
                    if (checked)
                        $scope.userSurveyResult.surveyMultipleAnswerQuestion[i].value += data[1] + ';';
                    else
                        $scope.userSurveyResult.surveyMultipleAnswerQuestion[i].value = $scope.userSurveyResult.surveyMultipleAnswerQuestion[i].value.replace(data[1] + ';', "");
                }
                else {
                    var checkBoxButtonAnswer = { key: data[0], value: data[1] + ';' };
                    $scope.userSurveyResult.surveyMultipleAnswerQuestion.push(checkBoxButtonAnswer);
                }
            }
        }
    });

});

function splitOptionsToList(data) {
    return data.split(';');
}

