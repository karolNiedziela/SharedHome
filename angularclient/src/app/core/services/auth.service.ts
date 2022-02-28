import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AuthenticationSucessResult } from '../models/authenticationSucessResult';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private identityApiUrl: string = '' + 'identity/';
  private tokenApiUrl: string = '' + 'tokens/';
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };
  private authResponseSubject: BehaviorSubject<AuthenticationSucessResult>;
  private refreshTokenExpiryTimeout: any;

  public authResponse: Observable<AuthenticationSucessResult>;

  constructor(private httpClient: HttpClient) {
    this.authResponseSubject = new BehaviorSubject<AuthenticationSucessResult>(
      null!
    );
    this.authResponse = this.authResponseSubject.asObservable();
  }

  public get authResponseValue(): AuthenticationSucessResult {
    return this.authResponseSubject.value;
  }

  login(username: string, password: string): Observable<any> {
    return this.httpClient
      .post<AuthenticationSucessResult>(
        this.identityApiUrl + 'login',
        {
          username,
          password,
        },
        this.httpOptions
      )
      .pipe(
        map((authResponse: AuthenticationSucessResult) => {
          this.authResponseSubject.next(authResponse);
          localStorage.setItem('jwt', JSON.stringify(authResponse));
          return authResponse;
        })
      );
  }
}
