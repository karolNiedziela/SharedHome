import { ApiResponse } from 'app/core/models/api-response';
import { BillService } from './../../bills/services/bill.service';
import { BarChartService } from './../../../core/services/charts/bar-chart.service';
import {
  LightTheme,
  DarkTheme,
} from './../../../core/services/theme/theme.constants';
import {
  Component,
  ViewChild,
  OnInit,
  ChangeDetectionStrategy,
} from '@angular/core';
import {
  ChartConfiguration,
  ChartData,
  ChartEvent,
  ChartOptions,
  ChartType,
} from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import * as DataLabelsPlugin from 'chartjs-plugin-datalabels';
import { BillMonthlyCost } from 'app/modules/bills/models/bill-mothly-cost';
import { Observable } from 'rxjs';

type Theme = 'light-theme' | 'dark-theme';

@Component({
  selector: 'app-shopping-lists-expenses',
  templateUrl: './shopping-lists-expenses.component.html',
  styleUrls: ['./shopping-lists-expenses.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ShoppingListsExpensesComponent implements OnInit {
  @ViewChild(BaseChartDirective) chart: BaseChartDirective | undefined;

  public billMonthlyCost$!: Observable<ApiResponse<BillMonthlyCost[]>>;

  public barChartOptions: ChartConfiguration['options'] = {
    responsive: true,
    maintainAspectRatio: false,
    // We use these empty structures as placeholders for dynamic theming.
    scales: {
      x: {},
      y: {},
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
          text: 'Shopping lists',
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
          label: function (context) {
            return `Amount: ${context.formattedValue}`;
          },
        },
      },
    },
    color: 'white',
  };

  public barChartPlugins = [DataLabelsPlugin];

  constructor(
    public barChartService: BarChartService,
    private billService: BillService
  ) {}

  ngOnInit(): void {
    this.billMonthlyCost$ = this.billService.getMonthlyCost(2022);

    this.barChartService.getOptionsColors(this.barChartOptions);
  }

  public getBarChartData(billMonthlyCost: BillMonthlyCost[]): ChartData<'bar'> {
    const barChartData: ChartData<'bar'> = {
      labels: [
        'Styczeń',
        'Luty',
        'Marzec',
        'Kwiecień',
        'Maj',
        'Czerwiec',
        'Lipiec',
        'Sierpień',
        'Wrzesień',
        'Październik',
        'Listopad',
        'Grudzień',
      ],
      datasets: [
        {
          data: billMonthlyCost.map((x) => x.totalCost!),
          label: 'Amount',
        },
      ],
    };

    this.barChartService.cleanDataset(barChartData);
    this.barChartService.getChartaDataColors(barChartData);

    return barChartData;
  }
}
