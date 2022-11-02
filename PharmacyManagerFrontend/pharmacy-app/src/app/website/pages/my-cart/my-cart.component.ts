import { Component, OnInit } from '@angular/core';
import {StoreService} from "../../../services/store.service";
import {PurchaseItemDto} from "../../../models/Dto/purchase-item-dto.model";
import {PurchasesService} from "../../../services/purchases.service";
import {PurchaseDto} from "../../../models/Dto/purchase-dto.model";

@Component({
  selector: 'app-my-cart',
  templateUrl: './my-cart.component.html',
  styleUrls: ['./my-cart.component.scss']
})
export class MyCartComponent implements OnInit {

  myShoppingCart : PurchaseItemDto[] = []
  userEmail: string | null = null
  purchaseStatus: 'loading' | 'success' | 'error' | null = null
  errorMessage: string = ''
  createdPurchaseCode: string = ''

  constructor(private storeService: StoreService, private purchasesService: PurchasesService) {
    this.myShoppingCart = this.storeService.getShoppingCart();
  }

  ngOnInit(): void {
  }

  onDelete(purchase: PurchaseItemDto) {
    this.storeService.removeDrug(purchase);
  }

  onSubmit() {
    this.purchaseStatus = 'loading'
    const purchase: PurchaseDto = {
      userEmail: this.userEmail || '',
      items: [...this.myShoppingCart]
    }
    this.purchasesService.create(purchase).subscribe({
      next: this.handleSuccessResponse.bind(this),
      error: this.handleError.bind(this)
    })
  }

  handleSuccessResponse(data: any) {
    this.createdPurchaseCode = data.code;
    this.purchaseStatus = 'success'
    this.storeService.emptyCart()
  }

  handleError(error: any) {
    this.purchaseStatus = 'error'
    this.errorMessage = error.error.message
  }

  isDisable() {
    return this.userEmail == null
  }

  onAmountChange() {
    this.purchaseStatus = null
  }

  onEmailChange() {
    this.purchaseStatus = null
  }
}
