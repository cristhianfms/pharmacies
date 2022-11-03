import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EmployeeRoutingModule } from './employee-routing.module';
import { LayoutComponent } from './components/layout/layout.component';
import { PurchaseDetailComponent } from './pages/purchase-detail/purchase-detail.component';
import {PurchasesComponent} from "./pages/purchases/purchases.component";
import { DrugCreateComponent } from './pages/drug-create/drug-create.component';
import { DrugDeleteComponent } from './pages/drug-delete/drug-delete.component';
import { DrugsComponent } from './pages/drugs/drugs.component';


@NgModule({
    declarations: [
        LayoutComponent,
        PurchasesComponent,
        PurchaseDetailComponent,
        DrugsComponent,
        DrugCreateComponent,
        DrugDeleteComponent
    ],
    exports: [
        PurchaseDetailComponent
    ],
    imports: [
        CommonModule,
        EmployeeRoutingModule,
        FormsModule
    ]
})
export class EmployeeModule { }
