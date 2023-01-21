import { ThemeService } from './core/services/theme/theme.service';
import { DOCUMENT, registerLocaleData } from '@angular/common';
import { Component, Inject, OnDestroy, OnInit, Renderer2 } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';
import { AuthenticationResponse } from './core/models/authentication-response';
import { LanguageService } from './core/services/language.service';
import { SignalrService } from './core/services/signalr.service';
import { AuthenticationService } from './modules/identity/services/authentication.service';
import localePl from '@angular/common/locales/pl';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit, OnDestroy {
  private isDark = true;

  title = 'sharedhomewebclient';
  authenticationResponse: AuthenticationResponse = null!;
  subscriptions: Subscription[] = [];

  constructor(
    private authenticationService: AuthenticationService,
    public translateService: TranslateService,
    private themeService: ThemeService,
    private languageService: LanguageService,
    private signalRService: SignalrService
  ) {
    this.subscriptions.push(
      this.authenticationService?.authenticationResponse.subscribe(
        (result: AuthenticationResponse) =>
          (this.authenticationResponse = result)
      )
    );

    registerLocaleData(localePl);
    this.translateService.addLangs(['en', 'pl']);
    this.translateService.setDefaultLang('en');
    this.languageService.setLanguageOnInit();

    if (this.authenticationResponse) {
      this.signalRService.initiateSignalRConnection(
        this.authenticationResponse.accessToken
      );
    }
  }

  ngOnInit(): void {
    this.themeService.setTheme();
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((subscription) => subscription.unsubscribe);
  }

  switchMode(): void {
    this.themeService.switchTheme();
  }
}
