import * as atlas from "azure-maps-control"
import { Helpers } from "./common/Helpers";
import { Logger, LogLevel } from "./common/Logger";

export class ImageSprite {
    public static async createFromTemplate(map: atlas.Map, templateDef: ImageTemplateDef, removeExisting: boolean = false): Promise<boolean> {
        const mapId = Helpers.getMapId(map);

        if (Helpers.isEmptyOrNull(templateDef.id)) {
            Logger.logMessage(mapId, LogLevel.Error, "ImageTemplateDef.id is null or empty.", templateDef);
            return false;
        }

        if (Helpers.isEmptyOrNull(templateDef.templateName)) {
            Logger.logMessage(mapId, LogLevel.Error, "ImageTemplateDef.templateName is null or empty.", templateDef);
            return false;
        }

        if (map.imageSprite.hasImage(templateDef.id)) {
            if (removeExisting) {
                map.imageSprite.remove(templateDef.id);
            }
            else {
                Logger.logMessage(mapId, LogLevel.Error, "Image template already exists.", templateDef);
                return false;
            }
        }

        await map.imageSprite.createFromTemplate(templateDef.id, templateDef.templateName, templateDef.color, templateDef.secondaryColor);
        return true;
    }

    public static hasImage(map: atlas.Map, id: string): boolean {
        return map.imageSprite.hasImage(id);
    }

    static async add(map: atlas.Map, id: string, icon: string | ImageData, meta?: atlas.StyleImageMetadata): Promise<void> {
        await map.imageSprite.add(id, icon, meta);
    }

    static clear(map: atlas.Map) {
        map.imageSprite.clear();
    }

    static getImageIds(map: atlas.Map): string[] {
        return map.imageSprite.getImageIds();
    }

    static remove(map: atlas.Map, id: string) {
        map.imageSprite.remove(id);
    }
}

interface ImageTemplateDef {
    id: string,
    templateName: string,
    color?: string,
    secondaryColor?: string,
    scale?: number
}
