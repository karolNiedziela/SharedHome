import { CalendarEvent } from './../../../../node_modules/calendar-utils/calendar-utils.d';
import { Component, OnInit } from '@angular/core';
import { CalendarView, DAYS_OF_WEEK } from 'angular-calendar';
import { Subject } from 'rxjs';
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

@Component({
  selector: 'app-bills',
  templateUrl: './bills.component.html',
  styleUrls: ['./bills.component.scss'],
})
export class BillsComponent implements OnInit {
  view: CalendarView = CalendarView.Month;
  calendarEvents: CalendarEvent[] = [
    {
      start: subDays(new Date(), 2),
      end: subDays(new Date(), 2),
      title: 'A 3 day event',
      color: { ...colors.red },
      // actions: this.actions,
      allDay: true,
      resizable: {
        beforeStart: true,
        afterEnd: true,
      },
      draggable: true,
    },
    {
      start: new Date(),
      end: new Date(),
      title: 'An event with no end date',
      color: { ...colors.yellow },
      // actions: this.actions,
    },
    {
      start: subDays(new Date(), 1),
      end: subDays(new Date(), 1),
      title: 'A long event that spans 2 months',
      color: { ...colors.blue },
      allDay: true,
    },
    {
      start: new Date(),
      end: new Date(),
      title: 'A draggable and resizable event',
      color: { ...colors.yellow },
      // actions: this.actions,
      resizable: {
        beforeStart: true,
        afterEnd: true,
      },
      draggable: true,
    },
  ];

  dayEvents: CalendarEvent[] = [];

  CalendarView = CalendarView;

  viewDate: Date = new Date();

  weekStartsOn: number = DAYS_OF_WEEK.MONDAY;

  refresh = new Subject<void>();

  activeDayIsOpen: boolean = true;

  constructor() {}

  ngOnInit(): void {}

  dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
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
    console.log(date);
    this.dayEvents = this.calendarEvents.filter(
      (x) => x.start >= startOfDay(date) && x.end! < endOfDay(date)
    );
  }
}
