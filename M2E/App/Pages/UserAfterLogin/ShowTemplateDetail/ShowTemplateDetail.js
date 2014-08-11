'use strict';
define([appLocation.userPostLogin], function (app) {

    //getting user info..
    app.controller('showTemplateDetailController', function ($scope, $http, $rootScope, $routeParams, CookieUtil) {

        //alert("showTemplateDetail");
//        $scope.refKey = $routeParams.refKey;
//        $scope.type = $routeParams.type;
//        $scope.subType = $routeParams.subType;
//        $scope.title = $routeParams.title;
//        $scope.creationTime = $routeParams.creationTime;
//        $scope.earningPerThreads = $routeParams.earningPerThreads;
//        $scope.currency = $routeParams.currency;
//        $scope.totalThreads = $routeParams.totalThreads;
//        $scope.remainingThreads = $routeParams.remainingThreads;

        var url = ServerContextPah + '/User/GetTemplateInformationByRefKey?refKey=' + $routeParams.refKey;
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
                $scope.TemplateInfo = data.Payload;
            }

        }).error(function (data, status, headers, config) {

        });
    });




});

