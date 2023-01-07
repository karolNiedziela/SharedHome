import { Observable } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { UserInformation } from '../../identity/models/user-information';
import { AuthenticationService } from '../../identity/services/authentication.service';

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
