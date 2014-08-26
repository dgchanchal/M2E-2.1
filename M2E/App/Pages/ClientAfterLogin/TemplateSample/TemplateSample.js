'use strict';
define([appLocation.postLogin], function (app) {

    app.controller('ClientAfterLoginTemplateSample', function ($scope, $http, $rootScope, $routeParams, CookieUtil) {
        $('title').html("sample page"); //TODO: change the title so cann't be tracked in log
        $scope.type = $routeParams.type;
        $scope.subType = $routeParams.subType;
        if ($routeParams.type == TemplateInfoModel.type_survey) {
            if ($routeParams.subType == TemplateInfoModel.subType_productSurvey) {
                $scope.TemplateUrl = "#/createTemplate";
            }
        }
        else if ($routeParams.type == TemplateInfoModel.type_moderation) {
            if ($routeParams.subType == TemplateInfoModel.subType_imageModeration) {
                $scope.TemplateUrl = "#/moderatingPhotos";
            }
        }
        else if ($routeParams.type == TemplateInfoModel.type_dataEntry) {
            if ($routeParams.subType == TemplateInfoModel.subType_Transcription) {
                $scope.TemplateUrl = "#/transcriptionTemplate";
            }
        }
    });

});
