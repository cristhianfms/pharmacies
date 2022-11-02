import {Component, Input, OnInit} from '@angular/core';
import {Purchase} from "../../../models/purchase.model";

@Component({
  selector: 'app-purchase-detail',
  templateUrl: './purchase-detail.component.html',
  styleUrls: ['./purchase-detail.component.scss']
})
export class PurchaseDetailComponent implements OnInit {

  @Input() purchase: Purchase = {
    id: 1,
    code: '',
    userEmail: '',
    createdDate: '',
    price: 1,
    items: []
  }

  constructor() { }

  ngOnInit(): void {
  }

}
