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
                $scope.TranscriptionTemplateInfo.optionsList = data.Payload.options.split(';');

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
            var Image_Moderation_value
            if(document.querySelector('input[name="Image_Moderation"]:checked') != null)
                Image_Moderation_value = document.querySelector('input[name="Image_Moderation"]:checked').value;
            else
                Image_Moderation_value =-1;
//            for (var i = 0; i < Image_Moderation.length; i++) {
//                if (Image_Moderation[i].checked) {
//                    Image_Moderation_value = Image_Moderation[i].value;
//                }
//            }

            console.log(Image_Moderation_value);
            if (Image_Moderation_value != -1) {
                var url = ServerContextPah + '/User/SubmitImageModerationInputTableDataByRefKey?refKey=' + $routeParams.refKey + '&data=' + Image_Moderation_value;
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
                    //$scope.persons = data; // assign  $scope.persons here as promise is resolved here
                    stopBlockUI();
                    if (data.Status == "200") {
                        showToastMessage("Success", "Successfully Submitted");
                    }

                }).error(function (data, status, headers, config) {

                });
            }
            else {
                showToastMessage("Error", "Please Select one option !!");
            }

        }
       


        // Instantiate models which will be passed to <panzoom> and <panzoomwidget>

        // The panzoom config model can be used to override default configuration values
        $scope.panzoomConfig = {
            zoomLevels: 12,
            neutralZoomLevel: 5,
            scalePerZoomLevel: 1.5
        };

        // The panzoom model should initialle be empty; it is initialized by the <panzoom>
        // directive. It can be used to read the current state of pan and zoom. Also, it will
        // contain methods for manipulating this state.
        $scope.panzoomModel = {};

    });

});

