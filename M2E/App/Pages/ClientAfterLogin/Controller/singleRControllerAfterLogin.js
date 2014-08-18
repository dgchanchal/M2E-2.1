$(function () {
    // Proxy created on the fly          
    var chat = $.connection.signalRHub;

    // Get the user name and store it to prepend to messages.
    //$('#displayname').val(prompt('Enter your name:', ''));

    // Declare a function on the chat hub so the server can invoke it          
    chat.client.sendMessage = function (name, message) {

        var encodedName = $('<div />').text(name).html();
        var encodedMsg = $('<div />').text(message).html();
        $('#messages').append('<li>' + encodedName +
						':  ' + encodedMsg + '</li>');
        showToastMessage("Success", name + ": sent Message - " + message);
    };

    chat.client.updateBeforeLoginUserProjectDetails = function (totalProjects, successRate, totalUsers, projectCategories) {
        //alert("updateBeforeLoginUserProjectDetailsService");
        var scope = angular.element(document.getElementById("beforeLoginIndexControllerId")).scope();
        scope.$apply(function () {
            scope.updateBeforeLoginUserProjectDetailsDiv(totalProjects, successRate, totalUsers, projectCategories);
        });
        //showStickyToastMessage("Success", totalProjects + " - " + successRate + " - " + totalUsers + " - " + projectCategories);
    };

    // Start the connection
    $.connection.hub.start().done(function () {
        $("#send").click(function () {
            // Call the chat method on the server
            chat.server.send($('#displayname').val(), $('#msg').val());
        });
    });
});

$(function () {

    $("#sendControls2").hide("fast");

    // Proxy created on the fly

    var notificationHub = $.connection.signalRHub;

    // Declare a function on the hub so the server can invoke it

    notificationHub.client.addMessage = function (message) {

        $("#messages2").append("<li>" + message + "</li>");

    };

    // Start the connection

    $.connection.hub.start().done(function () {

        $("#register2").click(function () {

            // Register client.

            notificationHub.server.registerClient($("#user2").val());

            $("#registration2").hide("fast");

            $("#sendControls2").show("fast");

        });

        $("#send2").click(function () {

            // Call the method on the server

            notificationHub.server.addNotification($("#msg2").val(), $("#toUsr2").val());

        });

    });

});