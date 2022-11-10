import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EmployeeRoutingModule } from './employee-routing.module';
import { LayoutComponent } from './components/layout/layout.component';
import { SolicitudesCreateComponent } from './pages/solicitudes-create/solicitudes-create.component';
import { SolicitudesComponent } from './pages/solicitudes/solicitudes.component';
import { SolicitudesListComponent } from './components/solicitudes-list/solicitudes-list.component';
import { PurchaseDetailComponent } from './pages/purchase-detail/purchase-detail.component';
import {PurchasesComponent} from "./pages/purchases/purchases.component";
import { ExportDrugComponent } from './pages/export-drug/export-drug.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {SharedModule} from "../shared/shared.module";
import { SolicitudesFilterPipe } from './pipes/solicitudes-filter.pipe';



@NgModule({
  declarations: [
    LayoutComponent,
    SolicitudesComponent,
    SolicitudesCreateComponent,
    SolicitudesListComponent,
    SolicitudesFilterPipe,
    PurchasesComponent,
    PurchaseDetailComponent,
    ExportDrugComponent,
  ],
  imports: [
    CommonModule,
    EmployeeRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule, 
  ],
    exports: [
        PurchaseDetailComponent

    ]
    
})
export class EmployeeModule { }
