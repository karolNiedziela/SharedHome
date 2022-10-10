import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class LanguageInterceptor implements HttpInterceptor {
  constructor() {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    let language: string = '';

    switch (navigator.language) {
      case 'pl':
        language = 'pl-PL';
        break;

      case 'en':
        language = 'en-US';
        break;

      default:
        language = 'en-US';
        break;
    }

    request = request.clone({
      headers: request.headers.set('Accept-Language', language),
    });

    return next.handle(request);
  }
}
