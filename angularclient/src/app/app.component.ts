import { LoadingService } from './core/services/loading.service';
import { Component } from '@angular/core';
import { AuthenticationResponse } from './core/models/authenticationResponse';
import { AuthenticationService } from './modules/identity/services/authentication.service';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs';

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
  loading$: Observable<boolean> = this.loader.loading$;

  constructor(
    private authenticationService: AuthenticationService,
    public translateService: TranslateService,
    public loader: LoadingService
  ) {
    this.authenticationService.authenticationResponse.subscribe(
      (result: AuthenticationResponse) => (this.authenticationResponse = result)
    );

    translateService.addLangs(['en', 'pl']);
    translateService.setDefaultLang('pl');
  }

  switchLanguages(lang: string) {
    this.translateService.use(lang);
  }
}
