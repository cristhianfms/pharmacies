import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Drug} from "../../../models/drug.model";

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
  @Output() addedProduct = new EventEmitter<Drug>();

  constructor() { }

  ngOnInit(): void {
  }

  onAddToCart(event: Event) {
    event.preventDefault()
    this.addedProduct.emit(this.drug)
  }
}
