import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { SignalrService } from 'src/app/core/services/signalr.service';
import { ProfileImage } from 'src/app/modules/identity/models/profile-image';
import { AuthenticationService } from 'src/app/modules/identity/services/authentication.service';

@Component({
  selector: 'app-profile-dropdown',
  templateUrl: './profile-dropdown.component.html',
  styleUrls: ['./profile-dropdown.component.scss'],
})
export class ProfileDropdownComponent implements OnInit {
  profileImage$!: Observable<ProfileImage | null>;
  userFullName: string = '';

  constructor(
    private authenticationService: AuthenticationService,
    private signalRService: SignalrService
  ) {}

  ngOnInit(): void {
    this.userFullName = this.buildUserFullName();

    this.authenticationService.getProfileImage();

    this.profileImage$ = this.authenticationService.profileImage$;
  }

  private buildUserFullName(): string {
    return (
      this.authenticationService.authenticationResponseValue?.firstName +
      ' ' +
      this.authenticationService.authenticationResponseValue?.lastName
    );
  }

  logout() {
    // this.signalRService.closeConnection();
    this.authenticationService.logout();
  }
}
