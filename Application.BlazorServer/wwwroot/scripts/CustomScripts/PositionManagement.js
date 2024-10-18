window.dotnetInstance;
export function setdotnetInstance(dotnetInstance) {
    window.dotnetInstance = dotnetInstance;
}

export function InitializeValidator() {
    const wizardValidationForm = document.querySelector('#pos-info');

    const MappingFormValidation = FormValidation.formValidation(wizardValidationForm, {
        fields: {
            'pos-name': {
                validators: {
                    notEmpty: {
                        message: 'Position Name is required',
                    },
                }
            }
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
        //alert('valid');
        dotnetInstance.invokeMethodAsync('SavePos');
    });
}