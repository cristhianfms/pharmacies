import {Purchase} from "./purchase.model";

export interface PurchaseList {
    totalPrice: number,
    purchases: Purchase[]
}
