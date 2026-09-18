import { EventInfo } from ".";

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

    removeCallback(mapId: string, event: EventInfo) {
        const eventId = this.#getCallbackId(mapId, event);
        if (this.#eventsMap.has(eventId)) {
            this.#eventsMap.delete(eventId);
        }
    }

    #getCallbackId(mapId: string, event: EventInfo) {
        return `${mapId}.${event.target}.${event.type}`;
    }

    clear() {
        this.#eventsMap.clear();
    }
}
