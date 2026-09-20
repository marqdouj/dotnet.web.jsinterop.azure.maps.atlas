import atlas from "azure-maps-control";
import { Helpers } from "./common/Helpers";
import { Logger, LogLevel } from "./common/Logger";
import { MapObjectReference } from "./common";
import { MapEventTarget } from "./events";

export class Popups {
    public static add(map: atlas.Map, popups: PopupInfo[], getReferences: boolean = false): MapObjectReference[] {
        const results: MapObjectReference[] = [];
        const mapId = Helpers.getMapId(map);

        if (Helpers.hasNoElements(popups))
            return results;

        popups.forEach(info => {
            let popup = new atlas.Popup(info.options);
            (popup as any).id = info.id

            map.popups.add(popup);
            if (getReferences)
                results.push(Helpers.createMapObjectReference(mapId, MapEventTarget.Popup, popup, info.id));
        });

        return results;
    }

    public static getReferences(map: atlas.Map, sources: PopupInfo[]): MapObjectReference[] {
        const mapId = Helpers.getMapId(map);
        const results: MapObjectReference[] = [];

        if (Helpers.hasNoElements(sources))
            return results;

        const popups = map.popups.getPopups();

        sources.forEach(info => {
            const popup = this.#getPopupFromSource(map, info.id, popups);
            const dnr = Helpers.createMapObjectReference(mapId, MapEventTarget.Popup, popup, info.id);
            results.push(dnr);
        });

        return results;
    }

    public static remove(map: atlas.Map, sources: any[]): void {
        if (Helpers.hasNoElements(sources))
            return;

        const popups = map.popups.getPopups();

        sources.forEach(source => {
            const popup = this.#getPopupFromSource(map, source, popups);
            if (popup) {
                map.popups.remove(popup);
            }
        });
    }

    static show(map: atlas.Map, sources: any[], open: boolean) {
        if (Helpers.hasNoElements(sources))
            return;

        const popups = map.popups.getPopups();

        sources.forEach(source => {
            const popup = this.#getPopupFromSource(map, source, popups);
            
            if (popup) {
                if (open)
                    popup.open();
                else
                    popup.close();
            }
        });
    }

    // #region Hover Popup

    public static addHoverPopup(map: atlas.Map, layerId: string, info: PopupInfo, placeholders: string[], getReference: boolean = false) {
        const mapId = Helpers.getMapId(map);
        const lyr = map.layers.getLayerById(layerId);

        if (!lyr) {
            Logger.logMapMessage(mapId, LogLevel.Error, `addHoverPopup: layer does not exist where id = '${layerId}'.`);
            return;
        }

        let popup = this.#getPopupFromSource(map, info.id);
        if (!popup) {
            this.add(map, [info]);
            popup = this.#getPopupFromSource(map, info.id);
        }

        if (!popup) {
            Logger.logMapMessage(mapId, LogLevel.Error, `addHoverPopup: popup was not created or can't be found where popup id = '${info.id}'.`);
            return;
        }

        //Close the popup when the mouse leaves the shape.
        map.events.add('mouseleave', lyr, () => this.#closeSymbolHovered(popup));

        /**
        * Open the popup on mouse move or touchstart on the symbol layer.
        * Mouse move is used as mouseover only fires when the mouse initially goes over a symbol. 
        * If two symbols overlap, moving the mouse from one to the other won't trigger the event for the new shape as the mouse is still over the layer.
        */
        map.events.add('mousemove', lyr, (e: atlas.MapMouseEvent) => this.#symbolHovered(e, mapId, info, popup, placeholders));
        map.events.add('touchstart', lyr, (e: atlas.MapTouchEvent) => this.#symbolHovered(e, mapId, info, popup, placeholders));

        if (getReference) {
            return Helpers.createMapObjectReference(mapId, MapEventTarget.Popup, popup, info.id);
        }
    }

    static #closeSymbolHovered(popup: atlas.Popup | undefined) {
        if (popup) {
            popup.close();
        }
    }

    static #symbolHovered(e: atlas.MapMouseEvent, mapId: string, info: PopupInfo, popup: atlas.Popup | undefined, placeholders: string[]) {
        if (!popup) {
            Logger.logMapMessage(mapId, LogLevel.Error, `symbolHovered: popup is undefined where popup id = '${info.id}'.`);
            return;
        }

        //Make sure the event occurred on a shape feature.
        if (e.shapes && e.shapes.length > 0) {
            const shape = e.shapes[0];
            const properties = (shape as any).getProperties();

            let content = info.options.content as string ?? "";
            let canShow = false;

            if (Helpers.isEmptyOrNull(content)) {
                const tooltipText = properties.tooltip ?? properties.description;
                if (Helpers.isNotEmptyOrNull(tooltipText)) {
                    content = `<div style="padding:5px;border-radius:6px;background-color:black;color:white">${tooltipText}</div>`;
                    canShow = true;
                }
            }
            else {
                let updated = content;
                if (Helpers.hasStringIndexSignature(properties)) {
                    placeholders.forEach((key) => {
                        const name = `{${key}}`;
                        if (updated.includes(name)) {
                            const value = properties[key];
                            if (Helpers.isNotEmptyOrNull(value))
                                canShow = true;
                            updated = updated.replace(name, value ?? "");
                        }
                    });
                }
                content = updated;
            }

            popup.setOptions({
                content: content,
                position: (shape as any).getCoordinates(),
            });

            //Open the popup.
            if (canShow)
                popup.open();
        }
    }

    // #endRegion

    static #getPopupFromSource(map: atlas.Map, source: any, popups?: atlas.Popup[]): atlas.Popup | undefined {
        const eventName = "Popups.#getPopupFromSource";

        let popup: atlas.Popup | undefined;

        if (source instanceof atlas.Popup) {
            popup = source;
        }
        else {
            const id = Helpers.getSourceId(source);
            popups ??= map.popups.getPopups();
            popup = popups.findLast(item => this.#hasId(item, id));
        }

        if (!popup) {
            const mapId = Helpers.getMapId(map);
            Logger.logMessage(mapId, LogLevel.Warn, `${eventName}: Popup not found.`, source);
        }

        return popup;
    }

    static #hasId(obj: any, id?: string): obj is atlas.Popup {
        return obj instanceof atlas.Popup && (obj as any).id === id;
    }
}

export interface PopupInfo {
    id: string;
    options: atlas.PopupOptions;
}
