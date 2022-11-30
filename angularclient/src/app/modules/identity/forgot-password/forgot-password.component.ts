import { AuthenticationService } from 'app/modules/identity/services/authentication.service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['../identity.component.scss', './forgot-password.component.scss'],
})
export class ForgotPasswordComponent implements OnInit {
  forgotPasswordForm!: FormGroup;
  errorMessages: string[] = [];

  constructor(
    private router: Router,
    private authenticationService: AuthenticationService,
    private toastrService: ToastrService
  ) {}

  ngOnInit(): void {
    this.forgotPasswordForm = new FormGroup({
      email: new FormControl('', Validators.required),
    });
  }

  onSubmit(): void {
    const email: string = this.forgotPasswordForm.controls['email'].value;

    this.authenticationService.forgotPassword(email).subscribe({
      next: () => {
        this.toastrService.success('Email sent, check your mailbox.');
        this.router.navigate(['/identity/login']);
      },
      error: (errors: any) => {
        this.errorMessages = errors;
      },
    });
  }
}
