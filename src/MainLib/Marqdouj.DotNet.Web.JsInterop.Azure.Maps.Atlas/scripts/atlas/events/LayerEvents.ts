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
            const target = this.#getTarget(map, me);

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
                        map.events.addOnce(me.type as any, target as atlas.layer.Layer, callback);
                    }
                    else {
                        map.events.add(me.type as any, target as atlas.layer.Layer, callback);
                    }
                } 
                else {
                    Logger.logMapMessage(mapId, LogLevel.Warn, `${eventName}: Callback not found.`, me);
                }
            }
            else {
                Logger.logMapMessage(mapId, LogLevel.Warn, `${eventName}: Target not found.`, me);
            }
        });
    }

    public static remove(map: atlas.Map, mapEvents: events.EventInfo[]) {
        const eventName = "LayerEvents.remove";
        const mapId = Helpers.getMapId(map);

        if (Helpers.hasNoElements(mapEvents))
            return;

        mapEvents.forEach((me) => {
            const callback: any = this.#eventsMap.getCallback(mapId, me);

            if (callback) {
                const target = this.#getTarget(map, me);

                if (target) {
                    map.events.remove(me.type, target as atlas.layer.Layer, callback);
                    this.#eventsMap.removeCallback(mapId, me);
                }
                else {
                    Logger.logMapMessage(mapId, LogLevel.Warn, `${eventName}: Target not found.`, me);
                }
            }
            else {
                Logger.logMapMessage(mapId, LogLevel.Warn, `${eventName}: Callback not found.`, me);
            }
        });
    }

    public static removeByLayer(map: atlas.Map, layers: any[]) {
        const eventName = "LayerEvents.removeBySource";
        const mapId = Helpers.getMapId(map);

        if (Helpers.hasNoElements(layers))
            return;

        layers.forEach((source) => {
            const target = this.#getTargetFromSource(map, source);

            if (target) {
                const callbacks = this.#eventsMap.getCallbacksBySource(mapId, events.MapEventTarget.Layer, source);
                callbacks.forEach((cb) => {
                    map.events.remove(cb.type, target as atlas.layer.Layer, cb.callback);
                    this.#eventsMap.removeCallbackById(cb.eventId);
                });
            }
            else {
                Logger.logMapMessage(mapId, LogLevel.Warn, `${eventName}: target not found.`, source);
            }
        });
    }

    static #getTarget(map: atlas.Map, mapEvent: events.EventInfo): atlas.layer.Layer | undefined {
        return this.#getTargetFromSource(map, mapEvent.source);
    }

    static #getTargetFromSource(map: atlas.Map, source: any): atlas.layer.Layer | undefined {
        let target: atlas.layer.Layer | undefined;

        if (source) {
            if (typeof source === 'string') {
                target = map.layers.getLayerById(source);
            }
            else if (source instanceof atlas.layer.Layer) {
                target = source;
            }
        }

        return target;
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

export const LayerEventType = {
    ...LayerEventGeneral,
    ...LayerEventMouse,
    ...LayerEventTouch,
    ...LayerEventWheel
};

export type LayerEventType = typeof LayerEventType;