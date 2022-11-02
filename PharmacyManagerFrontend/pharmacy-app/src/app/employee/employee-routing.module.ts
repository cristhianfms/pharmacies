import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LayoutComponent} from "../admin/components/layout/layout.component";
import {PharmaciesComponent} from "../admin/pages/pharmacies/pharmacies.component";

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      {
        path: '',
        redirectTo: 'pharmacies',
        pathMatch: 'full'
      },
      {
        path: 'pharmacies',
        component: PharmaciesComponent
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmployeeRoutingModule { }
