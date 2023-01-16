import { TranslateModule } from '@ngx-translate/core';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExpensesRoutingModule } from './expenses-routing.module';
import { NgChartsModule } from 'ng2-charts';
import { SharedModule } from 'src/app/shared/shared.module';
import { ExpensesListComponent } from './expenses-list/expenses-list.component';
import { ShoppingListsExpensesComponent } from './shopping-lists-expenses/shopping-lists-expenses.component';
import { BillsExpensesComponent } from './bills-expenses/bills-expenses.component';

@NgModule({
  declarations: [
    ExpensesListComponent,
    ShoppingListsExpensesComponent,
    BillsExpensesComponent,
  ],
  imports: [
    CommonModule,
    ExpensesRoutingModule,
    SharedModule,
    NgChartsModule,
    TranslateModule,
  ],
})
export class ExpensesModule {}
