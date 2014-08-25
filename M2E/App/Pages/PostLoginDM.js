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
            prettify: { //used
                deps: ["jquery"]
            },
            bootstrap_wysihtml5: { //used
                deps: ["jquery","wysihtml5"]
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
                deps: ["jquery", "filedrop", "clientAfterLoginCreateTemplate", "domReady"]
            },
            angularjs_fileUpload_shim: {//new
                deps: ["angular", "jquery"]
            },
            angularjs_fileUpload: {//new
                deps: ["angular", "jquery"]
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
//            morris_min: {//new
//                deps: ["jquery"]
//            },
            fancybox: {//new
                deps: ["jquery"]
            },
            ng_table: {//new
                deps: ["jquery","angular"]
            },
             ion_rangeSlider_min: {//new
                deps: ["jquery"]
            },
             bootstrap_slider: {//new
                deps: ["jquery"]
            },
             highcharts: {//new
                deps: ["jquery"]
            },
             highcharts_3d: {//new
                deps: ["jquery","highcharts"]
            },
             highcharts_exporting: {//new
                deps: ["jquery","highcharts_3d"]
            },
            clientAfterLoginCookieService: {
                deps: ["jquery", "jquery_cookie"]
            },
            beforeLoginAdminLTEApp: {//used
                deps: ["jquery", "jquery_slimscroll", "bootstrap", "bootstrap_switch", "iCheck","clientAfterLoginApp","beforeLoginAdminLTETree","domReady"]
            },            
            clientAfterLoginApp: { //new
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "clientAfterLoginCookieService", "fancybox"]
            },
            SessionManagement: { //used
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "clientAfterLoginCookieService", "fancybox"]
            },
            clientAfterLoginIndex: { //used
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "clientAfterLoginCookieService", "fancybox","ng_table"]
            },
            clientAfterLoginCreateTemplate: { //used
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "clientAfterLoginCookieService", "fancybox","wysihtml5","bootstrap_wysihtml5","prettify","AngularFileUploadController","ion_rangeSlider_min","bootstrap_slider"]
            },            
//            clientAfterLoginEditTemplate: {
//                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "clientAfterLoginCookieService", "fancybox","wysihtml5","bootstrap_wysihtml5","prettify"]
//            },
            clientAfterLoginEditPage: {
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "clientAfterLoginCookieService", "fancybox"]
            },
            clientAfterLoginTemplateInfo: {
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "clientAfterLoginCookieService", "fancybox","highcharts"]
            },
            ClientAfterLoginTemplateResponseDetail: {
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "clientAfterLoginCookieService", "fancybox","highcharts"]
            },
             ClientAfterLoginTranscriptionResponseDetail: {
                deps: ["jquery", "angular", "restangular", "configureBlockUI", "toastMessage", "clientAfterLoginCookieService", "fancybox","highcharts"]
            },
            AngularFileUploadController: {
                deps: ["angularjs_fileUpload_shim","angularjs_fileUpload"]
            },
             clientAfterLoginTemplateSample: {
                deps: ["jquery","angular"]
            },
            ClientAfterLoginModeratingPhotos: {
                deps: ["jquery","angular","filedrop"]
            },
            ClientAfterLoginTranscriptionTemplate: {
                deps: ["jquery","angular","filedrop"]
            },      
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
            wysihtml5:"../../App/third-Party/wysihtml5/lib/js/wysihtml5-0.3.0",
            prettify:"../../App/third-Party/wysihtml5/lib/js/prettify",
            bootstrap_wysihtml5:"../../Template/AdminLTE-master/js/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min",
            angularjs_fileUpload_shim:"../../App/third-Party/angular-file-upload-master/dist/angular-file-upload-shim.min",
            angularjs_fileUpload:"../../App/third-Party/angular-file-upload-master/dist/angular-file-upload.min",
            ng_table:"../../App/third-Party/ng-table-master/ng-table",
            ion_rangeSlider_min:"../../Template/AdminLTE-master/js/plugins/ionslider/ion.rangeSlider.min",
            bootstrap_slider:"../../Template/AdminLTE-master/js/plugins/bootstrap-slider/bootstrap-slider",
            //morris_min: "../../Template/AdminLTE-master/js/plugins/morris/morris.min",//used
            highcharts:"../../App/third-Party/highcharts/highcharts",
            highcharts_3d:"../../App/third-Party/highcharts/highcharts-3d",
            highcharts_exporting:"../../App/third-Party/highcharts/modules/exporting",
            angular_route: "../../App/js/angular-route",
            angular_animate: "../../App/js/angular-animate",
            sanitize: "../../App/js/angular/ngSanitize/sanitize",
            //==============================================================================================================
            // Application Related JS
            //==============================================================================================================
            clientAfterLoginApp: ".././../App/Pages/ClientAfterLogin/Controller/clientAfterLoginApp",//changed..
            SessionManagement: "../../App/Pages/ClientAfterLogin/Controller/common/SessionManagement",//new
            AngularFileUploadController: "../../App/Pages/ClientAfterLogin/Controller/common/AngularFileUploadController",//new
            clientAfterLoginIndex: "../../App/Pages/ClientAfterLogin/index/index",//new
            clientAfterLoginCreateTemplate: "../../App/Pages/ClientAfterLogin/Survey/CreateTemplate/CreateTemplate",//new
            clientAfterLoginCookieService: "../../../../App/Pages/ClientAfterLogin/Controller/common/CookieServiceClientView",//used
            //clientAfterLoginEditTemplate: "../../App/Pages/ClientAfterLogin/EditTemplate/EditTemplate",//used
            clientAfterLoginTemplateInfo: "../../App/Pages/ClientAfterLogin/TemplateInfo/TemplateInfo",//used
            clientAfterLoginEditPage: "../../App/Pages/ClientAfterLogin/EditPage/editPage",//used
            clientAfterLoginTemplateSample: "../../App/Pages/ClientAfterLogin/TemplateSample/TemplateSample",//used
            ClientAfterLoginModeratingPhotos: "../../App/Pages/ClientAfterLogin/Moderation/ModeratingPhotos/ModeratingPhotos",//used
            ClientAfterLoginTranscriptionTemplate: "../../App/Pages/ClientAfterLogin/DataEntry/TranscriptionTemplate/TranscriptionTemplate",//used
            ClientAfterLoginTemplateResponseDetail: "../../App/Pages/ClientAfterLogin/TemplateResponseDetail/TemplateResponseDetail",//used
            ClientAfterLoginTranscriptionResponseDetail: "../../App/Pages/ClientAfterLogin/DataEntry/TranscriptionResponseDetail/TranscriptionResponseDetail",//used
            
        },
        urlArgs: "123"
    });

appRequire(["underscore", "jquery", "angular", "jquery_toastmessage", "toastMessage", "jquery_cookie",
    "jquery_blockUI", "restangular", "moment", "bootstrap", "bootstrap_switch", "beforeLoginAdminLTEApp",
    "jquery_slimscroll", "iCheck", "angular_cookies", "configureBlockUI", "fancybox", "clientAfterLoginApp", "SessionManagement",
    "clientAfterLoginIndex", "clientAfterLoginCreateTemplate", "clientAfterLoginCookieService", "clientAfterLoginEditPage", "fancybox", "filedrop","wysihtml5",
    "fileDropScript", "domReady","prettify","bootstrap_wysihtml5","angularjs_fileUpload_shim","angularjs_fileUpload","AngularFileUploadController",
    "clientAfterLoginTemplateInfo","ng_table","clientAfterLoginTemplateSample","ClientAfterLoginModeratingPhotos","ClientAfterLoginTranscriptionTemplate",
    "bootstrap_slider","ion_rangeSlider_min","highcharts_3d","highcharts","highcharts_exporting","ClientAfterLoginTemplateResponseDetail","ClientAfterLoginTranscriptionResponseDetail",
    "angular_route","sanitize","angular_animate"
], function() {
    angular.bootstrap(document.getElementById("mainClient"), ["afterLoginClientApp"]);
});
