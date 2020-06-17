//-------------
//- DONUT CHART -
//-------------
// Get context with jQuery - using jQuery's .get() method.
var donutChartCanvas = $('#saleChart').get(0).getContext('2d')
var aylik = $('#aylik').val();
var ucaylik = $('#3aylik').val();
var altiaylik = $('#6aylik').val();
var donutData = {
    labels: [
        'Aylık',
        '3 Aylık',
        '6 Aylık',

    ],
    datasets: [
        {
            data: [aylik, ucaylik, altiaylik],
            backgroundColor: ['#f56954', '#00a65a', '#f39c12'],
        }
    ]
}
var donutOptions = {
    maintainAspectRatio: false,
    responsive: true,
}
//Create pie or douhnut chart
// You can switch between pie and douhnut using the method below.
var donutChart = new Chart(donutChartCanvas, {
    type: 'doughnut',
    data: donutData,
    options: donutOptions
})