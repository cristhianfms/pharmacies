import {Component, Input, OnInit} from '@angular/core';
import {CreateDrugDTO, Drug} from "../../../models/drug.model";
import {StoreService} from "../../../services/store.service";
import {DrugsService} from "../../../services/drugs.service";

@Component({
    selector: 'app-drugs',
    templateUrl: './drugs.component.html',
    styleUrls: ['./drugs.component.scss']
})
export class DrugsComponent {

    totalPrice : number = 0
    myShoppingCart : Drug[] = []
    @Input() drugs: Drug[] = []
    drugChosen: Drug = {
        id: 0,
        drugCode: "",
        name: "",
        price: 0,
        symptoms: "",
        presentation: "",
        quantityPerPresentation: 20,
        unitOfMeasurement: "",
        needsPrescription: true,
        pharmacyId: 0,
        stock: 0
    }
    statusGetAll: 'loading' | 'success' | 'error' | 'init' = 'init'

    constructor( private storeService: StoreService, private drugService: DrugsService) {
        this.myShoppingCart = this.storeService.getShoppingCart();
    }

    onAddToShoppingCart(drug: Drug) {
        this.storeService.addDrug(drug)
        this.totalPrice = this.storeService.getTotal()
    }

    createNewDrug(){
        const newDrug : CreateDrugDTO = {
            drugCode: "A01",
            name: "Nombre de droga nueva",
            price: 100.50,
            symptoms: "Sintomas locos",
            presentation: "presentacion nueva",
            quantityPerPresentation: 50,
            unitOfMeasurement: "ml",
            needsPrescription: true,
            pharmacyId: 1
        }
        this.drugService.create(newDrug).subscribe((data: any) => {
            console.log('created', data)
            this.drugs.push(data)
        })
    }

    deleteDrug() {
        const id = this.drugChosen.id;
        this.drugService.delete(id).subscribe(() => {
            const drugIndex = this.drugs.findIndex(d => d.id === id)
            this.drugs.splice(drugIndex, 1)
        })
    }
}
