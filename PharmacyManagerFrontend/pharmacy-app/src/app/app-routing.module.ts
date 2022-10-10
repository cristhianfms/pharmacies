import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MyCartComponent } from './pages/my-cart/my-cart.component';
import { RegisterComponent } from './pages/register/register.component';
import { HomeComponent } from './pages/home/home.component';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { SolicitudesComponent } from './pages/solicitudes/solicitudes.component';
import { PurchasesComponent } from './pages/purchases/purchases.component';
import { PharmaciesComponent } from './pages/pharmacies/pharmacies.component';
import { InvitationEditComponent } from './pages/invitation-edit/invitation-edit.component';
import {InvitationComponent} from "./pages/invitation/invitation.component";

const routes: Routes = [
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full'
  },
  {
    path: 'home',
    component: HomeComponent
  },
  {
    path: 'invitation/:id',
    component: InvitationEditComponent
  },
  {
    path: 'invitation',
    component: InvitationComponent
  },
  {
    path: 'solicitudes',
    component: SolicitudesComponent
  },
  {
    path: 'purchases',
    component: PurchasesComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'pharmacies',
    component: PharmaciesComponent
  },
  {
    path: '**',
    component: NotFoundComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
