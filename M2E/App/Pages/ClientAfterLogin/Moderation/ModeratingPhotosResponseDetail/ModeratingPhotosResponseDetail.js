'use strict';
define([appLocation.postLogin], function (app) {

    app.controller('ClientAfterLoginModeratingPhotosResponseDetail', function ($scope, $http, $rootScope, $routeParams, CookieUtil, $route) {
        $('title').html("Transcription Info page"); //TODO: change the title so cann't be tracked in log
        $scope.templateId = $routeParams.templateId;

        initializeGetTranscriptionResponseResultById();

        function initializeGetTranscriptionResponseResultById() {
            var url = ServerContextPah + '/Client/GetAllCompletedTranscriptionInformation?id=' + $routeParams.templateId;
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
                stopBlockUI();
                if (data.Status == "200") {
                    $scope.AllCompletedTranscriptions = data.Payload;
                    $scope.AllCompletedTranscriptions.optionsList = data.Payload.options.split(';');
                }
            }).error(function (data, status, headers, config) {
                console.log("failure");
            });
        };


        $scope.deleteTemplatePageWithId = function (id) {
            var url = ServerContextPah + '/Client/DeleteTemplateDetailById?id=' + id;
            var headers = {
                'Content-Type': 'application/json',
                'UTMZT': CookieUtil.getUTMZT(),
                'UTMZK': CookieUtil.getUTMZK(),
                'UTMZV': CookieUtil.getUTMZV()
            };
            if (confirm("Template will be permanently deleted. Are you sure?") == true) {
                startBlockUI('wait..', 3);
                $http({
                    url: url,
                    method: "POST",
                    headers: headers
                }).success(function (data, status, headers, config) {
                    //$scope.persons = data; // assign  $scope.persons here as promise is resolved here

                    if (data.Status == "200") {
                        stopBlockUI();
                        showToastMessage("Success", "Deleted Successfully");
                        location.href = "#/";
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

                });
            } else {

            }


        }

        $scope.downloadTemplateInfoPageWithId = function (id) {
            window.location = '/Client/DownloadAllCompletedTranscriptionInformation?id=' + id + '&guid=' + CookieUtil.getUTMZT() + '';
        }

        $scope.openTemplateEditPageWithId = function (id) {
            //$('#closeModalPopup' + id).click();
            //alert(id);
            location.href = "#/editTemplate/edit/" + id;
        }

        $scope.openTemplateInfoPageWithId = function (id) {
            //$('#closeModalPopup' + id).click();
            //alert(id);
            location.href = "#/templateInfo/Survey/ProductSurvey/" + id;
        }
    });

});