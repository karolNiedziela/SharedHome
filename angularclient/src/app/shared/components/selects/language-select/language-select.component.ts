import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { LanguageService } from 'src/app/core/services/language.service';

@Component({
  selector: 'app-language-select',
  templateUrl: './language-select.component.html',
  styleUrls: ['./language-select.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class LanguageSelectComponent implements OnInit {
  languages: string[] = this.translateService.getLangs();
  selectedLanguage: string = this.translateService.currentLang;

  flags: Record<string, string> = {
    pl: '../../../../../assets/flags/poland-flag.png',
    en: '../../../../../assets/flags/england-flag.png',
  };

  constructor(
    public translateService: TranslateService,
    public languageService: LanguageService
  ) {}

  ngOnInit(): void {}

  onLanguageChanged(language: string) {
    this.languageService.updateLanguage(language);
  }
}
