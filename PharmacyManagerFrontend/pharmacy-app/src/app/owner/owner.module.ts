import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OwnerRoutingModule } from './owner-routing.module';
import { LayoutComponent } from './components/layout/layout.component';
import { InvitationCreateComponent } from './pages/invitation-create/invitation-create.component';
import {FormsModule} from "@angular/forms";
import { PurchaseReportComponent } from './pages/purchase-report/purchase-report.component';


@NgModule({
  declarations: [
    LayoutComponent,
    InvitationCreateComponent,
    PurchaseReportComponent
  ],
  imports: [
    CommonModule,
    OwnerRoutingModule,
    FormsModule
  ]
})
export class OwnerModule { }
