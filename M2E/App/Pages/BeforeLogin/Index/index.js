'use strict';
define([appLocation.preLogin], function(app) {
    app.controller('beforeLoginIndex', function ($scope, $http, $rootScope, CookieUtil) {
        $('title').html("index"); //TODO: change the title so cann't be tracked in log

    });
});

