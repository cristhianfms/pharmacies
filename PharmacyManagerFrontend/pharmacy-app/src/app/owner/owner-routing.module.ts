import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {InvitationCreateComponent} from "./pages/invitation-create/invitation-create.component";
import {LayoutComponent} from "./components/layout/layout.component";
import {PurchaseReportComponent} from "./pages/purchase-report/purchase-report.component";

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      {
        path: 'invitations',
        component: InvitationCreateComponent
      },
      {
        path: 'purchase-report',
        component: PurchaseReportComponent
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OwnerRoutingModule { }
