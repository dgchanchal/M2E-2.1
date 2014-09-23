'use strict';
define([appLocation.postLogin], function (app) {

    app.controller('ClientAfterLoginTemplateSample', function ($scope, $http, $rootScope, $routeParams, CookieUtil) {
        $('title').html("sample page"); //TODO: change the title so cann't be tracked in log
        $scope.type = $routeParams.type;
        $scope.subType = $routeParams.subType;
        if ($routeParams.type == TemplateInfoModel.type_survey) {
            $scope.TemplateUrl = "#/createTemplate/" + $routeParams.type + "/" + $routeParams.subType;
//            if ($routeParams.subType == TemplateInfoModel.subType_productSurvey) {
//                $scope.TemplateUrl = "#/createTemplate/" + $routeParams.type + "/" + $routeParams.subType;
//            }
        }
        else if ($routeParams.type == TemplateInfoModel.type_moderation) {
            if ($routeParams.subType == TemplateInfoModel.subType_imageModeration) {
                $scope.TemplateUrl = "#/moderatingPhotos/" + $routeParams.type + "/" + $routeParams.subType;
            }
        }
        else if ($routeParams.type == TemplateInfoModel.type_dataEntry) {
            if ($routeParams.subType == TemplateInfoModel.subType_Transcription) {
                $scope.TemplateUrl = "#/transcriptionTemplate/" + $routeParams.type + "/" + $routeParams.subType;
            } else {
                $scope.TemplateUrl = "#/articleWrittingTemplate/" + $routeParams.type + "/" + $routeParams.subType;
            }
        }
        else if ($routeParams.type == TemplateInfoModel.type_contentWritting) {
            $scope.TemplateUrl = "#/articleWrittingTemplate/" + $routeParams.type + "/" + $routeParams.subType;
        }
        console.log($routeParams.type + "   " + TemplateInfoModel.type_dataEntry + "   " + $routeParams.subType + "   " + TemplateInfoModel.subType_Transcription);
    });

});
