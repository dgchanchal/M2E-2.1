'use strict';
define([appLocation.userPostLogin], function (app) {

    app.controller('UserAfterLoginRefrralPage', function ($scope, $http, $rootScope, CookieUtil) {
        $('title').html("edit page"); //TODO: change the title so cann't be tracked in log

        $scope.userGuid = "76b8a359-aea0-4d1c-9db7-4d90d1ebf274";
    });

});

