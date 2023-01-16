import { TranslateService } from '@ngx-translate/core';
import { finalize } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { ResetPassword } from './../models/reset-password';
import { FormGroup, FormControl } from '@angular/forms';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PasswordFormComponent } from 'src/app/shared/forms/password-form/password-form.component';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['../identity.component.scss', './reset-password.component.scss'],
})
export class ResetPasswordComponent implements OnInit {
  @ViewChild('passwordForm') passwordForm!: PasswordFormComponent;

  resetPasswordForm!: FormGroup;
  code!: string;
  email!: string;
  errorMessages: string[] = [];
  disabled: boolean = false;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private authenticationService: AuthenticationService,
    private toastrService: ToastrService,
    private translateService: TranslateService
  ) {}

  ngOnInit(): void {
    this.resetPasswordForm = new FormGroup({
      password: new FormControl(''),
    });

    this.email = this.activatedRoute.snapshot.queryParams['email'];
    this.code = this.activatedRoute.snapshot.queryParams['code'];

    if (!this.email || !this.code) {
      this.router.navigate(['']);
    }
  }

  onSubmit(): void {
    this.disabled = true;

    if (this.resetPasswordForm.invalid) {
      this.passwordForm.passwordForm.markAllAsTouched();
      return;
    }

    const passwordForm = this.resetPasswordForm.controls['password'].value;
    const resetPassword: ResetPassword = {
      code: this.code,
      email: this.email,
      newPassword: passwordForm.password,
      confirmNewPassword: passwordForm.password,
    };

    this.authenticationService
      .resetPassword(resetPassword)
      .pipe(
        finalize(() => {
          this.disabled = false;
        })
      )
      .subscribe({
        next: () => {
          this.toastrService.success(
            this.translateService.instant('Password successfully changed.')
          );
          this.router.navigateByUrl('/identity/login');
        },
        error: (error: string) => {
          this.errorMessages.push(error);
        },
      });
  }
}
