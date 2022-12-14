import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LayoutComponent} from "./components/layout/layout.component";
import {PharmaciesComponent} from "./pages/pharmacies/pharmacies.component";
import {InvitationsComponent} from "./pages/invitations/invitations.component";
import {InvitationEditComponent} from "./pages/invitation-edit/invitation-edit.component";
import {InvitationCreateComponent} from "./pages/invitation-create/invitation-create.component";
import {PharmacyCreateComponent} from "./pages/pharmacy-create/pharmacy-create.component";

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
      {
        path: 'invitations',
        component: InvitationsComponent
      },
      {
        path: 'invitation-edit/:id',
        component: InvitationEditComponent
      },
      {
        path: 'invitation-create',
        component: InvitationCreateComponent
      },
      {
        path: 'pharmacy-create',
        component: PharmacyCreateComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class AdminRoutingModule { }
