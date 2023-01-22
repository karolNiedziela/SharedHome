import { finalize } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthenticationService } from '../services/authentication.service';
import { TranslateService } from '@ngx-translate/core';
import { ApiResponse } from 'src/app/core/models/api-response';

@Component({
  selector: 'app-email-confirmation',
  templateUrl: './email-confirmation.component.html',
  styleUrls: ['./email-confirmation.component.scss'],
})
export class EmailConfirmationComponent implements OnInit {
  error: string[] = [];

  email!: string;
  loading: boolean = true;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private authenticationService: AuthenticationService,
    private translateService: TranslateService,
    private toastrService: ToastrService
  ) {}

  ngOnInit(): void {
    this.email = this.activatedRoute.snapshot.queryParams['email'];
    const code = this.activatedRoute.snapshot.queryParams['code'];

    if (!this.email || !code) {
      this.router.navigate(['']);
    }

    this.authenticationService
      .confirmEmail(this.email, code)
      .pipe(
        finalize(() => {
          this.loading = false;
        })
      )
      .subscribe({
        next: () => {
          this.router.navigate(['/identity/login']);
          this.toastrService.success(
            this.translateService.instant('Email adress confirmed.')
          );
        },
        error: (error: string[]) => {
          const translatedEmailConfirmed = this.translateService.instant(
            'Email adress confirmed.'
          );
          if (error[0] === translatedEmailConfirmed) {
            this.router.navigate(['/identity/login']);

            this.toastrService.success(
              this.translateService.instant(translatedEmailConfirmed)
            );
          }
          this.error = this.translateService.instant(error);
        },
      });
  }

  resendEmail(): void {
    this.loading = true;

    this.authenticationService
      .resendEmailConfirmation(this.email)
      .pipe(
        finalize(() => {
          this.router.navigate(['/identity/login']);
          this.loading = false;
        })
      )
      .subscribe({
        next: (response: ApiResponse<string>) => {
          this.toastrService.success(
            this.translateService.instant(response.data)
          );
        },
        error: (error: string[]) => {
          this.toastrService.error(error.join(' '));
        },
      });
  }
}
