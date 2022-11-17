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
    pharmacyId: number,
    stock: number,
    pharmacyName: string
}

export interface CreateDrugDTO extends Omit<Drug, 'id' | 'stock'> {
}
