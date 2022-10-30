import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {InvitationCreateComponent} from "./pages/invitation-create/invitation-create.component";
import {LayoutComponent} from "./components/layout/layout.component";

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      {
        path: 'invitations',
        component: InvitationCreateComponent
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OwnerRoutingModule { }
