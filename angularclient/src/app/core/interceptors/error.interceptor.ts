import { ErrorService } from './../services/error.service';
import { ErrorResponse } from './../models/error-response';
import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { AuthenticationService } from 'src/app/modules/identity/services/authentication.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(
    private authenticationService: AuthenticationService,
    private errorService: ErrorService
  ) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        let errorResponse: ErrorResponse = null!;
        if (
          [401, 403].includes(error.status) &&
          this.authenticationService.authenticationResponseValue
        ) {
          this.authenticationService.logout();
        }

        errorResponse = error.error as ErrorResponse;
        this.errorService.propagateErrors(errorResponse.errors);
        return throwError(() => errorResponse.errors);
      })
    );
  }
}
