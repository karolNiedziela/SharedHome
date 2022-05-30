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
  constructor(private authenticationService: AuthenticationService) {}

  ngOnInit(): void {}

  test(): void {
    alert('działa');
    this.authenticationService
      .login('charles@email.com', 'charles1=')
      .pipe(first())
      .subscribe((data) => {});
  }
}
