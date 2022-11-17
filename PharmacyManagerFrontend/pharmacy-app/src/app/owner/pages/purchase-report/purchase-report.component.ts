import { Component, OnInit } from '@angular/core';
import { PurchasesService } from '../../../services/purchases.service';
import { PurchaseReportDto } from '../../../models/Dto/purchase-report-dto.model';
import { PurchaseItemReportDto } from '../../../models/Dto/purchase-item-report-dto.model';

@Component({
  selector: 'app-purchase-report',
  templateUrl: './purchase-report.component.html',
  styleUrls: ['./purchase-report.component.scss'],
})
export class PurchaseReportComponent implements OnInit {
  dateFrom: string = "";
  dateTo: string = "";
  purchaseReport: PurchaseReportDto | null = null;
  purchaseItemList: PurchaseItemReportDto[] = [];
  totalAmount: number = 0;
  getStatus: 'loading' | 'success' | 'error' | null = null;
  errorMessage: string = '';

  constructor(private purchasesServices: PurchasesService) {}

  ngOnInit(): void {
    this.purchasesServices._purchaseItemReportBehaviorSubject$.next(
      this.purchaseItemList
    );
  }

  onSubmit() {
    this.getStatus = 'loading';

    this.purchasesServices.getPurchasesReport(this.dateFrom, this.dateTo)
      .subscribe((report: PurchaseReportDto) => {
        this.setPurchaseReportItems(report.purchases),
        this.totalAmount = report.totalPrice
      });
  }

  private setPurchaseReportItems = (
    drugs: PurchaseItemReportDto[] | undefined
  ) => {
    if (!drugs) this.purchaseItemList = [];
    else this.purchaseItemList = drugs;
  };
}
