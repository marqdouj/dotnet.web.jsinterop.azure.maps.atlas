import * as atlas from "azure-maps-control"
import * as spatial from "azure-maps-spatial-io"
import { Helpers } from "../common/Helpers";
import { Logger, LogLevel } from "../common/Logger";
import { Sources } from "../Sources";
import { MapObjectReference } from "../common";
import { Layers } from "../Layers";

export class SpatialLayers {
    public static add(map: atlas.Map, layers: SpatialLayer[], getReferences: boolean = false) {
        const eventName = "SpatialLayers.add";
        const mapId = Helpers.getMapId(map);
        const results: MapObjectReference[] = [];

        if (Helpers.hasNoElements(layers, `${eventName}: 'layers' has no elements where mapId = '${mapId}'.`))
            return;

        layers.forEach((sl) => {
            const id = Helpers.getSourceId(sl);
            let lyr: any;

            if (Helpers.isEmptyOrNull(id)) {
                Logger.logMapMessage(mapId, LogLevel.Error, `${eventName}: Unable to resolve layer id.`, sl);
                return; //continue
            }

            switch (sl.type) {
                case SpatialLayerType.OgcMapLayer:
                    lyr = new spatial.layer.OgcMapLayer(sl.options);
                    break;
                case SpatialLayerType.SimpleDataLayer:
                    const ds = Sources.getSourceInstance(map, sl.source) as atlas.source.DataSource;
                    lyr = new spatial.layer.SimpleDataLayer(ds, sl.options);
                    break;
                default:
                    Logger.logMapMessage(mapId, LogLevel.Error, `${eventName}: layer type not supported.`, sl);
            }

            if (lyr) {
                lyr.id = sl.id;
                lyr.metadata = sl.metadata;
                map.layers.add(lyr, sl.before);
                if (getReferences)
                    results.push(Layers.getReference(mapId, lyr, sl.id));
            }
        });

        return results;
    }

    public static getOptions(map: atlas.Map, source: any) {
        return Layers.getOptions(map, source);
    }

    public static setOptions(map: atlas.Map, source: any, options: any) {
        Layers.setOptions(map, source, options);
    }
}

enum SpatialLayerType {
    SimpleDataLayer = "SimpleDataLayer",
    OgcMapLayer = "OgcMapLayer",
}

interface SpatialLayer {
    type: SpatialLayerType;
    id: string;
    before?: string;
    source: any;
    options?: any;
    metadata: any;
}
