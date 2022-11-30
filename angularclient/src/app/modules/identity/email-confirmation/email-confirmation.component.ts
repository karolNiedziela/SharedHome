import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-email-confirmation',
  templateUrl: './email-confirmation.component.html',
  styleUrls: ['./email-confirmation.component.scss'],
})
export class EmailConfirmationComponent implements OnInit {
  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private authenticationService: AuthenticationService,
    private toastrService: ToastrService
  ) {}

  ngOnInit(): void {
    const email = this.activatedRoute.snapshot.queryParams['email'];
    const code = this.activatedRoute.snapshot.queryParams['code'];

    if (!email || !code) {
      this.router.navigate(['']);
    }

    this.authenticationService.confirmEmail(email, code).subscribe({
      next: () => {
        this.toastrService.success('Email adress confirmed.');
        this.router.navigate(['/identity/login']);
      },
      error: (error: string[]) => {},
    });
  }
}
