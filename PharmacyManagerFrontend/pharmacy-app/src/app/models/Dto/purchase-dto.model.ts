import {PurchaseItemDto, PurchaseItemGetDto, PurchaseItemPutDto} from "./purchase-item-dto.model";

export interface PurchaseDto {
    userEmail: string,
    items: PurchaseItemDto[]
}

export interface PurchaseGetDto {
    id: number,
    code: string,
    userEmail: string,
    createdDate: string,
    price: number,
    items: PurchaseItemGetDto[]
}

export interface PurchasePutDto {
    items: PurchaseItemPutDto[]
}
