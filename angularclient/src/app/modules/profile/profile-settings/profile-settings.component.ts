import { Observable } from 'rxjs';
import { AuthenticationService } from 'app/modules/identity/services/authentication.service';
import { Component, OnInit } from '@angular/core';
import { UserInformation } from 'app/modules/identity/models/user-information';

@Component({
  selector: 'app-profile-settings',
  templateUrl: './profile-settings.component.html',
  styleUrls: ['./profile-settings.component.scss'],
})
export class ProfileSettingsComponent implements OnInit {
  userInformation$!: Observable<UserInformation>;

  constructor(private authenticationService: AuthenticationService) {}

  ngOnInit(): void {
    this.userInformation$ = this.authenticationService.getUserInformation();
  }
}
