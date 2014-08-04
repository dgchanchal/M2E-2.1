'use strict';
define([appLocation.postLogin], function (app) {

    app.controller('ClientAfterLoginTemplateSample', function ($scope, $http, $rootScope, $routeParams, CookieUtil) {
        $('title').html("sample page"); //TODO: change the title so cann't be tracked in log
        $scope.type = $routeParams.type;
        $scope.subType = $routeParams.subType;
    });

});
