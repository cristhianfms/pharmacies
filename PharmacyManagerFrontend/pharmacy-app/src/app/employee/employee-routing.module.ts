import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SolicitudesCreateComponent } from './pages/solicitudes-create/solicitudes-create.component';
import { SolicitudesComponent } from './pages/solicitudes/solicitudes.component';
import {LayoutComponent} from "../employee/components/layout/layout.component";
import {PurchaseDetailComponent} from "./pages/purchase-detail/purchase-detail.component";
import {PurchasesComponent} from "./pages/purchases/purchases.component";
import { DrugCreateComponent } from './pages/drug-create/drug-create.component';
import { DrugsComponent } from './pages/drugs/drugs.component';
import {ExportDrugComponent} from "./pages/export-drug/export-drug.component";
import { SolicitudesListComponent } from './components/solicitudes-list/solicitudes-list.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      {
        path: '',
        redirectTo: 'solicitudes',
        pathMatch: 'full'
      },
      {
        path: 'solicitudes',
        component: SolicitudesComponent
      },
      {
        path: 'solicitudes-create',
        component: SolicitudesCreateComponent
      },
      {
        path: 'solicitudes-list',
        component: SolicitudesListComponent
      },
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
        path: 'drugs/drug-create',
        component: DrugCreateComponent
      },
      {
        path: 'export-drugs',
        component: ExportDrugComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmployeeRoutingModule { }
