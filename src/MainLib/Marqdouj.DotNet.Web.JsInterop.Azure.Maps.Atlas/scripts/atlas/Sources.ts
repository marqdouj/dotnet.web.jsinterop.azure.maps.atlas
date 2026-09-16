import * as atlas from "azure-maps-control"
import { MapObjectReference } from "../common"
import { Helpers } from "../common/Helpers";

export class Sources {
    public static add(map: atlas.Map, mapSources: MapSource[], getReferences: boolean = false) {
        const results: MapObjectReference[] = [];
        const mapId = Helpers.getMapId(map);

        mapSources ?? [];

        mapSources.forEach((ms) => {
            let ds: atlas.source.Source | undefined = undefined;

            switch (ms.type) {
                case SourceType.Data:
                    ds = new atlas.source.DataSource(ms.id, ms.options);
                    break;
                case SourceType.ElevationTile:
                    ds = new atlas.source.ElevationTileSource(ms.id, ms.options);
                    break;
                case SourceType.VectorTile:
                    ds = new atlas.source.VectorTileSource(ms.id, ms.options);
                    break;
                default:
            }

            if (ds) {
                map.sources.add(ds);

                if (getReferences)
                    results.push(this.#getReference(mapId, ds, ms.id));
            }
        });

        return results;
    }

    public static clear(sources: atlas.source.Source[]) {
        sources.forEach((src) => {
            if (src instanceof atlas.source.DataSource) {
                src.clear();
            }
        });
    }

    public static clearById(map: atlas.Map, ids: string[]) {
        ids.forEach((id) => {
            const src = map.sources.getById(id);
            if (src instanceof atlas.source.DataSource) {
                src.clear();
            }
        });
    }

    public static getSources(map: atlas.Map) {
        const results: MapObjectReference[] = [];
        const mapId = Helpers.getMapId(map);
        const sources = map.sources.getSources();

        sources.forEach((ds) => {
            results.push(this.#getReference(mapId, ds));
        });

        return results;
    }

    public static getSourcesById(map: atlas.Map, ids: string[]) {
        const results: MapObjectReference[] = [];
        const mapId = Helpers.getMapId(map);

        ids.forEach((srcId) => {
            results.push(this.#getReference(mapId, map.sources.getById(srcId), srcId));
        });

        return results;
    }

    public static remove(map: atlas.Map, sources: atlas.source.Source[]) {
        map.sources.remove(sources);
    }

    public static removeById(map: atlas.Map, ids: string[]) {
        map.sources.remove(ids);
    }

    static #getReference(mapId: string, ds: atlas.source.Source, id: string = "") {
        const type = this.#getSourceType(ds);
        const dnr = !ds ? null : DotNet.createJSObjectReference(ds);
        let dsId = ds?.getId();
        if (Helpers.isEmptyOrNull(dsId)) {
            dsId = id;
        }
        const msf: MapObjectReference = { mapId: mapId, id: dsId, type: type, jsReference: dnr };

        return msf;
    }

    static #getSourceType(ds?: atlas.source.Source | undefined): string | undefined {
        if (!ds)
            return;

        if (ds instanceof atlas.source.DataSource)
            return SourceType.Data; 

        if (ds instanceof atlas.source.ElevationTileSource)
            return SourceType.ElevationTile; 

        if (ds instanceof atlas.source.VectorTileSource)
            return SourceType.VectorTile; 
    }
}

enum SourceType {
    Data = 'Data',
    ElevationTile = 'ElevationTile',
    VectorTile = 'VectorTile'
}

interface MapSource {
    id: string;
    type: SourceType;
    options?: any; // atlas.DataSourceOptions | atlas.ElevationTileSourceOptions | atlas.VectorTileSourceOptions | undefined;
}
