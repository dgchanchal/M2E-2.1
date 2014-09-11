'use strict';
define([appLocation.userPostLogin], function (app) {

    app.controller('UserAfterLoginImageModeration', function ($scope, $http, $route, $rootScope, CookieUtil, $routeParams) {
        $('title').html("sample page"); //TODO: change the title so cann't be tracked in log
        console.log("angular transcription template");
        var tableRow = 1;
        var tableCol = 1;
        var zoomImageHeight = 600;
        $scope.TranscriptionImageWidthClass = "col-md-7";
        $scope.TranscriptionInputWidthClass = "col-md-5";
        $scope.TranscriptionRowToggleText = "Align in Two Rows";
        $scope.TranscriptionRowToggleButtonClass = "btn btn-warning btn-flat";

        $scope.TranscriptionImageUserInputResponse = [];

        var url = ServerContextPah + '/User/GetImageModerationTemplateInformationByRefKey?refKey=' + $routeParams.refKey;
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
                $scope.TranscriptionTemplateInfo = data.Payload;
                var i = 0;
                $.each(data.Payload, function (key, value) {
                    $scope.TranscriptionTemplateInfo[i].optionsList = this.options.split(';');
                    i++;
                });


                //$('#PanZoom').css("height", "600px");
            }

        }).error(function (data, status, headers, config) {

        });


        $scope.AlignTranscriptionBoxToggle = function () {
            //console.log($scope.TranscriptionRowToggleText);
            if ($scope.TranscriptionImageWidthClass == "col-md-12") {
                $scope.TranscriptionImageWidthClass = "col-md-7";
                $scope.TranscriptionInputWidthClass = "col-md-5";
                $scope.TranscriptionRowToggleText = "Align in Two Rows";
                $scope.TranscriptionRowToggleButtonClass = "btn btn-warning btn-flat";
            }
            else {
                $scope.TranscriptionImageWidthClass = "col-md-12";
                $scope.TranscriptionInputWidthClass = "col-md-12";
                $scope.TranscriptionRowToggleText = "Align in Single Rows";
                $scope.TranscriptionRowToggleButtonClass = "btn btn-primary btn-flat";
            }
        }

        $scope.SubmitImageModerationInputTableData = function () {
            SubmitImageModerationInputTableData();
        }

        function SubmitImageModerationInputTableData() {
            //var Image_Moderation = document.getElementsByName('Image_Moderation');
            var Image_Moderation_value = [];
            var isAllImageValidated = true;
            $.each($scope.TranscriptionTemplateInfo, function (key, value) {
                var temp = { imageUrl: this.imageUrl, selectedIndex: "" };
                if (document.querySelector('input[name="' + this.imageUrl + '"]:checked') != null)
                    temp.selectedIndex = document.querySelector('input[name="' + this.imageUrl + '"]:checked').value;
                else
                    isAllImageValidated = false;

                Image_Moderation_value.push(temp);
            });
            

            console.log(Image_Moderation_value);
            if (isAllImageValidated) {
                var url = ServerContextPah + '/User/SubmitImageModerationInputTableDataByRefKey?refKey=' + $routeParams.refKey;
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
                    data:Image_Moderation_value,
                    headers: headers
                }).success(function (data, status, headers, config) {
                    //$scope.persons = data; // assign  $scope.persons here as promise is resolved here
                    stopBlockUI();
                    if (data.Status == "200") {
                        showToastMessage("Success", "Successfully Submitted");
                        location.href = "#/";
                    }

                }).error(function (data, status, headers, config) {

                });
            }
            else {
                showToastMessage("Error", "Please Select one option !!");
            }

        }




    });

});

