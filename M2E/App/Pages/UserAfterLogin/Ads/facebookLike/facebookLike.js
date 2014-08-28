'use strict';
define([appLocation.userPostLogin], function (app) {

    app.controller('UserAfterLoginFacebookLike', function ($scope, $http, $route, $rootScope, CookieUtil, $sce) {
        $scope.logoImage = { url: logoImage };
        $('title').html("index"); //TODO: change the title so cann't be tracked in log

        $scope.facebookLikeIframe = "";
        $scope.facebookLikePageUrl = "";
        $scope.facebookLikePageId = "";

        $scope.showFacebookDetailDiv = false;
        $scope.facebookData = {};
        $scope.FacebookLikeList = [];
        var url = ServerContextPah + '/User/GetAllFacebookLikeTemplateInformation';
        var headers = {
            'Content-Type': 'application/json',
            'UTMZT': CookieUtil.getUTMZT(),
            'UTMZK': CookieUtil.getUTMZK(),
            'UTMZV': CookieUtil.getUTMZV()
        };
        startBlockUI('wait..', 3);
        $http({
            url: url,
            method: "POST",
            data: "",
            headers: headers
        }).success(function (data, status, headers, config) {
            //$scope.persons = data; // assign  $scope.persons here as promise is resolved here
            stopBlockUI();
            if (data.Status == "200") {
                var i = 0;
                console.log(data.Payload);
                $.each(data.Payload, function (key, value) {
                    $scope.FacebookLikeList[i] = this;
                    $scope.FacebookLikeList[i].pageUrl = $sce.trustAsHtml(this.pageUrl);
                    i++;
                });
                console.log($scope.FacebookLikeList);
            }
            else if (data.Status == "401") {
                location.href = "/?type=info&mssg=your session is expired/#/login";
            }

        }).error(function (data, status, headers, config) {

        });

        $scope.toggleActiveThreads = function () {
            console.log($scope.showUserActiveThreads);
            if ($scope.showUserActiveThreads == true)
                $scope.showUserActiveThreads = false;
            else
                $scope.showUserActiveThreads = true;

        }

    });

});
