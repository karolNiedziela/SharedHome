import { ScreenSizeHelper } from 'app/core/helpers/screen-size-helper';
import { Component, OnInit, ViewChild } from '@angular/core';
import { faBell } from '@fortawesome/free-solid-svg-icons';
import { AuthenticationService } from 'app/modules/identity/services/authentication.service';
import { NotificationService } from 'app/modules/notifications/services/notification.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  notificationIcon = faBell;
  notificationsCount$!: Observable<number>;
  userFullName: string = 'FN';

  constructor(
    public screenSizeHelper: ScreenSizeHelper,
    private notificationService: NotificationService,
    private authenticationService: AuthenticationService
  ) {}

  ngOnInit(): void {
    this.notificationsCount$ = this.notificationService.notificationsCount$;
    this.userFullName = this.buildUserFullName();
  }

  private buildUserFullName(): string {
    return (
      this.authenticationService.authenticationResponseValue.firstName +
      ' ' +
      this.authenticationService.authenticationResponseValue.lastName
    );
  }
}
