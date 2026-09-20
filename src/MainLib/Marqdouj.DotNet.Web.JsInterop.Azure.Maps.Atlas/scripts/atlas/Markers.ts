import atlas from "azure-maps-control";
import { MarkerEvents } from "../atlas";
import { Helpers } from "./common/Helpers";

export class Markers {
    public static add(map: atlas.Map, markers?: HtmlMarker[]): void {
        markers ??= [];

        markers.forEach(markerDef => {
            let options = { ...(markerDef as any).options };
            if (options.popup) {
                options.popup = new atlas.Popup(options.popup.options)
            }
            let marker = new atlas.HtmlMarker(options);
            (marker as any).id = markerDef.id;

            map.markers.add(marker);

            if (markerDef.togglePopupOnClick) {
                map.events.add('click', marker, () => {
                    marker.togglePopup();
                });
            }
        });
    }

    public static remove(map: atlas.Map, markers: HtmlMarker[]): void {
        markers ??= [];

        const sourceIds = markers.map(e => e.id);
        MarkerEvents.removeBySource(map, sourceIds)

        markers.forEach(markerDef => {
            let marker = Markers.#doGetMarker(map, markerDef.id);
            if (marker) {
                map.markers.remove(marker);
            }
        });
    }

    public static clear(map: atlas.Map) {
        map.markers.clear();
    }

    static getMarker(map: atlas.Map, id: string | undefined): atlas.HtmlMarker | undefined {
        return Markers.#doGetMarker(map, id);
    }

    static #doGetMarker(map: atlas.Map, id: string | undefined): atlas.HtmlMarker | undefined {
        if (!id)
            return;

        const markers = map.markers.getMarkers();
        const marker = markers.findLast(value => Helpers.hasId(value, id));

        return marker;
    }
}

interface HtmlMarker {
    options: atlas.HtmlMarkerOptions;
    togglePopupOnClick: boolean;
    id: string;
}
