$(document).ready(function ($) {

    //console.log("inside signalR after login user " + $.cookie('utmzt'));
    var username = "";
    $("#sendControls2").hide("fast");
    var headers = {
        'Content-Type': 'application/json',
        'UTMZT': $.cookie('utmzt'),
        'UTMZK': $.cookie('utmzk'),
        'UTMZV': $.cookie('utmzv')
    };

    // Proxy created on the fly

    var notificationUserHub = $.connection.signalRUserHub;

    // Declare a function on the hub so the server can invoke it

    notificationUserHub.client.addMessage = function (message) {
        $('#userRealTimeStatusId').removeClass('text-warning').addClass('text-success');
        $('#userRealTimeStatusId').prop('title', 'bi-directional server-client connection established. You will be able to receive real time data.');
        //$("#messages2").append("<li>" + message + "</li>");

    };

    notificationUserHub.client.updateUserNotificationMessage = function (userType, newLink, newImageUrl, newMessageTitle, newMessagePostedInTimeAgo, newMessageContent) {
        var scope = angular.element(document.getElementById("mainUser")).scope();
        scope.$apply(function () {
            scope.updateUserNotificationMessage(userType, newLink, newImageUrl, newMessageTitle, newMessagePostedInTimeAgo, newMessageContent);
        });

    };

    notificationUserHub.client.updateAllUserTaskNotification = function (userType, newLink, newMessageTitle, newMessagePostedInTimeAgo, newMessageBody) {
        var scope = angular.element(document.getElementById("mainUser")).scope();
        scope.$apply(function () {
            scope.updateAllUserTaskNotification(userType, newLink, newMessageTitle, newMessagePostedInTimeAgo, newMessageBody);
        });

    };

    notificationUserHub.client.updateUserNotification = function (userType, newLink, newImageUrl, newMessageTitle, newMessagePostedInTimeAgo) {
        var scope = angular.element(document.getElementById("mainUser")).scope();
        scope.$apply(function () {
            scope.updateUserNotification(userType, newLink, newImageUrl, newMessageTitle, newMessagePostedInTimeAgo);
        });

    };

    // Start the connection

    $.connection.hub.start().done(function () {

        notificationUserHub.server.registerUser($.cookie('utmzt'));
        $("#registration2").hide("fast");
        $("#sendControls2").show("fast");

        $("#register2").click(function () {

            // Register client.

            notificationUserHub.server.registerUser($("#user2").val());

            $("#registration2").hide("fast");

            $("#sendControls2").show("fast");

        });

        $("#send2").click(function () {

            // Call the method on the server

            notificationUserHub.server.addNotification($("#msg2").val(), $("#toUsr2").val());

        });

    });

});
