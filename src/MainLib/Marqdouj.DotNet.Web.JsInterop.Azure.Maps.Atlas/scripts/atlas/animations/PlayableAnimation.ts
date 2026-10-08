import * as anims from "azure-maps-animations"

export class PlayableAnimation {
    public static getDuration(animation: anims.IPlayableAnimation): number {
        return animation.getDuration();
    }

    public static getOptions(animation: any) {
        return animation.getOptions();
    }

    public static isPlaying(animation: anims.IPlayableAnimation): boolean {
        return animation.isPlaying();
    }

    public static pause(animation: anims.IPlayableAnimation): void {
        animation.pause();
    }

    public static play(animation: anims.IPlayableAnimation): void {
        animation.play();
    }

    public static reset(animation: anims.IPlayableAnimation): void {
        animation.reset();
    }

    public static setOptions(animation: any, options: any): void {
        animation.setOptions(options);
    }

    public static stop(animation: anims.IPlayableAnimation): void {
        animation.stop();
    }

    public static disposeAnimation(animation: anims.IPlayableAnimation): void {
        animation.dispose();
    }
}
