import { Injectable } from '@angular/core';
import {Drug} from "../models/drug.model";
import {BehaviorSubject} from "rxjs";
import {PurchaseDrugDto} from "../models/Dto/purchase-drug-dto.model";

@Injectable({
  providedIn: 'root'
})
export class StoreService {

  private myShoppingCart : PurchaseDrugDto[] = []
  private myCart = new BehaviorSubject<PurchaseDrugDto[]>([])
  myCart$ = this.myCart.asObservable();

  constructor() { }

  getShoppingCart(){
    return this.myShoppingCart;
  }

  addDrug(purchaseToAdd: PurchaseDrugDto){
    let purchase = this.myShoppingCart.find(p =>
        p.pharmacyName === purchaseToAdd.pharmacyName && p.drugCode === purchaseToAdd.drugCode)
    if (purchase) {
      purchase.quantity = purchase.quantity + purchaseToAdd.quantity;
    } else {
      this.myShoppingCart.push(purchaseToAdd)
    }

    this.myCart.next(this.myShoppingCart);
  }

  removeDrug(purchaseToDelete: PurchaseDrugDto) {
    const index = this.myShoppingCart.findIndex(p =>
        p.drugCode === purchaseToDelete.drugCode && p.pharmacyName === purchaseToDelete.pharmacyName);
    this.myShoppingCart.splice(index, 1);

    this.myCart.next(this.myShoppingCart);
  }
}
