import * as atlas from "azure-maps-control"
import * as anims from "azure-maps-animations"
import { MapObjectReference } from "./common";
import { Helpers } from "./common/Helpers";
import { DataSource } from "../atlas";
import { Logger, LogLevel } from "./common/Logger";
import { AnimationEventInfo, AnimationEvents } from "./events/AnimationEvents";

export class Animations {
    public static doAction(animation: any, action: string, returnsValue: boolean = false) {
        const eventName = "Animations.doAction";
        if (animation && animation[action]) {
            if (returnsValue)
                return animation[action]();
            else {
                animation[action]();
            }
        }
        else {
            Logger.logMessage(eventName, LogLevel.Error, "action was not found.", action);
        }
    }

    public static getEasingNames() : string[] {
        return anims.animations.getEasingNames();
    }

    public static setCoordinates(map: atlas.Map, source: any, shapeId: string, newCoordinates: any, options?: any, getReference: boolean = false): MapObjectReference | undefined {
        const eventName = "Animations.setCoordinates";
        const mapId = Helpers.getMapId(map);
        const shape = DataSource.getShapeById(map, source, shapeId, false);
        if (shape instanceof atlas.Shape) {
            options = Helpers.removeNullish(options);
            const result = anims.animations.setCoordinates(shape as any, newCoordinates, options);
            return getReference ? Helpers.createMapObjectReference(Helpers.getMapId(map), "PlayableAnimation", result, "") : undefined;
        }
        else {
            Logger.logMapMessage(mapId, LogLevel.Error, `${eventName}: shape not found where id = '${shapeId}'`, source);
            return;
        }
    }

    public static extractRoutePoints(shapes: any, timestampProperty?: string) {
        return anims.animations.extractRoutePoints(shapes, timestampProperty);
    }

    public static moveAlongRoute(dotNetRef: any, map: atlas.Map, route: any, shape?: any, options?: any, mapEvents?: AnimationEventInfo[]) {
        const eventName = "Animations.setCoordinates";
        const mapId = Helpers.getMapId(map);

        const animation = anims.animations.moveAlongRoute(route, shape, options);

        if (Helpers.hasElements(mapEvents))
            AnimationEvents.add(dotNetRef, map, animation, mapEvents!);

        return animation;
    }
}