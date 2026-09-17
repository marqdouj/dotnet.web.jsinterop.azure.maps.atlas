import * as atlas from "azure-maps-control"
import { Logger, LogLevel } from "./common/Logger"
import { Helpers } from "./common/Helpers";

export class Features {
    public static add(map: atlas.Map, sourceId: string, features: any[]) {
        const results: string[] = [];
        const mapId = Helpers.getMapId(map);

        const ds = map.sources.getById(sourceId);
        if (ds instanceof atlas.source.DataSource) {
            features.forEach((mf) => {
                const shape = new atlas.Shape(mf);
                ds.add(shape);
                results.push(shape.getId().toString());
            });
        }
        else {
            Logger.logMapMessage(mapId, LogLevel.Error, `Datasource not found where sourceId = '${sourceId}'`, ds);
        }

        return results;
    }
}
