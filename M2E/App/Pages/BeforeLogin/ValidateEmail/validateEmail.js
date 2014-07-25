'use strict';
define([appLocation.preLogin], function(app) {
    app.controller('validateEmailTemplate', function ($scope, $http, $routeParams, $location) {
        var
            accountRequest = {
                userName: $routeParams.userName,
                guid: $routeParams.guid
            };
        $scope.Header = {
            message: '',
            className: '',
            iconClassName: ''
        };
        $scope.Content = {
            header1: '',
            header2: '',
            contentClasstheme: '',
            header2IconClassName: '',
            message: '',
            companyName: ''
        };
        $http({
            url: '/Auth/ValidateAccount',
            method: "POST",
            data: accountRequest,
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data, status, headers, config) {
            //$scope.persons = data; // assign  $scope.persons here as promise is resolved here
            if (data.Status == "200") {
                $scope.Header.message = "Account Validated";
                $scope.Header.className = "alert-success";
                $scope.Header.iconClassName = "fa-check";
                $scope.Content.header1 = "Welcome to";
                $scope.Content.companyName = "MadeToEarn";
                $scope.Content.contentClasstheme = "callout-info";
                $scope.Content.header2IconClassName = "fa fa-info";
                $scope.Content.header2 = "Thanks for choosing MadeToEarn.";
                $scope.Content.message = "Your Account is successfully verified.";
            }
            else if (data.Status == "500") {
                $scope.Header.message = "Internal Error";
                $scope.Header.className = "alert-danger";
                $scope.Header.iconClassName = "fa-ban";
                $scope.Content.header1 = "500";
                $scope.Content.companyName = "Error";
                $scope.Content.contentClasstheme = "callout-danger";
                $scope.Content.header2IconClassName = "fa fa-warning text-yellow";
                $scope.Content.header2 = "Oops! Something went wrong.";
                $scope.Content.message = "We could not find the page you were looking for. we will soon fix fix it. Sorry for inconvenience.";
            } else {
                $scope.Header.message = "Link Expired";
                $scope.Header.className = "alert-warning";
                $scope.Header.iconClassName = "fa-warning";
                $scope.Content.header1 = "";
                $scope.Content.companyName = "";
                $scope.Content.contentClasstheme = "callout-warning";
                $scope.Content.header2IconClassName = "fa fa-warning text-yellow";
                $scope.Content.header2 = "Oops! You are trying to access expired link.";
                $scope.Content.message = "You have already used the link to validate your account.";
            }
            console.log(data);
        }).error(function (data, status, headers, config) {

        });
    });



});

