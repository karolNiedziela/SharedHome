import { Component, OnInit } from '@angular/core';
import { ProfileImage } from 'app/modules/identity/models/profile-image';
import { AuthenticationService } from 'app/modules/identity/services/authentication.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-profile-dropdown',
  templateUrl: './profile-dropdown.component.html',
  styleUrls: ['./profile-dropdown.component.scss'],
})
export class ProfileDropdownComponent implements OnInit {
  profileImage$!: Observable<ProfileImage>;
  userFullName: string = '';

  constructor(private authenticationService: AuthenticationService) {}

  ngOnInit(): void {
    this.userFullName = this.buildUserFullName();

    this.authenticationService.getProfileImage();

    this.profileImage$ = this.authenticationService.profileImage$;
  }

  private buildUserFullName(): string {
    return (
      this.authenticationService.authenticationResponseValue.firstName +
      ' ' +
      this.authenticationService.authenticationResponseValue.lastName
    );
  }

  logout() {
    this.authenticationService.logout();
  }
}
