function Chimera_LoadDashboardPage(ReportCategoryArray, ReportSeriesArray)
{
    $(".date-picker").datepicker();

    $('#container').highcharts({
        title: {
            text: 'Page View / Website Visits',
            x: -20,
            y: 2
        },
        xAxis: {
            categories: ReportCategoryArray
        },
        yAxis: {
            title: {
                text: ''
            },
            plotLines: [{
                value: 0,
                width: 1,
                color: '#808080'
            }]
        },
        exporting: {
            buttons: {
                contextButton: {
                    enabled: false
                },
                exportButton: {
                    text: 'Download',
                    // Use only the download related menu items from the default context button
                    menuItems: Highcharts.getOptions().exporting.buttons.contextButton.menuItems.splice(2)
                },
                printButton: {
                    text: 'Print',
                    onclick: function ()
                    {
                        this.print();
                    }
                }
            }
        },
        series: ReportSeriesArray
    });
}