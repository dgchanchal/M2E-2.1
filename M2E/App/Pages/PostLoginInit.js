/**
 * Simple bootstrapper to load all the pre-requisite AngularJS dependencies needed by Login SPA [Single Page Application]
 * @class PostLoginInit
 * @module PostLogin
 */
define(['angular','domReady'], function() {

    var dependances = ['restangular', 'angularFileUpload', 'ngTable', 'ngRoute', 'ngAnimate', 'ngSanitize'];
    var app = angular.module("afterLoginClientApp", dependances);
    return app;
});
