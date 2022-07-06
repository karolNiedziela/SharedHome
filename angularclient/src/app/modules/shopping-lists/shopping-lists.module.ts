import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';
import { ShoppingListsComponent } from './shopping-lists.component';
import { ShoppingListsRoutingModule } from './shopping-lists-routing.module';

@NgModule({
  declarations: [ShoppingListsComponent],
  imports: [SharedModule, ShoppingListsRoutingModule],
})
export class ShoppingListsModule {}
