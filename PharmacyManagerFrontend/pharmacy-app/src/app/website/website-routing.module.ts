import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {LayoutComponent} from "./components/layout/layout.component";
import {LoginComponent } from './components/login/login.component';
import {SignupComponent } from './components/signup/signup.component';
import {DrugsComponent} from "./pages/drugs/drugs.component";
import {PurchasesComponent} from "./pages/purchases/purchases.component";
import {MyCartComponent} from "./pages/my-cart/my-cart.component";


const routes: Routes = [
    {
        path: '',
        component: LayoutComponent,
        children: [
             {
                path: '',
                redirectTo: 'catalog',
                pathMatch: 'full'
            },
            {
                path: 'login',
                component: LoginComponent
             },
             {
                path: 'signup',
                component: SignupComponent
             },
             {
                path: 'catalog',
                component: DrugsComponent
            },
            {
                path: 'purchases',
                component: PurchasesComponent
            },
            {
                path: 'cart',
                component: MyCartComponent
            },
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class WebsiteRoutingModule {
}
