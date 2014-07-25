/**
 * Dashboard # Single Page Application [SPA] Dependency Manager Configurator to be resolved via Require JS library.
 * @class PreLoginDM
 * @module PreLogin
 */
appRequire = require
    .config({
        shim: {
            underscore: {
                exports: "_"
            },
            angular: {
                exports: "angular",
                deps: ["jquery"]
            },
            moment: {
                deps: ["jquery"]
            },            
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
            jquery_slimscroll: {
                deps: ["jquery"]
            },            
            beforeLoginAdminLTETree: {
                deps: ["jquery"]
            },
            iCheck: {
                deps: ["jquery"]
            },
            beforeLoginAdminLTEApp: {
                deps: ["jquery", "jquery_slimscroll", "bootstrap", "bootstrap_switch", "beforeLoginAdminLTETree", "iCheck"]
            },            
            beforeLoginApp: {
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage"]
            },
            showMessageTemplate: {
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage"]
            },
            beforeLoginIndex: {
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage"]
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
        },
        paths: {
            //==============================================================================================================
            // 3rd Party JavaScript Libraries
            //==============================================================================================================            
            underscore: "../../App/js/underscore-min",
            jquery: "../../App/js/jquery.min",
            angular: "../../App/js/angular.min",
            //m2ei18n: "../../App/js/m2ei18n",
            jquery_toastmessage: "../../App/third-Party/toastmessage/js/jquery.toastmessage",
            toastMessage: "../../App/js/toastMessage",
            jquery_cookie: "../../App/js/jquery.cookie",
            jquery_blockUI: "../../App/js/jquery.blockUI",                 
            restangular: "../../App/js/restangular.min",           
            moment: "../../App/js/moment.min",            
            bootstrap: "../../Template/AdminLTE-master/js/bootstrap.min",
            bootstrap_switch: "../../Template/AdminLTE-master/js/bootstrap-switch",
            beforeLoginAdminLTEApp: "../../Template/AdminLTE-master/js/AdminLTE/app",
            beforeLoginAdminLTETree: "../../Template/AdminLTE-master/js/AdminLTE/tree",
            jquery_slimscroll: "../../Template/AdminLTE-master/js/plugins/slimScroll/jquery.slimscroll",
            iCheck: "../../Template/AdminLTE-master/js/plugins/iCheck/icheck.min",
            angular_cookies: "../../App/js/angular-cookies",
            configureBlockUI: "../../App/js/configureBlockUI",

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
            
            
            
        },
        urlArgs: ""
    });

appRequire(["jquery", "angular", "jquery_toastmessage", "toastMessage", "jquery_cookie",
    "jquery_blockUI", "restangular", "moment", "bootstrap", "bootstrap_switch", "beforeLoginAdminLTEApp",
    "beforeLoginApp", "beforeLoginIndex", "beforeLoginFAQ", "beforeLoginLogin", "beforeLoginCookieService", "beforeLoginSignUpClient", "beforeLoginSignUpUser",
    "beforeLoginValidateEmail", "beforeLoginForgetPassword", "beforeLoginResetPassword", "showMessageTemplate", "underscore", "iCheck", "angular_cookies"
], function() {
    angular.bootstrap(document.getElementById("main"), ["beforeLoginApp"]);
});
