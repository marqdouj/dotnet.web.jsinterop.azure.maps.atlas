import * as atlas from "azure-maps-control"
import { MapObjectReference } from "./common"
import { Helpers } from "./common/Helpers";
import { LayerEvents } from "../atlas";

export class Layers {
    public static add(map: atlas.Map, mapLayers: MapLayer[], getReferences: boolean = false) {
        const results: MapObjectReference[] = [];
        const mapId = Helpers.getMapId(map);

        if (Helpers.hasNoElements(mapLayers))
            return;

        mapLayers.forEach((ml) => {
            let lyr: atlas.layer.Layer | undefined = undefined;

            switch (ml.type) {
                case LayerType.Bubble:
                    lyr = new atlas.layer.BubbleLayer(ml.source, ml.id, ml.options);
                    break;
                case LayerType.HeatMap:
                    lyr = new atlas.layer.HeatMapLayer(ml.source, ml.id, ml.options);
                    break;
                case LayerType.Image:
                    lyr = new atlas.layer.ImageLayer(ml.options, ml.id);
                    break;
                case LayerType.Line:
                    lyr = new atlas.layer.LineLayer(ml.source, ml.id, ml.options);
                    break;
                case LayerType.Polygon:
                    lyr = new atlas.layer.PolygonLayer(ml.source, ml.id, ml.options);
                    break;
                case LayerType.PolygonExtrusion:
                    lyr = new atlas.layer.PolygonExtrusionLayer(ml.source, ml.id, ml.options);
                    break;
                case LayerType.Symbol:
                    lyr = new atlas.layer.SymbolLayer(ml.source, ml.id, this.#resolveSymbolLayerOptions(ml.options));
                    break;
                case LayerType.Tile:
                    lyr = new atlas.layer.TileLayer(ml.options, ml.id);
                    break;
                default:
            }

            if (lyr) {
                map.layers.add(lyr, ml.before);
                if (getReferences)
                    results.push(this.#getReference(mapId, lyr, ml.id));
            }
        });

        return results;
    }

    public static getReferences(map: atlas.Map, ids?: string[]) {
        if (ids) 
            return this.#getLayersById(map, ids);
        else
            return this.#getLayersAll(map);
    }

    static #getLayersAll(map: atlas.Map) {
        const results: MapObjectReference[] = [];
        const mapId = Helpers.getMapId(map);
        const layers = map.layers.getLayers();

        layers.forEach((lyr) => {
            results.push(this.#getReference(mapId, lyr));
        });

        return results;
    }

    static #getLayersById(map: atlas.Map, ids: string[]) {
        const results: MapObjectReference[] = [];
        const mapId = Helpers.getMapId(map);

        if (Helpers.hasNoElements(ids))
            return;

        ids.forEach((id) => {
            results.push(this.#getReference(mapId, map.layers.getLayerById(id), id));
        });

        return results;
    }

    public static remove(map: atlas.Map, layers: any[]) {
        if (layers && layers.length > 0) {
            var first = layers[0];
            if (typeof first === "string") {
                this.#removeById(map, layers);
            }
            else if (first instanceof atlas.layer.Layer) {
                this.#removeByRef(map, layers);
            }
        }
    }

    static #removeByRef(map: atlas.Map, layers: atlas.layer.Layer[]) {
        const sourceIds = layers.map(e => e.getId());
        LayerEvents.removeByLayer(map, sourceIds)
        map.layers.remove(layers);
    }

    static #removeById(map: atlas.Map, ids: string[]) {
        LayerEvents.removeByLayer(map, ids)
        map.layers.remove(ids);
    }

    static #resolveSymbolLayerOptions(layerOptions: atlas.SymbolLayerOptions): atlas.SymbolLayerOptions | undefined {
        if (!layerOptions)
            return;

        const result = { ...layerOptions };
        const iconOptions = result.iconOptions;

        if (!iconOptions) return result;

        const imageId = iconOptions.imageId;
        if (Helpers.isNotEmptyOrNull(imageId)) {
            iconOptions.image = imageId;
        }

        const rotationSpec = iconOptions.rotationSpecification
        if (rotationSpec) {
            iconOptions.rotation = rotationSpec;
        }

        return result;
    }

    static #getReference(mapId: string, layer?: atlas.layer.Layer, layerId?: string | undefined) {
        const type = this.#getLayerType(layer);
        return Helpers.createMapObjectReference(mapId, type, layer, layerId);
    }

    static #getLayerType(layer: atlas.layer.Layer | undefined): string {
        if (layer instanceof atlas.layer.BubbleLayer)
            return LayerType.Bubble;

        if (layer instanceof atlas.layer.HeatMapLayer)
            return LayerType.HeatMap;

        if (layer instanceof atlas.layer.ImageLayer)
            return LayerType.Image;

        if (layer instanceof atlas.layer.LineLayer)
            return LayerType.Line;

        if (layer instanceof atlas.layer.PolygonLayer)
            return LayerType.Polygon;

        if (layer instanceof atlas.layer.PolygonExtrusionLayer)
            return LayerType.PolygonExtrusion;

        if (layer instanceof atlas.layer.SymbolLayer)
            return LayerType.Symbol;

        if (layer instanceof atlas.layer.TileLayer)
            return LayerType.Tile;

        return "Unknown";
    }
}

enum LayerType {
    Bubble = 'Bubble',
    HeatMap = 'HeatMap',
    Image = 'Image',
    Line = 'Line',
    Polygon = 'Polygon',
    PolygonExtrusion = 'PolygonExtrusion',
    Symbol = 'Symbol',
    Tile = 'Tile',
}

interface MapLayer {
    id: string;
    source?: any;
    type: LayerType;
    before?: string;
    options?: any;
}