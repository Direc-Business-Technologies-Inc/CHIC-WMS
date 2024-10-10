window.dotnetInstance;
export function setdotnetInstance(dotnetInstance) {
    window.dotnetInstance = dotnetInstance;
}

$(function () {
    var startIrradiation = document.getElementById('txt-start-irradiation');
    var durationIrradiation = document.getElementById('txt-duration-irradiation');
    var endIrradiation = document.getElementById('txt-end-irradiation');

    var start = startIrradiation.flatpickr({
        enableTime: true,
        noCalendar: true,
        dateFormat: "H:i", // Use 24-hour format
        time_24hr: true, // Use 24-hour clock
        onChange: durationChanged
    });

    var duration = durationIrradiation.flatpickr({
        enableTime: true,
        noCalendar: true,
        dateFormat: "H:i", // Use 24-hour format
        time_24hr: true, // Use 24-hour clock
        onChange: durationChanged
    });

    var end = endIrradiation.flatpickr({
        enableTime: true,
        noCalendar: true,
        dateFormat: "H:i", // Use 24-hour format
        time_24hr: true, // Use 24-hour clock
        allowInput: true
    });


    // Function to calculate and set end date based on start date and duration
    function durationChanged(selectedDates, dateStr, instance) {
        const startDate = start.selectedDates[0];
        const durationTime = duration.selectedDates[0];

        if (startDate && durationTime) {
            const durationHours = durationTime.getHours();
            const durationMinutes = durationTime.getMinutes();

            const newEndDate = new Date(startDate);
            newEndDate.setHours(startDate.getHours() + durationHours);
            newEndDate.setMinutes(startDate.getMinutes() + durationMinutes);

            end.setDate(newEndDate);

            dotnetInstance.invokeMethodAsync('durationChangeBackend', newEndDate);
        }
    }
})

