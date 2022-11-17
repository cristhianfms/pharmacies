export interface PurchaseItem {
    drugCode: string,
    quantity: number,
    pharmacyName: string,
    state: "Pending" | "Accepted" | "Rejected";
}
