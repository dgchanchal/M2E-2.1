$(function () {

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

    var notificationClientHub = $.connection.signalRClientHub;

    // Declare a function on the hub so the server can invoke it

    notificationClientHub.client.addMessage = function (message) {

        $("#messages2").append("<li>" + message + "</li>");

    };

    // Start the connection

    $.connection.hub.start().done(function () {

        //        $.ajax({
        //            url: ServerContextPah + '/Auth/GetUsernameFromSessionId',
        //            type: "POST",
        //            data: $.cookie('utmzt'),
        //            headers: headers
        //        }).success(function (data, status, headers, config) {
        //            //$scope.persons = data; // assign  $scope.persons here as promise is resolved here                
        //            if (data.Status == "200") {
        //                //console.log("registered " + data.Payload);
        //                notificationClientHub.server.registerClient(data.Payload);
        //                $("#registration2").hide("fast");
        //                $("#sendControls2").show("fast");
        //            }

        //        }).error(function (data, status, headers, config) {
        //            console.log("internal server error");
        //        });
        notificationClientHub.server.registerClient($.cookie('utmzt'));
        $("#registration2").hide("fast");
        $("#sendControls2").show("fast");

        $("#register2").click(function () {

            // Register client.

            notificationClientHub.server.registerClient($("#user2").val());

            $("#registration2").hide("fast");

            $("#sendControls2").show("fast");

        });

        $("#send2").click(function () {

            // Call the method on the server

            notificationClientHub.server.addNotification($("#msg2").val(), $("#toUsr2").val());

        });

    });

});