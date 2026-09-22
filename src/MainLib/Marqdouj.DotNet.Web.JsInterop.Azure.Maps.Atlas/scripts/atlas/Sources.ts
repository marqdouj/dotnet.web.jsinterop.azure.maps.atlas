import * as atlas from "azure-maps-control"
import { MapObjectReference } from "./common"
import { Helpers } from "./common/Helpers";
import { Logger, LogLevel } from "./common/Logger";

export class Sources {
    public static add(map: atlas.Map, sources?: MapSource[], getReferences: boolean = false): MapObjectReference[] {
        const eventName = "Sources.add";
        const results: MapObjectReference[] = [];
        const mapId = Helpers.getMapId(map);

        if (Helpers.hasNoElements(sources))
            return results;

        if (Helpers.hasDuplicates(sources, "id", true, true))
            return results; //Will never reach here if true.

        const existing = map.sources.getSources(); 

        sources!.forEach((source) => {
            let ds = this.#getInstance(map, source, existing);
            if (ds) {
                Logger.logMapMessage(mapId, LogLevel.Warn, `${eventName}: Source already exists.`, source);
                return; //continue
            }

            switch (source.type) {
                case SourceType.Data:
                    ds = new atlas.source.DataSource(source.id, source.options);
                    break;
                case SourceType.ElevationTile:
                    ds = new atlas.source.ElevationTileSource(source.id, source.options);
                    break;
                case SourceType.VectorTile:
                    ds = new atlas.source.VectorTileSource(source.id, source.options);
                    break;
                default:
            }

            if (ds) {
                map.sources.add(ds);
                if (getReferences)
                    results.push(this.#getReference(mapId, ds, source.id));
            }
        });

        return results;
    }

    public static getReferences(map: atlas.Map, sources?: any[]): MapObjectReference[] {
        const results: MapObjectReference[] = [];
        const mapId = Helpers.getMapId(map);
        const existing = map.sources.getSources();

        if (Helpers.hasNoElements(sources)) {
            return results;
        }

        sources!.forEach((source) => {
            const ds = this.#getInstance(map, source, existing);
            results.push(this.#getReference(mapId, ds));
        });

        return results;
    }

    public static remove(map: atlas.Map, sources: string | atlas.source.Source | Array<string | atlas.source.Source>) {
        map.sources.remove(sources);
    }

    static #getReference(mapId: string, ds?: atlas.source.Source, id?: string) {
        const type = this.#getSourceType(ds);
        return Helpers.createMapObjectReference(mapId, type, ds, id ?? ds?.getId());
    }

    static #getSourceType(ds?: atlas.source.Source | undefined): string {
        if (!ds)
            return "Undefined";

        if (ds instanceof atlas.source.DataSource)
            return SourceType.Data; 

        if (ds instanceof atlas.source.ElevationTileSource)
            return SourceType.ElevationTile; 

        if (ds instanceof atlas.source.VectorTileSource)
            return SourceType.VectorTile;

        return "Source";
    }

    static #getInstance(map: atlas.Map, source: any, sources: atlas.source.Source[], eventName?: string): atlas.source.Source | undefined {
        let item: any;

        if (source instanceof atlas.source.Source) {
            item = source;
        }
        else {
            const id = Helpers.getSourceId(source);
            item = sources.findLast(item => Helpers.hasId(item, id));
        }

        if (!item && Helpers.isNotEmptyOrNull(eventName)) {
            const mapId = Helpers.getMapId(map);
            Logger.logMessage(mapId, LogLevel.Warn, `${eventName}: instance not found.`, source);
        }

        return item;
    }
}

enum SourceType {
    Data = 'Data',
    ElevationTile = 'ElevationTile',
    VectorTile = 'VectorTile'
}

export interface MapSource {
    id: string;
    type: SourceType;
    options?: any; // atlas.DataSourceOptions | atlas.ElevationTileSourceOptions | atlas.VectorTileSourceOptions | undefined;
}
