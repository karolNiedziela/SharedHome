import { TranslateModule } from '@ngx-translate/core';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExpensesRoutingModule } from './expenses-routing.module';
import { ShoppingListsExpensesComponent } from './shopping-lists-expenses/shopping-lists-expenses.component';
import { ExpensesListingComponent } from './expenses-listing/expenses-listing.component';
import { SharedModule } from 'app/shared/shared.module';
import { NgChartsModule } from 'ng2-charts';
import { BillsExpensesComponent } from './bills-expenses/bills-expenses.component';

@NgModule({
  declarations: [ShoppingListsExpensesComponent, ExpensesListingComponent, BillsExpensesComponent],
  imports: [
    CommonModule,
    ExpensesRoutingModule,
    SharedModule,
    NgChartsModule,
    TranslateModule,
  ],
})
export class ExpensesModule {}
