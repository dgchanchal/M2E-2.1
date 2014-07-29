
'use strict';
define([appLocation.preLogin], function (app) {

    app.config(function ($routeProvider) {

        $routeProvider.when("/", { templateUrl: "../../App/Pages/BeforeLogin/Index/Index.html" }).
                       when("/signup/user/:ref", { templateUrl: "../../App/Pages/BeforeLogin/SignUpUser/SignUpUser.html" }).
                       when("/signup/client/:ref", { templateUrl: "../../App/Pages/BeforeLogin/SignUpClient/SignUpClient.html" }).
                       when("/signup/user", { templateUrl: "../../App/Pages/BeforeLogin/SignUpUser/SignUpUser.html" }).
                       when("/signup/client", { templateUrl: "../../App/Pages/BeforeLogin/SignUpClient/SignUpClient.html" }).
                       when("/login", { templateUrl: "../../App/Pages/BeforeLogin/Login/Login.html" }).
                       when("/login/:code", { templateUrl: "../../App/Pages/BeforeLogin/Login/Login.html" }).
                       when("/faq", { templateUrl: "../../App/Pages/BeforeLogin/FAQ/FAQ.html" }).
                       when("/facebookLogin/:userType", { templateUrl: "../../Resource/templates/beforeLogin/contentView/facebookLogin.html" }).
                       when("/facebookLogin", { templateUrl: "../../Resource/templates/beforeLogin/contentView/facebookLogin.html" }).
                       when("/googleLogin/:userType", { templateUrl: "../../Resource/templates/beforeLogin/contentView/googleLogin.html" }).
                       when("/googleLogin", { templateUrl: "../../Resource/templates/beforeLogin/contentView/googleLogin.html" }).
                       when("/linkedinLogin/:userType", { templateUrl: "../../Resource/templates/beforeLogin/contentView/linkedinLogin.html" }).
                       when("/linkedinLogin", { templateUrl: "../../Resource/templates/beforeLogin/contentView/linkedinLogin.html" }).
                       when("/validate/:userName/:guid", { templateUrl: "../../App/Pages/BeforeLogin/validateEmail/validateEmail.html" }).
                       when("/tnc", { templateUrl: "../../App/Pages/BeforeLogin/TnC/TnC.html" }).
                       when("/404", { templateUrl: "../../Resource/templates/beforeLogin/contentView/404.html" }).
                       when("/AboutUs", { templateUrl: "../../App/Pages/BeforeLogin/AboutUs/AboutUs.html" }).
                       when("/contactus", { templateUrl: "../../App/Pages/BeforeLogin/ContactUs/contactus.html" }).
                       when("/showmessage/:code", { templateUrl: "../../App/Pages/BeforeLogin/ShowMessage/showmessage.html" }).
                       when("/forgetpassword", { templateUrl: "../../App/Pages/BeforeLogin/ForgetPassword/forgetPassword.html" }).
                       when("/resetpassword/:userName/:guid", { templateUrl: "../../App/Pages/BeforeLogin/ResetPassword/resetpassword.html" }).
                       when("/UserDetails", { templateUrl: "../../App/Pages/BeforeLogin/UserMoreInfo/UserMoreInfo.html" }).
                       when("/ClientDetails", { templateUrl: "../../App/Pages/BeforeLogin/ClientMoreInfo/ClientMoreInfo.html" }).
                       otherwise({ templateUrl: "../../Resource/templates/beforeLogin/contentView/404.html" });

    });

    app.run(function ($rootScope, $location) { //Insert in the function definition the dependencies you need.

        $rootScope.$on("$locationChangeStart", function (event, next, current) {
            gaWeb("BeforeLogin-Page Visited", "Page Visited", next);
            var path = next.split('#');
            var contextPath = path[1];
            gaPageView(path, 'title');
            if (contextPath == "/signup" || contextPath == "/signup/user") {
                $rootScope.showSignUpButton = false;
                $rootScope.showLabelAlreadyRegistered = true;
            }
            else {
                $rootScope.showSignUpButton = true;
                $rootScope.showLabelAlreadyRegistered = false;
            }
        });
    });
    app.controller('beforeLoginMasterPageController', function ($scope, $http, $rootScope, CookieUtil) {

        _.defer(function () { $scope.$apply(); });

        $rootScope.logoImage = { url: logoImage };
        $('title').html("index"); //TODO: change the title so cann't be tracked in log
        $rootScope.beforeLoginFooterCopyRightInfo = {
            companyName: "MadeToEarn",
            contactUs: "Contact Us",
            FAQ: "FAQ",
            TnC: "Terms & Conditions"
        };
        if (detectmob()) {
            $rootScope.beforeLoginFooterCopyRightInfo.companyName = "M2E";
            $rootScope.beforeLoginFooterCopyRightInfo.TnC = "T&C";
        }
    });

    function loadjscssfile(filename, filetype) {
        var fileref = "";
        if (filetype == "js") { //if filename is a external JavaScript file
            fileref = document.createElement('script');
            fileref.setAttribute("type", "text/javascript");
            fileref.setAttribute("src", filename);
        }
        else if (filetype == "css") { //if filename is an external CSS file
            fileref = document.createElement("link");
            fileref.setAttribute("rel", "stylesheet");
            fileref.setAttribute("type", "text/css");
            fileref.setAttribute("href", filename);
        }
        if (typeof fileref != "undefined")
            document.getElementsByTagName("head")[0].appendChild(fileref);
    }

    //loadjscssfile("../../App/Pages/BeforeLogin/SignUpClient/signUpClientController.js", "js"); //dynamically load and add this .js file
    //loadjscssfile("../../App/Pages/BeforeLogin/Controller/common/CookieService.js", "js"); 

});
