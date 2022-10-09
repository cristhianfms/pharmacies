export interface Drug {
    id: number,
    code: string,
    name: string,
    price: number,
    symptoms: string,
    presentation: string,
    presentationQuantity: number,
    measureUnit: string,
    needsPrescription: boolean,
    pharmacyName: string
}
