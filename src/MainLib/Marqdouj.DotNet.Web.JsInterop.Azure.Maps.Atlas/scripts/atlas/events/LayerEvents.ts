import * as atlas from "azure-maps-control"
import { Helpers } from "../common/Helpers";
import * as events from "."
import { EventsMap } from "./EventsMap";
import { Logger, LogLevel } from "../common/Logger";

export class LayerEvents {
    static readonly #eventsMap: EventsMap = new EventsMap();

    public static add(dotNetRef: any, map: atlas.Map, mapEvents?: events.EventInfo[]) {
        const eventName = "LayerEvents.add";
        const mapId = Helpers.getMapId(map);
        mapEvents ??= [];

        mapEvents.forEach((me) => {
            me.eventName ??= events.MapEventNotify.NotifyMapEvent;
            let callback: any;
            let target: atlas.layer.Layer | undefined;

            const source = me.source;

            if (source) {
                if (typeof source === 'string') {
                    target = map.layers.getLayerById(source);
                }
                else if (source instanceof atlas.layer.Layer) {
                    target = source;
                }
            }

            if (target) {
                if (Helpers.isValueInEnum(LayerEventGeneral, me.type)) {
                    callback = this.#getNotifyLayerEventGeneralCallback(dotNetRef, mapId, me);
                }
                else if (Helpers.isValueInEnum(LayerEventMouse, me.type)) {
                    callback = this.#getNotifyLayerEventMouseCallback(dotNetRef, mapId, me);
                }
                else if (Helpers.isValueInEnum(LayerEventTouch, me.type)) {
                    callback = this.#getNotifyLayerEventTouchCallback(dotNetRef, mapId, me);
                }
                else if (Helpers.isValueInEnum(LayerEventWheel, me.type)) {
                    callback = this.#getNotifyLayerEventWheelCallback(dotNetRef, mapId, me);
                }

                if (callback) {
                    if (me.once) {
                        map.events.addOnce(me.type as unknown as any, target as any, callback);
                    }
                    else {
                        map.events.add(me.type as unknown as any, target as any, callback);
                    }
                } 
                else {
                    Logger.logMapMessage(mapId, LogLevel.Error, `${eventName}: Callback not found.`, me);
                }
            }
            else {
                Logger.logMapMessage(mapId, LogLevel.Error, `${eventName}: Target not found.`, me);
            }
        });
    }

    public static remove(map: atlas.Map, mapEvents: events.EventInfo[]) {
        const mapId = Helpers.getMapId(map);
        mapEvents ?? [];

        mapEvents.forEach((me) => {
            const callback: any = this.#eventsMap.getCallback(mapId, me);

            if (callback) {
                map.events.remove(me.type, callback);
                this.#eventsMap.removeCallback(mapId, me);
            }
        });
    }

    // #region LayerEventGeneral

    static #getNotifyLayerEventGeneralCallback(dotNetRef: any, mapId: string, event: events.EventInfo) {
        let callback: any = this.#eventsMap.getCallback(mapId, event);

        if (callback) {
            return callback;
        }

        callback = (e: atlas.layer.Layer) => this.#notifyLayerEventGeneral(e, dotNetRef, mapId, event);

        this.#eventsMap.addCallback(mapId, event, callback);

        return callback;
    }

    static #notifyLayerEventGeneral = (callback: atlas.layer.Layer, dotNetRef: any, mapId: string, mapEvent: events.EventInfo) => {
        const payload = { layer: { id: callback.getId() } };
        const args = events.Helpers.buildNotifyEventArgs(mapId, mapEvent, payload);
        dotNetRef.invokeMethodAsync(mapEvent.eventName, args);
    };

    // #endRegion

    // #region LayerEventMouse

    static #getNotifyLayerEventMouseCallback(dotNetRef: any, mapId: string, event: events.EventInfo) {
        let callback: any = this.#eventsMap.getCallback(mapId, event);

        if (callback) {
            return callback;
        }

        callback = (e: atlas.MapMouseEvent) => this.#notifyLayerEventMouse(e, dotNetRef, mapId, event);

        this.#eventsMap.addCallback(mapId, event, callback);

        return callback;
    }

    static #notifyLayerEventMouse = (callback: atlas.MapMouseEvent, dotNetRef: any, mapId: string, mapEvent: events.EventInfo) => {
        if (mapEvent.preventDefault)
            callback.preventDefault();

        const payload = events.Helpers.buildMouseEventPayload(callback);
        const args = events.Helpers.buildNotifyEventArgs(mapId, mapEvent, payload);
        dotNetRef.invokeMethodAsync(mapEvent.eventName, args);
    };

    // #endRegion

    // #region LayerEventTouch

    static #getNotifyLayerEventTouchCallback(dotNetRef: any, mapId: string, event: events.EventInfo) {
        let callback: any = this.#eventsMap.getCallback(mapId, event);

        if (callback) {
            return callback;
        }

        callback = (e: atlas.MapTouchEvent) => this.#notifyLayerEventTouch(e, dotNetRef, mapId, event);

        this.#eventsMap.addCallback(mapId, event, callback);

        return callback;
    }

    static #notifyLayerEventTouch = (callback: atlas.MapTouchEvent, dotNetRef: any, mapId: string, mapEvent: events.EventInfo) => {
        if (mapEvent.preventDefault)
            callback.preventDefault();
        const payload = events.Helpers.buildTouchEventPayload(callback);
        const args = events.Helpers.buildNotifyEventArgs(mapId, mapEvent, payload);
        dotNetRef.invokeMethodAsync(mapEvent.eventName, args);
    };

    // #endRegion

    // #region LayerEventWheel

    static #getNotifyLayerEventWheelCallback(dotNetRef: any, mapId: string, event: events.EventInfo) {
        let callback: any = this.#eventsMap.getCallback(mapId, event);

        if (callback) {
            return callback;
        }

        callback = (e: atlas.MapMouseWheelEvent) => this.#notifyLayerEventWheel(e, dotNetRef, mapId, event);

        this.#eventsMap.addCallback(mapId, event, callback);

        return callback;
    }

    static #notifyLayerEventWheel = (callback: atlas.MapMouseWheelEvent, dotNetRef: any, mapId: string, mapEvent: events.EventInfo) => {
        if (mapEvent.preventDefault)
            callback.preventDefault();
        const payload = events.Helpers.buildWheelEventPayload(callback);
        const args = events.Helpers.buildNotifyEventArgs(mapId, mapEvent, payload);
        dotNetRef.invokeMethodAsync(mapEvent.eventName, args);
    };

    // #endRegion
}

enum LayerEventGeneral {
    LayerAdded = 'layeradded',
    LayerRemoved = 'layerremoved',
}

enum LayerEventMouse {
    Click = 'click',
    ContextMenu = 'contextmenu',
    DblClick = 'dblclick',
    MouseDown = 'mousedown',
    MouseEnter = 'mouseenter',
    MouseLeave = 'mouseleave',
    MouseMove = 'mousemove',
    MouseOut = 'mouseout',
    MouseOver = 'mouseover',
    MouseUp = 'mouseup',
}

enum LayerEventTouch {
    TouchCancel = 'touchcancel',
    TouchEnd = 'touchend',
    TouchMove = 'touchmove',
    TouchStart = 'touchstart',
}

enum LayerEventWheel {
    Wheel = 'wheel',
}