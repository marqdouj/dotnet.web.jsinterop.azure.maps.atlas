import * as atlas from "azure-maps-control"

export class Controls {
    public static add(map: atlas.Map, mapControls: MapControl[]) {
        if (!mapControls || mapControls.length == 0) {
            return;
        }

        const controls = map.controls.getControls();

        mapControls.forEach((mc) => {
            let newControl: atlas.Control | undefined = undefined;

            switch (mc.type) {
                case MapControlType.Compass:
                    if (controls.some(c => c instanceof atlas.control.CompassControl)) { break; }

                    newControl = new atlas.control.CompassControl(mc.options as any);
                    break;
                case MapControlType.Fullscreen:
                    if (controls.some(c => c instanceof atlas.control.FullscreenControl)) { break; }

                    newControl = new atlas.control.FullscreenControl(mc.options as any);
                    break;
                case MapControlType.Pitch:
                    if (controls.some(c => c instanceof atlas.control.PitchControl)) { break; }

                    newControl = new atlas.control.PitchControl(mc.options as any);
                    break;
                case MapControlType.Scale:
                    if (controls.some(c => c instanceof atlas.control.ScaleControl)) { break; }

                    newControl = new atlas.control.ScaleControl(mc.options as any);
                    break;
                case MapControlType.Style:
                    if (controls.some(c => c instanceof atlas.control.StyleControl)) { break; }

                    newControl = new atlas.control.StyleControl(mc.options as any);
                    break;
                case MapControlType.Traffic:
                    if (controls.some(c => c instanceof atlas.control.TrafficControl)) { break; }

                    newControl = new atlas.control.TrafficControl(mc.options as any);
                    break;
                case MapControlType.TrafficLegend:
                    if (controls.some(c => c instanceof atlas.control.TrafficLegendControl)) { break; }

                    newControl = new atlas.control.TrafficLegendControl();
                    break;
                case MapControlType.Zoom:
                    if (controls.some(c => c instanceof atlas.control.ZoomControl)) { break; }

                    newControl = new atlas.control.ZoomControl(mc.options as any);
                    break;
                default:
            }

            if (newControl) {
                map.controls.add(newControl, mc.controlOptions);
            }
        });
    }

    public static remove(map: atlas.Map, mapControls: MapControlType[]) {
        const controls = map.controls.getControls();

        if (!mapControls || mapControls.length == 0) {
            return;
        }

        mapControls.forEach((mc) => {
            switch (mc) {
                case MapControlType.Compass:
                    map.controls.remove(controls.filter(c => c instanceof atlas.control.CompassControl));
                    break;
                case MapControlType.Fullscreen:
                    map.controls.remove(controls.filter(c => c instanceof atlas.control.FullscreenControl));
                    break;
                case MapControlType.Pitch:
                    map.controls.remove(controls.filter(c => c instanceof atlas.control.PitchControl));
                    break;
                case MapControlType.Scale:
                    map.controls.remove(controls.filter(c => c instanceof atlas.control.ScaleControl));
                    break;
                case MapControlType.Style:
                    map.controls.remove(controls.filter(c => c instanceof atlas.control.StyleControl));
                    break;
                case MapControlType.Traffic:
                    map.controls.remove(controls.filter(c => c instanceof atlas.control.TrafficControl));
                    break;
                case MapControlType.TrafficLegend:
                    map.controls.remove(controls.filter(c => c instanceof atlas.control.TrafficLegendControl));
                    break;
                case MapControlType.Zoom:
                    map.controls.remove(controls.filter(c => c instanceof atlas.control.ZoomControl));
                    break;
                default:
            }
        });
    }

    public static removeControls(map: atlas.Map, mapControls: any[]) {
        mapControls ?? [];

        mapControls.forEach((mc) => {
            if (mc instanceof atlas.control.ControlBase) {
                map.controls.remove(mc);
            }
        });
    }

    public static getControls(map: atlas.Map, mapId: string, mapControls: MapControlType[]) {
        const results: MapControlRef[] = [];
        const items = map.controls.getControls();

        items.forEach((c) => {
            let type: string = "";
            let addControl = false;
            const anyControl = !mapControls || mapControls.length == 0;

            if (this.#shouldProcessControl(MapControlType.Compass, mapControls, anyControl) && this.#isCompassControl(c)) {
                type = MapControlType.Compass;
                addControl = true;
            }
            else if (this.#shouldProcessControl(MapControlType.Fullscreen, mapControls, anyControl) && this.#isFullscreenControl(c)) {
                type = MapControlType.Fullscreen;
                addControl = true;
            }
            else if (this.#shouldProcessControl(MapControlType.Pitch, mapControls, anyControl) && this.#isPitchControl(c)) {
                type = MapControlType.Pitch;
                addControl = true;
            }
            else if (this.#shouldProcessControl(MapControlType.Scale, mapControls, anyControl) && this.#isScaleControl(c)) {
                type = MapControlType.Scale;
                addControl = true;
            }
            else if (this.#shouldProcessControl(MapControlType.Style, mapControls, anyControl) && this.#isStyleControl(c)) {
                type = MapControlType.Style;
                addControl = true;
            }
            else if (this.#shouldProcessControl(MapControlType.Traffic, mapControls, anyControl) && this.#isTrafficControl(c)) {
                type = MapControlType.Traffic;
                addControl = true;
            }
            else if (this.#shouldProcessControl(MapControlType.TrafficLegend, mapControls, anyControl) && this.#isTrafficLegendControl(c)) {
                type = MapControlType.TrafficLegend;
                addControl = true;
            }
            else if (this.#shouldProcessControl(MapControlType.Zoom, mapControls, anyControl) && this.#isZoomControl(c)) {
                type = MapControlType.Zoom;
                addControl = true;
            }

            if (addControl) {
                const mc: MapControlRef = { mapId: mapId, type: type, jsReference: DotNet.createJSObjectReference(c) };
                results.push(mc);
            }
        });

        return results;
    }

    static #shouldProcessControl(control: MapControlType, controls: string[], anyControl: boolean) {
        return anyControl || controls.some(item => item.toLowerCase() === control.toLowerCase())
    }

    static #isCompassControl(obj: any): obj is atlas.control.CompassControl {
        return obj && (obj instanceof atlas.control.CompassControl);
    }

    static #isFullscreenControl(obj: any): obj is atlas.control.FullscreenControl {
        return obj && (obj instanceof atlas.control.FullscreenControl);
    }

    static #isPitchControl(obj: any): obj is atlas.control.PitchControl {
        return obj && (obj instanceof atlas.control.PitchControl);
    }

    static #isScaleControl(obj: any): obj is atlas.control.ScaleControl {
        return obj && (obj instanceof atlas.control.ScaleControl);
    }

    static #isStyleControl(obj: any): obj is atlas.control.StyleControl {
        return obj && (obj instanceof atlas.control.StyleControl);
    }

    static #isTrafficControl(obj: any): obj is atlas.control.TrafficControl {
        return obj && (obj instanceof atlas.control.TrafficControl);
    }

    static #isTrafficLegendControl(obj: any): obj is atlas.control.TrafficLegendControl {
        return obj && (obj instanceof atlas.control.TrafficLegendControl);
    }

    static #isZoomControl(obj: any): obj is atlas.control.ZoomControl {
        return obj && (obj instanceof atlas.control.ZoomControl);
    }
}

enum MapControlType {
    Compass = "Compass",
    Fullscreen = "Fullscreen",
    Pitch = "Pitch",
    Scale = "Scale",
    Style = "Style",
    Traffic = "Traffic",
    TrafficLegend = "TrafficLegend",
    Zoom = "Zoom",
}

interface MapControl {
    type: MapControlType;
    controlOptions?: atlas.ControlOptions;
    options?: atlas.CompassControlOptions
    | atlas.FullscreenControlOptions
    | atlas.PitchControlOptions
    | atlas.ScaleControlOptions
    | atlas.StyleControlOptions
    | atlas.TrafficControlOptions
    | atlas.ZoomControlOptions;
}

interface MapControlRef {
    mapId: string;
    type: string;
    jsReference: any;
}