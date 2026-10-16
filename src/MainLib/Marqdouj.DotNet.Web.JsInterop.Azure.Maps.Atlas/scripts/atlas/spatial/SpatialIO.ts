import * as atlas from "azure-maps-control"
import * as anims from "azure-maps-animations"
import * as spatial from "azure-maps-spatial-io"
import { Helpers } from "../common/Helpers";
import { Sources } from "../Sources";
import { Logger, LogLevel } from "../common/Logger";
import { AnimationEventInfo, AnimationEvents } from "../events/AnimationEvents";
import { JsError } from "../common";
import { Animations } from "../Animations";

export class SpatialIO {
    public static async loadGPSTraceRoute(dotNetRef: any, map: atlas.Map, routeParams: GPSTraceRouteParameters): Promise<GPSTraceRouteResult> {
        const eventName = "SpatialIO.read";
        const mapId = Helpers.getMapId(map);
        const result: GPSTraceRouteResult = {};

        try {
            const dsRoute = Sources.getSourceInstance(map, routeParams.routeSourceId) as atlas.source.DataSource;
            if (!dsRoute) {
                throw new Error(`Unable to resolve datasource where id = '${routeParams.routeSourceId}'.`);
            }

            const dsPin = Sources.getSourceInstance(map, routeParams.pinSourceId) as atlas.source.DataSource;
            if (!dsPin) {
                throw new Error(`Unable to resolve datasource where id = '${routeParams.pinSourceId}'.`);
            }

            routeParams = Helpers.removeNullish(routeParams);

            dsRoute.clear();
            dsPin.clear();

            const readOptions = this.#buildReadOptions(routeParams.readOptions);

            await spatial.io.read(routeParams.spatialData, readOptions).then(r => {
                if (r) {
                    dsRoute.add(r);

                    if (r.bbox && routeParams.zoomToBBox) {
                        map.setCamera({
                            bounds: r.bbox,
                            padding: routeParams.padding ??= 50
                        });
                        const camera = map.getCamera();
                        result.center = camera.center;
                        result.zoom = camera.zoom;
                    }

                    const route = Animations.extractRoutePoints(r, routeParams.timestampProperty);

                    if (route && route.length > 0) {
                        const pin = new atlas.Shape(route[0]);
                        dsPin.add(pin);

                        const pathOptions: anims.RoutePathAnimationOptions = { ...routeParams?.pathOptions };

                        if (routeParams.follow) {
                            pathOptions.map = map as any;
                        }

                        const animation = anims.animations.moveAlongRoute(route, pin as any, pathOptions);

                        if (animation) {
                            result.animation = DotNet.createJSObjectReference(animation);
                            AnimationEvents.add(dotNetRef, map, animation, routeParams.events!);
                        }
                    }
                }
                else {
                    throw new Error("Read spatial data failed.");
                }
            });
        } catch (e: any) {
            Logger.logMapMessage(mapId, LogLevel.Error, `${eventName}: an error has ocurred.`, e);
            result.error = { name: e.name, message: e.message, cause: e.cause, stack: e.stack };
        }

        return result;
    }

    public static async read(map: atlas.Map, datasource: any, spatialData: any, options?: ReadOptions, dsReadAction: string = "add", zoomToBBox: boolean = true, padding: number | atlas.Padding = 50) : Promise<void> {
        const eventName = "SpatialIO.read";
        const mapId = Helpers.getMapId(map);

        const ds = Sources.getSourceInstance(map, datasource) as atlas.source.DataSource;
        if (!ds) {
            Logger.logMapMessage(mapId, LogLevel.Error, `${eventName}: Unable to resolve datasource.`, datasource);
            return;
        }

        const readOptions = this.#buildReadOptions(options);

        await spatial.io.read(spatialData, readOptions).then(r => {
            if (r) {
                //Add the feature data to the data source.
                const dsAction = Helpers.switchCaseInsensitive(dsReadAction, {
                    add: () => DsReadAction.Add,
                    setshapes: () => DsReadAction.SetShapes,
                }, () => "");

                switch (dsAction) {
                    case DsReadAction.Add:
                        ds.add(r);
                        break;
                    case DsReadAction.SetShapes:
                        ds.setShapes(r);
                        break;
                    default:
                        throw new Error(`${eventName}: read action not supported '${dsReadAction}'.`);
                }

                //If bounding box information is known for data, set the map view to it.
                if (r.bbox && zoomToBBox) {
                    map.setCamera({
                        bounds: r.bbox,
                        padding: padding ??= 50
                    });
                }
            }
        });
    }

    static #buildReadOptions(options?: any) {
        const readOptions = Helpers.removeNullish(options);
        const combinedOptions: spatial.SpatialDataReadOptions = {
            ...readOptions?.baseSpatialDataReadOptions,
            ...readOptions?.baseSpatialXmlReadOptions,
            ...readOptions?.gmlReadOptions,
            ...readOptions?.gpxReadOptions,
            ...readOptions?.kmlReadOptions,
            ...readOptions?.spatialCsvReadOptions
        };

        return combinedOptions;
    }
}

enum DsReadAction {
    Add = "Add",
    SetShapes = "SetShapes"
}

interface ReadOptions {
    baseSpatialDataReadOptions?: any;
    baseSpatialXmlReadOptions?: any;
    gmlReadOptions?: any;
    gpxReadOptions?: any;
    kmlReadOptions?: any;
    spatialCsvReadOptions?: any;
}

interface GPSTraceRouteParameters {
    routeSourceId: string;
    pinSourceId: string;
    spatialData: any;
    timestampProperty?: string;
    readOptions?: ReadOptions;
    pathOptions?: anims.RoutePathAnimationOptions;
    events?: AnimationEventInfo[];
    follow: boolean;
    zoomToBBox: boolean;
    padding?: any;
}

interface GPSTraceRouteResult {
    animation?: any;
    center?: atlas.data.Position;
    error?: JsError;
    zoom?: number;
}