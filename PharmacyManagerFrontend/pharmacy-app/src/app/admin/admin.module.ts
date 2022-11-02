import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { LayoutComponent } from './components/layout/layout.component';
import { InvitationsComponent } from './pages/invitations/invitations.component';
import { PharmaciesComponent } from './pages/pharmacies/pharmacies.component';
import { InvitationListComponent } from './components/invitation-list/invitation-list.component';
import {FormsModule} from "@angular/forms";
import { InvitationsFilterPipe } from './pipes/invitations-filter.pipe';
import {InvitationEditComponent} from "./pages/invitation-edit/invitation-edit.component";
import { InvitationCreateComponent } from './pages/invitation-create/invitation-create.component';
import { PharmacyListComponent } from './components/pharmacy-list/pharmacy-list.component';
import { PharmacyCreateComponent } from './pages/pharmacy-create/pharmacy-create.component';


@NgModule({
    declarations: [
        LayoutComponent,
        InvitationsComponent,
        PharmaciesComponent,
        InvitationListComponent,
        InvitationsFilterPipe,
        InvitationEditComponent,
        InvitationCreateComponent,
        PharmacyListComponent,
        PharmacyCreateComponent
    ],
    imports: [
        CommonModule,
        AdminRoutingModule,
        FormsModule
    ]
})
export class AdminModule { }
