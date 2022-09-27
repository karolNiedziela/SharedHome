import { SignalrService } from './core/services/signalr.service';
import { LanguageService } from './core/services/language.service';
import { ThemeService } from 'app/core/services/theme.service';
import { Component, OnInit } from '@angular/core';
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
export class AppComponent implements OnInit {
  title = 'sharedhomewebclient';
  authenticationResponse: AuthenticationResponse = null!;

  hubHelloMessage: any;

  constructor(
    private authenticationService: AuthenticationService,
    public translateService: TranslateService,
    private themeService: ThemeService,
    private languageService: LanguageService,
    private signalRService: SignalrService
  ) {
    this.authenticationService?.authenticationResponse.subscribe(
      (result: AuthenticationResponse) => (this.authenticationResponse = result)
    );

    registerLocaleData(localePl);
    this.translateService.addLangs(['en', 'pl']);
    this.translateService.setDefaultLang('en');
    this.languageService.setLanguageOnInit();

    this.themeService.setTheme();
  }

  ngOnInit(): void {
    this.signalRService.hubHelloMessage.subscribe((message: any) => {
      this.hubHelloMessage = message;
      console.log(message);
    });
  }
}
