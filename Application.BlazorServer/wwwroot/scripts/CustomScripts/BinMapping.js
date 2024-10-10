//GLOBAL VARIABLES
window.canvassPins = [];
window.canvas;
window.rowcount = 0;
window.intrinsicWidth = 0;
window.renderedWidth = 0;


window.dotnetInstance;
export function setdotnetInstance(dotnetInstance) {
    window.dotnetInstance = dotnetInstance;
}

//STEPPER WITH VALIDATIONS
export function InitializeStepper() {
    const wizardMapping = document.querySelector('#wizard-mapping');

    if (typeof wizardMapping !== undefined && wizardMapping !== null) {

        // Wizard form
        const wizardMappingForm = wizardMapping.querySelector('#wizard-mapping-form');

        // Wizard steps
        const wizardMappingFormStep1 = wizardMappingForm.querySelector('#select-warehouse');
        const wizardMappingFormStep2 = wizardMappingForm.querySelector('#mapping-warehouse');

        // Wizard next prev button
        const wizardMappingNext = [].slice.call(wizardMappingForm.querySelectorAll('.btn-next'));
        const wizardMappingPrev = [].slice.call(wizardMappingForm.querySelectorAll('.btn-prev'));

        let validationStepper = new Stepper(wizardMapping, {
            linear: false
        });

        // Select Warehouse
        const MappingFormValidation1 = FormValidation.formValidation(wizardMappingFormStep1, {
            fields: {
                'warehouse-code': {
                    validators: {
                        notEmpty: {
                            message: 'Warehouse Code is required',
                        },
                    }
                },
                'shelf': {
                    validators: {
                        notEmpty: {
                            message: 'Shelf is required',
                        },
                    }
                },
                'warehouse-name': {
                    validators: {
                        notEmpty: {
                            message: 'Warehouse Name is required',
                        },
                    }
                }
                // * Validate the fields here based on your requirements
            },

            plugins: {
                trigger: new FormValidation.plugins.Trigger(),
                bootstrap5: new FormValidation.plugins.Bootstrap5({
                    // Use this for enabling/changing valid/invalid class
                    // eleInvalidClass: '',
                    eleValidClass: ''
                    // rowSelector: '.col-lg-6'
                }),
                autoFocus: new FormValidation.plugins.AutoFocus(),
                submitButton: new FormValidation.plugins.SubmitButton()
            }
        }).on('core.form.valid', function () {
            // Jump to the next step when all fields in the current step are valid
            validationStepper.next();

            //setTimeout();

            loadCanvassContent(window.canvassPins);

            makeSticky();

            //FetchColumn();
        });

        // Warehouse
        const MappingFormValidation2 = FormValidation.formValidation(wizardMappingFormStep2, {
            fields: {
                // * Validate the fields here based on your requirements
            },
            plugins: {
                trigger: new FormValidation.plugins.Trigger(),
                bootstrap5: new FormValidation.plugins.Bootstrap5({
                    // Use this for enabling/changing valid/invalid class
                    // eleInvalidClass: '',
                    eleValidClass: ''
                    // rowSelector: '.col-lg-6'
                }),
                autoFocus: new FormValidation.plugins.AutoFocus(),
                submitButton: new FormValidation.plugins.SubmitButton()
            }
        }).on('core.form.valid', function () {
            // You can submit the form
            // wizardCheckoutForm.submit()
            // or send the form data to server via an Ajax request
            // To make the demo simple, I just placed an alert

            confirm('Save Mapping?').then(function (result) {
                if (result) {
                    saveBinMapping();
                }
            });
        });

        wizardMappingNext.forEach(item => {
            item.addEventListener('click', event => {
                // When click the Next button, we will validate the current step
                switch (validationStepper._currentIndex) {
                    case 0:
                        MappingFormValidation1.validate();
                        break;

                    case 1:
                        MappingFormValidation2.validate();
                        break;

                    default:
                        break;
                }
            });
        });

        wizardMappingPrev.forEach(item => {
            item.addEventListener('click', event => {
                switch (validationStepper._currentIndex) {
                    case 2:
                        validationStepper.previous();
                        break;

                    case 1:
                        validationStepper.previous();
                        window.canvas.dispose();
                        $('.selectPin').removeClass('pin-selected');
                        $('.selectPin').css('color', '');
                        $('.bxs-check-circle').css({ 'color': 'red' });
                        $('.bxs-check-circle').addClass('bxs-x-circle');
                        $('.bxs-check-circle').removeClass('bxs-check-circle');
                        break;

                    case 0:

                    default:
                        break;
                }
            });
        });
    }
}

function saveBinMapping() {
    dotnetInstance.invokeMethodAsync('SaveBinMapping')
        .then(data => {
            console.log(data);
        });
}
//END OF STEPPER WITH VALIDATIONS

$(document).ready(function () {
    $('#btn-select-shelf').removeAttr('data-bs-toggle');
    $('#btn-select-shelf').removeAttr('data-bs-target');
})

$(document).on('click', '.selectPin', function () {
    $('.selectPin').removeClass('pin-selected');
    $('.selectPin').css('color', '');
    $(this).addClass('pin-selected');
    $(this).css('color', '#696CFF');
}).on('click', '.removePin', function () {
    const thiselem = $(this);
    const pinRow = $(this).closest('tr');
    const pinIcon = $(pinRow).find('td').eq(0).find('i');
    const pinLogo = $(pinRow).find('td').eq(4).find('a');

    if ($(pinIcon).hasClass('bxs-x-circle')) {
        return;
    }
    confirm('Remove Pin?').then(function (result) {

        if (!result) {
            return;
        }

        removePinCanvass($(thiselem).data('id'));

        $(pinIcon).removeClass('bxs-check-circle');
        $(pinIcon).css('color', 'Red');
        $(pinIcon).addClass('bxs-x-circle');
        $(pinLogo).css('color', '');
    });
}).on('change', '#file-whs-map', function () {

    var file = $(this)[0].files[0];
    var reader = new FileReader();
    reader.onload = function (e) {
        $('#img-select-map').attr('src', e.target.result);
        $('#imgBg').attr('src', e.target.result);
        //$('#img-select-map #imgBg').attr('src', e.target.result);
        //$('').attr('src', e.target.result);

        SetImageUrlData(e.target.result, file.name);
    }
    reader.readAsDataURL(file);

}).on('click', '#btn-select-shelf', function () {
    if ($('#warehouse-code').val() == '') {
        ShowResult("Warning", "Select Warehouse First!");
        return;
    }

    $('#modal-shelf').modal('show');
})

export function SetImageUrlFromBackend(url) {
    $('#img-select-map').attr('src', url);
    $('#imgBg').attr('src', url);
}

export function SetCanvassPins(data) {
    window.canvassPins = data;
}

function SetImageUrlData(Url, FileName) {
    dotnetInstance.invokeMethodAsync('SetImageUrl', Url, FileName)
        .then(data => {
            console.log(data);
        });
}

export function InitializePinRange() {
    const sliderInput = document.getElementById("txt-pin-radius");
    const rangePin = document.getElementById("rangePin");

    noUiSlider.create(rangePin, {
        start: [15],
        step: 1,
        connect: true,
        range: {
            min: 5,
            max: 30
        },
        pips: {
            mode: "range",
            density: 10,
            stepped: true
        }
    });

    rangePin.noUiSlider.on("update", function (values, handle) {
        var value = values[handle];
        sliderInput.value = value;
        $(".dot").css('width', value * 2);
        $(".dot").css('font-size', `${value}px`);
        $(".dot").css('height', value * 2);
    });

    sliderInput.addEventListener("change", function () {
        rangePin.noUiSlider.set([this.value]);
        $(".dot").css('width', this.value * 2);
        $(".dot").css('font-size', `${value}px`);
        $(".dot").css('height', this.value * 2);
    });
}

//For fetching of column
async function FetchColumn() {
    await dotnetInstance.invokeMethodAsync('FetchColumn')
        .then(data => {
            //window.rowcount = data;
        });
}

//For fetching of row and rowcount
//async function FetchRows() {
//    await dotnetInstance.invokeMethodAsync('FetchRows')
//        .then(data => {
//            //window.rowcount = data;
//        });
//}

export function showPinTable(aisle, level, data) {

    $('#dt-pins tbody').empty();

    const txtWarehouse = $('#warehouse-code').val();
    const txtBin = $('#shelf').val();
    const canvasObjects = window.canvas.getObjects();

    //const rowcount = @Model.RowList.Count;
    //const rowcount = 5;

    for (let i = 0; i < data.length; i++) {
        //PIN
        //let pinId = `${txtWarehouse}-${txtBin}-${level.padStart(2, '0')}${i.toString().padStart(2, '0')}-${aisle.attr('code')}`;
        let pinId = `${txtWarehouse}-${txtBin}-${data[i].row.toString().padStart(2, '0')}-${level.padStart(2, '0')}-${aisle}`;

        //Check if used
        var available = "x-circle";
        var color = "red";

        if (canvasObjects.length > 0 && canvasObjects.some(x => x.id == pinId)) {
            available = "check-circle";
            color = "green";
        }

        //$('#dt-pins tbody').appendTablePins([color, available, level.padStart(2, '0'), i.toString().padStart(2, '0'), aisle.attr('code'), pinId]);
        $('#dt-pins tbody').appendTablePins([color, available, level.padStart(2, '0'), data[i].row.toString().padStart(2, '0'), aisle, pinId]);

    }

    if ($('#dvTable').hasClass('d-none')) {
        $('#dvTable').removeClass('d-none');
        $('#mapping-content').css({ 'height': 'auto' });
        makeSticky();
    }
}

function makeSticky() {
    const stickyelem = $('#insideWrapper');
    const parentHeight = $("#mapWrapper").height();
    const stickyHeight = stickyelem.height();
    var scrollTop = $(window).scrollTop();

    if (parentHeight - stickyHeight <= scrollTop) {
        $("#insideWrapper").unstick();
    } else {
        $("#insideWrapper").sticky({
            topSpacing: 75,
            bottomSpacing: 175
        });
    }
}

$.fn.appendTablePins = function (rowValues) {
    $(this).append(`<tr>
                <td style="text-align: center;"><i data-id="${rowValues[5]}" style="color: ${rowValues[0]};" class="bx bxs-${rowValues[1]}"></i></td>
                <td style="text-align: center;">${rowValues[2]}</td>
                <td style="text-align: center;">${rowValues[3]}</td>
                <td style="text-align: center;">${rowValues[4]}</td>
                <td style="text-align: center;"><a style="cursor: pointer;"  class="bx bxs-map-pin selectPin" data-id="${rowValues[5]}"></a></td>
                <td style="text-align: center;"><a style="cursor: pointer; color: red;" class="bx bxs-trash removePin" data-id="${rowValues[5]}"></a></td>
                </tr>`);
};

//CANVASS
function loadCanvassContent(pins) {

    // Store DOM elements in variables
    const coveredImage = document.getElementsByClassName('coveredImage')[0];

    // Get window.canvas dimensions
    const width = coveredImage.width;
    const height = coveredImage.height;

    const imageBg = $('#imgBg')[0];
    window.intrinsicWidth = imageBg.naturalWidth;
    window.renderedWidth = imageBg.width;

    $('#mapping-content').css({ 'height': 'auto' });
    $('#insideWrapper').css({ 'height': 'auto' });

    // Create Fabric.js window.canvas
    window.canvas = new fabric.Canvas('imgPin', {
        hoverCursor: 'pointer',
        selection: false,
        perPixelTargetFind: true,
        targetFindTolerance: 5,
        width: width,
        height: height
    });

    // Mouse down event
    window.canvas.on('mouse:down', function (e) {
        if (!$('.selectPin').hasClass('pin-selected')) {
            //swal('Please select pin.', 'warning');
            ShowResult('Warning', 'Please select pin.');
            return;
        }

        const warehousecode = $('#warehouse-code').val()
        const shelf = $('#shelf').val()
        const selectedPin = document.getElementsByClassName('pin-selected')[0];
        const txtPinRadius = $("#txt-pin-radius");
        const pinId = $(selectedPin).data('id');
        // Check if target object was clicked
        //if (e.target != null) {
        // Do something with target object
        // ...
        if (e.target == null) {
            // Check if pin already exists for row and column
            const canvasObjects = window.canvas.getObjects();
            let pinExists = false;

            for (let i = 0; i < canvasObjects.length; i++) {
                const obj = canvasObjects[i];
                if (obj.id === pinId) {
                    obj.left = e.e.offsetX - 11.5;
                    obj.top = e.e.offsetY - 11.5;

                    obj.forEachObject(function (o) {
                        if (o.get('type') === 'text') {
                            o.fontSize = +txtPinRadius.val();
                        } else if (o.get('type') === 'circle') {
                            o.radius = +txtPinRadius.val();
                        }
                    });

                    obj.addWithUpdate();
                    pinExists = true;


                    const objData = {
                        BinCode: obj.id,
                        Left: obj.left,
                        Top: obj.top,
                        Radius: obj._objects[0].radius,
                        Text: obj.row.toString(),
                        Row: +obj.row,
                        Level: +obj.column,
                        Aisle: obj.aisle,
                        WarehouseCode: warehousecode,
                        Shelf: shelf
                    }

                    savePinToViewModel("Patch", objData)

                    break;
                }
            }

            // If pin doesn't exist, create a new one
            if (!pinExists) {
                //addPinCanvass(e.e.offsetX - 11.5, e.e.offsetY - 11.5);
                addPinCanvass(e.e.offsetX, e.e.offsetY);
            }

            // Free up memory by clearing objects array
            canvasObjects.length = 0;
        }

        // Request window.canvas to render all objects
        window.canvas.requestRenderAll();
    });

    //For Saved Pins
    if (pins.length > 0) {
        const image = $('#imgBg')[0];
        const intrinsicWidth = image.naturalWidth;
        const renderedWidth = image.width;
        //const percentage = +renderedWidth / +intrinsicWidth;

        debugger;

        for (let x in pins) {
            const circle = window.canvas.circle || new fabric.Circle({
                //radius: +(pins[x].radius * percentage),
                radius: +((pins[x].radius * renderedWidth) / intrinsicWidth),
                stroke: 'red',
                strokeWidth: 3,
                fill: 'rgba(255, 0, 0, 0.4)',
                originX: 'center',
                originY: 'center'
            });

            //const text = window.canvas.text || new fabric.Text(`${pins[x].row.toString().padStart(2, '0')}`,
            const text = window.canvas.text || new fabric.Text('',
                {
                    //fontSize: +(pins[x].radius * percentage),
                    fontSize: +((pins[x].radius * renderedWidth) / intrinsicWidth),
                    fontWeight: 'bold',
                    fontFamily: 'calibri',
                    textAlign: 'center',
                    fill: 'white',
                    originX: 'center',
                    originY: 'center'
                });

            let group = null;

            //circle.set({ left: (pins[x].left * percentage) + 11.5, top: (pins[x].top * percentage) + 11.5 });
            //text.set({ left: (pins[x].left * percentage) + 11.5, top: (pins[x].top * percentage) + 11.5 });
            circle.set({ left: ((pins[x].left * renderedWidth) / intrinsicWidth) + 11.5, top: ((pins[x].top * renderedWidth) / intrinsicWidth) + 11.5 });
            text.set({ left: ((pins[x].left * renderedWidth) / intrinsicWidth) + 11.5, top: ((pins[x].top * renderedWidth) / intrinsicWidth) + 11.5 });

            group = new fabric.Group([circle, text], {
                hasControls: false,
                selectable: false,
                id: pins[x].binCode,
                row: pins[x].row,
                column: pins[x].level,
                aisle: pins[x].aisle
            });

            window.canvas.add(group);

            window.canvas.requestRenderAll();
        }
    }
}

//For Reflecting of Pin Datas to ViewModel List
function savePinToViewModel(Method, Data) {
    dotnetInstance.invokeMethodAsync('AdjustPinList', Method, Data, window.intrinsicWidth, window.renderedWidth)
        .then(data => {
            window.canvassPins = [];
            window.canvassPins = data;
        });
}

//Removing of pins
function removePinCanvass(id) {
    const warehousecode = $('#warehouse-code').val()
    const shelf = $('#shelf').val()
    const canvasObjects = window.canvas.getObjects();
    for (let i = 0; i < canvasObjects.length; i++) {
        const obj = canvasObjects[i];
        if (obj.id === id) {
            window.canvas.remove(obj);

            const objData = {
                BinCode: obj.id,
                Left: +obj.left,
                Top: +obj.top,
                Radius: +obj._objects[0].radius,
                Text: obj.row.toString(),
                Row: +obj.row,
                Level: +obj.column,
                Aisle: obj.aisle,
                WarehouseCode: warehousecode,
                Shelf: shelf
            }

            savePinToViewModel("Delete", objData)
            break;
        }
    }
}

//Adding pins upon clicking on canvass
function addPinCanvass(left, top) {
    const selectedPin = document.getElementsByClassName('pin-selected')[0];
    const pinRow = selectedPin.closest('tr');
    const txtRow = $(pinRow).find('td').eq(2).text();
    const txtAisle = $(pinRow).find('td').eq(3).text();
    const pinIcon = $(pinRow).find('td').eq(0).find('i');
    const pinId = $(selectedPin).data('id');
    const txtPinRadius = $('#txt-pin-radius');
    const txtColumn = $('#level').val().padStart(2, '0');
    const warehousecode = $('#warehouse-code').val()
    const shelf = $('#shelf').val()

    const circle = window.canvas.circle || new fabric.Circle({
        radius: +txtPinRadius.val(),
        stroke: 'red',
        strokeWidth: 3,
        fill: 'rgba(255, 0, 0, 0.4)',
        originX: 'center',
        originY: 'center'
    });

    //const text = window.canvas.text || new fabric.Text(txtRow,
    const text = window.canvas.text || new fabric.Text('',
        {
            fontSize: +txtPinRadius.val(),
            fontWeight: 'bold',
            fontFamily: 'calibri',
            textAlign: 'center',
            fill: 'white',
            originX: 'center',
            originY: 'center'
        });

    let group = null;
    const canvasObjects = window.canvas.getObjects();
    for (let i = 0; i < canvasObjects.length; i++) {
        if (canvasObjects[i].id === pinId) {
            group = canvasObjects[i];
            group.item(0).setRadius(+txtPinRadius.val());
            group.item(1).setFontSize(+txtPinRadius.val());
            group.item(1).setText(txtRow);
            group.setLeft(left);
            group.setTop(top);
            break;
        }
    }
    if (!group) {
        circle.set({ left: left, top: top });
        text.set({ left: left, top: top });
        group = new fabric.Group([circle, text], {
            hasControls: false,
            selectable: false,
            id: pinId,
            row: txtRow,
            column: txtColumn,
            aisle: txtAisle
        });
        window.canvas.add(group);
    }
    window.canvas.requestRenderAll();
    $(pinIcon).removeClass('bxs-x-circle');
    $(pinIcon).css('color', 'Green');
    $(pinIcon).addClass('bxs-check-circle');


    const objData = {
        BinCode: pinId,
        Left: +group.left,
        Top: +group.top,
        Radius: +group._objects[0].radius,
        Text: group.row.toString(),
        Row: +group.row,
        Level: +group.column,
        Aisle: group.aisle,
        WarehouseCode: warehousecode,
        Shelf: shelf
    }

    savePinToViewModel('POST', objData)

    group = null;
}

//Resize Image size when the browser size changed
$(window).resize(function () {
    const image = $('#imgBg')[0];
    const intrinsicWidth = image.naturalWidth;
    const renderedWidth = image.width;
    const percentageToOriginal = +intrinsicWidth / +renderedWidth;
});
//END OF CANVASS


