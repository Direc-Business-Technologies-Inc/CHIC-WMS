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

export function SetCanvassPins(data) {
    window.canvassPins = data;
}

export function ReloadCanvass() {
    window.canvas.dispose();
    loadCanvassContent(window.canvassPins);
}

async function getCanvassPins() {
    console.log('1st');
    await window.dotnetInstance.invokeMethodAsync('getCanvassPins')
        .then(data => {
            window.canvassPins = data;
            console.log(window.canvassPins);
        });
}

export async function initializeBinDashboard() {
    await getCanvassPins();
    await loadCanvassContent(window.canvassPins);
    //$("#dv-pin-details").sticky();

    //const openButton = document.getElementById("btn-show-pin-details");
    //const closeButton = document.getElementById("closeButton");
    //const popupContainer = document.getElementById("popupContainer");
    //let isDragging = false;
    //let offsetX, offsetY;

    //openButton.addEventListener("click", () => {
    //    popupContainer.style.display = "block";
    //});

    //closeButton.addEventListener("click", () => {
    //    popupContainer.style.display = "none";
    //});

    //document.addEventListener("keydown", (e) => {
    //    if (e.key === "Escape") {
    //        popupContainer.style.display = "none";
    //    }
    //});

    //popupContainer.addEventListener("mousedown", (e) => {
    //    isDragging = true;
    //    offsetX = e.clientX - popupContainer.getBoundingClientRect().left;
    //    offsetY = e.clientY - popupContainer.getBoundingClientRect().top;
    //});

    //document.addEventListener("mousemove", (e) => {
    //    if (isDragging) {
    //        popupContainer.style.left = e.clientX - offsetX + "px";
    //        popupContainer.style.top = e.clientY - offsetY + "px";
    //    }
    //});

    //document.addEventListener("mouseup", () => {
    //    isDragging = false;
    //});
}

export function SetImageUrlFromBackend(url) {
    $('#imgBg').attr('src', url);
}

//CANVASS
async function loadCanvassContent(pins) {
    let tooltip = document.getElementById("tooltip");
    let pinId = "";
    console.log('2nd');
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

    window.canvas.on('mouse:over', function (e) {
        if (e.target != null) {
            console.log()
            if (e.target.id != pinId && e.target.status == 'Occupied') {

                $('#lbl-so-no').text(e.target.soNo);
                $('#lbl-customer').text(e.target.customerName);
                $('#lbl-item-name').text(e.target.itemName);
                $('#lbl-no-boxes-pallet').text(e.target.noOfBoxesPerPallet);
                $('#lbl-dispatch-date').text(e.target.receivingDate);
                //$('#lbl-receiving-date').text(e.target.receivingDate);
                $('#lbl-irradiation-date').text(e.target.irradiationDate);
                $('#lbl-receiving-date').text(e.target.dispatchDate);
                //$('#lbl-dispatch-date').text(e.target.dispatchDate);
                $('#lbl-pallet-no').text(e.target.palletNo);

                tooltip.style.left = (e.target.left + e.target.height - ($(tooltip).width() / 2) - (e.target.height / 2)) + "px";
                tooltip.style.top = (e.target.top - e.target.height - $(tooltip).height() + (e.target.height / 2)) + "px";
                tooltip.style.display = "block";
                pinId = e.target.id;
            }
            else {
                tooltip.style.display = "none";
                pinId = "";
            }
        }
    });

    window.canvas.on('mouse:out', function (e) {
        console.log('mouse:out');
        console.log(e);
        //if (e.target == null || e.target.status != 'Occupied') {
        if (e.target && e.target.status && e.target.status != 'Occupied') {
            //e.target.set('fill', 'green');
            //window.canvas.requestRenderAll();
            tooltip.style.display = "none";
            pinId = "";
        }
    });

    //For Saved Pins
    if (pins.length > 0) {
        const image = $('#imgBg')[0];
        const intrinsicWidth = image.naturalWidth;
        const renderedWidth = image.width;
        //const percentage = +renderedWidth / +intrinsicWidth;

        for (let x in pins) {
            let stroke = '';
            let fill = '';
            if (pins[x].status == 'Available') {
                stroke = 'rgba(62, 180, 137, 1)';
                fill = 'rgba(62, 180, 137, 0.4)';
            }
            else {
                stroke = 'rgba(255, 62, 29, 1)';
                fill = 'rgba(255, 62, 29, 0.4)';
            }

            const circle = window.canvas.circle || new fabric.Circle({
                //radius: +(pins[x].radius * percentage),
                radius: +((pins[x].radius * renderedWidth) / intrinsicWidth),
                stroke: stroke,
                strokeWidth: 3,
                fill: fill,
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
                aisle: pins[x].aisle,
                soNo: pins[x].soNo,
                customerName: pins[x].customerName,
                itemName: pins[x].itemName,
                noOfBoxesPerPallet: pins[x].noOfBoxesPerPallet,
                receivingDate: pins[x].receivingDate,
                irradiationDate: pins[x].irradiationDate,
                dispatchDate: pins[x].dispatchDate,
                status: pins[x].status,
                palletNo: pins[x].palletNo
            });

            window.canvas.add(group);

            window.canvas.requestRenderAll();
        }
    }
}