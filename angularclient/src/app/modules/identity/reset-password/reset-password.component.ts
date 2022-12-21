import { TranslateService } from '@ngx-translate/core';
import { finalize } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { AuthenticationService } from 'app/modules/identity/services/authentication.service';
import { ResetPassword } from './../models/reset-password';
import { PasswordsFormComponent } from './../../../shared/components/forms/passwords-form/passwords-form.component';
import { FormGroup, FormControl } from '@angular/forms';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['../identity.component.scss', './reset-password.component.scss'],
})
export class ResetPasswordComponent implements OnInit {
  @ViewChild('passwordForm') passwordForm!: PasswordsFormComponent;

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

    const password = this.resetPasswordForm.controls['password'].value;
    const resetPassword: ResetPassword = {
      code: this.code,
      email: this.email,
      newPassword: password,
      confirmNewPassword: password,
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
          this.toastrService.info(
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
