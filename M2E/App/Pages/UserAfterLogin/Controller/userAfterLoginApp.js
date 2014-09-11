'use strict';
define([appLocation.userPostLogin], function (app) {
    app.config(function ($routeProvider) {
        //(mobileDevice) ? "../../App/Pages/UserAfterLogin/Index/MobileIndex.html"  : "../../App/Pages/UserAfterLogin/Index/Index.html" }).
        $routeProvider.when("/", { templateUrl: "../../App/Pages/UserAfterLogin/Index/Index.html" }).
                       when("/edit", { templateUrl: "../../App/Pages/UserAfterLogin/EditPage/EditPage.html" }).
        //when("/showTemplateDetail/:refKey", { templateUrl: "../../App/Pages/UserAfterLogin/ShowTemplateDetail/ShowTemplateDetail.html" }).
                       when("/showTemplateDetail/:type/:subType/:refKey", { templateUrl: "../../App/Pages/UserAfterLogin/ShowTemplateDetail/ShowTemplateDetail.html" }).
                       when("/userThreads/:status", { templateUrl: "../../App/Pages/UserAfterLogin/UserActiveThreads/UserActiveThreads.html" }).
                       when("/startSurvey/:refKey", { templateUrl: (mobileDevice) ? "../../App/Pages/UserAfterLogin/Survey/MobileSurvey.html" : "../../App/Pages/UserAfterLogin/Survey/WebSurvey.html" }).
                       when("/startTranscription/:refKey", { templateUrl: "../../App/Pages/UserAfterLogin/DataEntry/TranscriptionTemplate/TranscriptionTemplate.html" }).
                       when("/startAngularTranscription/:refKey", { templateUrl: "../../App/Pages/UserAfterLogin/DataEntry/TranscriptionTemplate/AngularTranscriptionTemplate.html" }).
                       when("/mobileModeration", { templateUrl: "../../App/Pages/UserAfterLogin/Moderation/MobileModeration.html" }).
                       when("/webModeration", { templateUrl: "../../App/Pages/UserAfterLogin/Moderation/WebModeration.html" }).
                       when("/imageModeration/:refKey", { templateUrl: "../../App/Pages/UserAfterLogin/Moderation/WebModeration.html" }).
                       when("/showTemplate", { templateUrl: "../../App/Pages/UserAfterLogin/ShowTemplate/ShowTemplate.html" }).
                       when("/editTemplate/:username/:templateid", { templateUrl: "../../App/Pages/UserAfterLogin/EditTemplate/EditTemplate.html" }).
                       when("/templateSample/:type/:subType", { templateUrl: "../../App/Pages/UserAfterLogin/TemplateSample/TemplateSample.html" }).
                       when("/mobileSlide", { templateUrl: "../../App/Pages/UserAfterLogin/MobileSlide/MobileSlide.html" }).
                       when("/mobileSlide2", { templateUrl: "../../App/Pages/UserAfterLogin/MobileSlide2/MobileSlide2.html" }).
                       when("/mobileSlide3", { templateUrl: "../../App/Pages/UserAfterLogin/MobileSlide2/MobileSlide3.html" }).
                       when("/facebookLikePage", { templateUrl: "../../App/Pages/UserAfterLogin/Ads/facebookLike/facebookLike.html" }).
                       when("/myReferrals", { templateUrl: "../../App/Pages/UserAfterLogin/Referrals/Referrals.html" }).
                       otherwise({ templateUrl: "../../Resource/templates/beforeLogin/contentView/404.html" });

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
            $(".sidebar .treeview").tree();
            var htmlQRImage = "";
            //htmlQRImage = "<img src=\"http://chart.apis.google.com/chart?cht=qr&chs=200x200&chl=" + next.replace("#", "%23") + "\"/>";
            $("#QRPageImageId").html(htmlQRImage);
            gaWeb("BeforeLogin-Page Visited", "Page Visited", next);
            var path = next.split('#');
            gaPageView(path, 'title');
        });
    });

    app.controller('UserAfterMasterPage', function ($scope, $http, $rootScope, CookieUtil) {

        _.defer(function () { $scope.$apply(); });

        $scope.signOut = function () {
            logout();
        }

        loadClientDetails();

        function loadClientDetails() {
            var url = ServerContextPah + '/Client/GetClientDetails';
            var headers = {
                'Content-Type': 'application/json',
                'UTMZT': CookieUtil.getUTMZT(),
                'UTMZK': CookieUtil.getUTMZK(),
                'UTMZV': CookieUtil.getUTMZV()
            };
            startBlockUI('wait..', 3);
            $http({
                url: url,
                method: "POST",
                headers: headers
            }).success(function (data, status, headers, config) {
                //$scope.persons = data; // assign  $scope.persons here as promise is resolved here
                stopBlockUI();
                if (data.Status == "200") {
                    $rootScope.clientDetailResponse = data.Payload;
                    CookieUtil.setUserName(data.Payload.FirstName + ' ' + data.Payload.LastName, userSession.keepMeSignedIn);
                    CookieUtil.setUserImageUrl(data.Payload.imageUrl, userSession.keepMeSignedIn);
                    if (data.Payload.isLocked == "true") {
                        location.href = "/Auth/LockAccount?status=true";
                    }
                }
                else if (data.Status == "404") {

                    alert("This template is not present in database");
                }
                else if (data.Status == "500") {

                    alert("Internal Server Error Occured");
                }
                else if (data.Status == "401") {
                    location.href = "/?type=info&mssg=your session is expired/#/login";
                }
            }).error(function (data, status, headers, config) {

            });
        }

        $scope.ClientCategoryList = [
       {
           MainCategory: "Category",
           subCategoryList: [
           {
               value: "Data entry", dropDownMenuShow: true, dropDownSubMenuClass: "dropdown-submenu", dropDownMenuClass: "dropdown-menu", dropDownSubMenuArrow: "dropdown", dropDownMenuList: [
               { value: "Verification & Duplication", link: "#/VerificationAndDuplicationSample" },
               { value: "Data Collection", link: "#" },
               { value: "Tagging of an Image", link: "#" },
               { value: "Search the web", link: "#" },
               { value: "Do Excel work", link: "#" },
               { value: "Find information", link: "#" },
               { value: "Post advertisements", link: "#" },
               { value: "Transcription", link: "#" },
               { value: "Transcription from A/V", link: "#" }
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
                 { value: "Pools", link: "#" },
                 { value: "Survey Link", link: "#" }
               ]
           },
           {
               value: "Moderation", dropDownMenuShow: true, dropDownSubMenuClass: "dropdown-submenu", dropDownMenuClass: "dropdown-menu", dropDownSubMenuArrow: "dropdown", dropDownMenuList: [
                 { value: "Moderating Ads", link: "#" },
                 { value: "Moderating Photos", link: "#/mobileModeration" },
                 { value: "Moderating Music", link: "#" },
                 { value: "Moderating Video", link: "#" }
               ]
           },
           {
               value: "Ads", dropDownMenuShow: true, dropDownSubMenuClass: "dropdown-submenu", dropDownMenuClass: "dropdown-menu", dropDownSubMenuArrow: "dropdown", dropDownMenuList: [
                 { value: "Facebook Views", link: "#" },
                 { value: "Facebook likes", link: "#/facebookLikePage" },
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
