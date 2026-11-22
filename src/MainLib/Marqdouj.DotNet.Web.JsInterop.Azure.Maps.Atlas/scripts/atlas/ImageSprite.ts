import * as atlas from "azure-maps-control"
import { JsHelpers } from "./common/JsHelpers";
import { Logger, LogLevel } from "./common/Logger";
import { MapHelpers } from "./common/MapHelpers";

export class ImageSprite {
    public static async createFromTemplate(map: atlas.Map, templateDef: ImageTemplateDef, removeExisting: boolean = false): Promise<boolean> {
        const mapId = MapHelpers.getMapId(map);

        if (JsHelpers.isEmptyOrNull(templateDef.id)) {
            Logger.logMessage(mapId, LogLevel.Error, "ImageTemplateDef.id is null or empty.", templateDef);
            return false;
        }

        if (JsHelpers.isEmptyOrNull(templateDef.templateName)) {
            Logger.logMessage(mapId, LogLevel.Error, "ImageTemplateDef.templateName is null or empty.", templateDef);
            return false;
        }

        if (removeExisting) {
            this.remove(map, templateDef.id);
        }
        else {
            if (this.hasImage(map, templateDef.id)) {
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

    static async add(map: atlas.Map, id: string, icon: any, meta?: any, removeExisting: boolean = false): Promise<void> {
        if (removeExisting) {
            this.remove(map, id);
        }
        else if (this.hasImage(map, id)) {
            Logger.logMessage(MapHelpers.getMapId(map), LogLevel.Error, `ImageSprint.add: template already exists where id = '${id}'.`);
        }

        meta = JsHelpers.nullToUndefined(meta);
        await map.imageSprite.add(id, icon, meta);
    }

    static clear(map: atlas.Map) {
        map.imageSprite.clear();
    }

    static getImageIds(map: atlas.Map): string[] {
        return map.imageSprite.getImageIds();
    }

    static remove(map: atlas.Map, id: string) {
        if (map.imageSprite.hasImage(id)) {
            map.imageSprite.remove(id);
        }
    }
}

interface ImageTemplateDef {
    id: string,
    templateName: string,
    color?: string,
    secondaryColor?: string,
    scale?: number
}
