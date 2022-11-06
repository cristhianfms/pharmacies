import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterOutlet} from "@angular/router";
import { RoleLayoutHeaderComponent } from './components/role-layout-header/role-layout-header.component';



@NgModule({
  declarations: [
    RoleLayoutHeaderComponent
  ],
  exports: [
    RoleLayoutHeaderComponent
  ],
  imports: [
    CommonModule,
    RouterOutlet
  ]
})
export class SharedModule { }
