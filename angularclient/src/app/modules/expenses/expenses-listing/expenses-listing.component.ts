import { Component, OnInit } from '@angular/core';
import { ChartOptions } from 'chart.js';
import { ThemeService } from 'ng2-charts';

type Theme = 'light-theme' | 'dark-theme';

@Component({
  selector: 'app-expenses-listing',
  templateUrl: './expenses-listing.component.html',
  styleUrls: ['./expenses-listing.component.scss'],
})
export class ExpensesListingComponent {}
