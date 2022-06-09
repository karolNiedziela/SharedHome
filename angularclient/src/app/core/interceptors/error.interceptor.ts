import { AuthenticationService } from './../services/authentication.service';
import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private authenticationService: AuthenticationService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((error) => {
        if (
          [401, 403].includes(error.status) &&
          this.authenticationService.authenticationResponseValue
        ) {
          this.authenticationService.logout();
        }

        const errorMessage = error.error.Errors[0];
        return throwError(() => errorMessage);
      })
    );
  }
}
