window.dotnetInstance;
export function setdotnetInstance(dotnetInstance) {
    window.dotnetInstance = dotnetInstance;
}

$(document).on('click', '#btn-clear-all', function () {
    confirm('Clear all Data?').then(function (result) {
        if (result) {
            clearData();
        }
    });
}).on('keypress', 'input[type="number"]', function (e) {
    const char = String.fromCharCode(e.which);
    const input = e.target.value + char;

    const regex = /^-?\d*\.?\d*$/;

    if (!regex.test(input)) {
        e.preventDefault();
    }
})
//    .on('keypress', 'input[type="number"]', function (e) {
//    var charCode = (e.which) ? e.which : event.keyCode
//    if (charCode > 31 && (charCode < 48 || charCode > 57))
//        return false;
//    return true;
//});

//$('input[type="number"]').on('keypress', function (e) {
//    var charCode = (e.which) ? e.which : event.keyCode
//    if (charCode > 31 && (charCode < 48 || charCode > 57))
//        return false;
//    return true;
//});

//function isNumberKey(evt) {
//    var charCode = (evt.which) ? evt.which : event.keyCode
//    if (charCode > 31 && (charCode < 48 || charCode > 57))
//        return false;
//    return true;
//}

function clearData() {
    dotnetInstance.invokeMethodAsync('ClearData')
        .then(data => {
            console.log(data);
        });
}


function saveQCMaintenance() {
    confirm('Save Inspection Plan?').then(function (result) {
        if (result) {
            dotnetInstance.invokeMethodAsync('SaveQCMaintenance')
                .then(data => {
                    console.log(data);
                });
        }
    });

}

//export function select2Dropdown(id) {
//    debugger;
//    $('#' + id).select2({});
//}

export function InitializeValidator() {
    const wizardValidationForm = document.querySelector('#wizard-validation-form');

    const MappingFormValidation = FormValidation.formValidation(wizardValidationForm, {
        fields: {
            'inspection-plan-name': {
                validators: {
                    notEmpty: {
                        message: 'Inspection Plan Name is required',
                    },
                }
            },
            'item-code': {
                validators: {
                    notEmpty: {
                        message: 'Item Code is required',
                    },
                }
            },
            'item-name': {
                validators: {
                    notEmpty: {
                        message: 'Item Name is required',
                    },
                }
            },
            //'customer-code': {
            //    validators: {
            //        notEmpty: {
            //            message: 'Customer Code field is required',
            //        },
            //    }
            //},
            //'customer-name': {
            //    validators: {
            //        notEmpty: {
            //            message: 'Customer Name field is required',
            //        },
            //    }
            //},
            //'NoOfSamples': {
            //    validators: {
            //        notEmpty: {
            //            message: 'No of Samples field is required',
            //        },
            //    }
            //},
            //'TotalNumberOfBoxes': {
            //    validators: {
            //        notEmpty: {
            //            message: 'Total Number of Boxes field is required',
            //        },
            //    }
            //},
            //'number-of-dosimeters': {
            //    validators: {
            //        callback: {
            //            message: 'Number of Dosimeters field is required',
            //            callback: function (input) {
            //                if ($('#slc-plan-type').val() == 'QA/QC Receiving Inspection Plan') {
            //                    return true;
            //                }
            //                else {
            //                    return $('#number-of-dosimeters').val() == "";
            //                }
            //            }
            //        },
            //    }
            //},
            //'sample-pass-tolerance-percentage': {
            //    validators: {
            //        notEmpty: {
            //            message: 'Sample Pass Tolerance Percentage field is required',
            //        },
            //    }
            //},
            //'overall-pass-tolerance-percentage': {
            //    validators: {
            //        notEmpty: {
            //            message: 'Overall Pass Tolerance Percentage field is required',
            //        },
            //    }
            //},
            //'dosimeter-location': {
            //    validators: {
            //        callback: {
            //            message: 'Dosimeter Location field is required',
            //            callback: function (input) {
            //                if ($('#slc-plan-type').val() == 'QA/QC Receiving Inspection Plan') {
            //                    return { valid: true };
            //                }
            //                else {
            //                    return {
            //                        valid: ($('#dosimeter-location').val() == "")
            //                    };
            //                }
            //            }
            //        },
            //    }
            //}
            // * Validate the fields here based on your requirements
        },

        plugins: {
            trigger: new FormValidation.plugins.Trigger(),
            bootstrap5: new FormValidation.plugins.Bootstrap5({
                // Use this for enabling/changing valid/invalid class
                // eleInvalidClass: '',
                eleValidClass: '',
                // rowSelector: '.col-lg-6'
            }),
            //autoFocus: new FormValidation.plugins.AutoFocus(),
            submitButton: new FormValidation.plugins.SubmitButton()
        }
    }).on('core.form.valid', function () {
        if ($('#slc-plan-type').val() != 'QA/QC Receiving Inspection Plan') {

            if ($('#number-of-dosimeters').val() == '') {
                ShowResult("Info", "Number of Dosimeters field is required");
                return;
            }

            //if ($('#dosimeter-location').val() == '') {
            //    ShowResult("Info", "Dosimeter Location field is required");
            //    return;
            //}
        }

        //if ($('#txtNoOfSamples').val() == '') {
        //    ShowResult("Info", "Number of Samples is required");
        //    return;
        //}

        //if ($('#txtTotalNumberOfBoxes').val() == '') {
        //    ShowResult("Info", "Total Number of Boxes is required");
        //    return;
        //}

        if ($('#sample-pass-tolerance-percentage').val() == '') {
            ShowResult("Info", "Sample Pass Tolerance Percentage is required");
            return;
        }

        if ($('#overall-pass-tolerance-percentage').val() == '') {
            ShowResult("Info", "Overall Pass Tolerance Percentage is required");
            return;
        }

        // Jump to the next step when all fields in the current step are valid
        if (+$('#txtTotalWeight').val() != 100) {
            ShowResult("Info", "Total Weight should be 100!");
            return;
        }

        saveQCMaintenance();
    });
}