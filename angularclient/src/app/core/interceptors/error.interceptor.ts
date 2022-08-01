import { ErrorResponse } from './../models/error-response';
import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { AuthenticationService } from 'app/modules/identity/services/authentication.service';

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

        const errorResponse = error.error as ErrorResponse;

        console.log(errorResponse);
        return throwError(() => errorResponse.errors);
      })
    );
  }
}
