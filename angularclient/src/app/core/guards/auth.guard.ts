import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { AuthenticationService } from 'app/modules/identity/services/authentication.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(
    private router: Router,
    private authenticationService: AuthenticationService
  ) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const authenticationResult =
      this.authenticationService.authenticationResponseValue;

    if (authenticationResult) {
      return true;
    } else {
      this.router.navigate(['/identity/login'], {
        queryParams: { returnUrl: state.url },
      });

      return false;
    }
  }
}
