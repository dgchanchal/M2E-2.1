'use strict';
define([appLocation.postLogin], function (app) {

    app.controller('ClientAfterLoginIndex', function ($scope, $http, $route, $rootScope, CookieUtil, ngTableParams) {
        $scope.logoImage = { url: logoImage };
        $scope.isMobile = false;
        $scope.InProgressTaskList = [];
        if (mobileDevice)
            $scope.isMobile = true;

        $('title').html("index"); //TODO: change the title so cann't be tracked in log

        //$scope.InProgressTaskList = [{ showEllipse: true, title: "my first template", timeShowType: "info", showTime: "5 hours", editId: "", creationDate: "an 2014" },
        //    { showEllipse: true, title: "my second template", timeShowType: "danger", showTime: "2 hours", editId: "", creationDate: "feb 2013" },
        //    { showEllipse: true, title: "my third template", timeShowType: "warning", showTime: "1 day", editId: "", creationDate: "march 3023" },
        //    { showEllipse: true, title: "my fourth template", timeShowType: "success", showTime: "3 days", editId: "", creationDate: "aug 1203" },
        //    { showEllipse: true, title: "my fifth template", timeShowType: "default", showTime: "5 hours", editId: "", creationDate: "nov 2015" }
        //];

        loadTaskInProgressTable();

        function loadTaskInProgressTable() {
            var url = ServerContextPah + '/Client/GetAllTemplateInformation';
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
                    $scope.InProgressTaskList = data.Payload;
                }
                else if (data.Status == "401") {
                    location.href = "/?type=info&mssg=your session is expired/#/login";
                }

            }).error(function (data, status, headers, config) {

            });
        }


        $scope.tableParams = new ngTableParams({
            page: 1,            // show first page
            count: 5           // count per page
        }, {
            total: $scope.InProgressTaskList.length, // length of data
            getData: function ($defer, params) {
                $defer.resolve($scope.InProgressTaskList.slice((params.page() - 1) * params.count(), params.page() * params.count()));
            }
        });


        $scope.openTemplateEditPageWithId = function (id) {
            //$('#closeModalPopup' + id).click();
            //alert(id);
            location.href = "#/editTemplate/edit/" + id;
        }

        $scope.openTemplateInfoPageWithId = function (type, subType, id) {
            //$('#closeModalPopup' + id).click();
            //alert(id);            
            location.href = "#/templateInfo/" + type + "/" + subType + "/" + id;
        }

        $scope.deleteTemplateEditPageWithId = function (type, subType, id) {
            $('#closeModalPopup' + id).click();
            var url = ServerContextPah + '/Client/DeleteTemplateDetailById?id=' + id + '&type=' + type + '&subType=' + subType;
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
                        loadTaskInProgressTable();
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
    });

});
