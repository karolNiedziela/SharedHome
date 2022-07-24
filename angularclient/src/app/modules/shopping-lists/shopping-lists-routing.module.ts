import { ShoppingListComponent } from './shopping-list-details/shopping-list-details.component';

import { AuthGuard } from './../../core/guards/auth.guard';
import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShoppingListsComponent } from './shopping-lists-list-view/shopping-lists-list/shopping-lists-list.component';

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
