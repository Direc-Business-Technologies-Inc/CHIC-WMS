export function InitializeStepper() {
    const wizardUser = document.querySelector('#wizard-user');

    if (typeof wizardUser !== undefined && wizardUser !== null) {

        // Wizard form
        const wizardUserForm = wizardUser.querySelector('#wizard-user-form');

        // Wizard steps
        const wizardUserFormStep1 = wizardUserForm.querySelector('#UserGroupDetails');
        const wizardUserFormStep2 = wizardUserForm.querySelector('#UserGroupSetup');

        // Wizard next prev button
        const wizardUserNext = [].slice.call(wizardUserForm.querySelectorAll('.btn-next'));
        const wizardUserPrev = [].slice.call(wizardUserForm.querySelectorAll('.btn-prev'));

        let validationStepper = new Stepper(wizardUser, {
            linear: false
        });

        wizardUserNext.forEach(item => {
            item.addEventListener('click', event => {
                // When click the Next button, we will validate the current step
                switch (validationStepper._currentIndex) {
                    case 0:
                        validationStepper.next();
                        break;

                    case 1:
                        //wizardUserFormStep2.validate();
                        break;

                    default:
                        break;
                }
            });
        });

        wizardUserPrev.forEach(item => {
            item.addEventListener('click', event => {
                switch (validationStepper._currentIndex) {
                    case 2:
                        validationStepper.previous();
                        break;

                    case 1:
                        validationStepper.previous();
                        break;

                    case 0:

                    default:
                        break;
                }
            });
        });
    }
}