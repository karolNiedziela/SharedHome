import { BarChartService } from './../../../core/services/charts/bar-chart.service';
import {
  LightTheme,
  DarkTheme,
} from './../../../core/services/theme/theme.constants';
import { Component, ViewChild, OnInit } from '@angular/core';
import {
  ChartConfiguration,
  ChartData,
  ChartEvent,
  ChartOptions,
  ChartType,
} from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import * as DataLabelsPlugin from 'chartjs-plugin-datalabels';

type Theme = 'light-theme' | 'dark-theme';

@Component({
  selector: 'app-shopping-lists-expenses',
  templateUrl: './shopping-lists-expenses.component.html',
  styleUrls: ['./shopping-lists-expenses.component.scss'],
})
export class ShoppingListsExpensesComponent implements OnInit {
  @ViewChild(BaseChartDirective) chart: BaseChartDirective | undefined;

  public barChartOptions: ChartConfiguration['options'] = {
    responsive: true,

    // We use these empty structures as placeholders for dynamic theming.
    scales: {
      x: {
        ticks: {
          color: 'white',
        },
      },
      y: {
        min: 10,
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
            let label = `Amount: ${context.formattedValue}`;
            return label;
          },
        },
      },
    },
    color: 'white',
  };

  constructor(public barChartService: BarChartService) {}

  ngOnInit(): void {
    this.barChartOptions = this.barChartService.getOptionsColors(
      this.barChartOptions
    );

    let datasetsData = this.barChartData!.datasets[0].data;
    let datasetsLabels = this.barChartData!.labels!;

    for (let i = 0; i <= datasetsData.length; i++) {
      if (datasetsData[i] === 0) {
        datasetsData.splice(i, 1);
        datasetsLabels.splice(i, 1);
        i--;
      }
    }

    this.barChartData!.datasets[0].data = datasetsData;
    this.barChartData!.labels = datasetsLabels;
  }

  public barChartPlugins = [DataLabelsPlugin];

  public barChartData: ChartData<'bar'> = {
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
        data: [65, 0, 80, 81, 0, 55, 40, 70, 34, 75, 62, 47],
        label: 'Amount',
        backgroundColor: DarkTheme.primaryColor,
      },
    ],
  };

  public chartClicked({
    event,
    active,
  }: {
    event?: ChartEvent;
    active?: {}[];
  }): void {
    console.log(event, active);
  }

  public chartHovered({
    event,
    active,
  }: {
    event?: ChartEvent;
    active?: {}[];
  }): void {}
}
