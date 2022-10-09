export interface Drug {
    id: number,
    drugCode: string,
    name: string,
    price: number,
    symptoms: string,
    presentation: string,
    quantityPerPresentation: number,
    unitOfMeasurement: string,
    needsPrescription: boolean,
    pharmacyName: string,
    stock: number
}
