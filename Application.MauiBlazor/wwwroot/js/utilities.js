

function FocusById(elementId) {
    if (elementId != null && elementId != undefined) {
        $("#" + elementId).focus();
    }
}

function FocusByElemRef(elementRef) {
    if (elementRef != null && elementRef != undefined) {
        elementRef.focus();
    }
}

function myKeyPress(elementRef) {
    elementRef.addEventListener('keydown', function (event) {
        return event.key; // "a", "1", "Shift", etc.
    });
}