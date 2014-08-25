'use strict';
define([appLocation.postLogin], function (app) {
    app.config(function ($routeProvider) {

        $routeProvider.when("/", { templateUrl: "../../App/Pages/ClientAfterLogin/Index/Index.html" }).
                       when("/edit", { templateUrl: "../../App/Pages/ClientAfterLogin/EditPage/EditPage.html" }).
                       when("/createTemplate", { templateUrl: "../../App/Pages/ClientAfterLogin/Survey/CreateTemplate/CreateTemplate.html" }).
                       when("/editTemplate/:username/:templateid", { templateUrl: "../../App/Pages/ClientAfterLogin/EditTemplate/EditTemplate.html" }).
                       when("/templateSample/:type/:subType", { templateUrl: "../../App/Pages/ClientAfterLogin/TemplateSample/TemplateSample.html" }).
                       when("/templateInfo/:type/:subType/:templateId", { templateUrl: "../../App/Pages/ClientAfterLogin/TemplateInfo/TemplateInfo.html" }).
                       when("/templateResponseDetail/:type/:subType/:templateId", { templateUrl: "../../App/Pages/ClientAfterLogin/TemplateResponseDetail/TemplateResponseDetail.html" }).
                       when("/transcriptionResponseDetail/:type/:subType/:templateId", { templateUrl: "../../App/Pages/ClientAfterLogin/DataEntry/TranscriptionResponseDetail/TranscriptionResponseDetail.html" }).
                       when("/moderatingPhotos", { templateUrl: "../../App/Pages/ClientAfterLogin/Moderation/ModeratingPhotos/ModeratingPhotos.html" }).
                       when("/moderatingPhotosResponseDetail/:type/:subType/:templateId", { templateUrl: "../../App/Pages/ClientAfterLogin/Moderation/ModeratingPhotosResponseDetail/ModeratingPhotosResponseDetail.html" }).
                       when("/transcriptionTemplate", { templateUrl: "../../App/Pages/ClientAfterLogin/DataEntry/TranscriptionTemplate/TranscriptionTemplate.html" }).
                       otherwise({ templateUrl: "../../Resource/templates/beforeLogin/contentView/404.html" });

    });

    app.directive("highChart", function ($parse) {
        return {
            link: function (scope, element, attrs, ngModel) {
                var props = $parse(attrs.highChart)(scope);
                props.chart.renderTo = element[0];
                //console.log(props)
                new Highcharts.Chart(props);
            }
        }
    });

    app.run(function ($rootScope, $location, CookieUtil, SessionManagementUtil) { //Insert in the function definition the dependencies you need.

        $rootScope.$on("$locationChangeStart", function (event, next, current) {

            //            var headerSessionData = {
            //                UTMZT: CookieUtil.getUTMZT(),
            //                UTMZK: CookieUtil.getUTMZK(),
            //                UTMZV: CookieUtil.getUTMZV()
            //            }

            //SessionManagementUtil.isValidSession(headerSessionData);
            /* Sidebar tree view */
            //userSession.guid = CookieUtil.getUTMZT();
            $(".sidebar .treeview").tree();

            gaWeb("BeforeLogin-Page Visited", "Page Visited", next);
            var path = next.split('#');
            gaPageView(path, 'title');
        });
    });


    app.controller('ClientAfterMasterPage', function ($scope, $http, $rootScope, CookieUtil) {

        _.defer(function () { $scope.$apply(); });

        $scope.openTemplateSamplePageWithId = function (id) {
            if (mobileDevice)
                $('#sideBarMenuToggleButtonId').click();
            //alert(id);
            location.href = id;
        }

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
               { value: "Transcription", link: "#/templateSample/dataEntry/transcription" }
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
                 { value: "Product survey", link: "#/templateSample/survey/productSurvey" },
                 { value: "User feedback survey", link: "#" },
                 { value: "Pools", link: "#" }
               ]
           },
           {
               value: "Moderation", dropDownMenuShow: true, dropDownSubMenuClass: "dropdown-submenu", dropDownMenuClass: "dropdown-menu", dropDownSubMenuArrow: "dropdown", dropDownMenuList: [
                 { value: "Moderating Ads", link: "#" },
                 { value: "Moderating Photos", link: "#/templateSample/moderation/moderatingPhotos" },
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
