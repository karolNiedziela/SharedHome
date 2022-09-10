import { AuthenticationService } from 'app/modules/identity/services/authentication.service';
import { Register } from './../models/register';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss', '../identity.component.scss'],
})
export class RegisterComponent implements OnInit {
  errorMessages: string[] = [];
  information?: string | null = null;

  registerForm!: FormGroup;
  constructor(
    private authenticationService: AuthenticationService,
    private toastr: ToastrService
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
      return;
    }

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

    this.authenticationService.register(register).subscribe({
      next: (response: any) => {
        this.information = response.data;
        this.registerForm.reset();
      },
      error: (error: string[]) => {
        this.errorMessages = error;
      },
    });
  }
}
