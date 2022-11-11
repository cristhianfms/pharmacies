import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpParams,
  HttpStatusCode,
} from '@angular/common/http';
import { environment } from '../../environments/environment';
import {
  PurchaseDto,
  PurchaseGetDto,
  PurchasePutDto,
} from '../models/Dto/purchase-dto.model';
import { PurchaseList } from '../models/purchase-list.model';
import { BehaviorSubject, map, of, catchError, tap } from 'rxjs';
import { Purchase } from '../models/purchase.model';
import { PurchaseListGetDto } from '../models/Dto/purchase-list-dto.model';
import { PurchaseItem } from '../models/purchase-item.model';
import { PurchaseItemGetDto } from '../models/Dto/purchase-item-dto.model';
import { PurchaseState } from '../models/Dto/purchase-state-dto.model';
import { PurchaseReportDto } from '../models/Dto/purchase-report-dto.model';
import { PurchaseItemReportDto } from '../models/Dto/purchase-item-report-dto.model';

@Injectable({
  providedIn: 'root',
})
export class PurchasesService {
  apiUrl: string = `${environment.API_URL}/api/purchases`;
  dateFromQueryParam: string = 'dateFrom';
  dateToQueryParam: string = 'dateTo';
  selectedPurchaseDetail = new BehaviorSubject<Purchase | null>(null);
  selectedPurchaseDetail$ = this.selectedPurchaseDetail.asObservable();
  _purchaseItemReportBehaviorSubject$: BehaviorSubject<
    PurchaseItemReportDto[] | undefined
  >;

  constructor(private http: HttpClient) {
    this._purchaseItemReportBehaviorSubject$ = new BehaviorSubject<
      PurchaseItemReportDto[] | undefined
    >(undefined);
  }

  create(purchase: PurchaseDto) {
    return this.http.post(`${this.apiUrl}`, purchase);
  }

  getAllPurchases() {
    return this.http.get<PurchaseListGetDto>(`${this.apiUrl}`).pipe(
      map((list: PurchaseListGetDto) => {
        let purchaseList: PurchaseList = {
          totalPrice: list.totalPrice,
          purchases: list.purchases.map(this.purchaseDtoToModel),
        };
        return purchaseList;
      })
    );
  }

  updatePurchase(id: number, purchase: Purchase) {
    let purchaseDto: PurchasePutDto = {
      items: purchase.items.map((i) => {
        return {
          drugCode: i.drugCode,
          state: PurchaseState[i.state],
        };
      }),
    };

    return this.http
      .put<PurchaseGetDto>(`${this.apiUrl}/${id}`, purchaseDto)
      .pipe(map(this.purchaseDtoToModel));
  }

  purchaseDtoToModel(p: PurchaseGetDto) {
    let purchase: Purchase = {
      id: p.id,
      code: p.code,
      userEmail: p.userEmail,
      createdDate: p.createdDate,
      price: p.price,
      items: p.items.map((i: PurchaseItemGetDto) => {
        let state: any = PurchaseState[i.state];
        let item: PurchaseItem = {
          drugCode: i.drugCode,
          quantity: i.quantity,
          pharmacyName: i.pharmacyName,
          state: state,
        };
        return item;
      }),
    };
    return purchase;
  }

  getPurchase(purchaseCode: string) {
    return this.http
      .get<PurchaseGetDto>(`${this.apiUrl}/${purchaseCode}`)
      .pipe(map(this.purchaseDtoToModel));
  }

  getPurchasesReport(dateFrom?: string, dateTo?: string) {
    return this.http
      .get<PurchaseReportDto>(
        `${this.apiUrl}/report` + '?dateFrom=' + dateFrom + '&dateTo=' + dateTo
      )
      .pipe(
        tap((report: PurchaseReportDto) =>
          this._purchaseItemReportBehaviorSubject$.next(report.purchases)
        )
      );
  }
}
