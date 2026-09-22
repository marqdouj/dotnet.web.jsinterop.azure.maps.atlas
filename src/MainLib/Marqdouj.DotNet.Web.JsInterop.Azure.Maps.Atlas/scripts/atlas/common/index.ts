
export interface JsError {
    name: string;
    message?: string;
    stack?: string;
    cause?: string;
}


export interface MapObjectReference {
    mapId: string;
    id?: string;
    type: string | undefined;
    jsReference: any;
}

export enum EditAction {
    Update = 'update',
    Replace = 'replace',
}
