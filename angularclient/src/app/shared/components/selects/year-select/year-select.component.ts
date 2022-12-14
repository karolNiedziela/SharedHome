import { UtilityService } from './../../../../core/services/utility.service';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-year-select',
  templateUrl: './year-select.component.html',
  styleUrls: ['./year-select.component.scss']
})
export class YearSelectComponent implements OnInit {

  @Output() changeYearEvent: EventEmitter<number> = new EventEmitter();
  years: number [] = [];
  chosenYear?: number;

  constructor(
    private utilityService : UtilityService
  ) { }

  ngOnInit(): void {
    this.years = this.utilityService.getYearListSince2022();
    this.chosenYear = this.getCurrentYear();
  }

  getCurrentYear() : number {
    return new Date().getFullYear();
  }

  onYearChange(year: number) {
    this.chosenYear = year
    this.changeYearEvent.emit(year);
  }
}
