import { AuthenticationService } from 'app/modules/identity/services/authentication.service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['../identity.component.scss', './forgot-password.component.scss'],
})
export class ForgotPasswordComponent implements OnInit {
  forgotPasswordForm!: FormGroup;
  information?: string | null = null;

  constructor(private authenticationService: AuthenticationService) {}

  ngOnInit(): void {
    this.forgotPasswordForm = new FormGroup({
      email: new FormControl('', Validators.required),
    });
  }

  onSubmit(): void {
    const email: string = this.forgotPasswordForm.controls['email'].value;

    this.authenticationService.forgotPassword(email).subscribe({
      next: () => {
        this.information = 'Email sent, check your mailbox.';
      },
      error: (error: any) => {
        console.log(error);
      },
    });
  }
}
