import { BillsComponent } from './bills.component';
import { NgModule } from '@angular/core';
import { BillsRoutingModule } from './bills-routing.module';

@NgModule({
  declarations: [BillsComponent],
  imports: [
    BillsRoutingModule
  ],
})
export class BillsModule {}
