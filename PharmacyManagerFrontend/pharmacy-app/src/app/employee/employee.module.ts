import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeRoutingModule } from './employee-routing.module';
import { LayoutComponent } from './components/layout/layout.component';
import { SolicitudesCreateComponent } from './pages/solicitudes-create/solicitudes-create/solicitudes-create.component';
import { SolicitudesComponent } from './pages/solicitudes/solicitudes/solicitudes.component';
import { SolicitudesListComponent } from './components/solicitudes-list/solicitudes-list.component';
import { PurchaseDetailComponent } from './pages/purchase-detail/purchase-detail.component';
import { PurchasesComponent } from './pages/purchases/purchases.component';
import { DrugCreateComponent } from './pages/drug-create/drug-create.component';
import { DrugsComponent } from './pages/drugs/drugs.component';
import { DrugListDeleteComponent } from './components/drug-list-delete/drug-list-delete.component';
import { ExportDrugComponent } from './pages/export-drug/export-drug.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [
    LayoutComponent,
    PurchasesComponent,
    PurchaseDetailComponent,
    DrugListDeleteComponent,
    DrugsComponent,
    DrugCreateComponent,
    ExportDrugComponent,
    SolicitudesComponent,
    SolicitudesCreateComponent,
    SolicitudesListComponent,
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
  exports: [PurchaseDetailComponent],
})
export class EmployeeModule {}
