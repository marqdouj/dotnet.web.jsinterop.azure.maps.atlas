import * as atlas from "azure-maps-control"

export enum MapEventTarget {
    Map = "Map",
    Animation = 'Animation',
    DataSource = 'DataSource',
    Marker = 'Marker',
    Layer = 'Layer',
    Popup = 'Popup',
    Shape = 'Shape',
    StyleControl = 'StyleControl',
}

export interface NotifyMapEventArgs {
    mapId: string;
    target?: MapEventTarget;
    targetId?: string;
    type?: string;
    payload?: atlas.Properties | undefined;
}

export enum MapEventNotify {
    NotifyMapEvent = 'NotifyMapEvent',
}

export interface EventInfo {
    target: MapEventTarget;
    source?: any;
    type: string;
    once: boolean;
    preventDefault: boolean;
    eventName?: string;
}
