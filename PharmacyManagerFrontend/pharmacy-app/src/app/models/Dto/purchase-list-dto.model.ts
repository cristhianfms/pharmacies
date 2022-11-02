import {PurchaseGetDto} from "./purchase-dto.model";

export interface PurchaseListGetDto {
    totalPrice: number,
    purchases: PurchaseGetDto[]
}
