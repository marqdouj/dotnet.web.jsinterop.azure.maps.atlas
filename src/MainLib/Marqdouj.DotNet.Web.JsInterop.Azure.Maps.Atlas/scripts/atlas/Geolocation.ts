import { Logger, LogLevel } from "./common/Logger";
import * as events from "./events"

export class Geolocation {
    //https://developer.mozilla.org/en-US/docs/Web/API/Geolocation/getCurrentPosition
    public static async getLocation(options?: PositionOptions): Promise<GeolocationPayload> {
        const result: GeolocationPayload = { type: GeolocationEventType.GetLocation, position: null, error: null };
        const getCurrentPositionPromise = new Promise((resolve, reject) => {
            if (!navigator.geolocation) {
                reject({ code: 0, message: 'This device does not support geolocation.' });
            } else {
                navigator.geolocation.getCurrentPosition(resolve, reject, Geolocation.#buildOptions(options));
            }
        });
        await getCurrentPositionPromise.then(
            (position: unknown) => { result.position = position; }
        ).catch(
            (error: any) => {
                result.error = { code: error.code, message: error.message };
            }
        );
        return result;
    }

    //https://developer.mozilla.org/en-US/docs/Web/API/Geolocation/watchPosition
    public static async watchPosition(dotNetRef: any, options?: PositionOptions, callbackMethod?: string): Promise<number | undefined > {
        if (!navigator.geolocation) return undefined;

        const mapId = "";
        callbackMethod ??= events.MapEventNotify.NotifyMapEvent;

        const id = navigator.geolocation.watchPosition(
            (position: GeolocationPosition) => {
                const payload: GeolocationPayload = { type: GeolocationEventType.WatchSuccess, position: position, error: null };
                const args: events.NotifyMapEventArgs = events.Helpers.buildNotifyEventArgs(mapId, undefined, { geolocation: payload });
                dotNetRef.invokeMethodAsync(callbackMethod, args);
            },
            (error: any) => {
                const payload: GeolocationPayload = { type: GeolocationEventType.WatchError, position: null, error: { code: error.code, message: error.message } };
                const args: events.NotifyMapEventArgs = events.Helpers.buildNotifyEventArgs(mapId, undefined, { geolocation: payload });
                dotNetRef.invokeMethodAsync(callbackMethod, args);
            },
            Geolocation.#buildOptions(options)
        );

        return id;
    }

    //https://developer.mozilla.org/en-US/docs/Web/API/Geolocation/clearWatch
    static clearWatch(watchId: number) {
        if (!navigator.geolocation) return;
        navigator.geolocation.clearWatch(watchId);
    }

    //If options are passed by JsInterop and timeout is null, it usually fails.
    //To workaround this, create an object that has only the values that contain an actual value.
    static #buildOptions(options?: PositionOptions) {
        if (options) {
            const result: PositionOptions = {};

            if (options.enableHighAccuracy) {
                result.enableHighAccuracy = options.enableHighAccuracy;
            }
            if (options.maximumAge) {
                result.maximumAge = options.maximumAge;
            }
            if (options.timeout) {
                result.timeout = options.timeout;
            }

            return result;
        }

        return undefined;
    }
}

enum GeolocationEventType {
    GetLocation = "GetLocation",
    WatchSuccess = "WatchSuccess",
    WatchError = "WatchError"
}

type GeolocationPayload = {
    type: GeolocationEventType;
    position: any;
    error: any;
}