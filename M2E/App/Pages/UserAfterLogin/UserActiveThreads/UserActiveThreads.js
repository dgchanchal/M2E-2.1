'use strict';
define([appLocation.userPostLogin], function (app) {

    app.controller('UserAfterLoginActiveThreads', function ($scope, $http, $route, $rootScope, $routeParams, CookieUtil) {
        $scope.logoImage = { url: logoImage };
        $('title').html("index"); //TODO: change the title so cann't be tracked in log

        if ($routeParams.status == "active") {
            $scope.divClasstype = "danger";
            $scope.isActivePage = true;
        }
        else {
            $scope.divClasstype = "info";
            $scope.isActivePage = false;
        }

        var url = ServerContextPah + '/User/GetUserActiveThreads?status=' + $routeParams.status;
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
                $scope.ActiveThreadList = data.Payload;
            }

        }).error(function (data, status, headers, config) {

        });


        $scope.userStartSurvey = function (refKey) {
            location.href = "#/startSurvey/" + refKey;
        }

    });

});
