/**
 * Simple bootstrapper to load all the pre-requisite AngularJS dependencies needed by Login SPA [Single Page Application]
 * @class userPostLoginInit
 * @module userPostLogin
 */
define(['angular','domReady'], function () {

    var dependances = ['restangular','angularFileUpload', 'panzoom', 'panzoomwidget', 'ngRoute', 'ngAnimate', 'ngSanitize'];
    var app = angular.module("afterLoginUserApp", dependances);
    return app;
});
