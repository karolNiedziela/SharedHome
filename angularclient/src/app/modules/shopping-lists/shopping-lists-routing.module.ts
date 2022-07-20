import { ShoppingListComponent } from './shopping-list/shopping-list.component';
import { ShoppingList } from './models/shopping-list';
import { AuthGuard } from './../../core/guards/auth.guard';
import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShoppingListsComponent } from './shopping-lists.component';

const routes: Routes = [
  {
    path: 'shoppinglists',
    component: ShoppingListsComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'shoppinglists/:shoppingListId',
    component: ShoppingListComponent,
  },
];

@NgModule({
  declarations: [],
  imports: [CommonModule, RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ShoppingListsRoutingModule {}
