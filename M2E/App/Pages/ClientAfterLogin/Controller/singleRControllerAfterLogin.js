$(document).ready(function () {

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


    notificationClientHub.client.updateClientProgressChart = function (Id, JobTotal, JobCompleted, JobAssigned, JobReviewed) {
        //showToastMessage("Success", "update client progress bar Successfully");
        $('#progressBar' + Id).css('width', (JobCompleted * 100) / JobTotal + '%');
        $('#progressBarValue' + Id).html(((JobCompleted * 100) / JobTotal) + '%');
        if ($("#container_highcharts_completed_vs_reviewed" + Id).length != 0) {
            showToastMessage("Success", "update client progress bar Successfully");
            render_container_highcharts_completed_vs_reviewed(Id, parseInt(JobTotal), parseInt(JobReviewed), parseInt(JobTotal) - parseInt(JobReviewed));
            render_container_highcharts_completed_vs_assigned_vs_remaining(Id, parseInt(JobCompleted), parseInt(JobAssigned), parseInt(JobTotal - JobCompleted));
            render_container_highcharts_horizontal_bar_chart_ratio_completed_reviewed_remaining(Id, parseInt(JobCompleted), parseInt(JobAssigned), parseInt(JobReviewed), parseInt(JobTotal - JobCompleted), parseInt(JobTotal));
        }       

    };

    // Start the connection

    $.connection.hub.start().done(function () {

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

function render_container_highcharts_horizontal_bar_chart_ratio_completed_reviewed_remaining(id, completed, assigned, reviewed, remaining, total) {

    Highcharts.setOptions({
        colors: ['#368ee0']
    });

    $('#container_highcharts_horizontal_bar_chart_ratio_completed_reviewed_remaining' + id).highcharts({
        chart: {
            type: 'column',
            margin: 75,
            options3d: {
                enabled: true,
                alpha: 10,
                beta: 25,
                depth: 70
            }
        },
        title: {
            text: 'Ratio'
        },
        subtitle: {
            text: 'completed : assigned : reviewed : remaining : total'
        },
        plotOptions: {
            column: {
                depth: 25
            }
        },
        xAxis: {
            categories: ['completed', 'assigned', 'reviewed', 'remaining', 'total']
        },
        yAxis: {
            opposite: true
        },
        credits: {
            enabled: false
        },
        series: [{
            name: 'Values',
            data: [completed, assigned, reviewed, remaining, total]
        }]
    });
}
function render_container_highcharts_completed_vs_assigned_vs_remaining(id, jobCompleted, jobAssigned, remaining) {

    Highcharts.setOptions({
        colors: ['#0000FF', '#FF0000', '#5cb85c']
    });

    $('#container_highcharts_completed_vs_reviewed' + id).highcharts({
        chart: {
            type: 'pie',
            options3d: {
                enabled: true,
                alpha: 45
            }
        },
        title: {
            text: 'Ratio'
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        subtitle: {
            text: 'Completed : Assigned : Remaining'
        },
        plotOptions: {
            pie: {
                size: '100%',
                innerSize: 100,
                depth: 45,
                dataLabels: {
                    enabled: false
                }
            }
        },
        series: [{
            name: 'Percentage - ',
            data: [
                                ['Completed', jobCompleted],
                                ['Assigned', jobAssigned],
                                ['remaining', remaining]
                            ]
        }]
    });
}
function render_container_highcharts_completed_vs_reviewed(id, total, reviewed, remaining) {
    Highcharts.setOptions({
        colors: ['#00FF00', '#0000FF']
    });

    $('#container_highcharts_completed_vs_remaining' + id).highcharts({
        chart: {
            type: 'pie',
            options3d: {
                enabled: true,
                alpha: 45
            }
        },
        title: {
            text: 'Total Tasks In Progress - ' + total
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        subtitle: {
            text: 'completed : reviewed'
        },
        plotOptions: {
            pie: {
                size: '100%',
                innerSize: 100,
                depth: 45,
                dataLabels: {
                    enabled: false
                }
            }
        },
        series: [{
            name: 'Percentage - ',
            data: [
                                ['Review Done', reviewed],
                                ['To be Reviewed', remaining]
                            ]
        }]
    });
}