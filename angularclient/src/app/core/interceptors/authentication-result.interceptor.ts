import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthenticationService } from 'src/app/modules/identity/services/authentication.service';

@Injectable()
export class AuthenticationResultInterceptor implements HttpInterceptor {
  constructor(private authenticationService: AuthenticationService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const authenticationResponse =
      this.authenticationService.authenticationResponseValue;

    const isLoggedIn =
      authenticationResponse && authenticationResponse.accessToken;

    const isApiUrl = request.url.startsWith(environment.apiUrl);

    if (isLoggedIn && isApiUrl) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${authenticationResponse.accessToken}`,
        },
      });
    }

    return next.handle(request);
  }
}
