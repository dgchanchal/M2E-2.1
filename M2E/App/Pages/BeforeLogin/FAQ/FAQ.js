'use strict';
define([appLocation.preLogin], function (app) {
    app.controller('beforeLoginFAQ', function ($scope, $location, $http, $rootScope, CookieUtil, $anchorScroll, $sce) {
        $scope.scrollTo = function (id) {
            $anchorScroll();
            $('#' + id).click();
            $location.hash(id);
            //$('#' + id).click();        
        };

        $scope.FAQOverviewList = FAQOverviewList;

        $scope.FAQGeneralList = FAQGeneralList;

        $scope.FAQRequesterGeneralPoliciesList = FAQRequesterGeneralPoliciesList;

        $scope.FAQRequesterPaymentList = FAQRequesterPaymentList;

        $scope.FAQRequesterCITRelatedtList = FAQRequesterCITRelatedtList;

        $scope.FAQAccepterList = FAQAccepterList;

        $scope.FAQAccepterPayList = FAQAccepterPayList;
    });
});

