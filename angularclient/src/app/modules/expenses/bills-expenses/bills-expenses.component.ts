import { TranslateService } from '@ngx-translate/core';
import { BillService } from './../../bills/services/bill.service';
import { BillMonthlyCost } from 'app/modules/bills/models/bill-monthly-cost';
import { ApiResponse } from 'app/core/models/api-response';
import { Observable, tap } from 'rxjs';
import { ChangeDetectionStrategy, Component, OnInit, ViewChild } from '@angular/core';
import { BaseChartDirective } from 'ng2-charts';
import * as DataLabelsPlugin from 'chartjs-plugin-datalabels';
import { BarChartService } from 'app/core/services/charts/bar-chart.service';
import { ChartConfiguration, ChartData, Tick } from 'chart.js';

@Component({
  selector: 'app-bills-expenses',
  templateUrl: './bills-expenses.component.html',
  styleUrls: ['./bills-expenses.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class BillsExpensesComponent implements OnInit {
  @ViewChild(BaseChartDirective) chart: BaseChartDirective | undefined;

  public billMonthlyCost$!: Observable<
    ApiResponse<BillMonthlyCost[]>
  >;

  public anyCosts: boolean = false;
  public toTranslateTexts: string[] = ['bills', 'amount'];
  public translatedTexts: Record<string, string> = {};

  public barChartPlugins = [DataLabelsPlugin];

  constructor(
    public barChartService: BarChartService,
    private billService: BillService,
    private translateService: TranslateService
  ) { }

  ngOnInit(): void {
    this.billMonthlyCost$ = this.billService
      .getMonthlyCost(2022) // TODO: I think it should be changed to dynamic. Here and in the shopping-lists-expenses.component also.
      .pipe(
        tap((response: ApiResponse<BillMonthlyCost[]>) => {
          this.anyCosts = response.data.some((x) => x.totalCost > 0);
        })
      );

    this.translateService
      .get(this.toTranslateTexts)
      .subscribe((translations) => {
        this.translatedTexts = translations;
      });
  }

  public getBarChartOptions(
    billMonthlyCost: BillMonthlyCost[]
  ): ChartConfiguration['options'] {
    const currency: string = billMonthlyCost.filter(
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
            text: this.translatedTexts['bills'],
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
              `${this.translatedTexts['amount']}: ${context.formattedValue}${currency}`,
          },
        },
      },
    };

    this.barChartService.getOptionsColors(barChartOptions);

    return barChartOptions;
  }

  public getBarChartData(
    billMonthlyCost: BillMonthlyCost[]
  ): ChartData<'bar'> {
    const barChartData: ChartData<'bar'> = {
      labels: billMonthlyCost.map((x) => x.monthName),
      datasets: [
        {
          data: billMonthlyCost.map((x) => x.totalCost!),
          label: `${this.translatedTexts['amount']}`,
        },
      ],
    };

    this.barChartService.cleanDataset(barChartData);
    this.barChartService.getChartaDataColors(barChartData);

    return barChartData;
  }
}
