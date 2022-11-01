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

  addDrug(purchaseDrug: PurchaseDrugDto){
    // TODO: ADD ONLY ONCE!
    this.myShoppingCart.push(purchaseDrug)
    this.myCart.next(this.myShoppingCart);
  }
}
