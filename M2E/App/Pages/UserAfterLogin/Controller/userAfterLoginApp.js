'use strict';
define([appLocation.userPostLogin], function (app) {
    app.config(function ($routeProvider) {
        //(mobileDevice) ? "../../App/Pages/UserAfterLogin/Index/MobileIndex.html"  : "../../App/Pages/UserAfterLogin/Index/Index.html" }).
        $routeProvider.when("/", { templateUrl: "../../App/Pages/UserAfterLogin/Index/Index.html" }).
                       when("/edit", { templateUrl: "../../App/Pages/UserAfterLogin/EditPage/EditPage.html" }).
                       when("/showTemplateDetail/:refKey", { templateUrl: "../../App/Pages/UserAfterLogin/ShowTemplateDetail/ShowTemplateDetail.html" }).
                       when("/startSurvey/:refKey", { templateUrl: (mobileDevice) ? "../../App/Pages/UserAfterLogin/Survey/MobileSurvey.html" : "../../App/Pages/UserAfterLogin/Survey/WebSurvey.html" }).
                       when("/showTemplate", { templateUrl: "../../App/Pages/UserAfterLogin/ShowTemplate/ShowTemplate.html" }).
                       when("/editTemplate/:username/:templateid", { templateUrl: "../../App/Pages/UserAfterLogin/EditTemplate/EditTemplate.html" }).
                       when("/templateSample/:type/:subType", { templateUrl: "../../App/Pages/UserAfterLogin/TemplateSample/TemplateSample.html" }).
                       when("/mobileSlide", { templateUrl: "../../App/Pages/UserAfterLogin/MobileSlide/MobileSlide.html" }).
                       when("/mobileSlide2", { templateUrl: "../../App/Pages/UserAfterLogin/MobileSlide2/MobileSlide2.html" }).
                       when("/mobileSlide3", { templateUrl: "../../App/Pages/UserAfterLogin/MobileSlide2/MobileSlide3.html" }).
                       otherwise({ templateUrl: "../../Resource/templates/beforeLogin/contentView/404.html" });

    });

    app.run(function ($rootScope, $location, CookieUtil, SessionManagementUtil) { //Insert in the function definition the dependencies you need.

        $rootScope.$on("$locationChangeStart", function (event, next, current) {

            var headerSessionData = {
                UTMZT: CookieUtil.getUTMZT(),
                UTMZK: CookieUtil.getUTMZK(),
                UTMZV: CookieUtil.getUTMZV()
            }

            //SessionManagementUtil.isValidSession(headerSessionData);
            /* Sidebar tree view */
            $(".sidebar .treeview").tree();

            gaWeb("BeforeLogin-Page Visited", "Page Visited", next);
            var path = next.split('#');
            gaPageView(path, 'title');
        });
    });

    app.controller('UserAfterMasterPage', function ($scope, $http, $rootScope, CookieUtil) {

        _.defer(function () { $scope.$apply(); });

        $scope.ClientCategoryList = [
       {
           MainCategory: "Category",
           subCategoryList: [
           {
               value: "Data entry", dropDownMenuShow: true, dropDownSubMenuClass: "dropdown-submenu", dropDownMenuClass: "dropdown-menu", dropDownSubMenuArrow: "dropdown", dropDownMenuList: [
               { value: "Verification & Duplication", link: "#/VerificationAndDuplicationSample" },
               { value: "Data Entry", link: "#" },
               { value: "Search the web", link: "#" },
               { value: "Do Excel work", link: "#" },
               { value: "Find information", link: "#" },
               { value: "Post advertisements", link: "#" },
               { value: "Transcription", link: "#" }
               ]
           },
           {
               value: "Content Writing", dropDownMenuShow: true, dropDownSubMenuClass: "dropdown-submenu", dropDownMenuClass: "dropdown-menu", dropDownSubMenuArrow: "dropdown", dropDownMenuList: [
                 { value: "Article writing", link: "#" },
                 { value: "Blog writing", link: "#" },
                 { value: "Copy typing", link: "#" },
                 { value: "Powerpoint", link: "#" },
                 { value: "Short stories", link: "#" },
                 { value: "Travel writing", link: "#" },
                 { value: "Reviews", link: "#" },
                 { value: "Product descriptions", link: "#" }
               ]
           },
           {
               value: "Survey", dropDownMenuShow: true, dropDownSubMenuClass: "dropdown-submenu", dropDownMenuClass: "dropdown-menu", dropDownSubMenuArrow: "dropdown", dropDownMenuList: [
                 { value: "Product survey", link: "#/templateSample/test/okay" },
                 { value: "User feedback survey", link: "#" },
                 { value: "Pools", link: "#" }
               ]
           },
           {
               value: "Moderation", dropDownMenuShow: true, dropDownSubMenuClass: "dropdown-submenu", dropDownMenuClass: "dropdown-menu", dropDownSubMenuArrow: "dropdown", dropDownMenuList: [
                 { value: "Moderating Ads", link: "#" },
                 { value: "Moderating Photos", link: "#" },
                 { value: "Moderating Music", link: "#" },
                 { value: "Moderating Video", link: "#" }
               ]
           },
           {
               value: "Ads", dropDownMenuShow: true, dropDownSubMenuClass: "dropdown-submenu", dropDownMenuClass: "dropdown-menu", dropDownSubMenuArrow: "dropdown", dropDownMenuList: [
                 { value: "Facebook Views", link: "#" },
                 { value: "Facebook likes", link: "#" },
                 { value: "Video reviewing", link: "#" },
                 { value: "Comments on social media", link: "#" }
               ]
           }
           ]
       }
        ];
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

});
