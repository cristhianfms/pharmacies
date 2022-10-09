import { Injectable } from '@angular/core';
import {Drug} from "../models/drug.model";
import {BehaviorSubject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class StoreService {

  private myShoppingCart : Drug[] = []
  private myCart = new BehaviorSubject<Drug[]>([])
  myCart$ = this.myCart.asObservable();

  constructor() { }

  getShoppingCart(){
    return this.myShoppingCart;
  }

  addDrug(drug: Drug){
    this.myShoppingCart.push(drug)
    this.myCart.next(this.myShoppingCart);
  }

  getTotal(){
    return this.myShoppingCart.reduce((sum, item) => sum + item.price, 0)
  }
}
