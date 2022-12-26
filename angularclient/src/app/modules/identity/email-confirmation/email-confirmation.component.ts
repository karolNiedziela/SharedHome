import { finalize } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthenticationService } from '../services/authentication.service';
import { TranslateService } from '@ngx-translate/core';

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
    private toastrService: ToastrService,
    private translateService: TranslateService
  ) {}

  ngOnInit(): void {
    const email = this.activatedRoute.snapshot.queryParams['email'];
    const code = this.activatedRoute.snapshot.queryParams['code'];

    if (!email || !code) {
      this.router.navigate(['']);
    }

    this.authenticationService
      .confirmEmail(email, code)
      .pipe(
        finalize(() => {
          this.router.navigate(['/identity/login']);
        })
      )
      .subscribe({
        next: () => {
          this.toastrService.success(
            this.translateService.instant('Email adress confirmed.')
          );
        },
        error: (error: string[]) => {
          this.toastrService.error(error.join(' '));
        },
      });
  }
}
