import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EmployeeRoutingModule } from './employee-routing.module';
import { LayoutComponent } from './components/layout/layout.component';
import { PurchaseDetailComponent } from './pages/purchase-detail/purchase-detail.component';
import {PurchasesComponent} from "./pages/purchases/purchases.component";


@NgModule({
    declarations: [
        LayoutComponent,
        PurchasesComponent,
        PurchaseDetailComponent
    ],
    exports: [
        PurchaseDetailComponent
    ],
    imports: [
        CommonModule,
        EmployeeRoutingModule
    ]
})
export class EmployeeModule { }
