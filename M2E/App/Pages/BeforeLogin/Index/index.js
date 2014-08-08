'use strict';
define([appLocation.preLogin], function (app) {
    app.controller('beforeLoginIndex', function ($scope, $http, $rootScope, CookieUtil) {
        $('title').html("index"); //TODO: change the title so cann't be tracked in log

        $scope.totalProjects = "124";
        $scope.successRate = "91";
        $scope.totalUsers = "3423";
        $scope.projectCategories = "25";



        $scope.updateBeforeLoginUserProjectDetailsDiv = function (totalProjects, successRate, totalUsers, projectCategories) {
            //alert("inside angular js function updateBeforeLoginUserProjectDetailsDiv");
            $scope.totalProjects = totalProjects;
            $scope.successRate = successRate;
            $scope.totalUsers = totalUsers;
            $scope.projectCategories = projectCategories;

        }
    });
});



			

