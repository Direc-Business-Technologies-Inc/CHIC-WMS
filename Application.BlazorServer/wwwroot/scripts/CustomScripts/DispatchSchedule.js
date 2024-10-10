window.dotnetInstance;
export function setdotnetInstance(dotnetInstance) {
    window.dotnetInstance = dotnetInstance;
}

//$(function () {
//    $('input[name="daterange"]').daterangepicker({
//        opens: 'left'
//    }, function (start, end, label) {
//        console.log("A new date selection was made: " + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD'));
//        filterSchedule(start.format('YYYY-MM-DD'), end.format('YYYY-MM-DD'), 'Dispatch')
//    });
//})

export function initializeDateRange() {
    $('input[name="daterange"]').daterangepicker({
        opens: 'left'
    }, function (start, end, label) {
        console.log("A new date selection was made: " + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD'));
        filterSchedule(start.format('YYYY-MM-DD'), end.format('YYYY-MM-DD'), 'Dispatch')
    });
}

function filterSchedule(start, end, type) {
    dotnetInstance.invokeMethodAsync('FilterSchedule', start, end, type)
        .then(data => {
            console.log(data);
        });
}


