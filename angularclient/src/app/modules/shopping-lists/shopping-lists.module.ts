import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';
import { ShoppingListsRoutingModule } from './shopping-lists-routing.module';
import { ShoppingListComponent } from './shopping-list-details/shopping-list-details.component';
import { ShoppingListsComponent } from './shopping-lists-list-view/shopping-lists-list/shopping-lists-list.component';
import { SingleShoppingListComponent } from './shopping-lists-list-view/single-shopping-list/single-shopping-list.component';
import { ShoppingListProductComponent } from './shopping-list-product/shopping-list-product.component';
import { AddShoppingListComponent } from './modals/add-shopping-list/add-shopping-list.component';
import { AddShoppingListProductComponent } from './modals/add-shopping-list-product/add-shopping-list-product.component';
import { PurchaseShoppingListProductComponent } from './modals/purchase-shopping-list-product/purchase-shopping-list-product.component';
import { AddShoppingListProductFormComponent } from './forms/add-shopping-list-product-form/add-shopping-list-product-form.component';
import { AddManyShoppingListProductsFormComponent } from './forms/add-many-shopping-list-products-form/add-many-shopping-list-products-form.component';

@NgModule({
  declarations: [
    ShoppingListsComponent,
    AddShoppingListComponent,
    ShoppingListComponent,
    SingleShoppingListComponent,
    ShoppingListProductComponent,
    AddShoppingListProductComponent,
    PurchaseShoppingListProductComponent,
    AddShoppingListProductFormComponent,
    AddManyShoppingListProductsFormComponent,
  ],
  imports: [SharedModule, ShoppingListsRoutingModule, TranslateModule],
})
export class ShoppingListsModule {}
