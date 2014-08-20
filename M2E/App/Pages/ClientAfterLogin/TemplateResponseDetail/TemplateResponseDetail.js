'use strict';
define([appLocation.postLogin], function (app) {

    app.controller('ClientAfterLoginTemplateResponseDetail', function ($scope, $http, $rootScope, $routeParams, CookieUtil, $route) {
        $('title').html("Template Info page"); //TODO: change the title so cann't be tracked in log
        $scope.templateId = $routeParams.templateId;

        

        initializeGetTemplateSurveyResponseResultById();
        function initializeGetTemplateSurveyResponseResultById() {
            var url = ServerContextPah + '/Client/GetTemplateSurveyResponseResultById?id=' + $routeParams.templateId;
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
                stopBlockUI();
                $scope.ClientTemplateResponseDetailModel = data.Payload;                
                $.each($scope.ClientTemplateResponseDetailModel.resultList, function () {
                    //console.log(this.index);
                    this.dataList=[];
                    this.dataListUI=[];
                    this.optionsList = this.options.split(';');
                    var i=0;             
                    for (var key in  this.resultMap) {
                        var dataOptionsUI = {text:this.optionsList[i],key:key,value:this.resultMap[key]};
                        var currentIndexData=[];
                        if(this.optionsList[i].match(/<img/))
                        {
                            currentIndexData = [key,this.resultMap[key]];  
                        }
                        else
                        {
                            currentIndexData = [this.optionsList[i],this.resultMap[key]];                                        
                        }
                                              
                        this.dataList.push(currentIndexData);                        
                        this.dataListUI.push(dataOptionsUI);
                        i++;           
                    }
                    //console.log(this.dataListUI);                  
                    this.highCharts = renderHighcharts(this.dataList);                    
                });                
                
                $('.fancybox').fancybox();
            }).error(function (data, status, headers, config) {
                console.log("failure");
            });
        };

        function renderHighcharts(dataList)
        {            
            var higchartsRender = {
                chart: {
                                    type: 'column'
                                },
                                title: {
                                    text: ''
                                },            
                                xAxis: {
                                    type: 'category',
                                    labels: {
                                    formatter: function() {
                                        return this.value;
                                    },
                                    useHTML: true
                                }
                                },
                                yAxis: {
                                    min: 0,
                                    title: {
                                        text: 'Result'
                                    }
                                },
                                legend: {
                                    enabled: false
                                },
                                tooltip: {
                                    pointFormat: 'Total : <b>{point.y:.1f} </b>',
                                },
                                series: [{
                                    name: 'Result',
                                    data: dataList,
                                    dataLabels: {
                                        enabled: true,
                                        rotation: -90,
                                        color: '#FFFFFF',
                                        align: 'right',
                                        x: 4,
                                        y: 10,
                                        style: {
                                            fontSize: '13px',
                                            fontFamily: 'Verdana, sans-serif',
                                            textShadow: '0 0 3px black'
                                        }
                                    }
                                }]
              };

              return higchartsRender;
        }
        
        function loadHighCharts() {                    
                    $('#TemplateResponseDetailSAQ12').highcharts({
                        chart: {
                            type: 'column'
                        },
                        title: {
                            text: 'This is Question?'
                        },            
                        xAxis: {
                            type: 'category',
                            labels: {
                            formatter: function() {
                                return this.value;
                            },
                            useHTML: true
                        }
                        },
                        yAxis: {
                            min: 0,
                            title: {
                                text: 'Population (millions)'
                            }
                        },
                        legend: {
                            enabled: false
                        },
                        tooltip: {
                            pointFormat: 'Population in 2008: <b>{point.y:.1f} millions</b>',
                        },
                        series: [{
                            name: 'Population',
                            data: [
                                ['0', 23.7],
                                ['1', 16,1],
                                ['2', 14.2],
                                ['3', 14.0],
                                ['4', 12.5],
                                ['5', 12.1],
                                ['6', 11.8],
                                ['7', 11.7],
                                ['8', 11.1],
                                ['9', 11.1],
                                ['10', 10.5],
                                ['11', 10.4],
                                ['12', 10.0]                    
                            ],
                            dataLabels: {
                                enabled: true,
                                rotation: -90,
                                color: '#FFFFFF',
                                align: 'right',
                                x: 4,
                                y: 10,
                                style: {
                                    fontSize: '13px',
                                    fontFamily: 'Verdana, sans-serif',
                                    textShadow: '0 0 3px black'
                                }
                            }
                        }]
                    });
                       
        };

        $scope.deleteTemplatePageWithId = function (id) {
            var url = ServerContextPah + '/Client/DeleteTemplateDetailById?id=' + id;
            var headers = {
                'Content-Type': 'application/json',
                'UTMZT': CookieUtil.getUTMZT(),
                'UTMZK': CookieUtil.getUTMZK(),
                'UTMZV': CookieUtil.getUTMZV()
            };
            if (confirm("Template will be permanently deleted. Are you sure?") == true) {
                startBlockUI('wait..', 3);
                $http({
                    url: url,
                    method: "POST",
                    headers: headers
                }).success(function (data, status, headers, config) {
                    //$scope.persons = data; // assign  $scope.persons here as promise is resolved here

                    if (data.Status == "200") {
                        stopBlockUI();
                        showToastMessage("Success", "Deleted Successfully");
                        location.href = "#/";
                    }
                    else if (data.Status == "404") {
                        stopBlockUI();
                        alert("This template is not present in database");
                    }
                    else if (data.Status == "500") {
                        stopBlockUI();
                        alert("Internal Server Error Occured");
                    }
                }).error(function (data, status, headers, config) {

                });
            } else {

            }


        }

        $scope.openTemplateEditPageWithId = function (id) {
            //$('#closeModalPopup' + id).click();
            //alert(id);
            location.href = "#/editTemplate/edit/" + id;
        }

        $scope.openTemplateInfoPageWithId = function (id) {
            //$('#closeModalPopup' + id).click();
            //alert(id);
            location.href = "#/templateInfo/Survey/ProductSurvey/" + id;
        }
    });

});
