'use strict';
define([appLocation.userPostLogin], function (app) {

    app.controller('UserAfterLoginAngularTranscriptionTemplate', function ($scope, $http, $rootScope, $routeParams, CookieUtil) {
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
                createDynamicTableForInput($scope.TranscriptionTemplateInfo.optionsList, 1, false);
                //$('#PanZoom').css("height", "600px");
            }

        }).error(function (data, status, headers, config) {

        });

        $scope.AddRowTranscriptionInputTableBox = function () {
            tableCol = 1;
            var dynamicInputTableHTML = "";
            dynamicInputTableHTML += "<tr>";
            var i = optionsList.length;
            $.each(optionsList, function () {
                if (i != tableCol)
                    dynamicInputTableHTML += "<td><input class='form-control' name='TranscriptionInput-" + tableRow + "-" + tableCol + "' type='text' placeholder='" + this + "' id='TranscriptionInput-" + tableRow + "-" + tableCol + "'/></td>";
                else
                    dynamicInputTableHTML += "<td><input class='form-control transcriptionTemplateInputTextBox" + tableRow + "' name='TranscriptionInput-" + tableRow + "-" + tableCol + "' type='text' placeholder='" + this + "' id='TranscriptionInput-" + tableRow + "-" + tableCol + "'/></td>";
                tableCol++;
            });
            dynamicInputTableHTML += "</tr>";
            tableRow++;
        }

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

        $scope.AddRowTranscriptionInputTableBox = function () {
            createDynamicTableForInput($scope.TranscriptionTemplateInfo.optionsList, 1, true);
            if (tableRow > 10) {
                zoomImageHeight += 60;
                $('#PanZoom').css({ "height": zoomImageHeight + 'px' });
            }
        }


        $scope.SubmitTranscriptionInputTableData = function () {

            for (var i = 1; i <= tableRow; i++) {
                var UserInputData = [];
                for (var j = 1; j <= $scope.TranscriptionTemplateInfo.optionsList.length; j++) {
                    UserInputData.push($('#TranscriptionInput-' + i + '-' + j + '').val());
                }
                $scope.TranscriptionImageUserInputResponse.push(UserInputData);
            }
            submitTranscriptionInputTableDataToServer($scope.TranscriptionImageUserInputResponse);
            //console.log($scope.TranscriptionImageUserInputResponse);
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
                    location.href = "#/";
                }

            }).error(function (data, status, headers, config) {

            });
        }
        function initializeEnterKeyEventOnTextBox() {

            $('.transcriptionTemplateInputTextBox' + (tableRow - 1)).keydown(function (e) {
                if (e.keyCode == 13 || e.keyCode == 9) {
                    createDynamicTableForInput($scope.TranscriptionTemplateInfo.optionsList, 1, true);
                    if (tableRow > 10) {
                        zoomImageHeight += 60;
                        $('#PanZoom').css({ "height": zoomImageHeight + 'px' });
                    }
                    //console.log(tableRow - 1);
                    //$("#TranscriptionInput-2-1").focus();
                }
            })
        }
        function createDynamicTableForInput(optionsList, count, isAppend) {
            var dynamicInputTableHTML = "";
            var k = $scope.TranscriptionTemplateInfo.optionsList.length;
            if (!isAppend) {
                dynamicInputTableHTML += "<b> title of job</b>";
                dynamicInputTableHTML += "<table class='table table-bordered'>";
                dynamicInputTableHTML += "<tbody>";

                dynamicInputTableHTML += "<tr>";
                dynamicInputTableHTML += "<th style='width: 10px'>#</th>";
                $.each(optionsList, function () {
                    dynamicInputTableHTML += "<th>" + this + "</th>";
                });
                dynamicInputTableHTML += "</tr>";

                tableRow = 1;
                for (var j = 1; j <= count; j++) {
                    tableCol = 1;
                    dynamicInputTableHTML += "<tr>";
                    $.each(optionsList, function () {
                        if (tableCol == 1)
                            dynamicInputTableHTML += "<td>" + tableRow + ".</td>";

                        if (k != tableCol)
                            dynamicInputTableHTML += "<td><input class='form-control' name='TranscriptionInput-" + tableRow + "-" + tableCol + "' type='text' placeholder='" + this + "' id='TranscriptionInput-" + tableRow + "-" + tableCol + "'/></td>";
                        else
                            dynamicInputTableHTML += "<td><input class='form-control transcriptionTemplateInputTextBox" + tableRow + "' name='TranscriptionInput-" + tableRow + "-" + tableCol + "' type='text' placeholder='" + this + "' id='TranscriptionInput-" + tableRow + "-" + tableCol + "'/></td>";
                        tableCol++;
                    });
                    dynamicInputTableHTML += "</tr>";
                    tableRow++;
                }

                dynamicInputTableHTML += "</tbody>";
                dynamicInputTableHTML += "</table>";

                $('#TranscriptionInputTableBoxId').html(dynamicInputTableHTML);
            }
            else {
                dynamicInputTableHTML += "<table class='table table-bordered'>";
                dynamicInputTableHTML += "<tbody>";

                for (var j = 1; j <= count; j++) {
                    tableCol = 1;
                    dynamicInputTableHTML += "<tr>";
                    $.each(optionsList, function () {
                        if (tableCol == 1)
                            dynamicInputTableHTML += "<td>" + tableRow + ".</td>";

                        if (k != tableCol)
                            dynamicInputTableHTML += "<td><input class='form-control' name='TranscriptionInput-" + tableRow + "-" + tableCol + "' type='text' placeholder='" + this + "' id='TranscriptionInput-" + tableRow + "-" + tableCol + "'/></td>";
                        else
                            dynamicInputTableHTML += "<td><input class='form-control transcriptionTemplateInputTextBox" + tableRow + "' name='TranscriptionInput-" + tableRow + "-" + tableCol + "' type='text' placeholder='" + this + "' id='TranscriptionInput-" + tableRow + "-" + tableCol + "'/></td>";

                        tableCol++;
                    });
                    dynamicInputTableHTML += "</tr>";
                    tableRow++;
                }

                dynamicInputTableHTML += "</tbody>";
                dynamicInputTableHTML += "</table>";

                $('#TranscriptionInputTableBoxId').append(dynamicInputTableHTML);
            }

            initializeEnterKeyEventOnTextBox();

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
