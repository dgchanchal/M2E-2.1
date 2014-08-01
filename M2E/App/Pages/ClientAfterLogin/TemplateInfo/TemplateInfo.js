'use strict';
define([appLocation.postLogin], function (app) {

    app.controller('ClientAfterLoginTemplateInfo', function ($scope, $http, $rootScope, CookieUtil) {
        $('title').html("Template Info page"); //TODO: change the title so cann't be tracked in log
        
    });

});
