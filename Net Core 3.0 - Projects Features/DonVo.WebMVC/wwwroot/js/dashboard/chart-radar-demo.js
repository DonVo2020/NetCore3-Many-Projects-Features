$('#radarChart').ready(function() {
    $.get("/Dashboard/RadarData", function(result) {
        var myRadarChart = new Chart(document.getElementById("radarChart").getContext('2d'), {
            type: 'radar',
            data: {
                labels: result.labels,
                datasets: [
                    {
                        label: result.data[0].label,
                        data: result.data[0].data,
                        backgroundColor: [
                            'rgba(105, 0, 132, .2)',
                        ],
                        borderColor: [
                            'rgba(200, 99, 132, .7)',
                        ],
                        borderWidth: 2
                    },
                    {
                        label: result.data[1].label,
                        data: result.data[1].data,
                        backgroundColor: [
                            'rgba(0, 250, 220, .2)',
                        ],
                        borderColor: [
                            'rgba(0, 213, 132, .7)',
                        ],
                        borderWidth: 2
                    },
                    {
                        label: result.data[2].label,
                        data: result.data[2].data,
                        backgroundColor: [
                            'rgba(0, 137, 132, .2)',
                        ],
                        borderColor: [
                            'rgba(0, 10, 130, .7)',
                        ],
                        borderWidth: 2
                    }
                ]
            },
            options: {
                responsive: true
            }
        });
    });
});