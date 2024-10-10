window.dotnetInstance;
export function setdotnetInstance(dotnetInstance) {
    window.dotnetInstance = dotnetInstance;
}

window.verticalStepper

export function InitializeStepper() {
    const wizardVertical = document.querySelector('.wizard-vertical'),
        wizardVerticalBtnNextList = [].slice.call(wizardVertical.querySelectorAll('.btn-next')),
        wizardVerticalBtnPrevList = [].slice.call(wizardVertical.querySelectorAll('.btn-prev')),
        wizardVerticalBtnSubmit = wizardVertical.querySelector('.btn-submit');

    if (typeof wizardVertical !== undefined && wizardVertical !== null) {
        window.verticalStepper = new Stepper(wizardVertical, {
            linear: true
        });
        if (wizardVerticalBtnNextList) {
            wizardVerticalBtnNextList.forEach(wizardVerticalBtnNext => {
                wizardVerticalBtnNext.addEventListener('click', event => {
                    if ($(wizardVerticalBtnNext).hasClass('btn-next-samples')) {
                        if (+$('#txt-header-sample-size').val() <= 0) {
                            ShowResult("Info", "Please Generate QC Order before proceeding");
                            return;
                        }
                    }
                    window.verticalStepper.next();
                });
            });
        }
        if (wizardVerticalBtnPrevList) {
            wizardVerticalBtnPrevList.forEach(wizardVerticalBtnPrev => {
                wizardVerticalBtnPrev.addEventListener('click', event => {
                    window.verticalStepper.previous();
                });
            });
        }

        if (wizardVerticalBtnSubmit) {
            wizardVerticalBtnSubmit.addEventListener('click', event => {
                dotnetInstance.invokeMethodAsync('SubmitParameter')
                    .then(data => {
                        console.log(data);
                        if (data.success) {
                            ShowResult("Success", "Parameter Details Saved");
                        }
                        if (data.previousPage) {
                            window.verticalStepper.previous();
                        }
                    });
            });
        }

    }

    const wizardValidationForm = document.querySelector('#wizard-validation-form');
    const MappingFormValidation = FormValidation.formValidation(wizardValidationForm, {
        fields: {
            //'po-no': {
            //    validators: {
            //        notEmpty: {
            //            message: 'PO No is required',
            //        },
            //    }
            //},
            //'manufacturing-lot-no': {
            //    validators: {
            //        notEmpty: {
            //            message: 'Manufacturing Lot No is required',
            //        },
            //    }
            //},
            //'service-order-no': {
            //    validators: {
            //        notEmpty: {
            //            message: 'Service Order No is required',
            //        },
            //    }
            //},
            //'storage-conditions': {
            //    validators: {
            //        notEmpty: {
            //            message: 'Storage Conditions is required',
            //        },
            //    }
            //},
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
            //submitButton: new FormValidation.plugins.SubmitButton()
        }
    }).on('core.form.valid', function () {

        //if ($('#select-plan-type-header').val() == 'Dosimetry Quality Control Order') {

        //    if ($('#eb-operation-log').val() == '') {
        //        ShowResult("Info", "EB Operation Log is required");
        //        return;
        //    }

        //    if ($('#txt-actual-energy').val() == '') {
        //        ShowResult("Info", "Actual Energy is required");
        //        return;
        //    }

        //    if ($('#txt-total-products-before-irradiation').val() == '') {
        //        ShowResult("Info", "Total Products Before Irradiation is required");
        //        return;
        //    }

        //    if ($('#txt-actual-power').val() == '') {
        //        ShowResult("Info", "Actual Power is required");
        //        return;
        //    }

        //    if ($('#txt-total-products-after-irradiation').val() == '') {
        //        ShowResult("Info", "Total Products After Irradiation is required");
        //        return;
        //    }

        //    if ($('#txt-actual-frequency').val() == '') {
        //        ShowResult("Info", "Actual Frequency is required");
        //        return;
        //    }

        //    if ($('#nc-report').val() == '') {
        //        ShowResult("Info", "NC Report is required");
        //        return;
        //    }

        //    if ($('#txt-dosimetry-remarks').val() == '') {
        //        ShowResult("Info", "Dosimetry Remarks is required");
        //        return;
        //    }
        //}

        //dotnetInstance.invokeMethodAsync('SaveQCOrder');
    });
}

export function nextStep() {
    window.verticalStepper.next();
}

export function checkInspectionPlan() {
    return new Promise((resolve) => {
        let checker = $('#inspection-plan').val() !== "";
        resolve(checker);
    });
}
$(document).on('change', '#select-plan-type', function () {
    const planType = $(this).val();
    dotnetInstance.invokeMethodAsync('SelectInputBackend', 'SO Plan Type', planType);
}).on('change', '#txt-so-sample-size', function () {
    const sampleSize = $(this).val();
    dotnetInstance.invokeMethodAsync('SelectInputBackend', 'SO Sample Size', sampleSize);
}).on('click', '#btn-generate', function () {
    if ($('#txt-sales-order-no').val() == "") {
        ShowResult("Info", "Please Insert SO Number");
        return;
    }

    if (+$('#txt-so-sample-size').val() <= 0) {
        ShowResult("Info", "Please Insert Sample Size");
        return;
    }

    dotnetInstance.invokeMethodAsync('GenerateSO');

    $('[data-bs-toggle=popover]').popover('hide');

    $('#txt-sales-order-no').val('');
    $('#select-plan-type').val('QA/QC Receiving Inspection Plan');
    $('#txt-so-sample-size').val('0');
    $('#txt-so-quantity').val('0');
}).on('keypress', 'input[type="number"]', function (e) {
    //var charCode = (e.which) ? e.which : event.keyCode
    //if (charCode > 31 && (charCode < 48 || charCode > 57))
    //    return false;
    //return true;
    var charCode = (e.which) ? e.which : event.keyCode;
    if (
        (charCode >= 48 && charCode <= 57) || // Allow digits (0-9)
        charCode === 46 // Allow the period (.)
    ) {
        return true;
    } else {
        return false;
    }
});

export function setDocNum(docNum, quantity) {
    $('#txt-sales-order-no').val(docNum);
    $('#txt-so-quantity').val(quantity);
}
