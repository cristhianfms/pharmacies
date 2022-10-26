import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { LayoutComponent } from './components/layout/layout.component';
import { InvitationsComponent } from './pages/invitations/invitations.component';
import { PharmaciesComponent } from './pages/pharmacies/pharmacies.component';


@NgModule({
  declarations: [
    LayoutComponent,
    InvitationsComponent,
    PharmaciesComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule
  ]
})
export class AdminModule { }
