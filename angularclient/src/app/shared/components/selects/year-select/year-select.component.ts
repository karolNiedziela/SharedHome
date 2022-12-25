import { DateHelper } from './../../../helpers/date-helper';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-year-select',
  templateUrl: './year-select.component.html',
  styleUrls: ['../select.scss', './year-select.component.scss'],
})
export class YearSelectComponent implements OnInit {
  @Output() changeYearEvent: EventEmitter<number> = new EventEmitter();
  years: number[] = [];
  chosenYear?: number;

  constructor() {}

  ngOnInit(): void {
    this.years = DateHelper.getYearListSince2022();
    this.chosenYear = this.getCurrentYear();
  }

  getCurrentYear(): number {
    return new Date().getFullYear();
  }

  onYearChange(year: number) {
    this.chosenYear = year;
    this.changeYearEvent.emit(year);
  }
}
