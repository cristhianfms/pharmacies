import {PurchaseItemDto} from "./purchase-item-dto.model";

export interface PurchaseDto {
    userEmail: string,
    items: PurchaseItemDto[]
}
