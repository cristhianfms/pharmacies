import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { LayoutComponent } from './components/layout/layout.component';
import { InvitationsComponent } from './pages/invitations/invitations.component';
import { PharmaciesComponent } from './pages/pharmacies/pharmacies.component';
import { InvitationListComponent } from './components/invitation-list/invitation-list.component';
import {FormsModule} from "@angular/forms";
import { InvitationsFilterPipe } from './pipes/invitations-filter.pipe';


@NgModule({
  declarations: [
    LayoutComponent,
    InvitationsComponent,
    PharmaciesComponent,
    InvitationListComponent,
    InvitationsFilterPipe
  ],
    imports: [
        CommonModule,
        AdminRoutingModule,
        FormsModule
    ]
})
export class AdminModule { }
