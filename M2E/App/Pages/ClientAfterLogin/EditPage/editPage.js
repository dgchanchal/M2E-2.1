'use strict';
define([appLocation.postLogin], function (app) {

    app.controller('ClientAfterLoginEditPage', function ($scope, $http, $rootScope, CookieUtil) {
        $('title').html("edit page"); //TODO: change the title so cann't be tracked in log
        
    });

});
