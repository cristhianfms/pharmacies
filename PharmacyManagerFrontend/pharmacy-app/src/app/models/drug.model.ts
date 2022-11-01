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
    stock: number
}

export interface CreateDrugDTO extends Omit<Drug, 'id' | 'stock' | 'pharmacyName'> {
}
