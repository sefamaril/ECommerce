import { NgModule } from '@angular/core';
import { StoreComponent } from './store.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', component: StoreComponent },
  { path: ':id', component: ProductDetailsComponent }
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class StoreRoutingModule { }
