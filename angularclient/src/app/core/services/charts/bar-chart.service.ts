import { LightTheme, BaseTheme } from './../theme/theme.constants';
import { Injectable } from '@angular/core';
import { ThemeService } from '../theme/theme.service';
import { ChartType } from 'chart.js';

@Injectable({
  providedIn: 'root',
})
export class BarChartService {
  public readonly barChartType: ChartType = 'bar';

  constructor(private themeService: ThemeService) {}

  public getOptionsColors(barChartOptions: any): any {
    if (this.themeService.theme == 'light') {
      barChartOptions.color = BaseTheme.blackColor;

      barChartOptions.scales.x.barChartScales.x.grid = {
        color: BaseTheme.greyColor,
      };

      barChartOptions.scales.y.grid = {
        color: BaseTheme.greyColor,
      };

      return barChartOptions;
    }

    barChartOptions.color = BaseTheme.whiteColor;

    barChartOptions.scales.x.ticks.color = BaseTheme.whiteColor;
    barChartOptions.scales.x.grid = {
      color: BaseTheme.whiteColor,
    };

    barChartOptions.scales.y.ticks = {
      color: BaseTheme.whiteColor,
    };
    barChartOptions.scales.y.grid = {
      color: BaseTheme.whiteColor,
    };

    return barChartOptions;
  }
}
