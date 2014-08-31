/**
 * Dashboard # Single Page Application [SPA] Dependency Manager Configurator to be resolved via Require JS library.
 * @class PreLoginDM
 * @module PreLogin
 */
appRequire = require
    .config({
        waitSeconds: 200,
        shim: {            
            underscore: {
                exports: "_"
            },
            angular: {
                exports: "angular",
                deps: ["jquery"]
            },
            //moment: {
            //    deps: ["jquery"]
            //},            
            bootstrap: {
                deps: ["jquery"]
            },
            bootstrap_switch: {
                deps: ["jquery"]
            },
            jquery: {
                exports: "$"
            },                        
            jquery_cookie: {
                deps: ["jquery"]
            },
            //m2ei18n: {
            //    deps: ["jquery"]
            //},
            restangular: {
                deps: ["angular", "underscore"]
            },
            angular_cookies: {
                deps: ["angular"]
            },
            angular_route: {
                deps: ["angular", "jquery"]
            },
            angular_animate: {
                deps: ["angular", "jquery", "angular_route"]
            },
            sanitize: {
                deps: ["angular", "jquery"]
            },
            jquery_toastmessage: {
                deps: ["jquery"]
            },
            toastMessage: {
                deps: ["jquery_toastmessage"]
            },            
            jquery_blockUI: {
                deps: ["jquery"]
            },
            configureBlockUI: {
                deps: ["jquery_blockUI"]
            },
            jquery_ui_min: {
                deps: ["jquery"]
            },
            jquery_ui_touch_punch_min: {
                deps: ["jquery", "jquery_ui_min"]
            },
            bannerscollection_zoominout: {
                deps: ["jquery", "jquery_ui_touch_punch_min", "jquery_ui_min"]
            },
            jquery_slimscroll: {
                deps: ["jquery"]
            },
            jquery_sidr_min: {
                deps: ["jquery"]
            },
            beforeLoginApp: {
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "jquery_sidr_min"]
            },
            showMessageTemplate: {
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage"]
            },
            beforeLoginIndex: {
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "bannerscollection_zoominout"]
            },
            beforeLoginFAQ: {
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage"]
            },
            beforeLoginLogin: {
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage"]
            },
            beforeLoginCookieService: {
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "jquery_blockUI", "toastMessage"]
            },
            beforeLoginSignUpClient: {
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage"]
            },
            beforeLoginSignUpUser: {
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage"]
            },
            beforeLoginValidateEmail: {
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage"]
            },
            beforeLoginForgetPassword: {
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage"]
            },
            beforeLoginResetPassword: {
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage"]
            },
            termsPrivacyController: {
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage"]
            }
        },
        paths: {
            //==============================================================================================================
            // 3rd Party JavaScript Libraries
            //==============================================================================================================            
            underscore: "../../App/js/underscore-min",
            jquery: "../../App/js/jquery.min",
            jquery_ui_min: "../../App/js/jquery-ui.min",
            //hammer_min: "../../App/js/hammer.min",
            angular: "../../App/js/angular.1.2.13",
            //m2ei18n: "../../App/js/m2ei18n",
            jquery_toastmessage: "../../App/third-Party/toastmessage/js/jquery.toastmessage",
            toastMessage: "../../App/js/toastMessage",
            jquery_cookie: "../../App/js/jquery.cookie",
            jquery_blockUI: "../../App/js/jquery.blockUI",                 
            restangular: "../../App/js/restangular.min",           
            //moment: "../../App/js/moment.min",            
            bootstrap: "../../Template/AdminLTE-master/js/bootstrap.min",
            bootstrap_switch: "../../Template/AdminLTE-master/js/bootstrap-switch",
            //beforeLoginAdminLTEApp: "../../Template/AdminLTE-master/js/AdminLTE/app",
            //beforeLoginAdminLTETree: "../../Template/AdminLTE-master/js/AdminLTE/tree",
            jquery_slimscroll: "../../Template/AdminLTE-master/js/plugins/slimScroll/jquery.slimscroll",
            //iCheck: "../../Template/AdminLTE-master/js/plugins/iCheck/icheck.min",
            angular_cookies: "../../App/js/angular-cookies",
            configureBlockUI: "../../App/js/configureBlockUI",
            angular_route: "../../App/js/angular-route",
            angular_animate: "../../App/js/angular-animate",
            sanitize: "../../App/js/angular/ngSanitize/sanitize",
            jquery_nivo_slider: "../../App/js/jquery.nivo.slider",
            bannerscollection_zoominout: "../../App/js/bannerscollection_zoominout",
            jquery_ui_touch_punch_min: "../../App/js/jquery.ui.touch-punch.min",
            jquery_sidr_min: "../../App/third-Party/sidr-package/jquery.sidr.min",
            //==============================================================================================================
            // Application Related JS
            //==============================================================================================================
            beforeLoginApp: ".././../App/Pages/BeforeLogin/Controller/beforeLoginApp",
            beforeLoginIndex: "../../App/Pages/BeforeLogin/Index/index",
            beforeLoginFAQ: "../../App/Pages/BeforeLogin/FAQ/FAQ",
            beforeLoginLogin: "../../App/Pages/BeforeLogin/Login/Login",
            beforeLoginCookieService: "../../../../App/Pages/BeforeLogin/Controller/common/CookieService",
            beforeLoginSignUpClient: "../../App/Pages/BeforeLogin/SignUpClient/SignUpClient",
            beforeLoginSignUpUser: "../../App/Pages/BeforeLogin/SignUpUser/SignUpUser",
            beforeLoginValidateEmail: "../../App/Pages/BeforeLogin/validateEmail/validateEmail",
            beforeLoginForgetPassword: "../../App/Pages/BeforeLogin/ForgetPassword/forgetPasswordTemplate",
            beforeLoginResetPassword: "../../App/Pages/BeforeLogin/ResetPassword/resetpasswordTemplate",
            showMessageTemplate: "../../App/Pages/BeforeLogin/ShowMessage/showMessageTemplate",
            termsPrivacyController: "../../App/Pages/BeforeLogin/TnC/TnC",
            //TweenMax_min: "http://cdnjs.cloudflare.com/ajax/libs/gsap/1.9.7/TweenMax.min",
            
        },
        urlArgs: ""
    });

appRequire(["jquery", "angular", "jquery_toastmessage", "toastMessage", "jquery_cookie",
    "jquery_blockUI", "restangular","angular_route", "angular_animate", "bootstrap", "bootstrap_switch", //"beforeLoginAdminLTEApp", "moment","iCheck",
    "beforeLoginApp", "beforeLoginIndex", "beforeLoginFAQ", "beforeLoginLogin", "beforeLoginCookieService", "beforeLoginSignUpClient", "beforeLoginSignUpUser",
    "beforeLoginValidateEmail", "beforeLoginForgetPassword", "beforeLoginResetPassword", "showMessageTemplate", "underscore", "angular_cookies", "termsPrivacyController",
    "sanitize", "bannerscollection_zoominout", "jquery_ui_touch_punch_min", "jquery_ui_min", "jquery_sidr_min"
], function() {
    angular.bootstrap(document.getElementById("main"), ["beforeLoginApp"]);
});
