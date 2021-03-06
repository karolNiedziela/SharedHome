import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationResponse } from 'app/core/models/authenticationResponse';
import { environment } from 'environments/environment';
import { BehaviorSubject, map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  private authenticationResponseSubject: BehaviorSubject<AuthenticationResponse>;

  public authenticationResponse: Observable<AuthenticationResponse>;

  private identityUrl: string = `${environment.apiUrl}/identity`;

  constructor(
    private router: Router,
    private http: HttpClient,
    private activatedRoute: ActivatedRoute
  ) {
    this.authenticationResponseSubject =
      new BehaviorSubject<AuthenticationResponse>(
        JSON.parse(localStorage.getItem('jwt')!)
      );

    this.authenticationResponse =
      this.authenticationResponseSubject.asObservable();
  }

  public get authenticationResponseValue(): AuthenticationResponse {
    return this.authenticationResponseSubject.value;
  }

  login(email: string, password: string): Observable<AuthenticationResponse> {
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
        map((result: AuthenticationResponse) => {
          localStorage.setItem('jwt', JSON.stringify(result));
          this.authenticationResponseSubject.next(result);

          const returnUrl =
            this.activatedRoute.snapshot.queryParams['returnUrl'];
          if (returnUrl) {
            this.router.navigate([`${returnUrl}`]);
          } else {
            this.router.navigate(['']);
          }

          return result;
        })
      );
  }

  confirmEmail(email: string, code: string): Observable<any> {
    return this.http.get<any>(`${this.identityUrl}/confirmemail`, {
      params: {
        email: email,
        code: code,
      },
    });
  }

  logout() {
    localStorage.removeItem('jwt');
    this.authenticationResponseSubject.next(null!);
    this.router.navigate(['/identity/login']);
  }
}
