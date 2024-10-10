window.dotnetInstance;
export function setdotnetInstance(dotnetInstance) {
    window.dotnetInstance = dotnetInstance;
}

//$(function () {
//    $('input[name="daterange"]').daterangepicker({
//        opens: 'left'
//    }, function (start, end, label) {
//        console.log("A new date selection was made: " + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD'));
//        filterIrradLabel(start.format('YYYY-MM-DD'), end.format('YYYY-MM-DD'))
//    });
//})
export function initializeDateRange() {
    $('input[name="daterange"]').daterangepicker({
        opens: 'left'
    }, function (start, end, label) {
        console.log("A new date selection was made: " + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD'));
        filterIrradLabel(start.format('YYYY-MM-DD'), end.format('YYYY-MM-DD'), 'Irradiation')
    });
}
function filterIrradLabel(start, end) {
    dotnetInstance.invokeMethodAsync('FilterIrradLabel', start, end)
        .then(data => {
            console.log(data);
        });
}
