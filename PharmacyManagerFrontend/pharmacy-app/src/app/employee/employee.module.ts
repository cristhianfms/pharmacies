import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EmployeeRoutingModule } from './employee-routing.module';
import { LayoutComponent } from './components/layout/layout.component';
import { PurchasesComponent } from './pages/purchases/purchases.component';
import { PurchaseDetailComponent } from './pages/purchase-detail/purchase-detail.component';


@NgModule({
  declarations: [
    LayoutComponent,
    PurchasesComponent,
    PurchaseDetailComponent
  ],
  imports: [
    CommonModule,
    EmployeeRoutingModule
  ]
})
export class EmployeeModule { }
