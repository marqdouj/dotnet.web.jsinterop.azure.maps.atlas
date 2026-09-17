import * as atlas from "azure-maps-control"

export class DataSource {
    public static clear(map: atlas.Map, sources: atlas.source.DataSource[] | string[]) {
        sources.forEach((src) => {
            let ds: atlas.source.DataSource;

            if (src instanceof atlas.source.DataSource) {
                ds = src;
            }
            else {
                ds = map.sources.getById(src) as atlas.source.DataSource;
            }

            ds.clear();
        });
    }

    public static async importDataFromUrl(map: atlas.Map, source: any, url: string)  {
        let ds: atlas.source.DataSource;

        if (source instanceof atlas.source.DataSource) {
            ds = source as atlas.source.DataSource;
        }
        else {
            ds = map.sources.getById(source) as atlas.source.DataSource;
        }

        await ds.importDataFromUrl(url);
    }
}