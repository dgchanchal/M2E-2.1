'use strict';
define([appLocation.userPostLogin], function (app) {

    //getting user info..
    app.controller('showTemplateDetailController', function ($scope, $http, $rootScope, $routeParams, CookieUtil) {

        $scope.TemplateDetailShowRulesAndRegulationInfoDiv = false;
        $scope.userShowTemplateDetailsIWillDoItAndReportAbuseButton = true;
        var url = ServerContextPah + '/User/GetTemplateInformationByRefKey?refKey=' + $routeParams.refKey;
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
            data: "",
            headers: headers
        }).success(function (data, status, headers, config) {
            //$scope.persons = data; // assign  $scope.persons here as promise is resolved here
            stopBlockUI();
            if (data.Status == "200") {
                $scope.TemplateInfo = data.Payload;
            }

        }).error(function (data, status, headers, config) {

        });

        $scope.TemplateDetailShowRulesAndRegulationInfoDivFunction = function () {
            $scope.TemplateDetailShowRulesAndRegulationInfoDiv = true;
            $scope.userShowTemplateDetailsIWillDoItAndReportAbuseButton = false;
        }

        $scope.userShowTemplateDetailFinalAcceptance = function (type, subType, refKey) {
            var url = ServerContextPah + '/User/AllocateThreadToUserByRefKey?refKey=' + $routeParams.refKey;
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
                data: "",
                headers: headers
            }).success(function (data, status, headers, config) {
                //$scope.persons = data; // assign  $scope.persons here as promise is resolved here
                stopBlockUI();
                console.log(type + "  " + TemplateInfoModel.type_moderation + "   " + subType + "   " + TemplateInfoModel.subType_imageModeration);
                if (data.Status == "200") {
                    if (type == TemplateInfoModel.type_survey && subType == TemplateInfoModel.subType_productSurvey)
                        location.href = "#/startSurvey/" + refKey;
                    else if (type == TemplateInfoModel.type_dataEntry && subType == TemplateInfoModel.subType_Transcription) {
                        location.href = "#/startAngularTranscription/" + refKey;
                    }
                    else if (type == TemplateInfoModel.type_moderation && subType == TemplateInfoModel.subType_imageModeration) {
                        location.href = "#/imageModeration/" + refKey;
                    }
                    else {
                        location.href = "#/startSurvey/" + refKey;
                    }

                }
                else if (data.Status == "403") {
                    alert("already applied to this job");
                }

            }).error(function (data, status, headers, config) {

            });
        }


    });

});

