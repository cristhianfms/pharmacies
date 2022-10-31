import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { WebsiteRoutingModule } from './website-routing.module';
import { LayoutComponent } from './components/layout/layout.component';
import { NavComponent } from './components/nav/nav.component';
import {LoginComponent} from "./components/login/login.component";
import {DrugComponent} from "./components/drug/drug.component";
import {HomeComponent} from "./pages/home/home.component";
import {PurchasesComponent} from "./pages/purchases/purchases.component";
import {MyCartComponent} from "./pages/my-cart/my-cart.component";
import {DrugsComponent} from "./components/drugs/drugs.component";
import { FormsModule } from '@angular/forms';


//Todo: INCLUIR HOME PAGE PERO CAMBIAR EL NOMBRE A DRUG LISTS!

@NgModule({
    declarations: [
        LayoutComponent,
        NavComponent,
        LoginComponent,
        DrugComponent,
        DrugsComponent,
        MyCartComponent,
        HomeComponent,
        PurchasesComponent
    ],
    imports: [
        CommonModule,
        WebsiteRoutingModule, 
        FormsModule
    ]
})
export class WebsiteModule { }
