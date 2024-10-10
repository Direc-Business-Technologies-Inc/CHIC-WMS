window.dotnetInstance;
export function setdotnetInstance(dotnetInstance) {
    window.dotnetInstance = dotnetInstance;
}

//$(function () {
//    $('input[name="daterange"]').daterangepicker({
//        opens: 'left'
//    }, function (start, end, label) {
//        console.log("A new date selection was made: " + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD'));
//        filterSchedule(start.format('YYYY-MM-DD'), end.format('YYYY-MM-DD'), 'Irradiation')
//    });
//})

export function initializeDateRange() {
    $('input[name="daterange"]').daterangepicker({
        opens: 'left'
    }, function (start, end, label) {
        console.log("A new date selection was made: " + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD'));
        filterSchedule(start.format('YYYY-MM-DD'), end.format('YYYY-MM-DD'), 'Irradiation')
    });
}

function filterSchedule(start, end, type) {
    dotnetInstance.invokeMethodAsync('FilterSchedule', start, end, type)
        .then(data => {
            console.log(data);
        });
}

$('#modal-schedules').on('shown.bs.modal', function () {
    // code to execute when the modal is opened
    $('.fc-dayGridMonth-button').click();
});

$("#search-input").on("input", function () {
    var searchValue = $(this).val().toLowerCase();
    $(".fc-list-event").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(searchValue) > -1);
    });
});

$(document).on('click', '.fc-button', function () {
    $("#search-input").val('');
    // Check if the element has a class of "fc-listMonth-button"
    if ($(this).hasClass('fc-listMonth-button')) {
        $('#div-search').removeClass('d-none');
    }
    else {
        $('#div-search').addClass('d-none');
    }
});

function AddAvailableHours(data) {
        const targetSelector = `.fc-daygrid-day-top`,
        daySelector = '.fc-daygrid-day-number';

    for (let n of document.getElementsByClassName('fc-daygrid-day')) {
        const date = n.getAttribute('data-date');
        const target = n.querySelector(targetSelector);

        let src = data[date];
        let hrs = src != null ? src : 6;

        let template = `
            <span class="p-1 ms-auto me-2 availHours badge bg-${hrs > 0 ? 'secondary' : 'danger'} d-flex align-items-center">${hrs}</span>
        `;

        $(".availHours", target).remove();
        $(target).append(template);

    }

    var targets = document.querySelector(`#calendar ${targetSelector}`);
        


}

function AddLineBorder() {
    for (let n of document.getElementsByClassName('fc-daygrid-event-harness')) {

        n.style.borderBottom = "1px solid black";

    }
}

export function loadCalendar() {
    let direction = 'ltr';

    //if (isRtl) {
    //    direction = 'rtl';
    //}

    let events = [];

    const calendarEl = document.getElementById('calendar'),
        appCalendarSidebar = document.querySelector('.app-calendar-sidebar'),
        addEventSidebar = document.getElementById('addEventSidebar'),
        appOverlay = document.querySelector('.app-overlay'),
        appEventDuration = document.getElementById('eventDuration'),
        //calendarsColor = {
        //  Business: 'primary',
        //  Holiday: 'success',
        //  Personal: 'danger',
        //  Family: 'warning',
        //  ETC: 'info'
        //},
        calendarsColor = {
            Scheduled: 'success',
            Tentative: 'danger',
            Received: 'primary',
        },
        offcanvasTitle = document.querySelector('.offcanvas-title'),
        btnToggleSidebar = document.querySelector('.btn-toggle-sidebar'),
        btnAddEvent = document.querySelector('.btn-add-event'),
        btnUpdateEvent = document.querySelector('.btn-update-event'),
        btnDeleteEvent = document.querySelector('.btn-delete-event'),
        btnCancel = document.querySelector('.btn-cancel'),
        eventCustomer = document.querySelector('#eventCustomer'),
        eventDeliveryDate = document.querySelector('#eventDeliveryDate'),
        eventPickupDate = document.querySelector('#eventPickupDate'),
        eventNoOfDosimeters = document.querySelector('#eventNoOfDosimeters'),
        //eventIrradiation = document.querySelector('#eventIrradiation'),
        eventSO = document.querySelector('#eventSO'),
        eventStatus = $('#eventStatus'), // ! Using jquery vars due to select2 jQuery dependency
        eventItemName = document.querySelector('#eventItemName'), // ! Using jquery vars due to select2 jQuery dependency
        eventBatchNo = document.querySelector('#eventBatchNo'),
        eventRemarks = document.querySelector('#eventRemarks'),  
        selectAll = document.querySelector('.select-all'),
        filterInput = [].slice.call(document.querySelectorAll('.input-filter')),
        inlineCalendar = document.querySelector('.inline-calendar');

    let eventToUpdate,
        currentEvents = events, // Assign app-calendar-events.js file events (assume events from API) to currentEvents (browser store/object) to manage and update calender events
        isFormValid = false,
        inlineCalInstance;

    // Init event Offcanvas
    const bsAddEventSidebar = new bootstrap.Offcanvas(addEventSidebar);

    //! TODO: Update Event label and guest code to JS once select removes jQuery dependency
    // Event Label (select2)
    if (eventStatus.length) {
        function renderBadges(option) {
            if (!option.id) {
                return option.text;
            }
            var $badge =
                "<span class='badge badge-dot bg-" + $(option.element).data('label') + " me-2'> " + '</span>' + option.text;

            return $badge;
        }
        eventStatus.wrap('<div class="position-relative"></div>').select2({
            placeholder: 'Select value',
            dropdownParent: eventStatus.parent(),
            templateResult: renderBadges,
            templateSelection: renderBadges,
            minimumResultsForSearch: -1,
            escapeMarkup: function (es) {
                return es;
            }
        });
    }

    // Event Guests (select2)
    //if (eventItemName.length) {
    //  function renderGuestAvatar(option) {
    //    if (!option.id) {
    //      return option.text;
    //    }
    //    var $avatar =
    //      "<div class='d-flex flex-wrap align-items-center'>" +
    //      "<div class='avatar avatar-xs me-2'>" +
    //      "<img src='" +
    //      assetsPath +
    //      'img/avatars/' +
    //      $(option.element).data('avatar') +
    //      "' alt='avatar' class='rounded-circle' />" +
    //      '</div>' +
    //      option.text +
    //      '</div>';

    //    return $avatar;
    //  }
    //  eventItemName.wrap('<div class="position-relative"></div>').select2({
    //    placeholder: 'Select value',
    //    dropdownParent: eventItemName.parent(),
    //    closeOnSelect: false,
    //    templateResult: renderGuestAvatar,
    //    templateSelection: renderGuestAvatar,
    //    escapeMarkup: function (es) {
    //      return es;
    //    }
    //  });
    //}

    var start, end, duration;

    // Event start (flatpicker)
    if (eventDeliveryDate) {
        start = eventDeliveryDate.flatpickr({
            enableTime: true,
            altFormat: 'Y-m-dTH:i:S',
            onReady: function (selectedDates, dateStr, instance) {
                if (instance.isMobile) {
                    instance.mobileInput.setAttribute('step', null);
                }
            },
            onChange: durationChanged
        });
    }

    // Event end (flatpicker)
    if (eventPickupDate) {
        end = eventPickupDate.flatpickr({
            enableTime: true,
            altFormat: 'Y-m-dTH:i:S',
            allowInput: true
            //disabled: true
            //onReady: function (selectedDates, dateStr, instance) {
            //    if (instance.isMobile) {
            //        instance.mobileInput.setAttribute('step', null);
            //    }
            //},
        });
        //end.set("readonly", true);
    }

    if (appEventDuration) {
        duration = appEventDuration.flatpickr({
            enableTime: true,
            noCalendar: true,
            dateFormat: "H:i", // Use 24-hour format
            time_24hr: true, // Use 24-hour clock
            onChange: durationChanged
        });
    }

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
        }
    }

    //// Event irradiation (flatpicker)
    //if (eventIrradiation) {
    //    var irrad = eventIrradiation.flatpickr({
    //        enableTime: true,
    //        altFormat: 'Y-m-dTH:i:S',
    //        onReady: function (selectedDates, dateStr, instance) {
    //            if (instance.isMobile) {
    //                instance.mobileInput.setAttribute('step', null);
    //            }
    //        }
    //    });
    //}



    // Inline sidebar calendar (flatpicker)
    if (inlineCalendar) {
        inlineCalInstance = inlineCalendar.flatpickr({
            monthSelectorType: 'static',
            inline: true
        });
    }

    // Event click function
    function eventClick(info) {
        eventToUpdate = info.event;
        if (eventToUpdate.url) {
            info.jsEvent.preventDefault();
            window.open(eventToUpdate.url, '_blank');
        }
        bsAddEventSidebar.show();

        btnAddEvent.classList.add('d-none');
        btnUpdateEvent.classList.remove('d-none');

        // For update event set offcanvas title text: Update Event
        if (offcanvasTitle) {
            offcanvasTitle.innerHTML = 'Update Event';
        }
        //btnDeleteEvent.classList.remove('d-none');

        eventCustomer.value = eventToUpdate.title;

        if (eventToUpdate.extendedProps.calendar === 'Received') {
            start.setDate(new Date(eventToUpdate.extendedProps.start), true, 'Y-m-d');
            eventToUpdate.extendedProps.end !== null
                ? end.setDate(new Date(eventToUpdate.extendedProps.end), true, 'Y-m-d')
                : end.setDate(new Date(eventToUpdate.extendedProps.start), true, 'Y-m-d');
        } else {
            start.setDate(eventToUpdate.start, true, 'Y-m-d');
            eventToUpdate.end !== null
                ? end.setDate(eventToUpdate.end, true, 'Y-m-d')
                : end.setDate(eventToUpdate.start, true, 'Y-m-d');
        }

        debugger;
        eventStatus.val(eventToUpdate.extendedProps.calendar).trigger('change');
        eventToUpdate.extendedProps.location !== undefined
            ? (eventBatchNo.value = eventToUpdate.extendedProps.location)
            : null;
        eventToUpdate.extendedProps.itemname !== undefined
            ? (eventItemName.value = eventToUpdate.extendedProps.itemname)
            : null;
        eventToUpdate.extendedProps.description !== undefined
            ? (eventRemarks.value = eventToUpdate.extendedProps.description)
            : null;
        eventToUpdate.id !== undefined
            ? (eventSO.value = eventToUpdate.id)
            : null;

        const startDateValue = start.selectedDates[0];
        const endDateValue = end.selectedDates[0];
        const timeDifference = endDateValue.getTime() - startDateValue.getTime();

        const hours = Math.floor(timeDifference / (1000 * 60 * 60));
        const minutes = Math.floor((timeDifference % (1000 * 60 * 60)) / (1000 * 60));

        duration.setDate(`${hours}:${minutes}`, true, 'H:i');

        eventToUpdate.extendedProps.noofdosimeters !== undefined
            ? (eventNoOfDosimeters.value = eventToUpdate.extendedProps.noofdosimeters)
            : null;
        // // Call removeEvent function
        // btnDeleteEvent.addEventListener('click', e => {
        //   removeEvent(parseInt(eventToUpdate.id));
        //   // eventToUpdate.remove();
        //   bsAddEventSidebar.hide();
        // });
    }

    // Modify sidebar toggler
    function modifyToggler() {
        const fcSidebarToggleButton = document.querySelector('.fc-sidebarToggle-button');
        fcSidebarToggleButton.classList.remove('fc-button-primary');
        fcSidebarToggleButton.classList.add('d-lg-none', 'd-inline-block', 'ps-0');
        while (fcSidebarToggleButton.firstChild) {
            fcSidebarToggleButton.firstChild.remove();
        }
        fcSidebarToggleButton.setAttribute('data-bs-toggle', 'sidebar');
        fcSidebarToggleButton.setAttribute('data-overlay', '');
        fcSidebarToggleButton.setAttribute('data-target', '#app-calendar-sidebar');
        fcSidebarToggleButton.insertAdjacentHTML('beforeend', '<i class="bx bx-menu bx-sm text-body"></i>');
    }

    // Filter events by calender
    function selectedCalendars() {
        let selected = [],
            filterInputChecked = [].slice.call(document.querySelectorAll('.input-filter:checked'));

        filterInputChecked.forEach(item => {
            selected.push(item.getAttribute('data-value'));
        });

        return selected;
    }

    // --------------------------------------------------------------------------------------------------
    // AXIOS: fetchEvents
    // * This will be called by fullCalendar to fetch events. Also this can be used to refetch events.
    // --------------------------------------------------------------------------------------------------
    function fetchEvents(info, successCallback) {
        let selectedEvents = [];
        // Fetch Events from API endpoint reference
        dotnetInstance.invokeMethodAsync('getAllSchedules')
            .then(result => {
                debugger;
                // Get requested calendars as Array
                var calendars = selectedCalendars();
                console.log(calendars);

                result.events.forEach((event) => {
                    event.start = new Date(event.start).toISOString();
                    event.end = new Date(event.end).toISOString();
                });

                selectedEvents = result.events.filter(function (event) {
                    // console.log(event.extendedProps.calendar.toLowerCase());
                    //console.log(event.start);
                    //console.log(event.end);
                    return calendars.includes(event.extendedProps.calendar.toLowerCase());
                });
                let d = result.availableHours;
                AddAvailableHours(d);
                successCallback(selectedEvents);
                AddLineBorder();
                //return result.events.filter(event => calendars.includes(event.extendedProps.calendar));
                //calendar.addEvent(result.events.filter(event => calendars.includes(event.extendedProps.calendar)));
            });

        //let calendars = selectedCalendars();
        //// We are reading event object from app-calendar-events.js file directly by including that file above app-calendar file.
        //// You should make an API call, look into above commented API call for reference
        //let selectedEvents = currentEvents.filter(function (event) {
        //    // console.log(event.extendedProps.calendar.toLowerCase());
        //    return calendars.includes(event.extendedProps.calendar.toLowerCase());
        //});
        //// if (selectedEvents.length > 0) {
        /*successCallback(selectedEvents);*/
        // }
    }

    // Init FullCalendar
    // ------------------------------------------------
    let { dayGrid, interaction, timeGrid, list } = calendarPlugins;
    let calendar = new Calendar(calendarEl, {
        initialView: 'dayGridMonth',
        events: fetchEvents,
        plugins: [interaction, dayGrid, timeGrid, list],
        editable: true,
        dragScroll: true,
        dayMaxEvents: 2,
        eventResizableFromStart: true,
        customButtons: {
            sidebarToggle: {
                text: 'Sidebar'
            }
        },
        headerToolbar: {
            start: 'sidebarToggle, prev,next, title',
            end: 'dayGridMonth,timeGridWeek,timeGridDay,listMonth'
        },
        direction: direction,
        initialDate: new Date(),
        navLinks: true, // can click day/week names to navigate views
        eventClassNames: function ({ event: calendarEvent }) {
            const colorName = calendarsColor[calendarEvent._def.extendedProps.calendar];
            // Background Color
            return ['fc-event-' + colorName];
        },
        dateClick: function (info) {
            //let date = moment(info.date).format('YYYY-MM-DD');
            //resetValues();
            //bsAddEventSidebar.show();

            //// For new event set offcanvas title text: Add Event
            //if (offcanvasTitle) {
            //  offcanvasTitle.innerHTML = 'Add Event';
            //}

            //btnAddEvent.classList.remove('d-none');
            //btnUpdateEvent.classList.add('d-none');
            //btnDeleteEvent.classList.add('d-none');
            //eventDeliveryDate.value = date;
            //eventPickupDate.value = date;
        },
        eventClick: function (info) {
            eventClick(info);
        },
        datesSet: function () {
            modifyToggler();
        },
        viewDidMount: function () {
            modifyToggler();
        },
        eventDrop: function (info) {
            confirm('Update schedule?').then(function (result) {
                if (result) {
                    const start = new Date(info.event.start);
                    const end = new Date(info.event.end);

                    const eventstart = `${start.getFullYear()}-${('0' + (start.getMonth() + 1)).slice(-2)}-${('0' + start.getDate()).slice(-2)} ${('0' + start.getHours()).slice(-2)}:${('0' + start.getMinutes()).slice(-2)}`
                    const eventend = `${end.getFullYear()}-${('0' + (end.getMonth() + 1)).slice(-2)}-${('0' + end.getDate()).slice(-2)} ${('0' + end.getHours()).slice(-2)}:${('0' + end.getMinutes()).slice(-2)}`

                    let eventData = {
                        id: +info.event.id,
                        title: info.event.title,
                        start: eventstart,
                        end: eventend,
                        url: info.event.url,
                        extendedProps: {
                            itemname: info.event.extendedProps.itemname,
                            calendar: info.event.extendedProps.calendar,
                            description: info.event.extendedProps.description
                        }
                    };

                    dotnetInstance.invokeMethodAsync('updateSchedule', eventData);

                    //$.ajax({
                    //    url: '/Schedules/updateSchedule',
                    //    type: 'POST',
                    //    data: {
                    //        eventData: eventData
                    //    },
                    //    success: function (response) {
                    //        // handle success response
                    //        swal('Saved successfuly', 'success');
                    //        setTimeout(function () {
                    //            window.location.reload();
                    //        }, 1500);
                    //    },
                    //    error: function (xhr, status, error) {
                    //        // handle error response
                    //        swal(`Error: ${xhr.responseText}`, 'error');
                    //        info.revert();
                    //    }
                    //});
                }
                else {
                    info.revert();
                }
                AddLineBorder();
            });
        },
        eventResize: function (info) {
            confirm('Update schedule?').then(function (result) {
                if (result) {

                    const start = new Date(info.event.start);
                    const end = new Date(info.event.end);

                    const eventstart = `${start.getFullYear()}-${('0' + (start.getMonth() + 1)).slice(-2)}-${('0' + start.getDate()).slice(-2)} ${('0' + start.getHours()).slice(-2)}:${('0' + start.getMinutes()).slice(-2)}`
                    const eventend = `${end.getFullYear()}-${('0' + (end.getMonth() + 1)).slice(-2)}-${('0' + end.getDate()).slice(-2)} ${('0' + end.getHours()).slice(-2)}:${('0' + end.getMinutes()).slice(-2)}`

                    let eventData = {
                        id: +info.event.id,
                        title: info.event.title,
                        start: eventstart,
                        end: eventend,
                        url: info.event.url,
                        extendedProps: {
                            itemname: info.event.extendedProps.itemname,
                            calendar: info.event.extendedProps.calendar,
                            description: info.event.extendedProps.description
                        }
                    };

                    dotnetInstance.invokeMethodAsync('updateSchedule', eventData);

                    //$.ajax({
                    //    url: '/Schedules/updateSchedule',
                    //    type: 'POST',
                    //    data: {
                    //        eventData: eventData
                    //    },
                    //    success: function (response) {
                    //        // handle success response
                    //        swal('Saved successfuly', 'success');
                    //        setTimeout(function () {
                    //            window.location.reload();
                    //        }, 1500);
                    //    },
                    //    error: function (xhr, status, error) {
                    //        // handle error response
                    //        swal(`Error: ${xhr.responseText}`, 'error');
                    //        info.revert();
                    //    }
                    //});
                }
                else {
                    info.revert();
                }
                AddLineBorder();
            });
        }
    });

    // Render calendar
    calendar.render();
    // Modify sidebar toggler
    modifyToggler();

    const eventForm = document.getElementById('eventForm');
    const fv = FormValidation.formValidation(eventForm, {
        fields: {
            eventCustomer: {
                validators: {
                    notEmpty: {
                        message: 'Please enter event title '
                    }
                }
            },
            eventDeliveryDate: {
                validators: {
                    notEmpty: {
                        message: 'Please enter start date '
                    }
                }
            },
            eventPickupDate: {
                validators: {
                    notEmpty: {
                        message: 'Please enter end date '
                    }
                }
            }
        },
        plugins: {
            trigger: new FormValidation.plugins.Trigger(),
            bootstrap5: new FormValidation.plugins.Bootstrap5({
                // Use this for enabling/changing valid/invalid class
                eleValidClass: '',
                rowSelector: function (field, ele) {
                    // field is the field name & ele is the field element
                    return '.mb-3';
                }
            }),
            submitButton: new FormValidation.plugins.SubmitButton(),
            // Submit the form when all fields are valid
            // defaultSubmit: new FormValidation.plugins.DefaultSubmit(),
            autoFocus: new FormValidation.plugins.AutoFocus()
        }
    }).on('core.form.valid', function () {
        // Jump to the next step when all fields in the current step are valid
        isFormValid = true;
    });

    // Sidebar Toggle Btn
    if (btnToggleSidebar) {
        btnToggleSidebar.addEventListener('click', e => {
            btnCancel.classList.remove('d-none');
        });
    }

    // Add Event
    // ------------------------------------------------
    function addEvent(eventData) {
        // ? Add new event data to current events object and refetch it to display on calender
        // ? You can write below code to AJAX call success response

        currentEvents.push(eventData);
        calendar.refetchEvents();

        // ? To add event directly to calender (won't update currentEvents object)
        // calendar.addEvent(eventData);
    }

    // Update Event
    // ------------------------------------------------
    function updateEvent(eventData) {
        // ? Update existing event data to current events object and refetch it to display on calender
        // ? You can write below code to AJAX call success response
        eventData.id = parseInt(eventData.id);
        currentEvents[currentEvents.findIndex(el => el.id === eventData.id)] = eventData; // Update event by id
        calendar.refetchEvents();

        // ? To update event directly to calender (won't update currentEvents object)
        // let propsToUpdate = ['id', 'title', 'url'];
        // let extendedPropsToUpdate = ['calendar', 'guests', 'location', 'description'];

        // updateEventInCalendar(eventData, propsToUpdate, extendedPropsToUpdate);
    }

    // Remove Event
    // ------------------------------------------------

    function removeEvent(eventId) {
        // ? Delete existing event data to current events object and refetch it to display on calender
        // ? You can write below code to AJAX call success response
        currentEvents = currentEvents.filter(function (event) {
            return event.id != eventId;
        });
        calendar.refetchEvents();

        // ? To delete event directly to calender (won't update currentEvents object)
        // removeEventInCalendar(eventId);
    }

    // (Update Event In Calendar (UI Only)
    // ------------------------------------------------
    const updateEventInCalendar = (updatedEventData, propsToUpdate, extendedPropsToUpdate) => {
        const existingEvent = calendar.getEventById(updatedEventData.id);

        // --- Set event properties except date related ----- //
        // ? Docs: https://fullcalendar.io/docs/Event-setProp
        // dateRelatedProps => ['start', 'end', 'allDay']
        // eslint-disable-next-line no-plusplus
        for (var index = 0; index < propsToUpdate.length; index++) {
            var propName = propsToUpdate[index];
            existingEvent.setProp(propName, updatedEventData[propName]);
        }

        // --- Set date related props ----- //
        // ? Docs: https://fullcalendar.io/docs/Event-setDates
        existingEvent.setDates(updatedEventData.start, updatedEventData.end, {
            allDay: updatedEventData.allDay
        });

        // --- Set event's extendedProps ----- //
        // ? Docs: https://fullcalendar.io/docs/Event-setExtendedProp
        // eslint-disable-next-line no-plusplus
        for (var index = 0; index < extendedPropsToUpdate.length; index++) {
            var propName = extendedPropsToUpdate[index];
            existingEvent.setExtendedProp(propName, updatedEventData.extendedProps[propName]);
        }
    };

    // Remove Event In Calendar (UI Only)
    // ------------------------------------------------
    function removeEventInCalendar(eventId) {
        calendar.getEventById(eventId).remove();
    }

    // Add new event
    // ------------------------------------------------
    btnAddEvent.addEventListener('click', e => {
        if (isFormValid) {
            let newEvent = {
                id: calendar.getEvents().length + 1,
                title: eventCustomer.value,
                start: eventDeliveryDate.value,
                end: eventPickupDate.value,
                startStr: eventDeliveryDate.value,
                endStr: eventPickupDate.value,
                display: 'block',
                extendedProps: {
                    location: eventBatchNo.value,
                    guests: eventItemName.val(),
                    calendar: eventStatus.val(),
                    description: eventRemarks.value
                }
            };
            if (eventSO.value) {
                newEvent.url = eventSO.value;
            }
            addEvent(newEvent);
            bsAddEventSidebar.hide();
        }
    });

    // Update event
    // ------------------------------------------------
    btnUpdateEvent.addEventListener('click', e => {
        if (isFormValid) {
            confirm('Update schedule?').then(function (result) {
                if (result) {
                    let eventData = {
                        id: +eventToUpdate.id,
                        title: eventCustomer.value,
                        start: eventDeliveryDate.value,
                        end: eventPickupDate.value,
                        url: eventSO.value,
                        extendedProps: {
                            itemname: eventItemName.value,
                            calendar: eventStatus.val(),
                            description: eventRemarks.value
                        },
                        display: 'block',
                    };

                    dotnetInstance.invokeMethodAsync('updateSchedule', eventData);

                    //$.ajax({
                    //    url: '/Schedules/updateSchedule',
                    //    type: 'POST',
                    //    data: {
                    //        eventData: eventData
                    //    },
                    //    success: function (response) {
                    //        // handle success response
                    //        updateEvent(eventData);
                    //        swal('Saved successfuly', 'success');
                    //        bsAddEventSidebar.hide();
                    //    },
                    //    error: function (xhr, status, error) {
                    //        // handle error response
                    //        swal(`Error: ${xhr.responseText}`, 'error');
                    //    }
                    //});
                }
            });
        }
    });

    // Call removeEvent function
    btnDeleteEvent.addEventListener('click', e => {
        removeEvent(parseInt(eventToUpdate.id));
        // eventToUpdate.remove();
        bsAddEventSidebar.hide();
    });

    // Reset event form inputs values
    // ------------------------------------------------
    function resetValues() {
        eventPickupDate.value = '';
        eventSO.value = '';
        eventDeliveryDate.value = '';
        //eventIrradiation.value = '';
        eventCustomer.value = '';
        //eventBatchNo.value = '';
        eventItemName.value = '';
        eventRemarks.value = '';
    }

    // When modal hides reset input values
    addEventSidebar.addEventListener('hidden.bs.offcanvas', function () {
        resetValues();
    });

    // Hide left sidebar if the right sidebar is open
    btnToggleSidebar.addEventListener('click', e => {
        btnDeleteEvent.classList.add('d-none');
        btnUpdateEvent.classList.add('d-none');
        btnAddEvent.classList.remove('d-none');
        appCalendarSidebar.classList.remove('show');
        appOverlay.classList.remove('show');
    });

    // Calender filter functionality
    // ------------------------------------------------
    if (selectAll) {
        selectAll.addEventListener('click', e => {
            if (e.currentTarget.checked) {
                document.querySelectorAll('.input-filter').forEach(c => (c.checked = 1));
            } else {
                document.querySelectorAll('.input-filter').forEach(c => (c.checked = 0));
            }
            calendar.refetchEvents();
        });
    }

    if (filterInput) {
        filterInput.forEach(item => {
            item.addEventListener('click', () => {
                document.querySelectorAll('.input-filter:checked').length < document.querySelectorAll('.input-filter').length
                    ? (selectAll.checked = false)
                    : (selectAll.checked = true);
                calendar.refetchEvents();
            });
        });
    }

    // Jump to date on sidebar(inline) calendar change
    inlineCalInstance.config.onChange.push(function (date) {
        calendar.changeView(calendar.view.type, moment(date[0]).format('YYYY-MM-DD'));
        modifyToggler();
        appCalendarSidebar.classList.remove('show');
        appOverlay.classList.remove('show');
    });
}

export function approveAll() {
    confirm('Are you sure you want to approve this schedule?').then(function (result) {
        if (result) {
            let calTitle = document.querySelector('.fc-toolbar-title')
            let selectedDate = new Date(calTitle.innerHTML);
            dotnetInstance.invokeMethodAsync('getAllSchedules').then(res => {
                const data = res
                debugger;
                const curDayEvents = data.events.filter(x => x.start.split(' ')[0] === `${selectedDate.getFullYear()}-${selectedDate.getMonth() + 1 > 12 ? selectedDate.getMonth() : selectedDate.getMonth() + 1}-${selectedDate.getDate()}`)

                let eventsSchedules = []
                curDayEvents.forEach(x => {
                    const start = new Date(x.start);
                    const end = new Date(x.end);

                    const eventstart = `${start.getFullYear()}-${('0' + (start.getMonth() + 1)).slice(-2)}-${('0' + start.getDate()).slice(-2)} ${('0' + start.getHours()).slice(-2)}:${('0' + start.getMinutes()).slice(-2)}`
                    const eventend = `${end.getFullYear()}-${('0' + (end.getMonth() + 1)).slice(-2)}-${('0' + end.getDate()).slice(-2)} ${('0' + end.getHours()).slice(-2)}:${('0' + end.getMinutes()).slice(-2)}`

                    let eventData = {
                        id: +x.id,
                        title: x.title + '-approve',
                        start: eventstart,
                        end: eventend,
                        url: x.url,
                        extendedProps: {
                            itemname: x.extendedProps.itemname,
                            calendar: 'Scheduled',
                            description: x.extendedProps.description
                        }
                    }

                    eventsSchedules = [...eventsSchedules, eventData]
                })

                dotnetInstance.invokeMethodAsync('approveEventsSchedule', eventsSchedules);
            })
        }
    })
}