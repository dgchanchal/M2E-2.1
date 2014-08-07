'use strict';
define([appLocation.userPostLogin], function (app) {

    app.controller('UserAfterLoginTemplateSample', function ($scope, $http, $rootScope, CookieUtil) {
        $('title').html("edit page"); //TODO: change the title so cann't be tracked in log

        $("input[type='checkbox'], input[type='radio']").iCheck({
            checkboxClass: 'icheckbox_minimal',
            radioClass: 'iradio_minimal'
        });
        
    });

});
