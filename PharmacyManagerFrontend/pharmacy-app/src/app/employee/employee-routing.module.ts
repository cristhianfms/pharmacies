import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LayoutComponent} from "../employee/components/layout/layout.component";
import {PurchaseDetailComponent} from "./pages/purchase-detail/purchase-detail.component";
import {PurchasesComponent} from "./pages/purchases/purchases.component";

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
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmployeeRoutingModule { }
