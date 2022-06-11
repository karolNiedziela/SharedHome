import { AuthenticationResponse } from './../models/authenticationResponse';
import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';

@Injectable({
  providedIn: 'root',
})
export class LoginGuard implements CanActivate {
  constructor(
    private router: Router,
    private authenticationService: AuthenticationService
  ) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    const authenticationResponse =
      this.authenticationService.authenticationResponseValue;
    if (!authenticationResponse) {
      return true;
    } else {
      this.router.navigate(['home']);

      return false;
    }
  }
}