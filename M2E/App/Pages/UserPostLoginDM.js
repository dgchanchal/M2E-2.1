/**
 * Dashboard # Single Page Application [SPA] Dependency Manager Configurator to be resolved via Require JS library.
 * @class PreLoginDM
 * @module PreLogin
 */
appRequire = require
    .config({
        waitSeconds: 200,
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
            angularjs_fileUpload_shim: {//new
                deps: ["angular", "jquery"]
            },
            angularjs_fileUpload: {//new
                deps: ["angular", "jquery", "angularjs_fileUpload_shim"]
            },
            angular_route: {//new
                deps: ["angular", "jquery"]
            },
            angular_animate: {
                deps: ["angular", "jquery"]
            },
            sanitize: {
                deps: ["angular", "jquery"]
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
            wysihtml5: { //used
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
            domReady: {//new
                deps: ["jquery"]
            },
            fileDropScript: {//new
                deps: ["jquery", "filedrop", "domReady"]
            },
            fancybox: {//new
                deps: ["jquery"]
            },
            hamster: {//new
                deps: ["jquery","angular"]
            },
            mousewheel: {//new
                deps: ["jquery","angular"]
            },
//            jquery_panzoom: {//new
//                deps: ["jquery"]
//            },
            panzoom: {//new
                deps: ["jquery","angular"]
            },
//            dragend: {//new
//                deps: ["jquery"]
//            },
            idangerous_swiper_2_1_min: {//new
                deps: ["jquery"]
            },
            PanZoomService: {
                deps: ["jquery", "panzoom","mousewheel","hamster","angular"]
            },
            panzoomwidget: {
                deps: ["jquery", "panzoom", "mousewheel", "hamster", "angular"]
            },
            userAfterLoginCookieService: {
                deps: ["jquery", "jquery_cookie"]
            },
            beforeLoginAdminLTEApp: {//used
                deps: ["jquery", "jquery_slimscroll", "bootstrap", "bootstrap_switch", "beforeLoginAdminLTETree", "iCheck"]
            },            
            userAfterLoginApp: { //new
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "userAfterLoginCookieService", "fancybox"]
            },
            AngularFileUploadController: { //new
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "userAfterLoginCookieService", "fancybox", "angularjs_fileUpload", "angularjs_fileUpload_shim"]
            },
            SessionManagement: { //used
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "userAfterLoginCookieService", "fancybox"]
            },
            userAfterLoginIndex: { //used
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "userAfterLoginCookieService", "fancybox"]
            },
            UserAfterLoginReputationHistory: { //used
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "userAfterLoginCookieService", "fancybox"]
            },
            showAfterLoginShowTemplate: { //used
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "userAfterLoginCookieService", "fancybox"]
            },
            userAfterLoginShowTemplateDetail: { //used
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "userAfterLoginCookieService", "fancybox"]
            },                       
            userAfterLoginEditPage: {
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "userAfterLoginCookieService", "fancybox"]
            },
            userAfterLoginTemplateSample: {
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "userAfterLoginCookieService", "fancybox"]
            },
             userAfterLoginActiveThreads: {
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "userAfterLoginCookieService", "fancybox"]
            },
            userAfterLoginSurvey: {
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "userAfterLoginCookieService", "fancybox"]
            },
            UserAfterLoginFacebookLike: {
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "userAfterLoginCookieService", "fancybox"]
            },
            UserAfterLoginReferralPage: {
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "userAfterLoginCookieService", "fancybox"]
            },
            UserAfterLoginSubmitIdProofPage: {
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "userAfterLoginCookieService", "fancybox", "AngularFileUploadController"]
            },
            UserAfterLoginAngularTranscriptionTemplate: {
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "userAfterLoginCookieService", "fancybox","PanZoomService","panzoomwidget"]
            },
            UserAfterLoginEarningHistory: {
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "userAfterLoginCookieService"]
            },
            UserAfterLoginImageModeration: {
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "userAfterLoginCookieService", "fancybox","PanZoomService","panzoomwidget"]
            },
            UserAfterLoginAllMessages: {
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "userAfterLoginCookieService"]
            },
            UserAfterLoginAllNotifications: {
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "userAfterLoginCookieService"]
            }
                        
        },
        paths: {
            //==============================================================================================================
            // 3rd Party JavaScript Libraries
            //==============================================================================================================            
            underscore: "../../App/js/underscore-min",
            jquery: "../../App/js/jquery.min",//used..
            angular: "../../App/js/angular.1.2.13",//used..
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
            domReady: "../../App/js/domReady",
            fileDropScript:"../../App/third-Party/html5-file-upload/assets/js/script",
            wysihtml5: "../../Template/AdminLTE-master/js/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min",
            //dragend: "../../App/js/dragend",//used..
            idangerous_swiper_2_1_min: "../../App/third-Party/Swiper-master/demos/js/idangerous.swiper-2.1.min",//used..
            //jquery_panzoom: "../../App/js/jquery.panzoom",//used..
            hamster: "../../App/Pages/UserAfterLogin/Controller/panzoom/hamster",//used..
            mousewheel: "../../App/Pages/UserAfterLogin/Controller/panzoom/mousewheel",//used..
            angularjs_fileUpload_shim: "../../App/third-Party/angular-file-upload-master/dist/angular-file-upload-shim.min",
            angularjs_fileUpload: "../../App/third-Party/angular-file-upload-master/dist/angular-file-upload.min",
            panzoom: "../../App/Pages/UserAfterLogin/Controller/panzoom/directives/panzoom",//used..
            PanZoomService: "../../App/Pages/UserAfterLogin/Controller/panzoom/services/PanZoomService",//used..
            panzoomwidget: "../../App/Pages/UserAfterLogin/Controller/panzoom/directives/panzoomwidget",//used..
            angular_route: "../../App/js/angular-route",
            angular_animate: "../../App/js/angular-animate",
            sanitize: "../../App/js/angular/ngSanitize/sanitize",
            //==============================================================================================================
            // Application Related JS
            //==============================================================================================================
            userAfterLoginApp: ".././../App/Pages/UserAfterLogin/Controller/userAfterLoginApp",//changed..
            SessionManagement: "../../App/Pages/UserAfterLogin/Controller/common/SessionManagement",//new
            AngularFileUploadController: "../../App/Pages/UserAfterLogin/Controller/common/AngularFileUploadController",//new
            userAfterLoginIndex: "../../App/Pages/UserAfterLogin/index/index",//new
            userAfterLoginShowTemplate: "../../App/Pages/UserAfterLogin/ShowTemplate/ShowTemplate",//new
            userAfterLoginShowTemplateDetail: "../../App/Pages/UserAfterLogin/ShowTemplateDetail/ShowTemplateDetail",//new
            userAfterLoginCookieService: "../../../../App/Pages/UserAfterLogin/Controller/common/CookieServiceUserView",//used            
            userAfterLoginEditPage: "../../App/Pages/UserAfterLogin/EditPage/editPage",//used
            userAfterLoginTemplateSample: "../../App/Pages/UserAfterLogin/TemplateSample/TemplateSample",//used
            userAfterLoginSurvey: "../../App/Pages/UserAfterLogin/Survey/Survey",//used
            userAfterLoginModeration: "../../App/Pages/UserAfterLogin/Survey/Survey",//used
            userAfterLoginActiveThreads: "../../App/Pages/UserAfterLogin/UserActiveThreads/UserActiveThreads",//used
            //userAfterLoginTranscriptionTemplate: "../../App/Pages/UserAfterLogin/DataEntry/TranscriptionTemplate/TranscriptionTemplate",//used
            UserAfterLoginAngularTranscriptionTemplate: "../../App/Pages/UserAfterLogin/DataEntry/TranscriptionTemplate/AngularTranscriptionTemplate",//used
            UserAfterLoginImageModeration: "../../App/Pages/UserAfterLogin/Moderation/Moderation", //used
            UserAfterLoginReferralPage: "../../App/Pages/UserAfterLogin/Referrals/Referrals", //used
            UserAfterLoginFacebookLike: "../../App/Pages/UserAfterLogin/Ads/facebookLike/facebookLike",//used
            UserAfterLoginReputationHistory: "../../App/Pages/UserAfterLogin/UserReputationHistory/UserReputationHistory",//used
            UserAfterLoginSubmitIdProofPage: "../../App/Pages/UserAfterLogin/SubmitIdProof/SubmitIdProof",//used
            UserAfterLoginEarningHistory: "../../App/Pages/UserAfterLogin/UserEarningHistory/UserEarningHistory",//used
            UserAfterLoginAllMessages: "../../App/Pages/UserAfterLogin/ShowAllMessages/ShowAllMessages",//used
            UserAfterLoginAllNotifications: "../../App/Pages/UserAfterLogin/ShowAllNotifications/ShowAllNotifications"//used
            
        },
        urlArgs: ""
    });

appRequire(["underscore", "jquery", "angular", "jquery_toastmessage", "toastMessage", "jquery_cookie",
    "jquery_blockUI", "restangular", "moment", "bootstrap", "bootstrap_switch", "beforeLoginAdminLTEApp","beforeLoginAdminLTETree",
    "jquery_slimscroll", "iCheck", "angular_cookies", "configureBlockUI", "fancybox", "userAfterLoginApp", "SessionManagement",
    "userAfterLoginIndex", "userAfterLoginShowTemplate", "userAfterLoginCookieService", "userAfterLoginEditPage", "fancybox", "filedrop","wysihtml5",
    "fileDropScript", "domReady", "userAfterLoginTemplateSample", "idangerous_swiper_2_1_min","userAfterLoginShowTemplateDetail","userAfterLoginSurvey",
    "userAfterLoginActiveThreads","UserAfterLoginAngularTranscriptionTemplate","hamster","mousewheel",
    "panzoom", "PanZoomService", "panzoomwidget", "angular_route", "sanitize", "angular_animate", "UserAfterLoginImageModeration", "UserAfterLoginFacebookLike",
    "UserAfterLoginReferralPage", "UserAfterLoginEarningHistory", "UserAfterLoginReputationHistory", "angularjs_fileUpload_shim", "angularjs_fileUpload",
    "UserAfterLoginSubmitIdProofPage", "UserAfterLoginAllMessages", "UserAfterLoginAllNotifications"
], function() {
    angular.bootstrap(document.getElementById("mainUser"), ["afterLoginUserApp"]);
});
