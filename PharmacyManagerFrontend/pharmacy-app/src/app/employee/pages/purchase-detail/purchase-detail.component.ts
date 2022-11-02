import { Component, OnInit } from '@angular/core';
import {PurchasesService} from "../../../services/purchases.service";
import {Purchase} from "../../../models/purchase.model";
import {PurchaseItem} from "../../../models/purchase-item.model";
import {Router} from "@angular/router";

@Component({
  selector: 'app-purchase-detail',
  templateUrl: './purchase-detail.component.html',
  styleUrls: ['./purchase-detail.component.scss']
})
export class PurchaseDetailComponent implements OnInit {

  purchase: Purchase = {
    id: 0,
    code: '',
    userEmail: '',
    createdDate: '',
    price: 0,
    items: []
  }

  updatingStatus: 'loading' | 'success' | 'error' | null = null
  errorMessage: string = ''

  constructor(private purchasesService: PurchasesService, private router: Router) { }

  ngOnInit(): void {
    this.purchasesService.selectedPurchaseDetail$.subscribe((selectedPurchase) => {
          if (selectedPurchase){
            this.purchase = selectedPurchase
          }
        }
    )
  }

  goToPurchasesHome() {
    this.router.navigate(['/employee/purchases']);
  }

  onAccept(item: PurchaseItem) {
    this.updatePurchaseStatus(item, 'Accepted')
  }

  isAcceptDisable(item: PurchaseItem) {
    return item.state === 'Accepted'|| item.state === 'Rejected'
  }

  onReject(item: PurchaseItem) {
    this.updatePurchaseStatus(item, 'Rejected')
  }

  isRejectDisable(item: PurchaseItem) {
    return item.state === 'Accepted'|| item.state === 'Rejected'
  }

  handleUpdateResponse(data: any){
    this.purchase = data;
    this.updatingStatus = 'success'
    setTimeout(() => this.updatingStatus = null,2000)
  }

  handleUpdateError(error: any){
    this.updatingStatus = 'error'
    this.errorMessage = error.error.message
    setTimeout(() => this.updatingStatus = null,2000)
  }

  updatePurchaseStatus(item: PurchaseItem, status: any){
    this.updatingStatus = 'loading'
    let itemToUpdate: PurchaseItem = { ...item }
    itemToUpdate.state = status
    let purchaseToUpdate: Purchase = {...this.purchase}
    purchaseToUpdate.items = [itemToUpdate]
    this.purchasesService.updatePurchase(this.purchase.id, purchaseToUpdate).subscribe({
          next: this.handleUpdateResponse.bind(this),
          error: this.handleUpdateError.bind(this)
        }
    )
  }
}
