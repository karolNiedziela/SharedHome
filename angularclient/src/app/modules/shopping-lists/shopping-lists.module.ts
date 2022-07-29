import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';
import { ShoppingListsRoutingModule } from './shopping-lists-routing.module';
import { AddShoppingListComponent } from './forms/add-shopping-list/add-shopping-list.component';
import { ShoppingListComponent } from './shopping-list-details/shopping-list-details.component';
import { ShoppingListsComponent } from './shopping-lists-list-view/shopping-lists-list/shopping-lists-list.component';
import { SingleShoppingListComponent } from './shopping-lists-list-view/single-shopping-list/single-shopping-list.component';
import { ShoppingListProductComponent } from './shopping-list-product/shopping-list-product.component';
import { AddShoppingListProductComponent } from './forms/add-shopping-list-product/add-shopping-list-product.component';

@NgModule({
  declarations: [
    ShoppingListsComponent,
    AddShoppingListComponent,
    ShoppingListComponent,
    SingleShoppingListComponent,
    ShoppingListProductComponent,
    AddShoppingListProductComponent,
  ],
  imports: [SharedModule, ShoppingListsRoutingModule],
})
export class ShoppingListsModule {}
