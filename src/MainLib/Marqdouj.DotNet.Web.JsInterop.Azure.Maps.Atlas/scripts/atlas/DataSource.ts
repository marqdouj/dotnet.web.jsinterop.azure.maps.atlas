import * as atlas from "azure-maps-control"

export class DataSource {
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