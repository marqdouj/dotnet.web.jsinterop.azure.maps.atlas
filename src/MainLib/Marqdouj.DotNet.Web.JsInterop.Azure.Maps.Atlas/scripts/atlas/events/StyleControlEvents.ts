import * as atlas from "azure-maps-control"
import { Helpers } from "../common/Helpers";
import * as events from "."
import { EventsMap } from "./EventsMap";
import { Logger, LogLevel } from "../common/Logger";

export class StyleControlEvents {
    static readonly #eventsMap: EventsMap = new EventsMap();

    public static add(dotNetRef: any, map: atlas.Map, styleControlEvents?: events.EventInfo[]) {
        const eventName = "StyleControlEvents.add";
        const mapId = Helpers.getMapId(map);
        styleControlEvents ??= [];

        styleControlEvents.forEach((me) => {
            me.eventName ??= events.MapEventNotify.NotifyMapEvent;
            let callback: any;
            const target = this.#getTarget(map, me);

            if (target) {
                if (Helpers.isValueInEnum(StyleControlEventGeneral, me.type)) {
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

    public static remove(map: atlas.Map, mapEvents: events.EventInfo[]) {
        const eventName = "StyleControlEvents.remove";
        const mapId = Helpers.getMapId(map);
        mapEvents ?? [];

        mapEvents.forEach((me) => {
            const callback: any = this.#eventsMap.getCallback(mapId, me);

            if (callback) {
                const target = this.#getTarget(map, me);

                if (target) {
                    map.events.remove(me.type, target as atlas.control.StyleControl, callback);
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
        const eventName = "StyleControlEvents.removeBySource";
        const mapId = Helpers.getMapId(map);

        sources ??= [];
        sources.forEach((source) => {
            const target = this.#getTargetFromSource(map, source);

            if (target) {
                const callbacks = this.#eventsMap.getCallbacksBySource(mapId, events.MapEventTarget.Marker, source);
                callbacks.forEach((cb) => {
                    map.events.remove(cb.type, target as atlas.control.StyleControl, cb.callback);
                    this.#eventsMap.removeCallbackById(cb.eventId);
                });
            }
            else {
                Logger.logMapMessage(mapId, LogLevel.Warn, `${eventName}: target not found.`, source);
            }
        });
    }

    static #getTarget(map: atlas.Map, mapEvent: events.EventInfo): atlas.control.StyleControl | undefined {
        return this.#getTargetFromSource(map, mapEvent.source);
    }

    static #getTargetFromSource(map: atlas.Map, source: any): atlas.control.StyleControl | undefined {
        let target: atlas.control.StyleControl | undefined;

        if (source) {
            if (typeof source === 'string') {
                target = this.#getControl(map, source);
            }
            else if (source instanceof atlas.control.StyleControl) {
                target = source;
            }
        }

        return target;
    }

    static #getControl(map: atlas.Map, id: string | undefined): atlas.control.StyleControl | undefined {
        return this.#doGetControl(map, id);
    }

    static #doGetControl(map: atlas.Map, id: string | undefined): atlas.control.StyleControl | undefined {
        if (!id)
            return;

        const controls = map.controls.getControls();
        const control = controls.findLast(value => this.#hasId(value, id));

        return control;
    }

    static #hasId(obj: any, id: string): obj is atlas.control.StyleControl {
        return obj instanceof atlas.control.StyleControl && (obj as any).id === id;
    }

    // #region General

    static #getNotifyEventGeneralCallback(dotNetRef: any, mapId: string, event: events.EventInfo) {
        let callback: any = this.#eventsMap.getCallback(mapId, event);

        if (callback) {
            return callback;
        }

        callback = (e: string) => this.#notifyEventGeneral(e, dotNetRef, mapId, event);

        this.#eventsMap.addCallback(mapId, event, callback);

        return callback;
    }

    static #notifyEventGeneral = (callback: string, dotNetRef: any, mapId: string, mapEvent: events.EventInfo) => {
        const payload = { styleControl: { style: callback } };
        const args = events.Helpers.buildNotifyEventArgs(mapId, mapEvent, payload);
        dotNetRef.invokeMethodAsync(mapEvent.eventName, args);
    };

    // #endRegion
}

enum StyleControlEventGeneral {
    StyleSelected = 'styleselected',
}