import { AddBillComponent } from './modals/add-bill/add-bill.component';
import { MaterialModule } from './../../material.module';
import { NgModule } from '@angular/core';
import { BillsRoutingModule } from './bills-routing.module';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { TranslateModule } from '@ngx-translate/core';
import { BillTypeIconsComponent } from './bill-type-icons/bill-type-icons.component';
import { PayForBillComponent } from './modals/pay-for-bill/pay-for-bill.component';
import { CancelBillPaymentComponent } from './modals/cancel-bill-payment/cancel-bill-payment.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { EditBillComponent } from './modals/edit-bill/edit-bill.component';
import { BillsCalendarComponent } from './bills-calendar/bills-calendar.component';

@NgModule({
  declarations: [
    BillsCalendarComponent,
    BillTypeIconsComponent,
    PayForBillComponent,
    CancelBillPaymentComponent,
    EditBillComponent,
    AddBillComponent,
  ],
  imports: [
    SharedModule,
    CalendarModule.forRoot({
      provide: DateAdapter,
      useFactory: adapterFactory,
    }),
    BillsRoutingModule,
    MaterialModule,
    TranslateModule,
  ],
})
export class BillsModule {}
