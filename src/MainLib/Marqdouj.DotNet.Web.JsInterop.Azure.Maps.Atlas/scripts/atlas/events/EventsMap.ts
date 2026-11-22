import { EventInfo, MapEventTarget } from ".";
import { MapHelpers } from "../common/MapHelpers";
import { EventHelpers } from "./EventHelpers";

export class EventsMap {
    #eventsMap: Map<string, object> = new Map<string, object>();

    addCallback(mapId: string, event: EventInfo, callback: any) {
        const eventId = this.#getCallbackId(mapId, event);
        if (!this.#eventsMap.has(eventId)) {
            this.#eventsMap.set(eventId, callback);
        }
    }

    getCallback(mapId: string, event: EventInfo) {
        const eventId = this.#getCallbackId(mapId, event);
        if (this.#eventsMap.has(eventId)) {
            return this.#eventsMap.get(eventId);
        }
    }

    getCallbacksBySource(mapId: string, target: MapEventTarget, source: object) {
        const eventId = this.#getCallbackSourceId(mapId, target, source);

        const callbacks: CallbackInfo[] = [];
        for (const [key, value] of this.#eventsMap.entries()) {
            if (key.startsWith(eventId)) {
                callbacks.push(new CallbackInfo(key, value));
            }
        }
        return callbacks;
    }

    removeCallback(mapId: string, event: EventInfo) : boolean {
        return this.removeCallbackById(this.#getCallbackId(mapId, event));
    }

    removeCallbackById(eventId: string): boolean {
        if (this.#eventsMap.has(eventId)) {
            this.#eventsMap.delete(eventId);
            return true;
        }

        return false;
    }

    #getCallbackId(mapId: string, event: EventInfo) {
        return `${mapId}.${event.target}.${EventHelpers.getEventInfoSourceId(event)}.${event.type}`;
    }

    #getCallbackSourceId(mapId: string, target: MapEventTarget, source: any) {
        return `${mapId}.${target}.${MapHelpers.getSourceId(source)}.`;
    }

    clear() {
        this.#eventsMap.clear();
    }
}

export class CallbackInfo {
    eventId: string;
    mapId: string;
    target: string;
    source: string;
    type: string;
    callback: any;
    constructor(eventId: string, callback: any) {
        this.eventId = eventId;
        const [mapId, target, source, type] = eventId.split(".");
        this.mapId = mapId;
        this.target = target;
        this.source = source;
        this.type = type;
        this.callback = callback;
    }
}