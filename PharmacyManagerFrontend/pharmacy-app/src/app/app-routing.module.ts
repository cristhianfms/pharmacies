import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterComponent } from './pages/register/register.component';
import { HomeComponent } from './pages/home/home.component';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { SolicitudesComponent } from './pages/solicitudes/solicitudes.component';
import { PurchasesComponent } from './pages/purchases/purchases.component';
import {AdminGuard} from "./guards/admin.guard";
import {EmployeeGuard} from "./guards/employee.guard";
import {OwnerGuard} from "./guards/owner.guard";

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
    path: '',
    loadChildren: () => import('./website/website.module').then(m => m.WebsiteModule),
    data: {
      preload: true,
    }
  },
  {
    path: 'admin',
    canActivate: [AdminGuard],
    loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule),
  },
  {
    path: 'employee',
    canActivate: [EmployeeGuard],
    loadChildren: () => import('./employee/employee.module').then(m => m.EmployeeModule),
  },
  {
    path: 'owner',
    canActivate: [OwnerGuard],
    loadChildren: () => import('./owner/owner.module').then(m => m.OwnerModule),
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
