'use strict';
define([appLocation.userPostLogin], function (app) {

    app.controller('UserAfterLoginTranscriptionTemplate', function ($scope, $http, $rootScope, $routeParams, CookieUtil) {
        $('title').html("sample page"); //TODO: change the title so cann't be tracked in log
        console.log("transcription template");
        
            var $section = $('#inverted-contain');
            $section.find('.panzoom').panzoom({
                $zoomIn: $section.find(".zoom-in"),
                $zoomOut: $section.find(".zoom-out"),
                $zoomRange: $section.find(".zoom-range"),
                $reset: $section.find(".reset"),
                startTransform: 'scale(1.1)',
                increment: 0.1,
                minScale: 0.8,
                contain: 'invert'
            }).panzoom('zoom');        
    });

});
