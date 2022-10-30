import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OwnerRoutingModule } from './owner-routing.module';
import { LayoutComponent } from './components/layout/layout.component';
import { InvitationCreateComponent } from './pages/invitation-create/invitation-create.component';


@NgModule({
  declarations: [
    LayoutComponent,
    InvitationCreateComponent
  ],
  imports: [
    CommonModule,
    OwnerRoutingModule
  ]
})
export class OwnerModule { }
