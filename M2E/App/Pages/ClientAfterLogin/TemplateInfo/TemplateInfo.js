'use strict';
define([appLocation.postLogin], function (app) {

    app.controller('ClientAfterLoginTemplateInfo', function ($scope, $http, $rootScope, $routeParams, CookieUtil, $route) {
        $('title').html("Template Info page"); //TODO: change the title so cann't be tracked in log
        $scope.templateId = $routeParams.templateId;
        //console.log("template info page");
        initializeClientChart();
        function initializeClientChart() {
            var url = ServerContextPah + '/Client/GetTemplateInformationByRefKey?id=' + $routeParams.templateId;
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
                    $scope.templateInfo = data.Payload;
                    //$scope.templateTitle = data.Payload.title;

                    var JobTotal_int = parseInt(data.Payload.JobTotal);
                    var JobReviewed_int = parseInt(data.Payload.JobReviewed);
                    var JobAssigned_int = parseInt(data.Payload.JobAssigned);
                    var JobCompleted_int = parseInt(data.Payload.JobCompleted);
                    if (JobCompleted_int > JobTotal_int)
                        JobCompleted_int = JobTotal_int;
                    var JobRemaining_int = JobTotal_int - JobCompleted_int - JobAssigned_int;
                    var JobReviewRemaining = JobTotal_int - JobReviewed_int;

                    //                console.log("JobTotal_int  : " + JobTotal_int);
                    //                console.log("JobReviewed_int  : " + JobReviewed_int);
                    //                console.log("JobAssigned_int  : " + JobAssigned_int);
                    //                console.log("JobCompleted_int  : " + JobCompleted_int);
                    //                console.log("JobRemaining_int  : " + JobRemaining_int);
                    //                console.log("JobReviewRemaining  : " + JobReviewRemaining);

                    render_container_highcharts_completed_vs_reviewed(data.Payload.editId, JobTotal_int, JobReviewed_int, JobReviewRemaining);
                    render_container_highcharts_completed_vs_assigned_vs_remaining(data.Payload.editId, JobCompleted_int, JobAssigned_int, JobRemaining_int);
                    render_container_highcharts_horizontal_bar_chart_ratio_completed_reviewed_remaining(data.Payload.editId, JobCompleted_int, JobAssigned_int, JobReviewed_int, JobRemaining_int, JobTotal_int);
                }
            }).error(function (data, status, headers, config) {

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

        $scope.openTemplateEditPageWithId = function (id) {
            //$('#closeModalPopup' + id).click();
            //alert(id);
            location.href = "#/editTemplate/edit/" + id;
        }

        $scope.openTemplateResponseDetailPageWithId = function () {
            //$('#closeModalPopup' + id).click();
            //alert(id);
            if ($scope.templateInfo.type == TemplateInfoModel.type_dataEntry && $scope.templateInfo.subType == TemplateInfoModel.subType_Transcription)
                location.href = "#/transcriptionResponseDetail/" + $scope.templateInfo.type + "/" + $scope.templateInfo.subType + "/" + $scope.templateInfo.editId;
            else if ($scope.templateInfo.type == TemplateInfoModel.type_moderation && $scope.templateInfo.subType == TemplateInfoModel.subType_imageModeration)
                location.href = "#/moderatingPhotosResponseDetail/" + $scope.templateInfo.type + "/" + $scope.templateInfo.subType + "/" + $scope.templateInfo.editId;
            else
                location.href = "#/templateResponseDetail/" + $scope.templateInfo.type + "/" + $scope.templateInfo.subType + "/" + $scope.templateInfo.editId;
        }
    });

});
