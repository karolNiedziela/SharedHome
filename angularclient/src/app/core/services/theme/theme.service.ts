import { DOCUMENT } from '@angular/common';
import { Inject, Injectable, Renderer2, RendererFactory2 } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ThemeService {
  private static key: string = 'sharedhome-theme';

  private renderer!: Renderer2;

  public static darkThemeClass: string = 'theme-dark';
  public static lightThemeClass: string = 'theme-light';

  public isDark: boolean = true;

  public get theme(): string {
    const theme: string | null = localStorage.getItem(ThemeService.key);
    if (theme) {
      return theme;
    }

    return this.isDark
      ? ThemeService.darkThemeClass
      : ThemeService.lightThemeClass;
  }

  constructor(
    @Inject(DOCUMENT) private document: Document,
    private rendererFactory: RendererFactory2
  ) {
    this.renderer = rendererFactory.createRenderer(null, null);
  }

  public setTheme(): void {
    this.renderer.addClass(this.document.body, this.theme);
  }

  public switchTheme(): void {
    const previousTheme = this.theme
      ? ThemeService.darkThemeClass
      : ThemeService.lightThemeClass;
    this.renderer.removeClass(this.document.body, previousTheme);
    const newTheme =
      this.theme == ThemeService.darkThemeClass
        ? ThemeService.lightThemeClass
        : ThemeService.darkThemeClass;
    this.renderer.addClass(this.document.body, newTheme);
    localStorage.setItem(ThemeService.key, newTheme);
    this.isDark = !this.isDark;
  }
}
