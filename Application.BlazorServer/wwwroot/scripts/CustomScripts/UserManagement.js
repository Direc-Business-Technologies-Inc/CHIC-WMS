window.dotnetInstance;
export function setdotnetInstance(dotnetInstance) {
    window.dotnetInstance = dotnetInstance;
}

export function InitializeValidator() {
    const wizardValidationForm = document.querySelector('#user-info');

    const MappingFormValidation = FormValidation.formValidation(wizardValidationForm, {
        fields: {
            'first-name': {
                validators: {
                    notEmpty: {
                        message: 'First Name is required',
                    },
                }
            },
            'lastName': {
                validators: {
                    notEmpty: {
                        message: 'Last Name is required',
                    },
                }
            },
            'email': {
                validators: {
                    notEmpty: {
                        message: 'Email is required',
                    },
                    emailAddress: {
                        message: 'The value is not a valid email address'
                    }
                }
            },
            'dept': {
                validators: {
                    notEmpty: {
                        message: 'Department is required',
                    },
                }
            },
            'userName': {
                validators: {
                    notEmpty: {
                        message: 'Username is required',
                    },
                }
            },
            'password': {
                validators: {
                    notEmpty: {
                        message: 'Password is required',
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
        dotnetInstance.invokeMethodAsync('SaveUser');
    });
}