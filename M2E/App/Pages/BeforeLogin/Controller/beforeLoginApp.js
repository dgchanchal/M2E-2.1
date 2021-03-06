
'use strict';
define([appLocation.preLogin], function (app) {

    app.config(function ($routeProvider) {

        $routeProvider.when("/", { templateUrl : "../../App/Pages/BeforeLogin/Index/Index.html" }).
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
                       when("/AccepterDetails", { templateUrl: "../../App/Pages/BeforeLogin/UserMoreInfo/UserMoreInfo.html" }).
                       when("/RequesterDetails", { templateUrl: "../../App/Pages/BeforeLogin/ClientMoreInfo/ClientMoreInfo.html" }).
                       otherwise({ templateUrl: "../../Resource/templates/beforeLogin/contentView/404.html" });

    });

    app.run(function ($rootScope, $location, $window) { //Insert in the function definition the dependencies you need.

        $rootScope.$on("$locationChangeStart", function (event, next, current) {
            detectIfUserLoggedIn();
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
            $window.scrollTo(0, 0);
        });
    });
    app.controller('beforeLoginMasterPageController', function ($scope, $location, $http, $rootScope, CookieUtil) {

        _.defer(function () { $scope.$apply(); });
        $rootScope.IsMobileDevice = (mobileDevice || isAndroidDevice) ? true : false;
        $rootScope.logoImage = { url: logoImage };
        //$('title').html("index"); //TODO: change the title so cann't be tracked in log
        $rootScope.beforeLoginFooterInfo = {
            requester: "Crowd Automation Requester",
            accepter: "Crowd Automation Accepter",
            knowMore: "Learn more about",
            impLinks: window.madetoearn.i18n.beforeLoginMasterPageFooterImportantLinks,
            FAQ: window.madetoearn.i18n.beforeLoginMasterPageFAQ,
            contactUs: window.madetoearn.i18n.beforeLoginMasterPageContactUs,
            TnC: window.madetoearn.i18n.beforeLoginMasterPageTnC,
            developers:"Developers Section",
            aboutus: window.madetoearn.i18n.beforeLoginMasterPageAboutUs,
            home: window.madetoearn.i18n.beforeLoginMasterPageHome,
            footerMost: window.madetoearn.i18n.beforeLoginMasterPageFooterMost
        };

        $rootScope.beforeLoginFooterFAQList = [];
        if (getParameterByName("lang") == "hi_in") {
            $rootScope.beforeLoginFooterFAQList[0] = { Question: FAQAccepterList[0].question_hi, Link: FAQAccepterList[0].id };
            $rootScope.beforeLoginFooterFAQList[1] = { Question: FAQAccepterPayList[2].question_hi, Link: FAQAccepterPayList[2].id };
            $rootScope.beforeLoginFooterFAQList[2] = { Question: FAQOverviewList[1].question_hi, Link: FAQOverviewList[1].id };
            $rootScope.beforeLoginFooterFAQList[3] = { Question: FAQRequesterCITRelatedtList[1].question_hi, Link: FAQRequesterCITRelatedtList[1].id };
            $rootScope.beforeLoginFooterFAQList[4] = { Question: FAQAccepterPayList[1].question_hi, Link: FAQAccepterPayList[1].id };
            $rootScope.beforeLoginFooterFAQList[5] = { Question: FAQRequesterCITRelatedtList[0].question_hi, Link: FAQRequesterCITRelatedtList[0].id };
        } else {
            $rootScope.beforeLoginFooterFAQList[0] = { Question: FAQAccepterList[0].question, Link: FAQAccepterList[0].id };
            $rootScope.beforeLoginFooterFAQList[1] = { Question: FAQAccepterPayList[2].question, Link: FAQAccepterPayList[2].id };
            $rootScope.beforeLoginFooterFAQList[2] = { Question: FAQOverviewList[1].question, Link: FAQOverviewList[1].id };
            $rootScope.beforeLoginFooterFAQList[3] = { Question: FAQRequesterCITRelatedtList[1].question, Link: FAQRequesterCITRelatedtList[1].id };
            $rootScope.beforeLoginFooterFAQList[4] = { Question: FAQAccepterPayList[1].question, Link: FAQAccepterPayList[1].id };
            $rootScope.beforeLoginFooterFAQList[5] = { Question: FAQRequesterCITRelatedtList[0].question, Link: FAQRequesterCITRelatedtList[0].id };
        }

        if (mobileDevice || isAndroidDevice) {
            $rootScope.headeClassName = "headerSize-Mobile";
        }
        else if (ipadDevice) {
            $rootScope.headeClassName = "headerSize-Ipad";
        }
        else
            $rootScope.headeClassName = "headerSize";

        $scope.FAQscrollTo = function (id) {
            //$anchorScroll();
            //$location.hash(id);
            $location.path('/faq');
            $location.hash(id);
        };

        $('#responsive-menu-button').sidr({
            name: 'sidr-main',
            side: 'right',
            source: '#nav_mobi'
        });
        $("body").click(function () {
            $.sidr('close', 'sidr-main');
        });

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
