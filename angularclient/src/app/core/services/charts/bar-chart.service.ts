import { LightTheme, BaseTheme, DarkTheme } from './../theme/theme.constants';
import { Injectable } from '@angular/core';
import { ThemeService } from '../theme/theme.service';
import { Chart, ChartData, ChartType } from 'chart.js';

@Injectable({
  providedIn: 'root',
})
export class BarChartService {
  public readonly barChartType: ChartType = 'bar';

  constructor(private themeService: ThemeService) {}

  public getOptionsColors(barChartOptions: any) {
    if (this.themeService.theme == 'light') {
      barChartOptions.color = BaseTheme.blackColor;

      barChartOptions.scales.x.ticks = {
        color: BaseTheme.blackColor,
      };
      barChartOptions.scales.x.grid = {
        color: BaseTheme.greyColor,
      };

      barChartOptions.scales.y.ticks.color = BaseTheme.blackColor;
      barChartOptions.scales.y.grid = {
        color: BaseTheme.greyColor,
      };

      return;
    }

    barChartOptions.color = BaseTheme.whiteColor;

    barChartOptions.scales.x.ticks = {
      color: BaseTheme.whiteColor,
    };
    barChartOptions.scales.x.grid = {
      color: BaseTheme.lightGrey,
    };

    barChartOptions.scales.y.ticks.color = BaseTheme.whiteColor;
    barChartOptions.scales.y.grid = {
      color: BaseTheme.lightGrey,
    };
  }

  public getChartaDataColors(chartData: ChartData<'bar'>) {
    if (this.themeService.theme == 'light') {
      chartData.datasets.map(
        (x) => (x.backgroundColor = LightTheme.primaryColor)
      );
      return;
    }

    chartData.datasets.map((x) => (x.backgroundColor = DarkTheme.primaryColor));
  }

  public cleanDataset(chartData: ChartData<'bar'>) {
    let datasetsData = chartData.datasets[0].data;
    let datasetsLabels = chartData!.labels!;

    for (let i = 0; i <= datasetsData.length; i++) {
      if (datasetsData[i] === 0) {
        datasetsData.splice(i, 1);
        datasetsLabels.splice(i, 1);
        i--;
      }
    }

    chartData!.datasets[0].data = datasetsData;
    chartData!.labels = datasetsLabels;
  }
}
