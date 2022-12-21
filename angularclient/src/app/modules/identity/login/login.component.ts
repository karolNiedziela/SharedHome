import { SignalrService } from './../../../core/services/signalr.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { first, finalize } from 'rxjs';
import { AuthenticationService } from 'app/modules/identity/services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss', '../identity.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  errorMessages: string[] = [];
  disabled: boolean = false;

  constructor(
    private authenticationService: AuthenticationService,
    private signalRService: SignalrService
  ) {}

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      email: new FormControl('', Validators.required),
      password: new FormControl(''),
    });
  }

  onSubmit(): void {
    this.disabled = true;

    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    const email = this.loginForm.controls['email'].value;
    const password = this.loginForm.controls['password'].value;

    this.authenticationService
      .login(email, password)
      .pipe(
        first(),
        finalize(() => {
          this.disabled = false;
        })
      )
      .subscribe({
        next: () => {
          this.signalRService.initiateSignalRConnection(
            this.authenticationService.authenticationResponseValue.accessToken
          );
        },
        error: (error: string[]) => {
          this.errorMessages = error;
        },
      });
  }
}
