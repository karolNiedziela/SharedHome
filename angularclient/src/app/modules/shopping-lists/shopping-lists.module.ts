import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';
import { ShoppingListsComponent } from './shopping-lists.component';
import { ShoppingListsRoutingModule } from './shopping-lists-routing.module';
import { AddShoppingListComponent } from './forms/add-shopping-list/add-shopping-list.component';

@NgModule({
  declarations: [ShoppingListsComponent, AddShoppingListComponent],
  imports: [SharedModule, ShoppingListsRoutingModule],
})
export class ShoppingListsModule {}
