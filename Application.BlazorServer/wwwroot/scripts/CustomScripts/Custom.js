function InitializeDatatable(tableId) {
    $(`#${tableId}`).DataTable();
}

function SelectInput(Id, Data) {
    $(`#${Id}`).val(Data);
    $('.modal').modal('hide');
}

function InitializeBreadcrumbs(breadcrumbsJson) {
    var breadcrumbs = JSON.parse(breadcrumbsJson);
    const bc = new window.DEV.Breadcrumbs("[data-dev-breadcrumbs]", breadcrumbs);
}

function HideModal() {
    $('.modal').modal('hide');
}

function swal(message, type) {
    Swal.fire({
        position: 'center',
        icon: type,
        title: message,
        showConfirmButton: false,
        timer: 1500
    });
}

function confirm(title, comfirmText = '', cancelText = '') {

    comfirmText = comfirmText == '' ? 'Yes' : comfirmText;
    cancelText = cancelText == '' ? 'Cancel' : cancelText;

    return new Promise((resolve, reject) => {
        Swal.fire({
            title: title,
            text: "",
            icon: "question",
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: comfirmText,
            cancelButtonText: cancelText,
            didOpen: function () {
                let cancelButton = document.querySelector('.swal2-cancel');
                cancelButton.focus();
            }
        }).then((result) => {
            if (result.isConfirmed) {
                resolve(true);
            }
            else {
                resolve(false);
            }
        })
    })
}

function confirmWarning(title) {
    return new Promise((resolve, reject) => {
        Swal.fire({
            title: title,
            text: "",
            icon: "question",
            iconColor: "#FFC30E",
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes',
            reverseButtons: true,
            didOpen: function () {
                let cancelButton = document.querySelector('.swal2-cancel');
                cancelButton.focus();
            }
        }).then((result) => {
            if (result.isConfirmed) {
                resolve(true);
            }
            else {
                resolve(false);
            }
        })
    })
}

function reload() {
    setTimeout(function () {
        location.reload(true);
    }, 1500);
}

function ActivateNavlinks(nav) {
    $('li.menu-item').removeClass('active');

    let baseNav = $(`[href="${nav}"]`).closest('li.menu-item');

    $(baseNav).addClass('active');

    let parentNav = $(baseNav).parents('li.menu-item').addClass('active');
}

function SoundNotification(isMute) {
    //var audio = new Audio('/assets/audio/Door_Bell_Sound_Effect.mp3');
    //var audio = document.getElementById("sound-notification");

    var sound = new Howl({
        src: ['/assets/audio/Door_Bell_Sound_Effect.mp3'],
        html5: true,
        mute: isMute
    });

    sound.play();

    //window.audio.muted = isMute;

    //if (!("Notification" in window)) {
    //    alert("This browser does not support desktop notifications");
    //} else if (Notification.permission === "granted") {
    //    // If notification permission is already granted
    //    //createNotification();
    //    let notification = new Notification("New SO");
    //    window.audio.play();
    //} else if (Notification.permission !== "denied") {
    //    // Ask for permission
    //    Notification.requestPermission().then(permission => {
    //        if (permission === "granted") {
    //            //createNotification();
    //            let notification = new Notification("New SO");
    //            window.audio.play();
    //        }
    //    });
    //}

    console.log(isMute);
    //window.audio.play();
}

function showNotification() {
    if (!("Notification" in window)) {
        alert("This browser does not support desktop notifications");
    } else if (Notification.permission === "granted") {
        // If notification permission is already granted
        //createNotification();
    } else if (Notification.permission !== "denied") {
        // Ask for permission
        Notification.requestPermission().then(permission => {
            if (permission === "granted") {
                //createNotification();
            }
        });
    }
}

////Remove Table Row
//$(document).on('click', '.table-remove-row', function () {
//    let dt = $(this).closest('table').DataTable();
//    let target = $(this).closest('tr');
//    dt.row(target).remove().draw();
//})