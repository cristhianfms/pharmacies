import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SolicitudesCreateComponent } from './pages/solicitudes-create/solicitudes-create/solicitudes-create.component';
import { SolicitudesListComponent } from './components/solicitudes-list/solicitudes-list.component';
import { SolicitudesComponent } from './pages/solicitudes/solicitudes/solicitudes.component';
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
