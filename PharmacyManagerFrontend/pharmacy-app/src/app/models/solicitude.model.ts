import { SolicitudeItemDto } from "./Dto/solicitude-item-dto.model";

export interface SolicitudeItem {
    drugCode: string,
    drugQuantity: string,
}

export interface Solicitude {
    state: string,
    date: Date,
    employeeUserName: string,
    pharmacy: string,
    solicitudeItems:SolicitudeItem[],
}

export interface CreateSolicitudeDto  {
    solicitudeItems: SolicitudeItemDto[]
}

export interface UpdateSolicitudeDto {
//TODO:
}