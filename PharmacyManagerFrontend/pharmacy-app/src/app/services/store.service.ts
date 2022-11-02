import { Injectable } from '@angular/core';
import {Drug} from "../models/drug.model";
import {BehaviorSubject} from "rxjs";
import {PurchaseItemDto} from "../models/Dto/purchase-item-dto.model";

@Injectable({
  providedIn: 'root'
})
export class StoreService {

  private myShoppingCart : PurchaseItemDto[] = []
  private myCart = new BehaviorSubject<PurchaseItemDto[]>([])
  myCart$ = this.myCart.asObservable();

  constructor() { }

  getShoppingCart(){
    return this.myShoppingCart;
  }

  addDrug(purchaseToAdd: PurchaseItemDto){
    let purchase = this.myShoppingCart.find(p =>
        p.pharmacyName === purchaseToAdd.pharmacyName && p.drugCode === purchaseToAdd.drugCode)
    if (purchase) {
      purchase.quantity = purchase.quantity + purchaseToAdd.quantity;
    } else {
      this.myShoppingCart.push(purchaseToAdd)
    }

    this.myCart.next(this.myShoppingCart);
  }

  removeDrug(purchaseToDelete: PurchaseItemDto) {
    const index = this.myShoppingCart.findIndex(p =>
        p.drugCode === purchaseToDelete.drugCode && p.pharmacyName === purchaseToDelete.pharmacyName);
    this.myShoppingCart.splice(index, 1);

    this.myCart.next(this.myShoppingCart);
  }

    emptyCart() {
      this.myShoppingCart = []
      this.myCart.next(this.myShoppingCart);
    }
}
