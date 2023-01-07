import { TranslateService } from '@ngx-translate/core';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { finalize } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/modules/identity/services/authentication.service';
import { Register } from '../models/register';
import { PasswordFormComponent } from 'src/app/shared/forms/password-form/password-form.component';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss', '../identity.component.scss'],
})
export class RegisterComponent implements OnInit {
  @ViewChild('passwordForm') passwordForm!: PasswordFormComponent;

  errorMessages: string[] = [];
  disabled: boolean = false;
  loadingSaveButton: boolean = false;

  registerForm!: FormGroup;
  constructor(
    private authenticationService: AuthenticationService,
    private toastrService: ToastrService,
    private router: Router,
    private translateService: TranslateService
  ) {}

  ngOnInit(): void {
    this.registerForm = new FormGroup({
      email: new FormControl('', [Validators.required]),
      firstName: new FormControl('', [Validators.required]),
      lastName: new FormControl('', [Validators.required]),
      password: new FormControl(''),
    });
  }

  onSubmit() {
    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      this.passwordForm.passwordForm.markAllAsTouched();
      return;
    }

    this.disabled = true;
    this.loadingSaveButton = true;

    const email = this.registerForm.get('email')?.value;
    const firstName = this.registerForm.get('firstName')?.value;
    const lastName = this.registerForm.get('lastName')?.value;
    const password = this.registerForm.get('password')?.value;

    const register: Register = {
      email: email,
      firstName: firstName,
      lastName: lastName,
      password: password,
      confirmPassword: password,
    };

    this.authenticationService
      .register(register)
      .pipe(
        finalize(() => {
          this.disabled = false;
          this.loadingSaveButton = false;
        })
      )
      .subscribe({
        next: (response: any) => {
          this.registerForm.reset();
          this.errorMessages = [];
          this.router.navigate(['/identity/login']);
          this.toastrService.success(
            this.translateService.instant(response.data)
          );
        },
        error: (error: string[]) => {
          this.errorMessages = error;
        },
      });
  }
}
