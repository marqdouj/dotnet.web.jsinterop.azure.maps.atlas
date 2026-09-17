import * as atlas from "azure-maps-control"
import { Helpers } from "./common/Helpers";
import * as events from "./events"

export class MapEvents {
    public static add(dotNetRef: any, map: atlas.Map, mapEvents?: MapEvent[]) {
        const mapId = Helpers.getMapId(map);
        mapEvents ??= [];

        mapEvents.forEach((me) => {
            me.eventName ??= events.MapEventNotify.NotifyMapEvent;

            if (Helpers.isValueInEnum(MapEventMouse, me.type as any)) {
                if (me.once) {
                    map.events.addOnce(me.type as unknown as MapEventMouse, (e) => this.#notifyMapEventMouse(e, dotNetRef, mapId, me));
                }
                else {
                    map.events.add(me.type as unknown as MapEventMouse, (e) => this.#notifyMapEventMouse(e, dotNetRef, mapId, me));
                }
            }
        });
    }

    public static remove(map: atlas.Map, mapEvents: MapEvent[]) {
        mapEvents ?? [];

        mapEvents.forEach((me) => {
            if (Helpers.isValueInEnum(MapEventMouse, me.type as any)) {
                map.events.remove(me.type as any, () => this.#notifyMapEventMouse);
            }
        });    
    }

    // #region Callbacks

    static #notifyMapEventMouse = (callback: atlas.MapMouseEvent, dotNetRef: any, mapId: string, mapEvent: MapEvent) => {
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

export interface MapEvent {
    type: MapEventType;
    once: boolean;
    preventDefault: boolean;
    eventName?: string;
}
