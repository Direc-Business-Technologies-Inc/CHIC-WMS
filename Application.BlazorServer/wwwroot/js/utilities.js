

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

function ShowResult(level, message) {
    switch (level) {
        case 'Info':
            toastr.info(message, "Info");
            break;
        case 'Success':
            toastr.success(message,"Success");
            break;
        case 'Warning':
            toastr.warning(message, "Warning");
            break;
        case 'Error':
        default:
            toastr.error(message, "Error")
            break;
    }
    console.log(level);
}