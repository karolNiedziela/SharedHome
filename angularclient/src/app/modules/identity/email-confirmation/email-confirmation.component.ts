import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-email-confirmation',
  templateUrl: './email-confirmation.component.html',
  styleUrls: ['./email-confirmation.component.scss'],
})
export class EmailConfirmationComponent implements OnInit {
  errorMessages: string[] = [];

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private authenticationService: AuthenticationService
  ) {}

  ngOnInit(): void {
    const email = this.activatedRoute.snapshot.queryParams['email'];
    const code = this.activatedRoute.snapshot.queryParams['code'];

    if (!email || !code) {
      this.router.navigate(['']);
    }

    this.authenticationService.confirmEmail(email, code).subscribe({
      next: () => {
        this.router.navigate(['emailconfirmed']);
      },
      error: (error: string[]) => {
        this.errorMessages = error;
      },
    });
  }
}
