import { environment } from './../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, identity, map, Observable } from 'rxjs';
import { Jwt } from '../models/jwt';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  private jwtSubject: BehaviorSubject<Jwt>;
  public jwt: Observable<Jwt>;
  private identityUrl: string;

  constructor(private router: Router, private http: HttpClient) {
    this.jwtSubject = new BehaviorSubject<Jwt>(null!);
    this.jwt = this.jwtSubject.asObservable();
    this.identityUrl = `${environment.apiUrl}/identity`;
  }

  public get authenticationResultValue(): Jwt {
    return this.jwtSubject.value;
  }

  login(email: string, password: string): Observable<Jwt> {
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
        map((result: Jwt) => {
          localStorage.setItem('jwt', JSON.stringify(result));
          this.jwtSubject.next(result);
          this.router.navigate(['']);
          return result;
        })
      );
  }

  logout() {
    localStorage.removeItem('jwt');
    this.jwtSubject.next(null!);
    this.router.navigate(['/login']);
  }
}
