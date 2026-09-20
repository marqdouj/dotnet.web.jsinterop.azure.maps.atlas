import * as atlas from "azure-maps-control"
import { Helpers } from "./common/Helpers";
import { Logger, LogLevel } from "./common/Logger";

export class DataSource {
    public static clear(map: atlas.Map, sources: any[]) {
        const eventName = "DataSource.clear";
        const mapId = Helpers.getMapId(map);

        if (Helpers.hasNoElements(sources))
            return;

        sources.forEach((source) => {
            const ds = this.#getDataSourceFromSource(map, mapId, source, eventName, true);

            if (ds) {
                ds.clear();
            }
            else {
                Logger.logMapMessage(mapId, LogLevel.Warn, `${eventName}: DataSource not found.`, source);
            }
        });
    }

    public static async importDataFromUrl(map: atlas.Map, source: any, url: string) {
        const eventName = "DataSource.importDataFromUrl";
        const mapId = Helpers.getMapId(map);
        const ds = this.#getDataSourceFromSource(map, mapId, source, eventName, true);

        if (ds)
            await ds.importDataFromUrl(url);
    }

    static #getDataSourceFromSource(map: atlas.Map, mapId: string, source: any, eventName: string, logNotFound: boolean) {
        let ds: atlas.source.DataSource | undefined;

        if (source instanceof atlas.source.DataSource) {
            ds = source;
        }
        else {
            const id = Helpers.getSourceId(source);
            if (Helpers.isNotEmptyOrNull(id)) {
                const src = map.sources.getById(id!);
                if (src instanceof atlas.source.DataSource) {
                    ds = src;
                }
            }
        }

        if (!ds && logNotFound) {
            Logger.logMapMessage(mapId, LogLevel.Warn, `${eventName}: DataSource not found.`, source);
        }

        return ds;
    }
}