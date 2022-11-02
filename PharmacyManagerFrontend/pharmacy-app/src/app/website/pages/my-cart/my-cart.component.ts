import { Component, OnInit } from '@angular/core';
import {StoreService} from "../../../services/store.service";
import {PurchaseDrugDto} from "../../../models/Dto/purchase-drug-dto.model";

@Component({
  selector: 'app-my-cart',
  templateUrl: './my-cart.component.html',
  styleUrls: ['./my-cart.component.scss']
})
export class MyCartComponent implements OnInit {

  myShoppingCart : PurchaseDrugDto[] = []
  userEmail: string | null = null
  purchaseStatus: 'loading' | 'success' | 'error' | null = null
  errorMessage: string = ''
  createdPurchase = {
    code: "test-code"
  }

  constructor(private storeService: StoreService) {
    this.myShoppingCart = this.storeService.getShoppingCart();
  }

  ngOnInit(): void {
  }

  onDelete(purchase: PurchaseDrugDto) {
    this.storeService.removeDrug(purchase);
  }

  onSubmit() {
    console.log("comprar!")
  }

  isDisable() {
    return this.userEmail == null
  }
}
