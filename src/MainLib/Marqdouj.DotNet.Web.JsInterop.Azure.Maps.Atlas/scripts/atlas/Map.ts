import * as atlas from "azure-maps-control"
import { Helpers } from "./common/Helpers";
import { EditAction } from "./common";

export class Map {
    // #region Camera
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
    // #endregion

    // #region Service
    public static getServiceOptions(map: atlas.Map): any {
        const options = map.getServiceOptions();
        return options;
    }

    public static setServiceOptions(map: atlas.Map, options: atlas.ServiceOptions | undefined, action: EditAction): void {
        if (!options) return;

        let updatedOptions = options;

        switch (action) {
            case EditAction.Replace:
                break;
            case EditAction.Update:
                updatedOptions = Helpers.removeNullish(updatedOptions);
                break;
        }

        map.setServiceOptions(updatedOptions);
    }
    // #endregion

    // #region Style
    public static getStyle(map: atlas.Map): any {
        const options = map.getStyle();

        return options;
    }

    public static setStyle(map: atlas.Map, options: atlas.StyleOptions | undefined, action: EditAction): void {
        if (!options) return;

        let updatedOptions = options;

        switch (action) {
            case EditAction.Replace:
                break;
            case EditAction.Update:
                updatedOptions = Helpers.removeNullish(updatedOptions);
                break;
        }

        const diff = action === EditAction.Update;
        map.setStyle(updatedOptions, diff);
    }
    // #endregion

    // #region Traffic
    public static getTraffic(map: atlas.Map): atlas.TrafficOptions | undefined {
        const options = map.getTraffic();
        return options;
    }

    public static setTraffic(map: atlas.Map, options: atlas.TrafficOptions | undefined, action: EditAction): void {
        if (!options) return;

        let updatedOptions = options;

        switch (action) {
            case EditAction.Replace:
                break;
            case EditAction.Update:
                updatedOptions = Helpers.removeNullish(updatedOptions);
                break;
        }

        map.setTraffic(updatedOptions);
    }
    // #endregion

    // #region UserInteraction
    public static getUserInteraction(map: atlas.Map): any {
        const options = map.getUserInteraction();
        return options;
    }

    public static setUserInteraction(map: atlas.Map, options: atlas.UserInteractionOptions | undefined, action: EditAction): void {
        if (!options) return;

        let updatedOptions = options;

        switch (action) {
            case EditAction.Replace:
                break;
            case EditAction.Update:
                updatedOptions = Helpers.removeNullish(updatedOptions);
                break;
        }

        map.setUserInteraction(updatedOptions);
    }
    // #endregion

    // #region View

    public static getVersion(): string {
        return atlas.getVersion();
    }

    public static setLanguage(language: string) {
        atlas.setLanguage(language);
    }

    public static setView(view: string) {
        atlas.setView(view);
    }

    // #endregion
}