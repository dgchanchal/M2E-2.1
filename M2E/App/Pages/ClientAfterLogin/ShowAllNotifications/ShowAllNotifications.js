'use strict';
define([appLocation.postLogin], function (app) {

    app.controller('ClientAfterLoginAllNotifications', function ($scope, $http, $rootScope, CookieUtil) {
        $('title').html("edit page"); //TODO: change the title so cann't be tracked in log
        $scope.referralKey = "init";
        var url = ServerContextPah + '/Client/GetAllNotifications?userType=client';
        var headers = {
            'Content-Type': 'application/json',
            'UTMZT': $.cookie('utmzt'),
            'UTMZK': $.cookie('utmzk'),
            'UTMZV': $.cookie('utmzv')
        };
        $.ajax({
            url: url,
            method: "GET",
            headers: headers
        }).done(function (data, status) {
            console.log(data.Status == "200");
            if (data.Status == "200") {                
                $scope.$apply(function () {
                    $scope.ClientAllNotificationsDetail = data.Payload;
                });
                
            }
            else {
                console.log("false");
            }
        });
    });

});

