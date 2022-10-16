import { LanguageService } from './../services/language.service';
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
  constructor(private languageService: LanguageService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    let language: string;

    switch (this.languageService.language) {
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
