'use strict';
define([appLocation.userPostLogin], function (app) {

    app.controller('UserAfterLoginAngularTranscriptionTemplate', function ($scope, $http, $rootScope, $routeParams, CookieUtil) {
        $('title').html("sample page"); //TODO: change the title so cann't be tracked in log
        console.log("angular transcription template");
        var tableRow=1;
        var tableCol=1;
        var url = ServerContextPah + '/User/GetTranscriptionTemplateInformationByRefKey?refKey=' + $routeParams.refKey;
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
                    createDynamicTableForInput($scope.TranscriptionTemplateInfo.optionsList);
                }

            }).error(function (data, status, headers, config) {

            });


            function createDynamicTableForInput(optionsList)
            {
                var dynamicInputTableHTML ="";
                 dynamicInputTableHTML += "<b> title of job</b>";
                 dynamicInputTableHTML += "<table class='table table-bordered'>";
                 dynamicInputTableHTML +="<tbody>";

                 dynamicInputTableHTML +="<tr>";
                 dynamicInputTableHTML +="<th style='width: 10px'>#</th>";                 
                 $.each(optionsList, function () {                 
                    dynamicInputTableHTML +="<th>"+this+"</th>";                    
                 });                                                                     
                 dynamicInputTableHTML +="</tr>";

                 dynamicInputTableHTML +="<tr>";
                 tableCol=1;
                 $.each(optionsList, function () {                 
                    if(tableCol == 1)
                        dynamicInputTableHTML +="<td>"+tableRow+".</td>";
                    dynamicInputTableHTML +="<td ><input type='text' placeholder='"+this+"' id='TranscriptionInput-"+tableRow+"-"+tableCol+"'/></td>";
                    tableCol++;                     
                 });
                 
                 dynamicInputTableHTML +="</tr>";
                 dynamicInputTableHTML +="</tbody>";
                 dynamicInputTableHTML +="</table>";

                 

                 $('#TranscriptionInputTableBoxId').html(dynamicInputTableHTML);
            }
        var shark = {
            x: 391,
            y: 371,
            width: 206,
            height: 136
        };
        var chopper = {
            x: 88,
            y: 213,
            width: 660,
            height: 144
        };
        var ladder = {
            x: 333,
            y: 325,
            width: 75,
            height: 200
        };

        $scope.rects = [chopper, shark, ladder];

        // Instantiate models which will be passed to <panzoom> and <panzoomwidget>

        // The panzoom config model can be used to override default configuration values
        $scope.panzoomConfig = {
            zoomLevels: 12,
            neutralZoomLevel: 5,
            scalePerZoomLevel: 1.5,
            initialZoomToFit: shark
        };

        // The panzoom model should initialle be empty; it is initialized by the <panzoom>
        // directive. It can be used to read the current state of pan and zoom. Also, it will
        // contain methods for manipulating this state.
        $scope.panzoomModel = {};

        $scope.zoomToShark = function () {
            $scope.panzoomModel.zoomToFit(shark);
        };

        $scope.zoomToChopper = function () {
            $scope.panzoomModel.zoomToFit(chopper);
        };

        $scope.zoomToLadder = function () {
            $scope.panzoomModel.zoomToFit(ladder);
        };      
    });

});
