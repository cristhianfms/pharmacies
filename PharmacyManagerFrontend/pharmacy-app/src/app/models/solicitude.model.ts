import { SolicitudeItemDto } from "./Dto/solicitude-item-dto.model";

export interface SolicitudeItem {
    drugCode: string,
    drugQuantity: number,
}

export interface Solicitude {
    id: number,
    state: "Pending" | "Accepted" | "Rejected",
    date: Date,
    employeeUserName: string,
    pharmacy: string,
    solicitudeItems:SolicitudeItem[],
}

export interface CreateSolicitudeDto  {
    solicitudeItems: SolicitudeItemDto[]
}
