import { PurchaseItemReportDto } from "./purchase-item-report-dto.model"
import { BehaviorSubject, map, of, catchError, throwError, tap } from 'rxjs';

export interface PurchaseReportDto {
  totalPrice: number,
  purchases: PurchaseItemReportDto[]
}
