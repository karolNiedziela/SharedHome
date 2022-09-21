import { TranslateService } from '@ngx-translate/core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LanguageService {
  private static key: string = 'sharedhome-language';

  public get language(): string {
    return (
      localStorage.getItem(LanguageService.key) ??
      this.translateService.defaultLang
    );
  }

  constructor(private translateService: TranslateService) {}

  setLanguageOnInit(): void {
    if (localStorage.getItem(LanguageService.key)) {
      this.translateService.use(localStorage.getItem(LanguageService.key)!);
      return;
    }

    if (this.translateService.currentLang) {
      this.translateService.use(this.translateService.currentLang);

      localStorage.setItem(
        LanguageService.key,
        this.translateService.currentLang
      );
      return;
    }

    const browserLanguage = this.translateService.getBrowserLang()!;

    const language = browserLanguage.match(/pl|en/)
      ? browserLanguage
      : this.translateService.defaultLang;

    this.translateService.use(language);
    localStorage.setItem(LanguageService.key, language);
  }

  public updateLanguage(language: string) {
    this.translateService.use(language);

    localStorage.setItem(LanguageService.key, language);
  }
}
