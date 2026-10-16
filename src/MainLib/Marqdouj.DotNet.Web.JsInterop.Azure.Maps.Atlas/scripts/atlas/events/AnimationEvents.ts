import * as atlas from "azure-maps-control"
import * as anims from "azure-maps-animations"
import { Helpers } from "../common/Helpers";
import * as events from "."
import { EventsMap } from "./EventsMap";
import { Logger, LogLevel } from "../common/Logger";

export class AnimationEvents {
    static readonly #eventsMap: EventsMap = new EventsMap();

    public static add(dotNetRef: any, map: atlas.Map, target: any, mapEvents: AnimationEventInfo[]) {
        const eventName = "AnimationEvents.add";
        const mapId = Helpers.getMapId(map);

        if (!target) {
            Logger.logMapMessage(mapId, LogLevel.Warn, `${eventName}: No target provided where mapId = '${mapId}'.`);
            return;
        }

        if (Helpers.hasNoElements(mapEvents, `${eventName}: No events provided where mapId = '${mapId}'.`)) {
            return;
        }

        mapEvents.forEach((me) => {
            me.eventName ??= events.MapEventNotify.NotifyMapEvent;
            let callback: any;
            const type = me.type?.toLowerCase();

            if (Helpers.isValueInEnum(AnimationEventGeneral, type)) {
                switch (type as AnimationEventGeneral) {
                    case AnimationEventGeneral.OnComplete:
                    case AnimationEventGeneral.OnProgress:
                        switch (me.payloadType) {
                            case AnimationEventPayloadType.RoutePath:
                                callback = this.#getRoutePathCallback(dotNetRef, mapId, me);
                                break;
                            default:
                                callback = this.#getPlayableCallback(dotNetRef, mapId, me);
                        }
                        break;
                    case AnimationEventGeneral.OnFrame:
                        switch (me.payloadType) {
                            case AnimationEventPayloadType.FrameBased:
                                callback = this.#getNotifyFrameBasedCallback(dotNetRef, mapId, me);
                                break;
                            default:
                        }
                        break;
                }

                if (callback) {
                    if (me.once) {
                        map.events.addOnce(type as any, target as any, callback);
                    }
                    else {
                        map.events.add(type as any, target as any, callback);
                    }
                }
                else {
                    Logger.logMapMessage(mapId, LogLevel.Warn, `${eventName}: Unable to create callback for event type '${me.type}' where mapId = '${mapId}'.`, me);
                }
            }
            else {
                Logger.logMapMessage(mapId, LogLevel.Warn, `${eventName}: Event type '${me.type}' is not supported where mapId = '${mapId}'.`, me);
            }
        });
    }

    public static remove(map: atlas.Map, target: any, mapEvents: events.EventInfo[]) {
        const eventName = "AnimationEvents.remove";
        const mapId = Helpers.getMapId(map);

        if (Helpers.hasNoElements(mapEvents, `${eventName}: No events provided where mapId = '${mapId}'.`)) {
            return;
        }

        if (!target) {
            Logger.logMapMessage(mapId, LogLevel.Warn, `${eventName}: No target provided where mapId = '${mapId}'.`);
            return;
        }

        mapEvents.forEach((me) => {
            const callback: any = this.#eventsMap.getCallback(mapId, me);

            if (callback) {
                map.events.remove(me.type, target, callback);
                this.#eventsMap.removeCallback(mapId, me);
            }
            else {
                Logger.logMapMessage(mapId, LogLevel.Warn, `${eventName}: Callback not found.`, me);
            }
        });
    }

    // #region PlayableCallback

    static #getPlayableCallback(dotNetRef: any, mapId: string, event: AnimationEventInfo) {
        let callback: any = this.#eventsMap.getCallback(mapId, event);

        if (callback) {
            return callback;
        }

        callback = (e: anims.PlayableAnimationEvent) => this.#notifyPlayable(e, dotNetRef, mapId, event);

        this.#eventsMap.addCallback(mapId, event, callback);

        return callback;
    }

    static #notifyPlayable = (callback: anims.PlayableAnimationEvent, dotNetRef: any, mapId: string, mapEvent: AnimationEventInfo) => {
        const eventPayload: PlayableEventPayload = {
            type: mapEvent.type,
            progress: callback.progress,
            easingProgress: callback.easingProgress,
            position: callback.position,
            heading: callback.heading,
            animation: mapEvent.includeJsReference ? DotNet.createJSObjectReference(callback.animation) : undefined
        };
        const payload: AnimationEventPayload = { type: AnimationEventPayloadType.Playable, playable: eventPayload };
        const args = events.Helpers.buildNotifyEventArgs(mapId, mapEvent, payload);
        dotNetRef.invokeMethodAsync(mapEvent.eventName, args);
    };

    // #endRegion

    // #region FrameBasedCallback

    static #getNotifyFrameBasedCallback(dotNetRef: any, mapId: string, event: AnimationEventInfo) {
        let callback: any = this.#eventsMap.getCallback(mapId, event);

        if (callback) {
            return callback;
        }

        callback = (e: anims.FrameBasedAnimationEvent) => this.#notifyFrameBased(e, dotNetRef, mapId, event);

        this.#eventsMap.addCallback(mapId, event, callback);

        return callback;
    }

    static #notifyFrameBased = (callback: anims.FrameBasedAnimationEvent, dotNetRef: any, mapId: string, mapEvent: AnimationEventInfo) => {
        const eventPayload: FrameBasedEventPayload = {
            type: mapEvent.type,
            frameIdx: callback.frameIdx,
            numFrames: callback.numFrames,
            animation: mapEvent.includeJsReference ? DotNet.createJSObjectReference(callback.animation) : undefined
        };
        const payload: AnimationEventPayload = { type: AnimationEventPayloadType.FrameBased, frameBased: eventPayload };
        const args = events.Helpers.buildNotifyEventArgs(mapId, mapEvent, payload);
        dotNetRef.invokeMethodAsync(mapEvent.eventName, args);
    };

    // #endRegion

    // #region RoutePathCallback

    static #getRoutePathCallback(dotNetRef: any, mapId: string, event: AnimationEventInfo) {
        let callback: any = this.#eventsMap.getCallback(mapId, event);

        if (callback) {
            return callback;
        }

        callback = (e: anims.RoutePathAnimationEvent) => this.#notifyRoutePath(e, dotNetRef, mapId, event);

        this.#eventsMap.addCallback(mapId, event, callback);

        return callback;
    }

    static #notifyRoutePath = (callback: anims.RoutePathAnimationEvent, dotNetRef: any, mapId: string, mapEvent: AnimationEventInfo) => {
        var ts = (callback as any).timestamp;
        var sp = (callback as any).speed;
        const eventPayload: RoutePathEventPayload = {
            type: mapEvent.type,
            progress: callback.progress,
            easingProgress: callback.easingProgress,
            position: callback.position,
            heading: callback.heading,
            animation: mapEvent.includeJsReference ? DotNet.createJSObjectReference(callback.animation) : undefined,
            timestamp: ts,
            timestampUTC: ts != null ? new Date(ts).toUTCString() : undefined,
            speed: sp,
            speedInKph: sp != null ? Math.round(atlas.math.convertSpeed(sp, atlas.math.SpeedUnits.metersPerSecond, atlas.math.SpeedUnits.kilometersPerHour) * 100) / 100 : undefined,
            speedInMph: sp != null ? Math.round(atlas.math.convertSpeed(sp, atlas.math.SpeedUnits.metersPerSecond, atlas.math.SpeedUnits.kilometersPerHour) * 100) / 100 : undefined,
        };
        const payload: AnimationEventPayload = { type: AnimationEventPayloadType.RoutePath, routePath: eventPayload };
        const args = events.Helpers.buildNotifyEventArgs(mapId, mapEvent, payload);
        dotNetRef.invokeMethodAsync(mapEvent.eventName, args);
    };

    // #endRegion
}

export interface AnimationEventInfo extends events.EventInfo {
    includeJsReference: boolean;
    payloadType: AnimationEventPayloadType;
}

enum AnimationEventGeneral {
    OnComplete = 'oncomplete',
    OnFrame = 'onframe',
    OnProgress = 'onprogress',
}

interface FrameBasedEventPayload {
    type: string;
    frameIdx?: number;
    numFrames?: number;
    animation?: any;
}

interface PlayableEventPayload {
    type: string;
    progress: number;
    easingProgress: number;
    position?: atlas.data.Position;
    heading?: number;
    animation?: any;
}

interface RoutePathEventPayload extends PlayableEventPayload {
    speed?: number;
    speedInKph?: number;
    speedInMph?: number;
    timestamp?: number;
    timestampUTC?: string;
}

enum AnimationEventPayloadType {
    Playable = 'playable',
    FrameBased = 'frameBased',
    RoutePath = 'routePath',
}

interface AnimationEventPayload {
    type: AnimationEventPayloadType;
    frameBased?: FrameBasedEventPayload;
    playable?: PlayableEventPayload;
    routePath?: RoutePathEventPayload;
}
