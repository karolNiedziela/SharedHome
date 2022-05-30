import { environment } from './../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, identity, map, Observable } from 'rxjs';
import { AuthenticationResult } from '../models/authenticationResult';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  private authenticationResultSubject: BehaviorSubject<AuthenticationResult>;
  public authenticationResult: Observable<AuthenticationResult>;
  private identityUrl: string;
  private refreshTokenTimeout: any;

  constructor(private router: Router, private http: HttpClient) {
    this.authenticationResultSubject =
      new BehaviorSubject<AuthenticationResult>(null!);
    this.authenticationResult = this.authenticationResultSubject.asObservable();
    this.identityUrl = `${environment.apiUrl}/identity`;
  }

  public get authenticationResultValue(): AuthenticationResult {
    return this.authenticationResultSubject.value;
  }

  login(email: string, password: string): Observable<AuthenticationResult> {
    return this.http
      .post<any>(
        `${this.identityUrl}/login`,
        {
          email,
          password,
        },
        { withCredentials: true }
      )
      .pipe(
        map((result: AuthenticationResult) => {
          localStorage.setItem('jwt', JSON.stringify(result));
          this.authenticationResultSubject.next(result);
          this.startRefreshTokenTimer();
          this.router.navigate(['']);
          return result;
        })
      );
  }

  refreshToken(): Observable<AuthenticationResult> {
    const refreshToken = this.authenticationResultValue.refreshToken;

    return this.http
      .post<any>(
        `${this.identityUrl}/refreshtoken`,
        { refreshToken },
        { withCredentials: true }
      )
      .pipe(
        map((result: AuthenticationResult) => {
          this.authenticationResultSubject.next(result);
          this.startRefreshTokenTimer();
          return result;
        })
      );
  }

  logout() {
    this.http
      .post<any>(`${this.identityUrl}/logout`, {}, { withCredentials: true })
      .subscribe();
    localStorage.removeItem('jwt');
    this.stopRefreshTokenTimer();
    this.authenticationResultSubject.next(null!);
    this.router.navigate(['/login']);
  }

  private startRefreshTokenTimer() {
    const expires = new Date(this.authenticationResultValue.expiry * 1000);
    const timeout = expires.getTime() - Date.now() - 60 * 1000;
    this.refreshTokenTimeout = setTimeout(
      () => this.refreshToken().subscribe(),
      timeout
    );
  }

  private stopRefreshTokenTimer() {
    clearTimeout(this.refreshTokenTimeout);
  }
}
