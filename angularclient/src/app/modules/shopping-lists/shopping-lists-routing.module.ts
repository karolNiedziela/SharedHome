import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShoppingListsComponent } from './shopping-lists.component';

const routes: Routes = [
  {
    path: '',
    component: ShoppingListsComponent,
  },
];

@NgModule({
  declarations: [],
  imports: [CommonModule, RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ShoppingListsRoutingModule {}
