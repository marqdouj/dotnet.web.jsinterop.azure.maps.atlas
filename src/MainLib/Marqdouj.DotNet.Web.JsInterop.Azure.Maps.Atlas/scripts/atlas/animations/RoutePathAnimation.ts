import * as anims from "azure-maps-animations"
import { Animations } from "../Animations";

export class RoutePathAnimation {
    public static getDuration(animation: anims.RoutePathAnimation): number {
        return animation.getDuration();
    }

    public static getOptions(animation: anims.RoutePathAnimation) {
        return animation.getOptions();
    }

    public static getPath(animation: anims.RoutePathAnimation) {
        return animation.getPath();
    }

    public static getTimeSpan(animation: anims.RoutePathAnimation) {
        return animation.getTimeSpan();
    }

    public static setOptions(animation: anims.RoutePathAnimation, options: any): void {
        animation.setOptions(options);
    }

    public static disposeAnimation(animation: anims.RoutePathAnimation): void {
        animation.dispose();
    }

    // #region Playable
    public static isPlaying(animation: anims.RoutePathAnimation): boolean {
        return Animations.doAction(animation, "isPlaying", true);
    }

    public static pause(animation: anims.RoutePathAnimation): void {
        Animations.doAction(animation, "pause");
    }

    public static play(animation: anims.RoutePathAnimation): void {
        Animations.doAction(animation, "play");
    }

    public static reset(animation: anims.RoutePathAnimation): void {
        Animations.doAction(animation, "reset");
    }

    public static stop(animation: anims.RoutePathAnimation): void {
        Animations.doAction(animation, "stop");
    }
    // #endregion

}