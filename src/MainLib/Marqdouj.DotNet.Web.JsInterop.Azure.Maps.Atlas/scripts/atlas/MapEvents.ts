import * as atlas from "azure-maps-control"
import { Helpers } from "./common/Helpers";
import * as events from "./events"
import { Logger, LogLevel } from "./common/Logger";
import { EventsMap } from "./events/EventsMap";

export class MapEvents {
    static readonly #eventsMap: EventsMap = new EventsMap ();

    public static add(dotNetRef: any, map: atlas.Map, mapEvents?: events.EventInfo[]) {
        const mapId = Helpers.getMapId(map);
        mapEvents ??= [];

        mapEvents.forEach((me) => {
            me.eventName ??= events.MapEventNotify.NotifyMapEvent;
            let callback = this.#getNotifyMapEventMouseCallback(dotNetRef, mapId, me);

            if (Helpers.isValueInEnum(MapEventMouse, me.type)) {
                if (me.once) {
                    map.events.addOnce(me.type as MapEventMouse, callback);
                }
                else {
                    map.events.add(me.type as MapEventMouse, callback);
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

    // #region Callbacks

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
        const args: events.NotifyMapEventArgs = { mapId: mapId, target: events.MapEventTarget.Map, type: mapEvent.type as any, payload: payload };
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

