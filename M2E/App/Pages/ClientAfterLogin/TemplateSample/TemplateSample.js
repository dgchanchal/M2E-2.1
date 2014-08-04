'use strict';
define([appLocation.postLogin], function (app) {

    app.controller('ClientAfterLoginTemplateSample', function ($scope, $http, $rootScope, $routeParams, CookieUtil) {
        $('title').html("sample page"); //TODO: change the title so cann't be tracked in log
        $scope.type = $routeParams.type;
        $scope.subType = $routeParams.subType;
        if ($routeParams.type == "survey") {
            if ($routeParams.subType == "productSurvey") {
                $scope.TemplateUrl = "#/createTemplate";
            }
        }
        else if ($routeParams.type == "moderation") {
            if ($routeParams.subType == "moderatingPhotos") {
                $scope.TemplateUrl = "#/moderatingPhotos";
            }
        }
        else if ($routeParams.type == "dataEntry") {
            if ($routeParams.subType == "transcription") {
                $scope.TemplateUrl = "#/transcriptionTemplate";
            }
        }
    });

});
