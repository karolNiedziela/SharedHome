import { CalendarEvent } from './../../../../node_modules/calendar-utils/calendar-utils.d';
import { Component, OnInit } from '@angular/core';
import { CalendarView, DAYS_OF_WEEK } from 'angular-calendar';

@Component({
  selector: 'app-bills',
  templateUrl: './bills.component.html',
  styleUrls: ['./bills.component.scss'],
})
export class BillsComponent implements OnInit {
  view: CalendarView = CalendarView.Month;
  events: CalendarEvent[] = [];

  CalendarView = CalendarView;

  viewDate: Date = new Date();

  weekStartsOn: number = DAYS_OF_WEEK.MONDAY;

  constructor() {}

  ngOnInit(): void {}
}
