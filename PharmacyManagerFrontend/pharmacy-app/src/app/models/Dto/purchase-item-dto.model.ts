export interface PurchaseItemDto {
    drugCode: string,
    pharmacyName: string,
    quantity: number
}

export interface PurchaseItemGetDto  extends PurchaseItemDto{
    state: 0 | 1 | 2
}

export interface PurchaseItemPutDto {
    drugCode: string,
    state: number
}
