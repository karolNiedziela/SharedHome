import { BillEvent } from './models/bill-event';
import { ApiResponse } from 'app/core/models/api-response';
import { BillService } from './services/bill.service';
import { CalendarEvent } from './../../../../node_modules/calendar-utils/calendar-utils.d';
import { Component, OnInit } from '@angular/core';
import {
  CalendarMonthViewDay,
  CalendarView,
  DAYS_OF_WEEK,
} from 'angular-calendar';
import { Subject, Observable, map, of } from 'rxjs';
import { colors } from 'app/shared/utils/colors';
import {
  startOfDay,
  endOfDay,
  subDays,
  addDays,
  endOfMonth,
  isSameDay,
  isSameMonth,
  addHours,
} from 'date-fns';
import { Bill } from './models/bill';

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
            cost: bill.cost,
            currency: bill.currency,
          };

          return billEvent;
        });
        return billEvents;
      })
    );
  }

  dayClicked(
    calendarMonthViewDay: CalendarMonthViewDay,
    events: BillEvent[]
  ): void {
    console.log(calendarMonthViewDay);
    const date: Date = calendarMonthViewDay.date;
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
}
