import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExpensesRoutingModule } from './expenses-routing.module';
import { ShoppingListsExpensesComponent } from './shopping-lists-expenses/shopping-lists-expenses.component';
import { ExpensesListingComponent } from './expenses-listing/expenses-listing.component';
import { SharedModule } from 'app/shared/shared.module';
import { NgChartsModule } from 'ng2-charts';

@NgModule({
  declarations: [ShoppingListsExpensesComponent, ExpensesListingComponent],
  imports: [CommonModule, ExpensesRoutingModule, SharedModule, NgChartsModule],
})
export class ExpensesModule {}
