import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { finalize, first } from 'rxjs';
import { SignalrService } from 'src/app/core/services/signalr.service';
import { AuthenticationService } from 'src/app/modules/identity/services/authentication.service';
import { passwordStrengthValidator } from 'src/app/shared/validators/password.validator';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  errorMessages: string[] = [];
  disabled: boolean = false;
  loadingSaveButton: boolean = false;

  constructor(
    private authenticationService: AuthenticationService,
    private signalRService: SignalrService
  ) {}

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [
        Validators.required,
        passwordStrengthValidator,
      ]),
    });
  }

  onSubmit(): void {
    this.disabled = true;

    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    this.loadingSaveButton = true;

    const email = this.loginForm.controls['email'].value;
    const password = this.loginForm.controls['password'].value;

    this.authenticationService
      .login(email, password)
      .pipe(
        first(),
        finalize(() => {
          this.disabled = false;
          this.loadingSaveButton = false;
          this.signalRService.initiateSignalRConnection(
            this.authenticationService.authenticationResponseValue.accessToken
          );
        })
      )
      .subscribe({
        next: () => {},
        error: (error: string[]) => {
          this.errorMessages = error;
        },
      });
  }
}
