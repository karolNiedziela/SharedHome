import { LanguageService } from './../../../../core/services/language.service';
import { TranslateService } from '@ngx-translate/core';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-language-select',
  templateUrl: './language-select.component.html',
  styleUrls: ['../select.scss'],
})
export class LanguageSelectComponent implements OnInit {
  languages: string[] = this.translateService.getLangs();

  constructor(
    public translateService: TranslateService,
    public languageService: LanguageService
  ) {}

  ngOnInit(): void {}

  onLanguageChanged(language: string) {
    this.languageService.updateLanguage(language);
  }
}
