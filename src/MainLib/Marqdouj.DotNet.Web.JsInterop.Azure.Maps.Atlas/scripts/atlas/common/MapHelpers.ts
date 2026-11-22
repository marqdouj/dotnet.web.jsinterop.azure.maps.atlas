import * as atlas from "azure-maps-control"
import { MapObjectReference } from ".";
import { Logger, LogLevel } from "./Logger";

export class MapHelpers {
    static createMapObjectReference(mapId: string, type: string, item?: object, id?: string | undefined): MapObjectReference {
        const dnr = !item ? null : DotNet.createJSObjectReference(item);
        id ??= this.getSourceId(item);
        const msf: MapObjectReference = { mapId: mapId, id: id, type: type, jsReference: dnr };

        return msf;
    }

    static getSourceId(source: any) {
        let id: string | undefined;

        if (source) {
            if (typeof source === 'string') {
                id = source;
            }
            else if (source instanceof Object) {
                if (source.id) {
                    id = source.id;
                }
                else if (source.getId) {
                    id = source.getId();
                }
            }
        }

        return id;
    }

    static getMapId(map: atlas.Map) {
        const div = map.getMapContainer();
        if (!div) {
            Logger.logMapMessage("mapId:?", LogLevel.Error, "Helpers.getMapId: map.getMapContainer() returned null.");
            return "mapId:#ERROR#";
        }
        return div.id;
    }

    static isFeature(obj: any): obj is atlas.data.Feature<atlas.data.Geometry, any> {
        return obj && obj.type === 'Feature';
    }

    static isShape(obj: any): obj is atlas.Shape {
        return obj && obj.getType != undefined;
    }
}
