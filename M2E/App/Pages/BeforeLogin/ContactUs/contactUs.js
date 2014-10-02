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

        $scope.ForgetPasswordContent = true;
        $scope.ForgetPasswordForm = true;
        $scope.ResendValidationOrSignup =
        {
            visible: false,
            title: '',
            buttonName: '',
            functionName: ''
        }
        $scope.ForgetPasswordAlertContent = {
            visible: false,
            message: ''
        }
        $scope.ForgetPasswordSendRequest = function () {
            if (isValidEmailAddress($('#forgetPasswordInputBoxId').val())) {
                startBlockUI('wait..', 3);
                $http({
                    url: '/Auth/ForgetPassword/' + $('#forgetPasswordInputBoxId').val(),
                    method: "GET"
                }).success(function (data, status, headers, config) {
                    stopBlockUI();
                    if (data.Status == "200") {
                        location.href = "/?email=" + $('#forgetPasswordInputBoxId').val() + "#/showmessage/2/";
                    }
                    else if (data.Status == "404") {
                        $scope.ForgetPasswordContent = false;
                        $scope.ForgetPasswordAlertContent.visible = true;
                        $scope.ForgetPasswordAlertContent.message = "Entered email id is not registerd with us. Please enter your email address which is registered with us to set new password.";
                    }
                    else if (data.Status == "402") {
                        $scope.ForgetPasswordContent = false;
                        $scope.ForgetPasswordForm = false;
                        $scope.ForgetPasswordAlertContent.visible = true; $scope.ForgetPasswordAlertContent.message = "Email Address-" + $('#forgetPasswordInputBoxId').val() + " is not valideted yet. please check your email for validation.";
                        $scope.ResendValidationOrSignup.visible = true;
                        $scope.ResendValidationOrSignup.title = "Don't have emaill address validation Link?";
                        $scope.ResendValidationOrSignup.buttonName = "Resend validation link";
                        $scope.ResendValidationOrSignup.functionName = "ResendValidationCodeRequest";
                    }
                    else if (data.Status == "500") {
                        location.href = "/?email=" + $('#forgetPasswordInputBoxId').val() + "#/showmessage/3/";
                    }
                }).error(function (data, status, headers, config) {
                    //alert("Not Working");
                    showToastMessage("Error", "Unable to reach the server. Refresh the page and try again");
                });
            }
                // Check Status, Email Id is valid or registered or not 
            else {
                $scope.ForgetPasswordContent = false;
                $scope.ForgetPasswordAlertContent.visible = true;
                $scope.ForgetPasswordAlertContent.message = "Please enter a valid email address to set new password.";
                showToastMessage("Error", "Email id field cann't be empty.");
            }
        }

        $scope.ResendValidationCodeRequest = function () {

            var resendValidationRequest = {
                userName: $('#forgetPasswordInputBoxId').val()
            };

            if (isValidEmailAddress($('#forgetPasswordInputBoxId').val())) {
                startBlockUI('wait..', 3);
                $http({
                    url: '/Auth/ResendValidationCode/',
                    data: resendValidationRequest,
                    method: "POST"
                }).success(function (data, status, headers, config) {
                    stopBlockUI();
                    if (data.Status == "200") {
                        location.href = "/?email=" + $('#forgetPasswordInputBoxId').val() + "#/showmessage/2/";
                    }
                    else if (data.Status == "404") {
                        $scope.ForgetPasswordContent = false;
                        $scope.ForgetPasswordAlertContent.visible = true;
                        $scope.ForgetPasswordAlertContent.message = "Entered email id is not registerd with us. Please enter your email address which is registered with us to set new password.";
                        $scope.ResendValidationOrSignup.visible = true;
                        $scope.ResendValidationOrSignup.title = "Please go to Home page and registered yourself.";
                        $scope.ResendValidationOrSignup.buttonName = "Home";
                        $scope.ResendValidationOrSignup.functionName = "HomeLink()";
                    }
                    else if (data.Status == "402") {
                        $scope.ForgetPasswordContent = false;
                        $scope.ForgetPasswordForm = false;
                        $scope.ForgetPasswordAlertContent.visible = true;
                        $scope.ForgetPasswordAlertContent.message = "Email Address-" + $('#forgetPasswordInputBoxId').val() + " has been already valideted. To continue, Please login into account.";
                        $scope.ResendValidationOrSignup.visible = false;
                    }
                    else if (data.Status == "500") {
                        location.href = "/?email=" + $('#forgetPasswordInputBoxId').val() + "#/showmessage/3/";
                    }
                }).error(function (data, status, headers, config) {
                    alert("false");
                });
            }

        }

        $scope.ContactUsRequestSubmit = function () {
            

            if (true) {
                startBlockUI('wait..', 3);
                $http({
                    url: '/Auth/ContactUs/',
                    data: $scope.ContactUsData,
                    method: "POST"
                }).success(function (data, status, headers, config) {
                    stopBlockUI();
                    if (data.Status == "200") {
                        
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

