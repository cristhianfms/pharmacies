import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DrugComponent } from './components/drug/drug.component';
import { DrugsComponent } from './components/drugs/drugs.component';
import { NavComponent } from './components/nav/nav.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './components/login/login.component';
import { MyCartComponent } from './pages/my-cart/my-cart.component';
import { RegisterComponent } from './pages/register/register.component';
import { HomeComponent } from './pages/home/home.component';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { SolicitudesComponent } from './pages/solicitudes/solicitudes.component';
import { PurchasesComponent } from './pages/purchases/purchases.component';
import { PharmaciesComponent } from './pages/pharmacies/pharmacies.component';
import { InvitationEditComponent } from './pages/invitation-edit/invitation-edit.component';
import { InvitationComponent } from './pages/invitation/invitation.component';

@NgModule({
  declarations: [
    AppComponent,
    DrugComponent,
    DrugsComponent,
    NavComponent,
    LoginComponent,
    MyCartComponent,
    RegisterComponent,
    HomeComponent,
    NotFoundComponent,
    SolicitudesComponent,
    PurchasesComponent,
    PharmaciesComponent,
    InvitationEditComponent,
    InvitationComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
