import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { DateHelper } from 'src/app/shared/helpers/date-helper';

@Component({
  selector: 'app-year-select',
  templateUrl: './year-select.component.html',
  styleUrls: ['./year-select.component.scss', '../select.scss'],
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
