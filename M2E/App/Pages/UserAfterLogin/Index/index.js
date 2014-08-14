'use strict';
define([appLocation.userPostLogin], function (app) {

    app.controller('UserAfterLoginIndex', function ($scope, $http, $route, $rootScope, CookieUtil) {
        $scope.logoImage = { url: logoImage };
        $('title').html("index"); //TODO: change the title so cann't be tracked in log

        //$scope.InProgressTaskList = [{ showEllipse: true, title: "my first template", timeShowType: "info", showTime: "5 hours", editId: "", creationDate: "an 2014" },
        //    { showEllipse: true, title: "my second template", timeShowType: "danger", showTime: "2 hours", editId: "", creationDate: "feb 2013" },
        //    { showEllipse: true, title: "my third template", timeShowType: "warning", showTime: "1 day", editId: "", creationDate: "march 3023" },
        //    { showEllipse: true, title: "my fourth template", timeShowType: "success", showTime: "3 days", editId: "", creationDate: "aug 1203" },
        //    { showEllipse: true, title: "my fifth template", timeShowType: "default", showTime: "5 hours", editId: "", creationDate: "nov 2015" }
        //];
        var url = ServerContextPah + '/User/GetAllTemplateInformation';
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

        }).error(function (data, status, headers, config) {

        });
        

    });

});
