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
                render_container_highcharts_completed_vs_reviewed(data.Payload.editId, parseInt(data.Payload.JobTotal), parseInt(data.Payload.JobReviewed), parseInt(data.Payload.JobTotal) - parseInt(data.Payload.JobReviewed));
                render_container_highcharts_completed_vs_assigned_vs_remaining(data.Payload.editId,parseInt(data.Payload.JobCompleted), parseInt(data.Payload.JobAssigned), parseInt(data.Payload.JobTotal - data.Payload.JobCompleted));
                render_container_highcharts_horizontal_bar_chart_ratio_completed_reviewed_remaining(data.Payload.editId,parseInt(data.Payload.JobCompleted), parseInt(data.Payload.JobAssigned), parseInt(data.Payload.JobReviewed), parseInt(data.Payload.JobTotal - data.Payload.JobCompleted), parseInt(data.Payload.JobTotal));
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

    });

});
