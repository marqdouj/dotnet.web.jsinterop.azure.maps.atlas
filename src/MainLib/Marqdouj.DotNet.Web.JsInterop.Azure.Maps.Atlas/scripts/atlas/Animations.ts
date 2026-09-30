import * as atlas from "azure-maps-control"
import * as anims from "azure-maps-animations"
import { MapObjectReference } from "./common";
import { Helpers } from "./common/Helpers";
import { DataSource } from "../atlas";
import { Logger, LogLevel } from "./common/Logger";

export class Animations {
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
}