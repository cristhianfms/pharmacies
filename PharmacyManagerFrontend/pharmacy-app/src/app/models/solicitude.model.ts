import { SolicitudeItemDto } from "./Dto/solicitude-item-dto.model";

export interface SolicitudeItem {
    drugCode: string,
    drugQuantity: string,
}

export interface Solicitude {
    id: number,
    state: "PENDING" | "ACCEPTED" | "REJECTED";
    date: Date,
    employeeUserName: string,
    pharmacy: string,
    solicitudeItems:SolicitudeItem[],
}

export interface CreateSolicitudeDto  {
    solicitudeItems: SolicitudeItemDto[]
}

export interface SolicitudePutModel {
   // state: "PENDING" | "ACCEPTED" | "REJECTED";
    state: number
}