import { PopupMenuConfig } from './../../shared/components/menus/popup-menu/popup-menu.config';
import { ColumnSetting } from './../../shared/components/tables/column-setting';
import { BillEvent } from './models/bill-event';
import { ApiResponse } from 'app/core/models/api-response';
import { BillService } from './services/bill.service';
import { CalendarEvent } from './../../../../node_modules/calendar-utils/calendar-utils.d';
import { Component, OnInit } from '@angular/core';
import { CalendarView, DAYS_OF_WEEK } from 'angular-calendar';
import { Subject, Observable, map, of } from 'rxjs';
import { startOfDay, endOfDay, isSameDay, isSameMonth } from 'date-fns';
import { Bill } from './models/bill';
import { CellPipeFormat } from 'app/shared/components/tables/cell-pipe-format';

@Component({
  selector: 'app-bills',
  templateUrl: './bills.component.html',
  styleUrls: ['./bills.component.scss'],
})
export class BillsComponent implements OnInit {
  events$!: Observable<BillEvent[]>;
  view: CalendarView = CalendarView.Month;

  currentDayEvents: CalendarEvent[] = [];

  CalendarView = CalendarView;

  viewDate: Date = new Date();

  weekStartsOn: number = DAYS_OF_WEEK.MONDAY;

  refresh = new Subject<void>();

  activeDayIsOpen: boolean = true;

  columnSettings: ColumnSetting[] = [
    {
      propertyName: 'serviceProvider',
      header: 'Service Provider',
    },
    {
      propertyName: 'dateOfPayment',
      header: 'Date of Payment',
      format: CellPipeFormat.DATE,
    },
    {
      propertyName: 'billType',
      header: 'Type',
    },
    {
      propertyName: 'cost',
      header: 'Cost',
      format: CellPipeFormat.MONEY,
    },
    {
      propertyName: 'isPaid',
      header: 'Paid',
    },
  ];

  tablePopupMenuConfig: PopupMenuConfig = {
    additionalPopupMenuItems: [
      {
        text: 'Test',
        onClick: () => {
          alert('hello');
        },
      },
    ],
  };

  constructor(private billService: BillService) {}

  ngOnInit(): void {
    this.events$ = this.billService.getAllByYearAndMonthAndIsDone(2022, 9).pipe(
      map((response: ApiResponse<Bill[]>) => {
        const billEvents: BillEvent[] = response.data.map((bill: Bill) => {
          const billEvent: BillEvent = {
            id: bill.id,
            start: new Date(Date.parse(bill.dateOfPayment.toString())),
            end: new Date(Date.parse(bill.dateOfPayment.toString())),
            title: bill.serviceProvider,
            isPaid: bill.isPaid,
            serviceProvider: bill.serviceProvider,
            dateOfPayment: bill.dateOfPayment,
            billType: bill.billType,
            cost:
              bill.cost == null
                ? null
                : {
                    price: bill.cost.price,
                    currency: bill.cost.currency,
                  },
          };

          return billEvent;
        });

        this.dayClicked(new Date(Date.now()), billEvents);
        return billEvents;
      })
    );
  }

  dayClicked(date: Date, events: BillEvent[]): void {
    if (isSameMonth(date, this.viewDate)) {
      if (
        (isSameDay(this.viewDate, date) && this.activeDayIsOpen === true) ||
        events.length === 0
      ) {
        this.activeDayIsOpen = false;
      } else {
        this.activeDayIsOpen = true;
      }
      this.viewDate = date;
    }

    this.currentDayEvents = events.filter(
      (x) => x.start >= startOfDay(date) && x.end! < endOfDay(date)
    );
  }

  getHeaders(events: BillEvent[]): string[] {
    return ['test', 'test2'];
  }

  getColumns(events: BillEvent[]): string[] {
    return events.map((x) => x.serviceProvider);
  }
}
