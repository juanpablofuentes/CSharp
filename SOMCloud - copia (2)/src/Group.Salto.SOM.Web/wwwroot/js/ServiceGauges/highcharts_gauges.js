function createVisitGauge(visits, max, Interval1, interval2, name, Name2, esp) {
    var needle = (visits > 0) ? visits : 0;
    var needmax = (max > 0) ? max : 1;
    var interval1 = parseFloat(Interval1);
    var needInterval1 = needmax * interval1;
    var needInterval2 = needmax * interval2;

    var chart = new Highcharts.Chart({
        chart: {
            renderTo: name,
            type: 'gauge',
            alignTicks: false,
            plotBackgroundColor: null,
            plotBackgroundImage: null,
            plotBorderWidth: 0,
            plotShadow: false
        },
        title: {
            text: Name2
        },
        pane: {
            startAngle: -140,
            endAngle: 140,
            size: 155,
            innerRadius: 20
        },
        yAxis: [{
            min: 0,
            max: needmax,
            offset: -5,
            lineWidth: 2,
            minorTickPosition: 'outside',
            tickPosition: 'outside',
            labels: {
                rotation: 'auto',
                distance: 20
            },
            plotBands: [{
                from: 0,
                to: needInterval1,
                color: '#36EBAB',
                innerRadius: '93%',
                outerRadius: '105%'
            }, {
                from: needInterval1,
                to: needInterval2,
                color: '#FFCE56',
                innerRadius: '93%',
                outerRadius: '105%'
            }, {
                from: needInterval2,
                to: needmax,
                color: '#FF6384',
                innerRadius: '93%',
                outerRadius: '105%'
            }],
            pane: 0,
            title: {
                text: esp,
                y: 95
            }
        }],
        plotOptions: {
            gauge: {
                dial: {
                    radius: '115%'
                }
            }
        },
        series: [{
            name: name,
            data: [needle],
            dataLabels: {
                formatter: function () {
                    var DATA = this.y;
                    return '<span style="color:#339">' + DATA + '  </span><br/>';
                },
                backgroundColor: {
                    linearGradient: {
                        x1: 0,
                        y1: 0,
                        x2: 0,
                        y2: 1
                    },
                    stops: [
                        [0, '#DDD'],
                        [1, '#FFF']
                    ]
                }
            },
            function(chart) {
                setInterval(function () {
                    var point = chart.series[0].points[0],
                        newVal, point;
                    newVal = point.y;
                    point.update(newVal);

                })
            }
        }]
    });
}

function drawPiChar2data(data1, data2, name, title) {

    var dataOut = data2 - data1;

    var chart = new Highcharts.Chart({
            chart: {
                renderTo: name,
                plotBackgroundColor: null,
                plotBorderWidth: null,
                plotShadow: false,
                type: 'pie'
            },
            title: {
                text: title
            },
            tooltip: {
                pointFormat: '<b>{point.name}</b>: {point.percentage:.1f} % <br><b>{point.name}</b>: {point.y}'
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: true,
                        format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                        style: {
                            color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                        }
                    }
                }
            },
            series: [{
                name: '',
                colorByPoint: true,
                data: [{
                    name: 'In SLA',
                    y: data1,
                    sliced: true,
                    selected: true
                }, {
                    name: 'Out SLA',
                    y: dataOut
                }]
            }]
    });
}

function createLinesGauge(container, datas, name, title, projectStr, start, end) {

    var months = getMonthsBetweenDates(start, end);
    var Datas = [];
    for (var x = 0; x < datas.length; x++) {
        var tmp = datas[x];
        Datas[x] = fillSeries(tmp, months);
    }
    var names = [];
    var data = [];
    for (var w = 0; w < Datas.length; w++) {
        for (var q = 0; q < Datas[w].length; q++) {
            names.push(Datas[w][q].name);
            data.push(Datas[w][q].data);
        }
    }
    var chart = new Highcharts.Chart({

        chart: {
            renderTo: name,
            type: 'spline'

        },

        title: {
            text: title
        },

        yAxis: {
            title: {
                text: ''

            },
            labels: {

                format: '{value} €'

            }
        },

        xAxis: {
            categories: names,

            type: 'category',

            labels: {

                rotation: -45,

                style: {

                    fontSize: '10px',

                    fontFamily: 'Verdana, sans-serif'

                }

            }

        },

        legend: {
            layout: 'horizontal',
            align: 'center',
            verticalAlign: 'bottom'
        },

        series: [{
            name: projectStr,
            data: data
        }]
    });
}

function getMonthsBetweenDates(start, end) {
    var months = [];
    var first = new Date(start);
    var last = new Date(end)
    var firstMonth = new Date(first.getFullYear(), first.getMonth(), 1);
    var lastMonth = new Date(last.getFullYear(), last.getMonth() + 1, 1);
    while (firstMonth < lastMonth) {
        firstday = new Date(firstMonth.getFullYear(), firstMonth.getMonth(), 1, 0, 0, 0);
        months.push(firstday.getFullYear() + ' - ' + (firstday.getMonth() + 1));
        firstMonth = firstMonth.addMonths(1);
    }
    return months;
}

function fillSeries(series, months) {
    var arr = [];
    for (var i = 0; i < months.length; i++) {
        var l = arr.length;
        //for (var j = 0; j < 1/*series.length*/; j++) {
        if (months[i] === series/*[j]*/.name) {
            arr.push({ name: series/*[j]*/.name, data: series/*[j]*/.data });
        }
        //}
        else  {
            arr.push({ name: months[i], data: null });
        }
    }
    return arr;
}

Date.prototype.addMonths = function (m) {
    var d = new Date(this);
    var years = Math.floor(m / 12);
    var months = m - (years * 12);
    if (years) d.setFullYear(d.getFullYear() + years);
    if (months) d.setMonth(d.getMonth() + months);
    return d;
}