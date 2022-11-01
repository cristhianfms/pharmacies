import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {LayoutComponent} from "./components/layout/layout.component";
import {DrugsComponent} from "./pages/drugs/drugs.component";
import {PurchasesComponent} from "./pages/purchases/purchases.component";


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
                path: 'catalog',
                component: DrugsComponent
            },
            {
                path: 'purchases',
                component: PurchasesComponent
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
