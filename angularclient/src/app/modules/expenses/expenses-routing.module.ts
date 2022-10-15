import { AuthGuard } from './../../core/guards/auth.guard';
import { ExpensesListingComponent } from './expenses-listing/expenses-listing.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'expenses',
    component: ExpensesListingComponent,
    canActivate: [AuthGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ExpensesRoutingModule {}
