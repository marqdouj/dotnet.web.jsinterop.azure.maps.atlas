import * as atlas from "azure-maps-control"

export class Position {
    public static areEqual(pos1: atlas.data.Position, pos2: atlas.data.Position, precision?: number) {
        return atlas.data.Position.areEqual(pos1, pos2, precision);
    }

    public static fromLatLng(latLng: object) {
        return atlas.data.Position.fromLatLng(latLng);
    }

    public static fromLatLngValues(lat: number, lng: number, elv?: number) {
        return atlas.data.Position.fromLatLng(lat, lng, elv);
    }

    public static fromLatLngs(latLngs: Array<object | number[]>) {
        return atlas.data.Position.fromLatLngs(latLngs);
    }

    public static fromPosition(position: atlas.data.Position) {
        return atlas.data.Position.fromPosition(position);
    }
}