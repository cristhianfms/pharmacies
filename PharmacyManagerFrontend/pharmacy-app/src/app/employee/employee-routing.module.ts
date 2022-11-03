import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LayoutComponent} from "../employee/components/layout/layout.component";
import {PurchaseDetailComponent} from "./pages/purchase-detail/purchase-detail.component";
import {PurchasesComponent} from "./pages/purchases/purchases.component";
import { DrugCreateComponent } from './pages/drug-create/drug-create.component';
import { DrugDeleteComponent } from './pages/drug-delete/drug-delete.component';
import { DrugsComponent } from './pages/drugs/drugs.component';

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
      },{
        path: 'drugs',
        component: DrugsComponent
      },
      {
        path: 'drug-delete',
        component: DrugDeleteComponent
      },
      {
        path: 'drug-create',
        component: DrugCreateComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmployeeRoutingModule { }
