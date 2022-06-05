import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthenticationService } from './../../../core/services/authentication.service';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { first } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss', '../identity.scss'],
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

  test(): void {
    alert('działa');
    this.authenticationService
      .login('charles@email.com', 'charles1=')
      .pipe(first())
      .subscribe((data) => {});
  }

  onSubmit() {
    if (this.loginForm.invalid) {
      console.log('form is invalid');
      return;
    }

    console.log('submitted');
  }
}
