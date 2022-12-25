import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ThemeService {
  private static key: string = 'sharedhome-theme';
  private stylesheet!: HTMLLinkElement;

  public static isDefaultDark: boolean = window.matchMedia(
    '(prefers-color-scheme: dark)'
  ).matches;

  public get theme(): string {
    const theme: string | null = localStorage.getItem(ThemeService.key);
    if (theme) {
      return theme;
    }

    return ThemeService.isDefaultDark ? 'dark' : 'light';
  }

  constructor() {}

  public setTheme(theme?: string): void {
    const link = this.getExistingLinkElementByKey();
    link?.remove();

    this.stylesheet = document.createElement('link');
    this.stylesheet.rel = 'stylesheet';
    this.stylesheet.href = `/${this.theme}.css`;

    if (theme && theme.length > 0) {
      this.stylesheet.href = `/${theme}.css`;
      localStorage.setItem(ThemeService.key, theme);
    } else {
      this.stylesheet.href = `/${this.theme}.css`;
      localStorage.setItem(ThemeService.key, this.theme);
    }

    document.head.appendChild(this.stylesheet);
  }

  public switchTheme(): void {
    if (this.theme === 'light') {
      this.setTheme('dark');
    } else {
      this.setTheme('light');
    }
  }

  private getExistingLinkElementByKey(): HTMLLinkElement {
    return document.head.querySelector(
      `[href='/${this.theme}.css']`
    )! as HTMLLinkElement;
  }
}
