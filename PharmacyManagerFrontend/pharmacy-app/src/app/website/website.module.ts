import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { WebsiteRoutingModule } from './website-routing.module';
import { LayoutComponent } from './components/layout/layout.component';
import { NavComponent } from './components/nav/nav.component';
import {LoginComponent} from "./components/login/login.component";
import {PurchasesComponent} from "./pages/purchases/purchases.component";
import {MyCartComponent} from "./pages/my-cart/my-cart.component";
import { FormsModule } from '@angular/forms';
import { SignupComponent } from './components/signup/signup.component';
import {DrugsComponent} from "./pages/drugs/drugs.component";
import {DrugComponent} from "./components/drug/drug.component";


@NgModule({
    declarations: [
        LayoutComponent,
        NavComponent,
        LoginComponent,
        MyCartComponent,
        PurchasesComponent,
        SignupComponent,
        DrugsComponent,
        DrugComponent
    ],
    imports: [
        CommonModule,
        WebsiteRoutingModule, 
        FormsModule
    ],
})
export class WebsiteModule { }
