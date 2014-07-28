/**
 * Dashboard # Single Page Application [SPA] Dependency Manager Configurator to be resolved via Require JS library.
 * @class PreLoginDM
 * @module PreLogin
 */
appRequire = require
    .config({
        shim: {
            underscore: { //used
                exports: "_"
            },
            angular: {//used
                exports: "angular",
                deps: ["jquery"]
            },
            moment: {//used
                deps: ["jquery"]
            },            
            bootstrap: {//used
                deps: ["jquery"]
            },
            bootstrap_switch: { //used
                deps: ["jquery"]
            },
            jquery: {//used
                exports: "$"
            },//         
            jquery_cookie: {//used
                deps: ["jquery"]
            },            
            //m2ei18n: {
            //    deps: ["jquery"]
            //},
            restangular: {//used
                deps: ["angular", "underscore"]
            },
            angular_cookies: {//used
                deps: ["angular"]
            },          
            jquery_toastmessage: { //used
                deps: ["jquery"]
            },
            toastMessage: {
                deps: ["jquery_toastmessage"]
            },            
            jquery_blockUI: {//used
                deps: ["jquery"]
            },
            configureBlockUI: {//used
                deps: ["jquery_blockUI"]
            },            
            jquery_slimscroll: {//used
                deps: ["jquery"]
            },            
            beforeLoginAdminLTETree: {//used
                deps: ["jquery"]
            },
            iCheck: {//used
                deps: ["jquery"]
            },
            filedrop: {//new
                deps: ["jquery"]
            },
            fileDropScript: {//new
                deps: ["jquery", "filedrop", "clientAfterLoginEditTemplate", "clientAfterLoginCreateTemplate"]
            },
            fancybox: {//new
                deps: ["jquery"]
            },
            clientAfterLoginCookieService: {
                deps: ["jquery", "jquery_cookie"]
            },
            beforeLoginAdminLTEApp: {//used
                deps: ["jquery", "jquery_slimscroll", "bootstrap", "bootstrap_switch", "beforeLoginAdminLTETree", "iCheck"]
            },            
            clientAfterLoginApp: { //new
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "clientAfterLoginCookieService", "fancybox"]
            },
            SessionManagement: { //used
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "clientAfterLoginCookieService", "fancybox"]
            },
            clientAfterLoginIndex: { //used
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "clientAfterLoginCookieService", "fancybox"]
            },
            clientAfterLoginCreateTemplate: { //used
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "clientAfterLoginCookieService", "fancybox"]
            },            
            clientAfterLoginEditTemplate: {
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "clientAfterLoginCookieService", "fancybox"]
            },
            clientAfterLoginEditPage: {
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "clientAfterLoginCookieService", "fancybox"]
            },
                        
        },
        paths: {
            //==============================================================================================================
            // 3rd Party JavaScript Libraries
            //==============================================================================================================            
            underscore: "../../App/js/underscore-min",
            jquery: "../../App/js/jquery.min",//used..
            angular: "../../App/js/angular.min",//used..
            //m2ei18n: "../../App/js/m2ei18n",
            jquery_toastmessage: "../../App/third-Party/toastmessage/js/jquery.toastmessage",//used
            toastMessage: "../../App/js/toastMessage",//used
            jquery_cookie: "../../App/js/jquery.cookie",//used..
            jquery_blockUI: "../../App/js/jquery.blockUI",//used                 
            restangular: "../../App/js/restangular.min",           
            moment: "../../App/js/moment.min",            
            bootstrap: "../../Template/AdminLTE-master/js/bootstrap.min",//used
            bootstrap_switch: "../../Template/AdminLTE-master/js/bootstrap-switch",
            beforeLoginAdminLTEApp: "../../Template/AdminLTE-master/js/AdminLTE/app",//used
            beforeLoginAdminLTETree: "../../Template/AdminLTE-master/js/AdminLTE/tree",//used
            jquery_slimscroll: "../../Template/AdminLTE-master/js/plugins/slimScroll/jquery.slimscroll",
            iCheck: "../../Template/AdminLTE-master/js/plugins/iCheck/icheck.min",
            angular_cookies: "../../App/js/angular-cookies",//used..
            configureBlockUI: "../../App/js/configureBlockUI",//used..
            fancybox: "../../App/third-Party/fancybox/source/jquery.fancybox.js?v=2.1.5",//new
            filedrop: "../../App/third-Party/html5-file-upload/assets/js/jquery.filedrop",
            fileDropScript:"../../App/third-Party/html5-file-upload/assets/js/script",
            //==============================================================================================================
            // Application Related JS
            //==============================================================================================================
            clientAfterLoginApp: ".././../App/Pages/ClientAfterLogin/Controller/clientAfterLoginApp",//changed..
            SessionManagement: "../../App/Pages/ClientAfterLogin/Controller/common/SessionManagement",//new
            clientAfterLoginIndex: "../../App/Pages/ClientAfterLogin/index/index",//new
            clientAfterLoginCreateTemplate: "../../App/Pages/ClientAfterLogin/CreateTemplate/CreateTemplate",//new
            clientAfterLoginCookieService: "../../../../App/Pages/ClientAfterLogin/Controller/common/CookieServiceClientView",//used
            clientAfterLoginEditTemplate: "../../App/Pages/ClientAfterLogin/EditTemplate/EditTemplate",//used
            clientAfterLoginEditPage: "../../App/Pages/ClientAfterLogin/EditPage/editPage",//used
            
        },
        urlArgs: ""
    });

appRequire(["underscore", "jquery", "angular", "jquery_toastmessage", "toastMessage", "jquery_cookie",
    "jquery_blockUI", "restangular", "moment", "bootstrap", "bootstrap_switch", "beforeLoginAdminLTEApp","beforeLoginAdminLTETree",
    "jquery_slimscroll", "iCheck", "angular_cookies", "configureBlockUI", "fancybox", "clientAfterLoginApp", "SessionManagement",
    "clientAfterLoginIndex", "clientAfterLoginCreateTemplate", "clientAfterLoginCookieService", "clientAfterLoginEditTemplate", "clientAfterLoginEditPage", "fancybox", "filedrop",
    "fileDropScript"
], function() {
    angular.bootstrap(document.getElementById("mainClient"), ["afterLoginClientApp"]);
});
