/**
 * Simple bootstrapper to load all the pre-requisite AngularJS dependencies needed by Login SPA [Single Page Application]
 * @class PostLoginInit
 * @module PostLogin
 */
define(['angular'], function() {

    var dependances = ['restangular', 'angularFileUpload', 'ngTable'];
    var app = angular.module("afterLoginClientApp", dependances);
    return app;
});
