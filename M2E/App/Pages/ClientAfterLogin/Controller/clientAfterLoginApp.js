'use strict';
define([appLocation.postLogin], function (app) {
    app.config(function ($routeProvider) {

        $routeProvider.when("/", { templateUrl: "../../App/Pages/ClientAfterLogin/Index/Index.html" }).
                       when("/edit", { templateUrl: "../../App/Pages/ClientAfterLogin/EditPage/EditPage.html" }).
                       when("/createTemplate/:type/:subType", { templateUrl: "../../App/Pages/ClientAfterLogin/Survey/CreateTemplate/CreateTemplate.html" }).
                       when("/editTemplate/:username/:templateid", { templateUrl: "../../App/Pages/ClientAfterLogin/EditTemplate/EditTemplate.html" }).
                       when("/templateSample/:type/:subType", { templateUrl: "../../App/Pages/ClientAfterLogin/TemplateSample/TemplateSample.html" }).
                       when("/templateInfo/:type/:subType/:templateId", { templateUrl: "../../App/Pages/ClientAfterLogin/TemplateInfo/TemplateInfo.html" }).
                       when("/templateResponseDetail/:type/:subType/:templateId", { templateUrl: "../../App/Pages/ClientAfterLogin/TemplateResponseDetail/TemplateResponseDetail.html" }).
                       when("/transcriptionResponseDetail/:type/:subType/:templateId", { templateUrl: "../../App/Pages/ClientAfterLogin/DataEntry/TranscriptionResponseDetail/TranscriptionResponseDetail.html" }).
                       when("/moderatingPhotos/:type/:subType", { templateUrl: "../../App/Pages/ClientAfterLogin/Moderation/ModeratingPhotos/ModeratingPhotos.html" }).
                       when("/moderatingPhotosResponseDetail/:type/:subType/:templateId", { templateUrl: "../../App/Pages/ClientAfterLogin/Moderation/ModeratingPhotosResponseDetail/ModeratingPhotosResponseDetail.html" }).
                       when("/transcriptionTemplate/:type/:subType", { templateUrl: "../../App/Pages/ClientAfterLogin/DataEntry/TranscriptionTemplate/TranscriptionTemplate.html" }).
                       when("/dataCollectionTemplate/:type/:subType", { templateUrl: "../../App/Pages/ClientAfterLogin/DataEntry/dataCollection/dataCollection.html" }).
                       when("/surveyLinkTemplate/:type/:subType", { templateUrl: "../../App/Pages/ClientAfterLogin/Survey/SurveyLink/SurveyLink.html" }).
                       when("/taggingImageTemplate/:type/:subType", { templateUrl: "../../App/Pages/ClientAfterLogin/DataEntry/taggingImage/taggingImage.html" }).
                       when("/transcribeAVTemplate/:type/:subType", { templateUrl: "../../App/Pages/ClientAfterLogin/DataEntry/TranscribeAV/TranscribeAV.html" }).
                       when("/articleWrittingTemplate/:type/:subType", { templateUrl: "../../App/Pages/ClientAfterLogin/ContentWritting/articleWritting/articleWritting.html" }).
                       when("/facebookLikeTemplate/:type/:subType", { templateUrl: "../../App/Pages/ClientAfterLogin/Ads/facebookLike/facebookLike.html" }).
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

        $scope.signOut = function () {
            CookieUtil.removeUTMZT();
            CookieUtil.removeUTMZK();
            CookieUtil.removeUTMZV();
            CookieUtil.removeUTIME();
            CookieUtil.removeKMSI();
            CookieUtil.removeKMSI();            
            location.href = "/";
        }

        $scope.ClientCategoryList = [
       {
           MainCategory: "Category",
           subCategoryList: [
           {
               value: "Data entry", dropDownMenuShow: true, dropDownSubMenuClass: "dropdown-submenu", dropDownMenuClass: "dropdown-menu", dropDownSubMenuArrow: "dropdown", dropDownMenuList: [
               { value: "Verification & Duplication", link: "#/VerificationAndDuplicationSample" },
               { value: "Data Collection", link: "#/dataCollectionTemplate/" + TemplateInfoModel.type_dataEntry + "/" + TemplateInfoModel.subType_dataCollection },
               { value: "Tagging of an Image", link: "#/taggingImageTemplate/" + TemplateInfoModel.type_dataEntry + "/" + TemplateInfoModel.subType_taggingImage },
               { value: "Search the web", link: "#" },
               { value: "Do Excel work", link: "#" },
               { value: "Find information", link: "#" },
               { value: "Post advertisements", link: "#" },
               { value: "Transcription", link: "#/templateSample/" + TemplateInfoModel.type_dataEntry + "/" + TemplateInfoModel.subType_Transcription },
               { value: "Transcription from A/V", link: "#/transcribeAVTemplate/" + TemplateInfoModel.type_dataEntry + "/" + TemplateInfoModel.subType_transcribeAV }
               ]
           },
           {
               value: "Content Writing", dropDownMenuShow: true, dropDownSubMenuClass: "dropdown-submenu", dropDownMenuClass: "dropdown-menu", dropDownSubMenuArrow: "dropdown", dropDownMenuList: [
                 { value: "Article writing", link: "#/articleWrittingTemplate/" + TemplateInfoModel.type_contentWritting + "/" + TemplateInfoModel.subType_articleWritting },
                 { value: "Blog writing", link: "#/articleWrittingTemplate/" + TemplateInfoModel.type_contentWritting + "/" + TemplateInfoModel.subType_blogWriting },
                 { value: "Copy typing", link: "#/articleWrittingTemplate/" + TemplateInfoModel.type_contentWritting + "/" + TemplateInfoModel.subType_copyTyping },
                 { value: "Powerpoint", link: "#/articleWrittingTemplate/" + TemplateInfoModel.type_contentWritting + "/" + TemplateInfoModel.subType_powerpoint },
                 { value: "Short stories", link: "#/articleWrittingTemplate/" + TemplateInfoModel.type_contentWritting + "/" + TemplateInfoModel.subType_shortStories },
                 { value: "Travel writing", link: "#/articleWrittingTemplate/" + TemplateInfoModel.type_contentWritting + "/" + TemplateInfoModel.subType_travelWriting },
                 { value: "Reviews", link: "#/articleWrittingTemplate/" + TemplateInfoModel.type_contentWritting + "/" + TemplateInfoModel.subType_reviews },
                 { value: "Product descriptions", link: "#/articleWrittingTemplate/" + TemplateInfoModel.type_contentWritting + "/" + TemplateInfoModel.subType_productDescriptions }
               ]
           },
           {
               value: "Survey", dropDownMenuShow: true, dropDownSubMenuClass: "dropdown-submenu", dropDownMenuClass: "dropdown-menu", dropDownSubMenuArrow: "dropdown", dropDownMenuList: [
                 { value: "Product survey", link: "#/templateSample/" + TemplateInfoModel.type_survey + "/" + TemplateInfoModel.subType_productSurvey },
                 { value: "User feedback survey", link: "#/templateSample/" + TemplateInfoModel.type_survey + "/" + TemplateInfoModel.subType_productSurvey },
                 { value: "Pools", link: "#/templateSample/" + TemplateInfoModel.type_survey + "/" + TemplateInfoModel.subType_productSurvey },
                 { value: "Survey Link", link: "#/surveyLinkTemplate/" + TemplateInfoModel.type_survey + "/" + TemplateInfoModel.subType_surveyLink }
               ]
           },
           {
               value: "Moderation", dropDownMenuShow: true, dropDownSubMenuClass: "dropdown-submenu", dropDownMenuClass: "dropdown-menu", dropDownSubMenuArrow: "dropdown", dropDownMenuList: [
                 { value: "Moderating Ads", link: "#" },
                 { value: "Moderating Photos", link: "#/templateSample/" + TemplateInfoModel.type_moderation + "/" + TemplateInfoModel.subType_imageModeration },
                 { value: "Moderating Music", link: "#" },
                 { value: "Moderating Video", link: "#" }
               ]
           },
           {
               value: "Ads", dropDownMenuShow: true, dropDownSubMenuClass: "dropdown-submenu", dropDownMenuClass: "dropdown-menu", dropDownSubMenuArrow: "dropdown", dropDownMenuList: [
                 { value: "Facebook Views", link: "#" },
                 { value: "Facebook likes", link: "#/facebookLikeTemplate/" + TemplateInfoModel.type_Ads + "/" + TemplateInfoModel.subType_facebookLike },
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
