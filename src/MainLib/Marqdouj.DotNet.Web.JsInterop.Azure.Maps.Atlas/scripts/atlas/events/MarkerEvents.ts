import * as atlas from "azure-maps-control"
import { JsHelpers } from "../common/JsHelpers";
import * as events from "."
import { EventsMap } from "./EventsMap";
import { Logger, LogLevel } from "../common/Logger";
import { Markers } from "../Markers";
import { MapHelpers } from "../common/MapHelpers";
import { EventHelpers } from "./EventHelpers";

export class MarkerEvents {
    static readonly #eventsMap: EventsMap = new EventsMap();

    public static add(dotNetRef: any, map: atlas.Map, markerEvents?: events.EventInfo[]) {
        const eventName = "MarkerEvents.add";
        const mapId = MapHelpers.getMapId(map);

        if (JsHelpers.hasNoElements(markerEvents))
            return;

        markerEvents!.forEach((me) => {
            me.eventName ??= events.MapEventNotify.NotifyMapEvent;
            let callback: any;
            const target = this.#getTarget(map, me);

            if (target) {
                if (JsHelpers.isValueInEnum(MarkerEventGeneral, me.type)) {
                    callback = this.#getNotifyEventGeneralCallback(dotNetRef, mapId, me);
                }

                if (callback) {
                    if (me.once) {
                        map.events.addOnce(me.type as any, target, callback);
                    }
                    else {
                        map.events.add(me.type as any, target, callback);
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

    public static remove(map: atlas.Map, markerEvents: events.EventInfo[]) {
        const eventName = "MarkerEvents.remove";
        const mapId = MapHelpers.getMapId(map);

        if (JsHelpers.hasNoElements(markerEvents))
            return;

        markerEvents.forEach((me) => {
            const callback: any = this.#eventsMap.getCallback(mapId, me);

            if (callback) {
                const target = this.#getTarget(map, me);

                if (target) {
                    map.events.remove(me.type, target as atlas.HtmlMarker, callback);
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

    public static removeBySource(map: atlas.Map, sources: any[]) {
        const eventName = "MarkerEvents.removeBySource";
        const mapId = MapHelpers.getMapId(map);

        if (JsHelpers.hasNoElements(sources))
            return;

        sources.forEach((source) => {
            const target = this.#getTargetFromSource(map, source);

            if (target) {
                const callbacks = this.#eventsMap.getCallbacksBySource(mapId, events.MapEventTarget.Marker, source);
                callbacks.forEach((cb) => {
                    map.events.remove(cb.type, target as atlas.HtmlMarker, cb.callback);
                    this.#eventsMap.removeCallbackById(cb.eventId);
                });
            }
            else {
                Logger.logMapMessage(mapId, LogLevel.Warn, `${eventName}: target not found.`, source);
            }
        });
    }

    static #getTarget(map: atlas.Map, mapEvent: events.EventInfo): atlas.HtmlMarker | undefined {
        return this.#getTargetFromSource(map, mapEvent.source);
    }

    static #getTargetFromSource(map: atlas.Map, source: any): atlas.HtmlMarker | undefined {
        let target: atlas.HtmlMarker | undefined;

        if (source) {
            if (typeof source === 'string') {
                target = Markers.getMarker(map, source);
            }
            else if (source instanceof atlas.HtmlMarker) {
                target = source;
            }
        }

        return target;
    }

    // #region LayerEventGeneral

    static #getNotifyEventGeneralCallback(dotNetRef: any, mapId: string, event: events.EventInfo) {
        let callback: any = this.#eventsMap.getCallback(mapId, event);

        if (callback) {
            return callback;
        }

        callback = (e: atlas.TargetedEvent) => this.#notifyEventGeneral(e, dotNetRef, mapId, event);

        this.#eventsMap.addCallback(mapId, event, callback);

        return callback;
    }

    static #notifyEventGeneral = (callback: atlas.TargetedEvent, dotNetRef: any, mapId: string, mapEvent: events.EventInfo) => {
        const args = EventHelpers.buildNotifyEventArgs(mapId, mapEvent);
        dotNetRef.invokeMethodAsync(mapEvent.eventName ??= callback.type, args);
    };

    // #endRegion
}

enum MarkerEventGeneral {
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

    Drag = 'drag',
    DragEnd = 'dragend',
    DragStart = 'dragstart',
}