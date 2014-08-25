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

        var url = ServerContextPah + '/User/GetTranscriptionTemplateInformationByRefKey?refKey=59d86207-6372-4401-b976-93f7e57190601638';
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

        
        function submitTranscriptionInputTableDataToServer(data) {
            var url = ServerContextPah + '/User/SubmitTranscriptionInputTableDataByRefKey?refKey=' + $routeParams.refKey;
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
                data: data,
                headers: headers
            }).success(function (data, status, headers, config) {
                //$scope.persons = data; // assign  $scope.persons here as promise is resolved here
                stopBlockUI();
                if (data.Status == "200") {
                    alert("successfully submitted");
                }

            }).error(function (data, status, headers, config) {

            });
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

