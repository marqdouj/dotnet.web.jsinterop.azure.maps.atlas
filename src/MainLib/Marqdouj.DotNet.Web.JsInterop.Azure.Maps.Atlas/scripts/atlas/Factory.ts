import * as atlas from "azure-maps-control"
import { Helpers } from "../common/Helpers"
import { Controls, MapControl } from "./Controls"
import { Logger, LogLevel } from "../common/Logger"

export class Factory {
    public static setLogLevel(logLevel: LogLevel) {
        if (Logger.currentLevel === logLevel) return;

        const previousLevel = Logger.currentLevel;
        Logger.currentLevel = logLevel;
        Logger.logMessage("Setting LogLevel", LogLevel.Information, `LogLevel was [${LogLevel[previousLevel]}]. LogLevel set to [${LogLevel[Logger.currentLevel]}].`);
    }

    public static createMap(dotNetRef: any, mapId: string, config: MapConfiguration, mapControls?: MapControl[]) {
        const eventNames: CreateMapEventNames = (config as any).eventNames as CreateMapEventNames ?? {};

        if (Helpers.isEmptyOrNull(eventNames.error)) {
            eventNames.error = MapEventNotify.NotifyMapEvent;
        }
        if (Helpers.isEmptyOrNull(eventNames.ready)) {
            eventNames.ready = MapEventNotify.NotifyMapEvent;
        }

        const options = this.#buildOptions(config);
        const azmap = new atlas.Map(mapId, options);

        azmap.events.addOnce(MapEventType.Ready, event => {
            azmap.events.add(MapEventType.Error, event => {
                const error: JsError = { name: event.error.name, message: event.error.message, stack: event.error.stack, cause: event.error.cause?.toString() }
                const payload: atlas.Properties = { error: error };
                const errorArgs: MapEventArgs = { mapId: mapId, target: MapEventTarget.Map, type: event.type, payload: payload };
                dotNetRef.invokeMethodAsync(eventNames.error, errorArgs);
            });

            Controls.add(azmap, mapControls);

            const readyArgs: MapEventArgs = { mapId: mapId, target: MapEventTarget.Map, type: event.type };
            dotNetRef.invokeMethodAsync(eventNames.ready, readyArgs);
        });

        return azmap;
    }

    public static removeMap(map?: atlas.Map) {
        map?.dispose();
    }

    static #buildOptions(config: MapConfiguration): CreateMapOptions {
        let options: CreateMapOptions = {};

        const mapOptions = Helpers.nullToUndefined(config.mapOptions);
        const authOptions = Helpers.nullToUndefined(config.authOptions);

        if (mapOptions) {
            //Camera and CameraBounds are mutually exclusive
            if (mapOptions.camera) {
                options = { ...options, ...mapOptions.camera };
            }
            else if (mapOptions.cameraBounds) {
                options = { ...options, ...mapOptions.cameraBounds };
            }

            if (mapOptions.service) {
                options = { ...options, ...mapOptions.service };
            }

            if (mapOptions.style) {
                options = { ...options, ...mapOptions.style };
            }
            if (mapOptions.userInteraction) {
                options = { ...options, ...mapOptions.userInteraction };
            }
        }

        const sasTokenUrl = (authOptions as any).sasTokenUrl;
        const tokenInfo: TokenInfo = (authOptions as any).tokenInfo;

        if (Helpers.isNotEmptyOrNull(sasTokenUrl)) {
            authOptions.getToken = function (resolve) {
                fetch(sasTokenUrl).then(r => r.text()).then(token => resolve(token));
            }
        }
        else if (tokenInfo) {
            authOptions.authType = tokenInfo.authType;
            authOptions.getToken = (resolve) => {
                DotNet.invokeMethodAsync(tokenInfo.id, tokenInfo.identifier)
                    .then(function (response: any) {
                        return response;
                    }).then(function (token: any) {
                        resolve(token);
                    });
            };
        }

        options.authOptions = authOptions;
        return options;
    }
}

interface CreateMapEventNames {
    ready?: string;
    error?: string;
}
interface TokenInfo {
    id: string;
    identifier: string;
    authType: atlas.AuthenticationType;
}

interface MapOptions {
    camera?: atlas.CameraOptions;
    cameraBounds?: atlas.CameraBoundsOptions;
    service?: atlas.ServiceOptions;
    style?: atlas.StyleOptions;
    traffic?: atlas.TrafficOptions;
    userInteraction?: atlas.UserInteractionOptions;
}

interface MapConfiguration {
    authOptions: atlas.AuthenticationOptions;
    mapOptions: MapOptions;
}

type CreateMapOptions = atlas.ServiceOptions & atlas.StyleOptions & atlas.UserInteractionOptions & (atlas.CameraOptions | atlas.CameraBoundsOptions);

interface JsError {
    name: string;
    message?: string;
    stack?: string;
    cause?: string;
}

interface MapEventArgs {
    mapId: string;
    target: MapEventTarget;
    type: string;
    payload?: atlas.Properties | undefined;
}

enum MapEventNotify {
    NotifyMapEvent = 'NotifyMapEvent',
}

enum MapEventTarget {
    Map = "map",
}

enum MapEventType {
    Error = 'error',
    Ready = 'ready',
}