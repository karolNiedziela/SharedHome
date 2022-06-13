import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthenticationService } from '../../core/services/authentication.service';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { first } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss', '../identity.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  constructor(private authenticationService: AuthenticationService) {
    this.loginForm = new FormGroup({
      email: new FormControl('', Validators.required),
      password: new FormControl(''),
    });
  }

  ngOnInit(): void {}

  onSubmit() {
    if (this.loginForm.invalid) {
      console.log('form is invalid');
      return;
    }

    const email = this.loginForm.controls['email'].value;
    const password = this.loginForm.controls['password'].value;

    this.authenticationService
      .login(email, password)
      .pipe(first())
      .subscribe({
        next: () => {},
        error: (error) => {
          console.log('error in logging');
          console.log(error);
        },
      });
  }
}
