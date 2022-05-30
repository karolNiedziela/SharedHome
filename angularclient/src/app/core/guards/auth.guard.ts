import { AuthenticationService } from './../services/authentication.service';
import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';

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
      this.authenticationService.authenticationResultValue;

    if (authenticationResult) {
      return true;
    } else {
      this.router.navigate(['/login'], {
        queryParams: { returnUrl: state.url },
      });

      return false;
    }
  }
}
