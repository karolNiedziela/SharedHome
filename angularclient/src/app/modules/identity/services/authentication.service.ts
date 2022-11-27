import { UploadProfileImage } from './../models/upload-profile-image';
import { ProfileImage } from './../models/profile-image';
import { Register } from './../models/register';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationResponse } from 'app/core/models/authentication-response';
import { environment } from 'environments/environment';
import { BehaviorSubject, map, Observable, tap } from 'rxjs';

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

          this.getProfileImage;

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
}
