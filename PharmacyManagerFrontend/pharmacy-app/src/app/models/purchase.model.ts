import {PurchaseItem} from "./purchase-item.model";

export interface Purchase {
    id: number,
    code: string,
    userEmail: string,
    createdDate: string,
    price: number,
    items: PurchaseItem[]
}
