import * as atlas from "azure-maps-control"
import { Helpers as CHelpers} from "../common/Helpers"

export enum MapEventTarget {
    Map = "Map",
    Animation = 'Animation',
    DataSource = 'DataSource',
    Marker = 'Marker',
    Layer = 'Layer',
    Popup = 'Popup',
    Shape = 'Shape',
    StyleControl = 'StyleControl',
}

export interface NotifyMapEventArgs {
    mapId: string;
    target: MapEventTarget;
    targetId?: string;
    type: string;
    payload?: atlas.Properties | undefined;
}

export enum MapEventNotify {
    NotifyMapEvent = 'NotifyMapEvent',
}

export interface EventInfo {
    target: MapEventTarget;
    source?: any;
    type: string;
    once: boolean;
    preventDefault: boolean;
    eventName?: string;
}

export class Helpers {
    static #isFeature(obj: any): obj is atlas.data.Feature<atlas.data.Geometry, any> {
        return obj && obj.type === 'Feature';
    }

    static #isShape(obj: any): obj is atlas.Shape {
        return obj && obj.getType != undefined;
    }

    static #getFeatureResult(feature: atlas.data.Feature<atlas.data.Geometry, any>): object {

        const item: object = {
            id: feature.id?.toString(),
            type: feature.geometry.type,
            bbox: feature.bbox,
            source: "feature",
            properties: feature.properties
        };
        return item;
    }

    static #getShapeResult(shape: atlas.Shape): object {
        const item: object = {
            id: shape.getId()?.toString(),
            type: shape.getType(),
            bbox: shape.getBounds(),
            source: "shape",
            properties: shape.getProperties()
        };
        return item;
    }

    static #buildShapeResults(shapes: (atlas.data.Feature<atlas.data.Geometry, any> | atlas.Shape)[] | undefined): object[] {
        const results: object[] = [];

        if (!shapes)
            return results;

        shapes.filter(feature => this.#isFeature(feature)).forEach(feature => {
            results.push(this.#getFeatureResult(feature));
        });
        shapes.filter(shape => this.#isShape(shape)).forEach(shape => {
            results.push(this.#getShapeResult(shape));
        });

        return results;
    }

    static buildMouseEventPayload(mouseEvent: atlas.MapMouseEvent) {
        const mouse = {
            layerId: mouseEvent.layerId,
            pixel: mouseEvent.pixel,
            position: mouseEvent.position,
            shapes: this.#buildShapeResults(mouseEvent.shapes)
        };

        return { mouse: mouse };
    }

    static buildTouchEventPayload(touchEvent: atlas.MapTouchEvent) {
        const payload = {
            pixel: touchEvent.pixel,
            pixels: touchEvent.pixels,
            position: touchEvent.position,
            positions: touchEvent.positions,
            layerId: touchEvent.layerId,
            shapes: this.#buildShapeResults(touchEvent.shapes)
        };

        return { touch: payload };
    }

    static buildWheelEventPayload(wheelEvent: atlas.MapMouseWheelEvent) {
        const payload = {
            type: wheelEvent.type,
        };

        return { wheel: payload };
    }

    static buildNotifyEventArgs(mapId: string, event: EventInfo, payload?: any) {
        const args: NotifyMapEventArgs = { mapId: mapId, target: event.target, targetId: this.getEventInfoSourceId(event), type: event.type, payload: payload };
        return args;
    }

    static getEventInfoSourceId(eventInfo: EventInfo) {
        return CHelpers.getSourceId(eventInfo.source);
    }
}