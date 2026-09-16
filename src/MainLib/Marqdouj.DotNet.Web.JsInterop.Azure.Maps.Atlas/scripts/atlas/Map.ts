import * as atlas from "azure-maps-control"
import { Logger, LogLevel } from "../common/Logger"
import { Helpers } from "../common/Helpers";

export class Map {
    public static getCamera(map: atlas.Map) {
        return map.getCamera();
    }

    public static setCamera(map: atlas.Map, cameraOptions?: any, cameraBoundsOptions?: any, animationOptions?: any) {
        let options: (atlas.CameraOptions | (atlas.CameraBoundsOptions & { pitch?: number, bearing?: number })) & atlas.AnimationOptions = {};

        cameraOptions = Helpers.removeNullish(cameraOptions) ?? {};
        cameraBoundsOptions = Helpers.removeNullish(cameraBoundsOptions) ?? {};
        animationOptions = Helpers.removeNullish(animationOptions) ?? {};

        //cameraOptions and cameraBoundsOptions are mutually exclusive.
        if (Helpers.isObjectNotEmptyOrNull(cameraOptions)) {
            options = { ...options, ...cameraOptions };
        }
        else if (Helpers.isObjectNotEmptyOrNull(cameraBoundsOptions)) {
            options = { ...options, ...cameraBoundsOptions };
        }

        if (Helpers.isObjectNotEmptyOrNull(animationOptions)) {
            options = { ...options, ...animationOptions };
        }

        options = Helpers.removeNullish(options);

        if (Helpers.isObjectNotEmptyOrNull(options)) {
            map.setCamera(options);
        }
    }
}