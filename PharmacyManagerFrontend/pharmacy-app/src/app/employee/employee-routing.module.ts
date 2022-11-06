import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LayoutComponent} from "../employee/components/layout/layout.component";
import {PurchaseDetailComponent} from "./pages/purchase-detail/purchase-detail.component";
import {PurchasesComponent} from "./pages/purchases/purchases.component";
import {ExportDrugComponent} from "./pages/export-drug/export-drug.component";

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      {
        path: '',
        redirectTo: 'purchases',
        pathMatch: 'full'
      },
      {
        path: 'purchases',
        component: PurchasesComponent
      },
      {
        path: 'purchase-detail/:id',
        component: PurchaseDetailComponent
      },
      {
        path: 'export-drugs',
        component: ExportDrugComponent
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmployeeRoutingModule { }
