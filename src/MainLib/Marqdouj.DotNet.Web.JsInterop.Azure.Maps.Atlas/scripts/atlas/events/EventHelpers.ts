import * as atlas from "azure-maps-control"
import { MapHelpers } from "../common/MapHelpers";
import { EventInfo, NotifyMapEventArgs } from ".";

export class EventHelpers {
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

    static buildNotifyEventArgs(mapId: string, event?: EventInfo, payload?: any) {
        const args: NotifyMapEventArgs = { mapId: mapId, target: event?.target, targetId: this.getEventInfoSourceId(event), type: event?.type, payload: payload };
        return args;
    }

    static getEventInfoSourceId(eventInfo?: EventInfo) {
        return MapHelpers.getSourceId(eventInfo?.source);
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

        shapes.filter(feature => MapHelpers.isFeature(feature)).forEach(feature => {
            results.push(this.#getFeatureResult(feature));
        });
        shapes.filter(shape => MapHelpers.isShape(shape)).forEach(shape => {
            results.push(this.#getShapeResult(shape));
        });

        return results;
    }
}
