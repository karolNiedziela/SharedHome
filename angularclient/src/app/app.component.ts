import { ThemeService } from 'app/core/services/theme.service';
import { Component } from '@angular/core';
import { AuthenticationResponse } from './core/models/authenticationResponse';
import { AuthenticationService } from './modules/identity/services/authentication.service';
import { TranslateService } from '@ngx-translate/core';
import { registerLocaleData } from '@angular/common';
import localePl from '@angular/common/locales/pl';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: [
    './app.component.scss',
    './shared/components/sidebar/sidebar.component.scss',
  ],
})
export class AppComponent {
  title = 'sharedhomewebclient';
  authenticationResponse: AuthenticationResponse = null!;

  constructor(
    private authenticationService: AuthenticationService,
    public translateService: TranslateService,
    private themeService: ThemeService
  ) {
    this.authenticationService?.authenticationResponse.subscribe(
      (result: AuthenticationResponse) => (this.authenticationResponse = result)
    );

    translateService.addLangs(['en', 'pl']);
    translateService.setDefaultLang('en');

    registerLocaleData(localePl);
  }

  switchLanguages(lang: string) {
    this.translateService.use(lang);
  }
}
