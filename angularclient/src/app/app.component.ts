import { Component } from '@angular/core';
import { AuthenticationResponse } from './core/models/authenticationResponse';
import { AuthenticationService } from './modules/identity/services/authentication.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'sharedhomewebclient';
  authenticationResponse: AuthenticationResponse = null!;

  constructor(
    private authenticationService: AuthenticationService,
    public translateService: TranslateService
  ) {
    this.authenticationService.authenticationResponse.subscribe(
      (result: AuthenticationResponse) => (this.authenticationResponse = result)
    );

    translateService.addLangs(['en', 'pl']);
    translateService.setDefaultLang('en');
  }

  logout() {
    this.authenticationService.logout();
  }

  switchLanguages(lang: string) {
    this.translateService.use(lang);
  }
}
