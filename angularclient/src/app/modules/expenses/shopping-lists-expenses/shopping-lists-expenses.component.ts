import { TranslateService } from '@ngx-translate/core';
import { ShoppingListsService } from './../../shopping-lists/services/shopping-lists.service';
import { ShoppingListMonthlyCost } from './../../shopping-lists/models/shopping-list-monthly-cost';
import { BarChartService } from './../../../core/services/charts/bar-chart.service';
import {
  Component,
  ViewChild,
  OnInit,
  ChangeDetectionStrategy,
} from '@angular/core';
import { ChartConfiguration, ChartData, Tick } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { Observable, tap } from 'rxjs';
import { ApiResponse } from 'src/app/core/models/api-response';
import ChartDataLabels from 'chartjs-plugin-datalabels';

@Component({
  selector: 'app-shopping-lists-expenses',
  templateUrl: './shopping-lists-expenses.component.html',
  styleUrls: ['../expenses-list/expenses-list.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ShoppingListsExpensesComponent implements OnInit {
  @ViewChild(BaseChartDirective) chart: BaseChartDirective | undefined;

  public shoppingListMonthlyCost$!: Observable<
    ApiResponse<ShoppingListMonthlyCost[]>
  >;

  public anyCosts: boolean = false;
  public toTranslateTexts: string[] = [
    'shopping_lists.module',
    'expenses.module',
  ];
  public translatedTexts: Record<string, string> = {};

  public barChartPlugins = [ChartDataLabels];

  public barChartData!: ChartData<'bar'>;

  public barChartConfiguration!: ChartConfiguration['options'];

  chosenYear?: number;

  constructor(
    public barChartService: BarChartService,
    private shoppingListService: ShoppingListsService,
    private translateService: TranslateService
  ) {}

  ngOnInit(): void {
    this.chosenYear = new Date().getFullYear();
    this.shoppingListMonthlyCost$ = this.getShoppingListMonthlyCost(
      this.chosenYear
    );

    this.translateService
      .get(this.toTranslateTexts)
      .subscribe((translations) => {
        this.translatedTexts = translations;
      });
  }

  getShoppingListMonthlyCost(
    year: number
  ): Observable<ApiResponse<ShoppingListMonthlyCost[]>> {
    return this.shoppingListService.getMonthlyCostByYear(year).pipe(
      tap((response: ApiResponse<ShoppingListMonthlyCost[]>) => {
        this.getBarChartData(response.data);
        this.anyCosts = response.data.some((x) => x.totalCost > 0);
      })
    );
  }

  setDataOnYearChange(year: number): void {
    this.chosenYear = year;
    this.shoppingListMonthlyCost$ = this.getShoppingListMonthlyCost(year);
  }

  public getBarChartOptions(
    shoppingListMonthlyCost: ShoppingListMonthlyCost[]
  ): ChartConfiguration['options'] {
    const currency: string = shoppingListMonthlyCost.filter(
      (x) => x.currency != ''
    )[0].currency;
    let barChartOptions: ChartConfiguration['options'] = {
      responsive: true,
      maintainAspectRatio: false,
      // We use these empty structures as placeholders for dynamic theming.
      scales: {
        x: {},
        y: {
          ticks: {
            callback(tickValue: any, index: number, ticks: Tick[]) {
              return tickValue.toFixed(2) + currency;
            },
          },
        },
      },
      plugins: {
        legend: {
          display: true,
          labels: {
            filter: function (item: any, chart: any): any {
              // Skip to hide label above chart
              return item.index > 0;
            },
          },
          title: {
            display: true,
            text:
              this.translatedTexts['shopping_lists.module'] +
              ' [' +
              this.chosenYear +
              ']',
            font: {
              size: 24,
            },
          },
        },
        datalabels: {
          labels: {
            title: null,
          },
          anchor: 'end',
          align: 'end',
        },
        tooltip: {
          displayColors: false,
          callbacks: {
            label: (context) =>
              `${this.translatedTexts['expenses.module']}: ${context.formattedValue}${currency}`,
          },
        },
      },
    };

    this.barChartService.getOptionsColors(barChartOptions);

    return barChartOptions;
  }

  public getBarChartData(shoppingListMonthlyCost: ShoppingListMonthlyCost[]) {
    const barChartData: ChartData<'bar'> = {
      labels: shoppingListMonthlyCost.map((x) => x.monthName),
      datasets: [
        {
          data: shoppingListMonthlyCost.map((x) => x.totalCost!),
          label: `${this.translatedTexts['expenses.module']}`,
        },
      ],
    };

    this.barChartService.cleanDataset(barChartData);
    this.barChartService.getChartaDataColors(barChartData);

    this.barChartData = barChartData;
  }
}
