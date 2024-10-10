window.dotnetInstance;
export function setdotnetInstance(dotnetInstance) {
    window.dotnetInstance = dotnetInstance;
}


/**
 *  Pages Authentication
 */
//let fv;

window.fv;

export function loadPageAuth() {
    'use strict';
    const formAuthentication = document.querySelector('#formAuthentication');

    if (formAuthentication) {
        window.fv = FormValidation.formValidation(formAuthentication, {
            fields: {
                'email-username': {
                    validators: {
                        notEmpty: {
                            message: 'Please enter username'
                        },
                        //stringLength: {
                        //    min: 6,
                        //    message: 'Username must be more than 6 characters'
                        //}
                    }
                },
                'password': {
                    validators: {
                        notEmpty: {
                            message: 'Please enter your password'
                        },
                        //stringLength: {
                        //    min: 6,
                        //    message: 'Password must be more than 6 characters'
                        //}
                    }
                }
            },
            plugins: {
                trigger: new FormValidation.plugins.Trigger(),
                bootstrap5: new FormValidation.plugins.Bootstrap5({
                    eleValidClass: '',
                    rowSelector: '.mb-3'
                }),
                submitButton: new FormValidation.plugins.SubmitButton(),

                //defaultSubmit: new FormValidation.plugins.DefaultSubmit(),
                autoFocus: new FormValidation.plugins.AutoFocus()
            },
            init: instance => {
                instance.on('plugins.message.placed', function (e) {
                    if (e.element.parentElement.classList.contains('input-group')) {
                        e.element.parentElement.insertAdjacentElement('afterend', e.messageElement);
                    }
                });
            }
        }).on('core.form.valid', function (e) { //Gumagana lang pag navalidate na yung inputs
            dotnetInstance.invokeMethodAsync('Login');
            //$("#loginBtn").removeClass("disabled")
            //e.preventDefault();
        })
    }

    $(document).on("keyup", "#username", function (event) {
        if (event.keyCode === 13) {
            // Trigger a click event on the submit button
            $('#loginBtn').click();
        }
    }).on("keyup", "#password", function (event) {
        if (event.keyCode === 13) {
            // Trigger a click event on the submit button
            $('#loginBtn').click();
        }
    })

    //$(document).on("change", "#username", function () {
    //    fv.validate().then(function (status) {
    //        if (status == "Valid") {
    //            $("#loginBtn").removeClass("disabled");
    //        } else {
    //            $("#loginBtn").addClass("disabled");
    //        }
    //    });
    //}).on("change", "#password", function () {
    //    fv.validate().then(function (status) {
    //        if (status == "Valid") {
    //            $("#loginBtn").removeClass("disabled");
    //        } else {
    //            $("#loginBtn").addClass("disabled");
    //        }
    //    });
    //});
    //  Two Steps Verification
    const numeralMask = document.querySelectorAll('.numeral-mask');

    // Verification masking
    if (numeralMask.length) {
        numeralMask.forEach(e => {
            new Cleave(e, {
                numeral: true
            });
        });
    }

}