'use strict';
define([appLocation.preLogin], function (app) {
    app.controller('resetPasswordTemplate', function ($scope, $http, $routeParams, $location) {

        var resetPasswordRequestData = {
            Username: $routeParams.userName,
            Guid: $routeParams.guid,
            Password: ''
        };
        $scope.FormData = {
            pass: '',
            repass: ''
        };
        var validatePassword = false;
        $scope.PasswordAlertContent = {
            visible: false,
            message: ''
        }
        $scope.resetPasswordRequest = function () {
            if ($scope.FormData.pass == $scope.FormData.repass) {
                if ($scope.FormData.pass != "") {
                    validatePassword = true;
                    resetPasswordRequestData.Password = $scope.FormData.pass;
                }
                else {
                    validatePassword = false;
                    $scope.PasswordAlertContent.visible = true;
                    $scope.PasswordAlertContent.message = "Your Password Cannot be Empty!!! Please re-enter password";
                }
            }
            else {
                validatePassword = false;
                $scope.PasswordAlertContent.visible = true;
                $scope.PasswordAlertContent.message = "Password didn't match!!! Please re-enter password. Make sure your caps lock is off.";
            }

            if (validatePassword) {
                //startBlockUI('wait..', 3);
                $http({
                    url: '/Auth/ResetPassword',
                    method: "POST",
                    data: resetPasswordRequestData,
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data, status, headers, config) {
                    //stopBlockUI();
                    if (data.Status == "200") {
                        //showToastMessage("Success", "Password has been successfully changed.");
                        location.href = "#/login/Password200";
                    }
                    else if (data.Status == "404") {
                        $scope.ForgetPasswordAlertContent.visible = true;
                        $scope.ForgetPasswordAlertContent.message = "Entered email id is not registerd with us. Please enter your email address which is registered with us to set new password.";
                    }
                    else if (data.Status == "402") {
                        location.href = "/?email=" + $('#forgetPasswordInputBoxId').val() + "#/showmessage/4/";
                    }
                    else if (data.Status == "500") {
                        location.href = "/?email=" + $('#forgetPasswordInputBoxId').val() + "#/showmessage/3/";
                    }
                }).error(function (data, status, headers, config) {

                });
            } else {
                showToastMessage("Error", "Some Fields are Invalid !!!");
            }
        }
    });

});

