import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ScreenSizeHelper {
  public static XS: number = 380;
  public static SM: number = 576;
  public static MD: number = 768;
  public static LG: number = 992;
  public static XL: number = 1200;
  public static XXL: number = 1400;

  constructor() {}

  public isXS(): boolean {
    const width: number = this.getWidth();

    return width < ScreenSizeHelper.XS;
  }

  public isSM(): boolean {
    const width: number = this.getWidth();
    return width >= ScreenSizeHelper.SM && width <= ScreenSizeHelper.MD;
  }

  public isLowerThanSM(): boolean {
    const width: number = this.getWidth();

    return width <= ScreenSizeHelper.SM;
  }

  public isMD(): boolean {
    const width: number = this.getWidth();

    return width >= ScreenSizeHelper.MD && width <= ScreenSizeHelper.LG;
  }

  public isLowerThanMD(): boolean {
    const width: number = this.getWidth();

    return width <= ScreenSizeHelper.MD;
  }

  public isLG(): boolean {
    const width: number = this.getWidth();
    return width >= ScreenSizeHelper.LG && width <= ScreenSizeHelper.XL;
  }

  public isXL(): boolean {
    const width: number = this.getWidth();
    return width >= ScreenSizeHelper.XL && width <= ScreenSizeHelper.XXL;
  }

  public isXXL(): boolean {
    const width: number = this.getWidth();
    return width >= ScreenSizeHelper.XXL;
  }

  public isMobile(): boolean {
    const width: number = this.getWidth();
    return width <= ScreenSizeHelper.SM;
  }

  public getWidth(): number {
    return (
      window.innerWidth ||
      document.documentElement.clientWidth ||
      document.body.clientWidth
    );
  }
}
