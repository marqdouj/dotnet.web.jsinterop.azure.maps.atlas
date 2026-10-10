import * as atlas from "azure-maps-control"
import * as spatial from "azure-maps-spatial-io"
import { Helpers } from "../common/Helpers";
import { Sources } from "../Sources";
import { Logger, LogLevel } from "../common/Logger";

export class SpatialIO {
    public static async read(map: atlas.Map, datasource: any, spatialData: any, options?: ReadOptions, zoomToBBox: boolean = true) : Promise<void> {
        const eventName = "SpatialIO.read";
        const mapId = Helpers.getMapId(map);

        const ds = Sources.getSourceInstance(map, datasource) as atlas.source.DataSource;
        if (!ds) {
            Logger.logMapMessage(mapId, LogLevel.Error, `${eventName}: Unable to resolve datasource.`, datasource);
            return;
        }

        const readOptions = Helpers.removeNullish(options);
        const combinedOptions: spatial.SpatialDataReadOptions = {
            ...readOptions?.baseSpatialDataReadOptions,
            ...readOptions?.baseSpatialXmlReadOptions,
            ...readOptions?.gmlReadOptions,
            ...readOptions?.gpxReadOptions,
            ...readOptions?.kmlReadOptions,
            ...readOptions?.spatialCsvReadOptions
        };

        await spatial.io.read(spatialData, combinedOptions).then(r => {
            if (r) {
                //Add the feature data to the data source.
                ds.add(r);

                //If bounding box information is known for data, set the map view to it.
                if (r.bbox && zoomToBBox) {
                    map.setCamera({
                        bounds: r.bbox,
                        padding: 50
                    });
                }
            }
        });
    }
}

interface ReadOptions {
    baseSpatialDataReadOptions?: any;
    baseSpatialXmlReadOptions?: any;
    gmlReadOptions?: any;
    gpxReadOptions?: any;
    kmlReadOptions?: any;
    spatialCsvReadOptions?: any;
}
