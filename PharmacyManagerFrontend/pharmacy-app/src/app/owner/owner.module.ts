import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OwnerRoutingModule } from './owner-routing.module';
import { LayoutComponent } from './components/layout/layout.component';
import { InvitationCreateComponent } from './pages/invitation-create/invitation-create.component';
import {FormsModule} from "@angular/forms";


@NgModule({
  declarations: [
    LayoutComponent,
    InvitationCreateComponent
  ],
  imports: [
    CommonModule,
    OwnerRoutingModule,
    FormsModule
  ]
})
export class OwnerModule { }
