import * as atlas from "azure-maps-control"
import { Helpers } from "./common/Helpers";
import * as events from "./events"
import { EventsMap } from "./events/EventsMap";

export class MapEvents {
    static readonly #eventsMap: EventsMap = new EventsMap ();

    public static add(dotNetRef: any, map: atlas.Map, mapEvents?: events.EventInfo[]) {
        const mapId = Helpers.getMapId(map);
        mapEvents ??= [];

        mapEvents.forEach((me) => {
            me.eventName ??= events.MapEventNotify.NotifyMapEvent;
            let callback: any;

            //General/Mouse are most common so check for them first.
            if (Helpers.isValueInEnum(MapEventGeneral, me.type)) {
                callback = this.#getNotifyMapEventGeneralCallback(dotNetRef, mapId, me);
            }
            else if (Helpers.isValueInEnum(MapEventMouse, me.type)) {
                callback = this.#getNotifyMapEventMouseCallback(dotNetRef, mapId, me);
            }
            else if (Helpers.isValueInEnum(MapEventConfig, me.type)) {
                callback = this.#getNotifyMapEventConfigCallback(dotNetRef, mapId, me);
            }
            else if (Helpers.isValueInEnum(MapEventData, me.type)) {
                callback = this.#getNotifyMapEventDataCallback(dotNetRef, mapId, me);
            }
            else if (Helpers.isValueInEnum(MapEventLayer, me.type)) {
                callback = this.#getNotifyMapEventLayerCallback(dotNetRef, mapId, me);
            }
            else if (Helpers.isValueInEnum(MapEventSource, me.type)) {
                callback = this.#getNotifyMapEventSourceCallback(dotNetRef, mapId, me);
            }
            else if (Helpers.isValueInEnum(MapEventStyle, me.type)) {
                callback = this.#getNotifyMapEventStyleCallback(dotNetRef, mapId, me);
            }
            else if (Helpers.isValueInEnum(MapEventTouch, me.type)) {
                callback = this.#getNotifyMapEventTouchCallback(dotNetRef, mapId, me);
            }
            else if (Helpers.isValueInEnum(MapEventWheel, me.type)) {
                callback = this.#getNotifyMapEventWheelCallback(dotNetRef, mapId, me);
            }

            if (callback) {
                if (me.once) {
                    map.events.addOnce(me.type as unknown as any, callback);
                }
                else {
                    map.events.add(me.type as unknown as any, callback);
                }
            }
        });
    }

    public static remove(map: atlas.Map, mapEvents: events.EventInfo[]) {
        const mapId = Helpers.getMapId(map);
        mapEvents ?? [];

        mapEvents.forEach((me) => {
            const callback: any = this.#eventsMap.getCallback(mapId, me);

            if (callback) {
                map.events.remove(me.type as any, callback);
                this.#eventsMap.removeCallback(mapId, me);
            }
        });    
    }

    // #region MapEventConfig

    static #getNotifyMapEventConfigCallback(dotNetRef: any, mapId: string, event: events.EventInfo) {
        let callback: any = this.#eventsMap.getCallback(mapId, event);

        if (callback) {
            return callback;
        }

        callback = (e: atlas.MapConfiguration) => this.#notifyMapEventConfig(e, dotNetRef, mapId, event);

        this.#eventsMap.addCallback(mapId, event, callback);

        return callback;
    }

    static #notifyMapEventConfig = (callback: atlas.MapConfiguration, dotNetRef: any, mapId: string, mapEvent: events.EventInfo) => {
        const payload = { config: { ...callback } };
        const args = events.Helpers.buildNotifyMapEventArgs(mapId, mapEvent, payload);
        dotNetRef.invokeMethodAsync(mapEvent.eventName, args);
    };

    // #endRegion

    // #region MapEventData

    static #getNotifyMapEventDataCallback(dotNetRef: any, mapId: string, event: events.EventInfo) {
        let callback: any = this.#eventsMap.getCallback(mapId, event);

        if (callback) {
            return callback;
        }

        callback = (e: atlas.MapDataEvent) => this.#notifyMapEventData(e, dotNetRef, mapId, event);

        this.#eventsMap.addCallback(mapId, event, callback);

        return callback;
    }

    static #notifyMapEventData = (callback: atlas.MapDataEvent, dotNetRef: any, mapId: string, mapEvent: events.EventInfo) => {
        const payload = this.#buildMapDataEventPayload(callback);
        const args = events.Helpers.buildNotifyMapEventArgs(mapId, mapEvent, payload);
        dotNetRef.invokeMethodAsync(mapEvent.eventName, args);
    };

    static #buildMapDataEventPayload(dataEvent: atlas.MapDataEvent) {
        const payload = {
            dataType: dataEvent.dataType,
            isSourceLoaded: dataEvent.isSourceLoaded,
            source: dataEvent.source?.getId(),
            sourceDataType: dataEvent.sourceDataType,
            tile: dataEvent.tile
        };

        return { data: payload };
    }

    // #endRegion

    // #region MapEventGeneral

    static #getNotifyMapEventGeneralCallback(dotNetRef: any, mapId: string, event: events.EventInfo) {
        let callback: any = this.#eventsMap.getCallback(mapId, event);

        if (callback) {
            return callback;
        }

        callback = (e: atlas.MapEvent) => this.#notifyMapEventGeneral(dotNetRef, mapId, event);

        this.#eventsMap.addCallback(mapId, event, callback);

        return callback;
    }

    static #notifyMapEventGeneral = (dotNetRef: any, mapId: string, mapEvent: events.EventInfo) => {
        const payload: any = undefined;
        const args = events.Helpers.buildNotifyMapEventArgs(mapId, mapEvent, payload);
        dotNetRef.invokeMethodAsync(mapEvent.eventName, args);
    };

    // #endRegion

    // #region MapEventLayer

    static #getNotifyMapEventLayerCallback(dotNetRef: any, mapId: string, event: events.EventInfo) {
        let callback: any = this.#eventsMap.getCallback(mapId, event);

        if (callback) {
            return callback;
        }

        callback = (e: atlas.layer.Layer) => this.#notifyMapEventLayer(e, dotNetRef, mapId, event);

        this.#eventsMap.addCallback(mapId, event, callback);

        return callback;
    }

    static #notifyMapEventLayer = (callback: atlas.layer.Layer, dotNetRef: any, mapId: string, mapEvent: events.EventInfo) => {
        const payload = { layer: { id: callback.getId() } };
        const args = events.Helpers.buildNotifyMapEventArgs(mapId, mapEvent, payload);
        dotNetRef.invokeMethodAsync(mapEvent.eventName, args);
    };

    // #endRegion

    // #region MapEventMouse

    static #getNotifyMapEventMouseCallback(dotNetRef: any, mapId: string, event: events.EventInfo) {
        let callback: any = this.#eventsMap.getCallback(mapId, event);

        if (callback) {
            return callback;
        }

        callback = (e: atlas.MapMouseEvent) => this.#notifyMapEventMouse(e, dotNetRef, mapId, event);

        this.#eventsMap.addCallback(mapId, event, callback);

        return callback;
    }

    static #notifyMapEventMouse = (callback: atlas.MapMouseEvent, dotNetRef: any, mapId: string, mapEvent: events.EventInfo) => {
        if (mapEvent.preventDefault)
            callback.preventDefault();

        const payload = events.Helpers.buildMouseEventPayload(callback);
        const args = events.Helpers.buildNotifyMapEventArgs(mapId, mapEvent, payload);
        dotNetRef.invokeMethodAsync(mapEvent.eventName, args);
    };

    // #endRegion

    // #region MapEventSource

    static #getNotifyMapEventSourceCallback(dotNetRef: any, mapId: string, event: events.EventInfo) {
        let callback: any = this.#eventsMap.getCallback(mapId, event);

        if (callback) {
            return callback;
        }

        callback = (e: atlas.source.Source) => this.#notifyMapEventSource(e, dotNetRef, mapId, event);

        this.#eventsMap.addCallback(mapId, event, callback);

        return callback;
    }

    static #notifyMapEventSource = (callback: atlas.source.Source, dotNetRef: any, mapId: string, mapEvent: events.EventInfo) => {
        const payload = { source: { id: callback.getId() } };
        const args = events.Helpers.buildNotifyMapEventArgs(mapId, mapEvent, payload);
        dotNetRef.invokeMethodAsync(mapEvent.eventName, args);
    };

    // #endRegion

    // #region MapEventStyle

    static #getNotifyMapEventStyleCallback(dotNetRef: any, mapId: string, event: events.EventInfo) {
        let callback: any = this.#eventsMap.getCallback(mapId, event);

        if (callback) {
            return callback;
        }

        switch (event.type.toLowerCase()) {
            case MapEventStyle.StyleChanged:
                callback = (source: atlas.StyleChangedEvent) => this.#notifyMapEventStyle(source.style, dotNetRef, mapId, event);
                break;
            case MapEventStyle.StyleImageMissing:
                callback = (style: string) => this.#notifyMapEventStyle(style, dotNetRef, mapId, event);
                break;
            default:
        }

        this.#eventsMap.addCallback(mapId, event, callback);

        return callback;
    }

    static #notifyMapEventStyle = (style: string, dotNetRef: any, mapId: string, mapEvent: events.EventInfo) => {
        const payload = { style: { style: style } };
        const args = events.Helpers.buildNotifyMapEventArgs(mapId, mapEvent, payload);
        dotNetRef.invokeMethodAsync(mapEvent.eventName, args);
    };

    // #endRegion

    // #region MapEventTouch

    static #getNotifyMapEventTouchCallback(dotNetRef: any, mapId: string, event: events.EventInfo) {
        let callback: any = this.#eventsMap.getCallback(mapId, event);

        if (callback) {
            return callback;
        }

        callback = (e: atlas.MapTouchEvent) => this.#notifyMapEventTouch(e, dotNetRef, mapId, event);

        this.#eventsMap.addCallback(mapId, event, callback);

        return callback;
    }

    static #notifyMapEventTouch = (callback: atlas.MapTouchEvent, dotNetRef: any, mapId: string, mapEvent: events.EventInfo) => {
        if (mapEvent.preventDefault)
            callback.preventDefault();
        const payload = events.Helpers.buildTouchEventPayload(callback);
        const args = events.Helpers.buildNotifyMapEventArgs(mapId, mapEvent, payload);
        dotNetRef.invokeMethodAsync(mapEvent.eventName, args);
    };

    // #endRegion

    // #region MapEventWheel

    static #getNotifyMapEventWheelCallback(dotNetRef: any, mapId: string, event: events.EventInfo) {
        let callback: any = this.#eventsMap.getCallback(mapId, event);

        if (callback) {
            return callback;
        }

        callback = (e: atlas.MapMouseWheelEvent) => this.#notifyMapEventWheel(e, dotNetRef, mapId, event);

        this.#eventsMap.addCallback(mapId, event, callback);

        return callback;
    }

    static #notifyMapEventWheel = (callback: atlas.MapMouseWheelEvent, dotNetRef: any, mapId: string, mapEvent: events.EventInfo) => {
        if (mapEvent.preventDefault)
            callback.preventDefault();
        const payload = events.Helpers.buildWheelEventPayload(callback);
        const args = events.Helpers.buildNotifyMapEventArgs(mapId, mapEvent, payload);
        dotNetRef.invokeMethodAsync(mapEvent.eventName, args);
    };

    // #endRegion
}

enum MapEventConfig {
    MapConfigChanged = 'mapconfigurationchanged',
}

enum MapEventData {
    Data = 'data',
    SourceData = 'sourcedata',
    StyleData = 'styledata',
}

enum MapEventGeneral {
    BoxZoomEnd = 'boxzoomend',
    BoxZoomStart = 'boxzoomstart',
    Error = "error",
    Drag = 'drag',
    DragEnd = 'dragend',
    DragStart = 'dragstart',
    Idle = 'idle',
    Load = 'load',
    Move = 'move',
    MoveEnd = 'moveend',
    MoveStart = 'movestart',
    Pitch = 'pitch',
    PitchEnd = 'pitchend',
    PitchStart = 'pitchstart',
    Ready = 'ready',
    Render = 'render',
    Resize = 'resize',
    Rotate = 'rotate',
    RotateEnd = 'rotateend',
    RotateStart = 'rotatestart',
    TokenAcquired = 'tokenacquired',
    Zoom = 'zoom',
    ZoomEnd = 'zoomend',
    ZoomStart = 'zoomstart'
}

enum MapEventLayer {
    LayerAdded = 'layeradded',
    LayerRemoved = 'layerremoved'
}

enum MapEventSource {
    SourceAdded = 'sourceadded',
    SourceRemoved = 'sourceremoved'
}

enum MapEventStyle {
    StyleChanged = 'stylechanged',
    StyleImageMissing = 'styleimagemissing',
}

enum MapEventMouse {
    Click = 'click',
    ContextMenu = 'contextmenu',
    DblClick = 'dblclick',
    MouseDown = 'mousedown',
    MouseMove = 'mousemove',
    MouseOut = 'mouseout',
    MouseOver = 'mouseover',
    MouseUp = 'mouseup',
}

export enum MapEventTouch {
    TouchCancel = 'touchcancel',
    TouchEnd = 'touchend',
    TouchMove = 'touchmove',
    TouchStart = 'touchstart'
}

export enum MapEventWheel {
    Wheel = 'wheel',
}

export const MapEventType = {
    ...MapEventConfig,
    ...MapEventData,
    ...MapEventGeneral,
    ...MapEventLayer,
    ...MapEventMouse,
    ...MapEventSource,
    ...MapEventStyle,
    ...MapEventTouch,
    ...MapEventWheel
};

export type MapEventType = typeof MapEventType;

