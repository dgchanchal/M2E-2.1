'use strict';
define([appLocation.preLogin], function (app) {

    app.controller('contactUsController', function ($scope, $http, $routeParams, $location) {

        $scope.ContactUsData = {
            Name :"",
            Email:"",
            Phone:"",
            Type:"",
            Message:"",
            SendMeACopy:true
        };
        $scope.showErrors = false;
        $scope.EmailIdAlert = {
            visible: false,
            message: ''
        };
        $scope.NameAlert = {
            visible: false,
            message: ''
        };
        $scope.PhoneAlert = {
            visible: false,
            message: ''
        };
        $scope.TypeAlert = {
            visible: false,
            message: ''
        };
        $scope.MessageAlert = {
            visible: false,
            message: ''
        };

        $scope.ContactUsRequestSubmit = function () {
            if ($scope.ContactUsData.Name != "") {
                $scope.NameAlert.visible = false;
            }
            else {
                $scope.NameAlert.visible = true;
                $scope.NameAlert.message = "Name field cannot be empty !!!";
            }

            if ($scope.ContactUsData.Phone != "") {
                $scope.PhoneAlert.visible = false;
            }
            else {
                $scope.PhoneAlert.visible = true;
                $scope.PhoneAlert.message = "Phone field cannot be empty !!!";
            }
            if (isValidEmailAddress($scope.ContactUsData.Email)) {
                $scope.EmailIdAlert.visible = false;
            }
            else {
                $scope.EmailIdAlert.visible = true;
                $scope.EmailIdAlert.message = "Incorrect Email Id !!!";
            }
            if ($scope.ContactUsData.Type != "") {
                $scope.TypeAlert.visible = false;
            }
            else {
                $scope.TypeAlert.visible = true;
                $scope.TypeAlert.message = "Subject type field cannot be emoty!!!";
            }

            if ($scope.ContactUsData.Message != "") {
                $scope.MessageAlert.visible = false;
            }
            else {
                $scope.MessageAlert.visible = true;
                $scope.MessageAlert.message = "Message field cannot be empty !!!";
            }

            if (!$scope.MessageAlert.visible && !$scope.NameAlert.visible && !$scope.PhoneAlert.visible && !$scope.EmailIdAlert.visible && !$scope.TypeAlert.visible) {
                startBlockUI('wait..', 3);
                $http({
                    url: '/Auth/ContactUs/',
                    data: $scope.ContactUsData,
                    method: "POST"
                }).success(function (data, status, headers, config) {
                    stopBlockUI();
                    if (data.Status == "200") {
                        location.href = "#/login/ConatctUs200";
                    }
                    else if (data.Status == "404") {
                        
                    }
                    else if (data.Status == "402") {
                        
                    }
                    else if (data.Status == "500") {
                        location.href = "/?email=" + $('#forgetPasswordInputBoxId').val() + "#/showmessage/3/";
                    }
                }).error(function (data, status, headers, config) {
                    alert("false");
                });
            } else {
                $scope.showErrors = true;
            }

        }

        $scope.HomeLink = function () {
            location.href = "/";
        }
    });

    function isValidEmailAddress(emailAddress) {
        var pattern = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
        return pattern.test(emailAddress);
    };
});

