import { AuthenticationResponse } from './core/models/authenticationResponse';
import { Component } from '@angular/core';
import { AuthenticationService } from './core/services/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'sharedhomewebclient';
  authenticationResponse: AuthenticationResponse = null!;

  constructor(private authenticationService: AuthenticationService) {
    this.authenticationService.authenticationResponse.subscribe(
      (result: AuthenticationResponse) => (this.authenticationResponse = result)
    );
  }

  logout() {
    this.authenticationService.logout();
  }
}
