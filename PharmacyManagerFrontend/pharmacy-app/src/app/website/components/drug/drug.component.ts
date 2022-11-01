import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Drug} from "../../../models/drug.model";
import {PurchaseDrugDto} from "../../../models/Dto/purchase-drug-dto.model";

@Component({
  selector: 'app-drug',
  templateUrl: './drug.component.html',
  styleUrls: ['./drug.component.scss']
})
export class DrugComponent implements OnInit {

  @Input() drug: Drug = {
    id: 0,
    drugCode: '',
    name: '',
    price: 0,
    symptoms: '',
    presentation: '',
    quantityPerPresentation: 0,
    unitOfMeasurement: '',
    needsPrescription: true,
    stock: 0,
    pharmacyId: 0
  }
  @Output() addedProduct = new EventEmitter<PurchaseDrugDto>();
  amount : number = 1

  constructor() { }

  ngOnInit(): void {
  }

  onAddToCart(event: Event) {
    event.preventDefault()

    let purchaseDrug : PurchaseDrugDto = {
      pharmacyName: "",
      drugCode: this.drug.drugCode,
      quantity: this.amount
    }
    this.addedProduct.emit(purchaseDrug)
    this.amount = 1
  }
}
