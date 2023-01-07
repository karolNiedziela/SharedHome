import { ShoppingListDetailsComponent } from './shopping-list-details/shopping-list-details.component';
import { AuthGuard } from './../../core/guards/auth.guard';
import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShoppingListsListComponent } from './shopping-lists-list-view/shopping-lists-list/shopping-lists-list.component';

const routes: Routes = [
  {
    path: 'shoppinglists',
    component: ShoppingListsListComponent,
    canActivate: [AuthGuard],
    title: 'Shopping lists',
  },
  {
    path: 'shoppinglists/:shoppingListId',
    component: ShoppingListDetailsComponent,
    title: 'Shopping list',
  },
];

@NgModule({
  declarations: [],
  imports: [CommonModule, RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ShoppingListsRoutingModule {}
