import * as atlas from "azure-maps-control"
import { Helpers } from "./common/Helpers";
import { Logger, LogLevel } from "./common/Logger";
import { EditAction, MapObjectReference } from "./common";

export class DataSource {
    public static clear(map: atlas.Map, sources: any[]) {
        const eventName = "DataSource.clear";
        const mapId = Helpers.getMapId(map);

        if (Helpers.hasNoElements(sources))
            return;

        sources.forEach((source) => {
            const ds = this.getDataSourceFromSource(map, mapId, source, eventName, true);
            ds?.clear();
        });
    }

    public static getShapeById(map: atlas.Map, source: any, shapeId: string, getReference: boolean = true) {
        const eventName = "DataSource.getShapeById";
        const mapId = Helpers.getMapId(map);
        const ds = this.getDataSourceFromSource(map, mapId, source, eventName, true);
        const shape = ds?.getShapeById(shapeId);
        return getReference ? Helpers.createMapObjectReference(mapId, "Shape", shape, shapeId) : shape;
    }

    public static getShapes(map: atlas.Map, source: any, getReference: boolean = true) {
        const eventName = "DataSource.getShapes";
        const mapId = Helpers.getMapId(map);
        const ds = this.getDataSourceFromSource(map, mapId, source, eventName, true);
        const shapes = ds?.getShapes();

        return getReference ? Helpers.createMapObjectReference(mapId, "Shapes", shapes, ds?.getId()) : shapes;
    }

    public static async importDataFromUrl(map: atlas.Map, source: any, url: string) {
        const eventName = "DataSource.importDataFromUrl";
        const mapId = Helpers.getMapId(map);
        const ds = this.getDataSourceFromSource(map, mapId, source, eventName, true);

        if (ds)
            await ds.importDataFromUrl(url);
    }

    public static getDataSourceFromSource(map: atlas.Map, mapId: string, source: any, eventName: string, logNotFound: boolean) {
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

    // #region Options

    public static getOptions(map: atlas.Map, source: any) {
        const eventName = "DataSource.getOptions";
        const mapId = Helpers.getMapId(map);
        const ds = this.getDataSourceFromSource(map, mapId, source, eventName, true);

        return ds?.getOptions();
    }

    public static setOptions(map: atlas.Map, source: any, options: any, action: EditAction) {
        const eventName = "DataSource.getOptions";
        const mapId = Helpers.getMapId(map);
        const ds = this.getDataSourceFromSource(map, mapId, source, eventName, true);

        let updatedOptions = options;

        switch (action) {
            case EditAction.Replace:
                break;
            case EditAction.Update:
                updatedOptions = Helpers.removeNullish(updatedOptions);
                break;
        }

        ds?.setOptions(updatedOptions);
    }

    // #endregion
}