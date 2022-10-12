import { AppNotification } from './core/models/app-notification';
import { Subscription } from 'rxjs/internal/Subscription';
import { SignalrService } from './core/services/signalr.service';
import { LanguageService } from './core/services/language.service';
import { ThemeService } from 'app/core/services/theme.service';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthenticationResponse } from './core/models/authentication-response';
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
export class AppComponent implements OnInit, OnDestroy {
  title = 'sharedhomewebclient';
  authenticationResponse: AuthenticationResponse = null!;
  subscriptions: Subscription[] = [];

  notification?: AppNotification;

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

    this.themeService.setTheme();

    if (this.authenticationResponse) {
      this.signalRService.initiateSignalRConnection(
        this.authenticationResponse.accessToken
      );
    }
  }

  ngOnInit(): void {
    this.subscriptions.push(
      this.signalRService.notification.subscribe(
        (notification: AppNotification) => {
          this.notification = notification;
        }
      )
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((subscription) => subscription.unsubscribe);
  }
}
