import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Drug} from "../../models/drug.model";

@Component({
  selector: 'app-drug',
  templateUrl: './drug.component.html',
  styleUrls: ['./drug.component.scss']
})
export class DrugComponent implements OnInit {

  @Input() drug: Drug = {
    id: 0,
    code: '',
    name: '',
    price: 0,
    symptoms: '',
    presentation: '',
    presentationQuantity: 0,
    measureUnit: '',
    needsPrescription: true,
    pharmacyName: ''
  }
  @Output() addedProduct = new EventEmitter<Drug>();

  constructor() { }

  ngOnInit(): void {
  }

  onAddToCart() {
    this.addedProduct.emit(this.drug)
  }
}
