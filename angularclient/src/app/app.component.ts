import { Jwt } from './core/models/jwt';
import { Component } from '@angular/core';
import { AuthenticationService } from './core/services/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'sharedhomewebclient';
  jwt: Jwt = null!;

  constructor(private authenticationService: AuthenticationService) {
    this.authenticationService.jwt.subscribe(
      (result: Jwt) => (this.jwt = result)
    );
  }

  logout() {
    this.authenticationService.logout();
  }
}
