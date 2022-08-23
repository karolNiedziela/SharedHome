import { BillsComponent } from './bills.component';
import { NgModule } from '@angular/core';
import { BillsRoutingModule } from './bills-routing.module';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { SharedModule } from 'app/shared/shared.module';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
  declarations: [BillsComponent],
  imports: [
    CalendarModule.forRoot({
      provide: DateAdapter,
      useFactory: adapterFactory,
    }),
    BillsRoutingModule,
    SharedModule,
    TranslateModule,
  ],
})
export class BillsModule {}
