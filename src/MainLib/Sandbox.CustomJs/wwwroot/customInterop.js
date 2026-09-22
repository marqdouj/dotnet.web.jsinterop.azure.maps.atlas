export function addControls(azmap, controls) {
    removeControls(azmap, controls); //We don't want duplicates

    controls.forEach((mc) => {
        let newControl;

        switch (mc.type) {
            case MapControlType.Compass:
                newControl = new atlas.control.CompassControl(mc.options);
                break;
            case MapControlType.Fullscreen:
                newControl = new atlas.control.FullscreenControl(mc.options);
                break;
            case MapControlType.Pitch:
                newControl = new atlas.control.PitchControl(mc.options);
                break;
            case MapControlType.Scale:
                newControl = new atlas.control.ScaleControl(mc.options);
                break;
            case MapControlType.Style:
                newControl = new atlas.control.StyleControl(mc.options);
                break;
            case MapControlType.Traffic:
                newControl = new atlas.control.TrafficControl(mc.options);
                break;
            case MapControlType.TrafficLegend:
                newControl = new atlas.control.TrafficLegendControl();
                break;
            case MapControlType.Zoom:
                newControl = new atlas.control.ZoomControl(mc.options);
                break;
            default:
        }

        if (newControl) {
            newControl.id = mc.id;
            azmap.controls.add(newControl, mc.controlOptions);
        }
    });
}

export function removeControls(azmap, controls) {
    const existing = azmap.controls.getControls();

    controls.forEach((mc) => {
        const control = getControlById(mc.id, existing);

        if (control) {
            azmap.controls.remove(control);
        }
    });
}

function getControlById(id, existing) {
    for (const control of existing) {
        if (control.id === id) {
            return control;
        }
    }
}

const MapControlType = Object.freeze({
    Compass: "Compass",
    Fullscreen: "Fullscreen",
    Pitch: "Pitch",
    Scale: "Scale",
    Style: "Style",
    Traffic: "Traffic",
    TrafficLegend: "TrafficLegend",
    Zoom: "Zoom",
});
