import { ApiResponse } from 'src/app/core/models/api-response';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject, map, Observable, tap } from 'rxjs';
import { AuthenticationResponse } from 'src/app/core/models/authentication-response';
import { ProfileImage } from 'src/app/modules/identity/models/profile-image';
import { Register } from 'src/app/modules/identity/models/register';
import { ResetPassword } from 'src/app/modules/identity/models/reset-password';
import { environment } from 'src/environments/environment';
import { ChangePassword } from '../models/change-password';
import { UserInformation } from '../models/user-information';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  private authenticationResponseSubject: BehaviorSubject<AuthenticationResponse>;
  public authenticationResponse: Observable<AuthenticationResponse>;

  private _profileImageSubject: BehaviorSubject<ProfileImage> =
    new BehaviorSubject<ProfileImage>(null!);
  public readonly profileImage$: Observable<ProfileImage> =
    this._profileImageSubject.asObservable();

  private identityUrl: string = `${environment.apiUrl}/identity`;

  private defaultHttpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

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

          this.getProfileImage();

          return result;
        })
      );
  }

  register(register: Register): Observable<any> {
    return this.http.post<any>(`${this.identityUrl}/register`, register);
  }

  confirmEmail(email: string, code: string): Observable<any> {
    return this.http.post<any>(`${this.identityUrl}/confirmemail`, {
      email: email,
      code: code,
    });
  }

  resendEmailConfirmation(email: string): Observable<ApiResponse<string>> {
    return this.http.post<ApiResponse<string>>(
      `${this.identityUrl}/resendconfirmationemail`,
      {
        email: email,
      }
    );
  }

  forgotPassword(email: string): Observable<any> {
    return this.http.post<any>(`${this.identityUrl}/forgotpassword`, {
      email: email,
    });
  }

  resetPassword(resetPassword: ResetPassword): Observable<any> {
    return this.http.post<any>(
      `${this.identityUrl}/resetpassword`,
      resetPassword,
      this.defaultHttpOptions
    );
  }

  changePassword(changePassword: ChangePassword): Observable<any> {
    return this.http.put<any>(
      `${this.identityUrl}/changepassword`,
      changePassword,
      this.defaultHttpOptions
    );
  }

  logout(): void {
    localStorage.removeItem('jwt');
    this.authenticationResponseSubject.next(null!);
    this.router.navigate(['/identity/login']);
    this._profileImageSubject.next(null!);
  }

  getProfileImage() {
    return this.http
      .get<ProfileImage>(
        `${this.identityUrl}/getprofileimage`,
        this.defaultHttpOptions
      )
      .subscribe({
        next: (profileImage: ProfileImage) => {
          this._profileImageSubject.next(profileImage);
        },
      });
  }

  uploadProfileImage(uploadProfileImage: any) {
    return this.http
      .put<any>(`${this.identityUrl}/uploadprofileimage`, uploadProfileImage)
      .subscribe({
        next: (profileImage: ProfileImage) => {
          this._profileImageSubject.next(profileImage);
        },
      });
  }

  getUserInformation(): Observable<UserInformation> {
    return this.http.get<UserInformation>(
      `${this.identityUrl}/userinformation`
    );
  }
}
