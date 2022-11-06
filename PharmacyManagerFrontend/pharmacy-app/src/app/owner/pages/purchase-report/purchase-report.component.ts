import { Component, OnInit } from '@angular/core';
import {DrugsService} from "../../../services/drugs.service";
import {Drug} from "../../../models/drug.model";
import {PurchasesService} from "../../../services/purchases.service";
import {Purchase} from "../../../models/purchase.model";
import {PurchaseList} from "../../../models/purchase-list.model";

@Component({
  selector: 'app-purchase-report',
  templateUrl: './purchase-report.component.html',
  styleUrls: ['./purchase-report.component.scss']
})
export class PurchaseReportComponent implements OnInit {

  dateFrom: string = ''
  dateTo: string = ''
  purchaseList: any = null
  totalAmount: number = 0
  getStatus: 'loading' | 'success' | 'error' | null = null
  errorMessage: string = ''

  constructor(private purchasesServices: PurchasesService) { }

  ngOnInit(): void {
  }

  onSubmit() {
    this.getStatus = 'loading'
    this.purchasesServices.getPurchasesReport(this.dateFrom, this.dateTo).subscribe({
          next: this.handleGetResponse.bind(this),
          error: this.handleError.bind(this)
        }
    )
  }

  handleGetResponse(data: any){
    this.purchaseList = data.purchases;
    this.totalAmount = data.totalPrice
    this.getStatus = 'success'
  }

  handleError(error: any){
    this.getStatus = 'error'
    this.purchaseList = null
    this.errorMessage = error.error.message
  }
}
