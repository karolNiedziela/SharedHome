import { AuthGuard } from './../../core/guards/auth.guard';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ExpensesListComponent } from './expenses-list/expenses-list.component';

const routes: Routes = [
  {
    path: 'expenses',
    component: ExpensesListComponent,
    canActivate: [AuthGuard],
    title: 'Expenses',
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ExpensesRoutingModule {}
