import * as atlas from "azure-maps-control"
import { Helpers } from "./common/Helpers";
import { MapObjectReference } from "./common";
import { DataSource } from "./DataSource";
import { Logger, LogLevel } from "./common/Logger";

export class Features {
    public static add(map: atlas.Map, source: any, features: any[], getReferences: boolean = false): MapObjectReference[] {
        const eventName = "Features.add";
        const mapId = Helpers.getMapId(map);
        const results: MapObjectReference[] = [];
        const ds = DataSource.getDataSourceFromSource(map, mapId, source, eventName, true);

        if (ds) {
            features.forEach((mf) => {
                const id = Helpers.getSourceId(mf);
                let item: any;

                if (Helpers.isNotEmptyOrNull(id)) {
                    item = ds.getShapeById(id!);
                }

                if (!item) {
                    let shape = new atlas.Shape(mf);
                    ds.add(shape);
                    if (getReferences)
                        results.push(Helpers.createMapObjectReference(mapId, "Feature", shape, shape.getId().toString()));
                }
                else {
                    Logger.logMapMessage(mapId, LogLevel.Warn, `${eventName}: Feature already exists where id = '${id}'.`);
}
            });
        }

        return results;
    }
}
