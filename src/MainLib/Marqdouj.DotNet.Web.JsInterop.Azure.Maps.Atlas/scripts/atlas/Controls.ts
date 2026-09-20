import * as atlas from "azure-maps-control"
import { MapObjectReference } from "./common"
import { Helpers } from "./common/Helpers";
import { Logger, LogLevel } from "./common/Logger";

export class Controls {
    public static add(map: atlas.Map, mapControls?: MapControl[], getReferences: boolean = false): MapObjectReference[] {
        const eventName = "Controls.add";
        const mapId = Helpers.getMapId(map);
        const results: MapObjectReference[] = [];
        
        if (Helpers.hasNoElements(mapControls))
            return results;

        const controls = map.controls.getControls();

        mapControls!.forEach((mc) => {
            let newControl = this.#getItemFromSource(map, mc, controls, false);

            if (newControl) {
                Logger.logMapMessage(mapId, LogLevel.Warn, `${eventName}: Control already exists where id = '${mc.id}'.`);
                return; //continue
            }

            switch (mc.type) {
                case MapControlType.Compass:
                    newControl = new atlas.control.CompassControl(mc.options as any);
                    break;
                case MapControlType.Fullscreen:
                    newControl = new atlas.control.FullscreenControl(mc.options as any);
                    break;
                case MapControlType.Pitch:
                    newControl = new atlas.control.PitchControl(mc.options as any);
                    break;
                case MapControlType.Scale:
                    newControl = new atlas.control.ScaleControl(mc.options as any);
                    break;
                case MapControlType.Style:
                    newControl = new atlas.control.StyleControl(mc.options as any);
                    break;
                case MapControlType.Traffic:
                    newControl = new atlas.control.TrafficControl(mc.options as any);
                    break;
                case MapControlType.TrafficLegend:
                    newControl = new atlas.control.TrafficLegendControl();
                    break;
                case MapControlType.Zoom:
                    newControl = new atlas.control.ZoomControl(mc.options as any);
                    break;
                default:
            }

            if (newControl) {
                (newControl as any).id = mc.id;
                map.controls.add(newControl, mc.controlOptions);
                if (getReferences)
                    results.push(Helpers.createMapObjectReference(mapId, mc.type, newControl, mc.id));
            }
            else {
                Logger.logMapMessage(mapId, LogLevel.Warn, `${eventName}: Control type not supported - '${mc.type}'.`);
            }
        });

        return results;
    }

    public static remove(map: atlas.Map, mapControls: MapControl[]) {
        const eventName = "Controls.remove";

        if (Helpers.hasNoElements(mapControls))
            return;

        const mapId = Helpers.getMapId(map);
        const controls = map.controls.getControls();

        mapControls.forEach((mc) => {
            let control = this.#getItemFromSource(map, mc, controls, false);

            if (!control) {
                Logger.logMapMessage(mapId, LogLevel.Warn, `${eventName}: Control does not exists where id = '${mc.id}'.`);
                return; //continue
            }

            map.controls.remove(control);
        });
    }

    public static getReferences(map: atlas.Map, mapControls: MapControl[]) {
        const eventName = "Controls.getReferences";
        const results: MapObjectReference[] = [];
        const mapId = Helpers.getMapId(map);
        const controls = map.controls.getControls();

        mapControls.forEach((mc) => {
            let control = this.#getItemFromSource(map, mc, controls);

            if (control) {
                results.push(Helpers.createMapObjectReference(mapId, mc.type, control, mc.id));
            }
            else {
                Logger.logMapMessage(mapId, LogLevel.Warn, `${eventName}: Control does not exists where id = '${mc.id}'.`);
            }
        });

        return results;
    }

    static #getItemFromSource(map: atlas.Map, source: any, items?: atlas.Control[], logNotFound: boolean = true): atlas.Control | undefined {
        const eventName = "Controls.#getControlFromSource";

        let item: atlas.Control | undefined;

        if (source instanceof atlas.control.ControlBase) {
            item = source;
        }
        else {
            const id = Helpers.getSourceId(source);
            items ??= map.controls.getControls();
            item = items.findLast(item => Helpers.hasId(item, id));
        }

        if (!item && logNotFound) {
            const mapId = Helpers.getMapId(map);
            Logger.logMessage(mapId, LogLevel.Warn, `${eventName}: Control not found.`, source);
        }

        return item;
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

export interface MapControl {
    id?: string;
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
