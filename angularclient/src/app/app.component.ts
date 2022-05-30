import { AuthenticationResult } from './core/models/authenticationResult';
import { Component } from '@angular/core';
import { AuthenticationService } from './core/services/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'sharedhomewebclient';
  authenticationResult: AuthenticationResult = null!;

  constructor(private authenticationService: AuthenticationService) {
    this.authenticationService.authenticationResult.subscribe(
      (result: AuthenticationResult) => (this.authenticationResult = result)
    );
  }

  logout() {
    this.authenticationService.logout();
  }
}
