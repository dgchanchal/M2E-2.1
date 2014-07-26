'use strict';
define([appLocation.postLogin], function(app) {

    app.factory('CookieUtil', function ($rootScope, $routeParams) {

        return {
            setUTMZT: function (UTMZT, keepMeSignedIn) {
                if (keepMeSignedIn) {
                    $.cookie('utmzt', UTMZT, { expires: 365, path: '/' });
                }
                else {
                    $.cookie('utmzt', UTMZT, { path: '/' });
                }
            },
            setUTMZK: function (UTMZK, keepMeSignedIn) {
                if (keepMeSignedIn) {
                    $.cookie('utmzk', UTMZK, { expires: 365, path: '/' });
                }
                else {
                    $.cookie('utmzk', UTMZK, { path: '/' });
                }
            },
            setUTMZV: function (UTMZV, keepMeSignedIn) {
                if (keepMeSignedIn) {
                    $.cookie('utmzv', UTMZV, { expires: 365, path: '/' });
                }
                else {
                    $.cookie('utmzv', UTMZV, { path: '/' });
                }
            },
            setUTIME: function (UTIME, keepMeSignedIn) {
                if (keepMeSignedIn) {
                    $.cookie('utime', UTIME, { expires: 365, path: '/' });
                }
                else {
                    $.cookie('utime', UTIME, { path: '/' });
                }
            },
            setKMSI: function (KMSI, keepMeSignedIn) {
                if (keepMeSignedIn) {
                    $.cookie('kmsi', KMSI, { expires: 365, path: '/' });
                }
                else {
                    $.cookie('kmsi', KMSI, { path: '/' });
                }
            },
            getUTMZT: function () {
                return $.cookie('utmzt');
            },
            getUTMZK: function () {
                return $.cookie('utmzk');
            },
            getUTMZV: function () {
                return $.cookie('utmzv');
            },
            getUTIME: function () {
                return $.cookie('utime');
            },
            getKMSI: function () {
                return $.cookie('kmsi');
            },
            removeUTMZT: function () {
                $.removeCookie('utmzt', { path: '/' });
            },
            removeUTMZK: function () {
                $.removeCookie('utmzk', { path: '/' });
            },
            removeUTMZV: function () {
                $.removeCookie('utmzv', { path: '/' });
            },
            removeUTIME: function () {
                $.removeCookie('utime', { path: '/' });
            },
            removeKMSI: function () {
                $.removeCookie('kmsi', { path: '/' });
            }
        };

    });


});
