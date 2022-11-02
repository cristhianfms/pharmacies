import { Component, OnInit } from '@angular/core';
import {Purchase} from "../../../models/purchase.model";
import {PurchasesService} from "../../../services/purchases.service";

@Component({
  selector: 'app-purchases',
  templateUrl: './purchases.component.html',
  styleUrls: ['./purchases.component.scss']
})
export class PurchasesComponent implements OnInit {

  foundPurchase: Purchase | null = null;
  purchaseCode = ''

  findStatus: 'loading' | 'success' | 'error' | null = null
  errorMessage: string = ''

  constructor(private purchasesServices: PurchasesService) { }

  ngOnInit(): void {

  }

  onSearchCode() {
    this.purchasesServices.getPurchase(this.purchaseCode).subscribe({
          next: this.handleGetResponse.bind(this),
          error: this.handleError.bind(this)
        }
    )
  }

  handleGetResponse(data: any){
    this.foundPurchase = data;
    this.findStatus = 'success'
  }

  handleError(error: any){
    this.findStatus = 'error'
    this.foundPurchase = null
    this.errorMessage = error.error.message
  }

  onInputChange() {
    this.errorMessage = ''
  }

  isButtonDisabled() {
    return this.purchaseCode.trim() == '' || this.purchaseCode.trim() == null
  }
}
