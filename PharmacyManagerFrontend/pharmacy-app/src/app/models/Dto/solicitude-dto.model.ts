import {SolicitudeItem} from "../solicitude.model";

export interface SolicitudeGetDto {
    id: number,
    state: number;
    date: Date,
    employeeUserName: string,
    pharmacy: string,
    solicitudeItems:SolicitudeItem[],
}

export interface SolicitudePutDto {
    state: string
}
