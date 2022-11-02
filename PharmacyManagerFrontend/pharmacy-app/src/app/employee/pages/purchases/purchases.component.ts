import { Component, OnInit } from '@angular/core';
import {PurchasesService} from "../../../services/purchases.service";
import {PurchaseList} from "../../../models/purchase-list.model";
import {Purchase} from "../../../models/purchase.model";

@Component({
  selector: 'app-purchases',
  templateUrl: './purchases.component.html',
  styleUrls: ['./purchases.component.scss']
})
export class PurchasesComponent implements OnInit {

  purchaseList: PurchaseList = {
    purchases : [],
    totalPrice: 0
  }
  constructor(private purchasesService: PurchasesService) { }

  ngOnInit(): void {
    this.purchasesService.getAllPurchases().subscribe({
          next: this.handleGetAllResponse.bind(this),
          error: this.handleError.bind(this)
        }
    )
  }

  handleGetAllResponse(data: any){
    this.purchaseList = data
  }

  handleError(error: any){
    window.alert("Error getting invitations")
  }

  onDetails(purchase: Purchase) {
    console.log(purchase)
  }
}
