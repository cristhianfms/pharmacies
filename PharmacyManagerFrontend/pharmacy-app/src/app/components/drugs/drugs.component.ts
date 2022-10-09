import {Component, OnInit} from '@angular/core';
import {Drug} from "../../models/drug.model";
import {StoreService} from "../../services/store.service";
import {DrugsService} from "../../services/drugs.service";

@Component({
    selector: 'app-drugs',
    templateUrl: './drugs.component.html',
    styleUrls: ['./drugs.component.scss']
})
export class DrugsComponent implements OnInit {

    totalPrice : number = 0
    myShoppingCart : Drug[] = []
    drugs: Drug[] = []

    constructor( private storeService: StoreService, private drugService: DrugsService) {
        this.myShoppingCart = this.storeService.getShoppingCart();
    }

    ngOnInit(): void {
        this.drugService.getAllDrugs().subscribe(
            data => this.drugs = data
        )
    }

    onAddToShoppingCart(drug: Drug) {
        this.storeService.addDrug(drug)
        this.totalPrice = this.storeService.getTotal()
    }

}
