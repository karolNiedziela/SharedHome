import { ScreenSizeHelper } from 'app/core/helpers/screen-size-helper';
import { Component, OnInit, ViewChild } from '@angular/core';
import { faBell, faBars, faXmark } from '@fortawesome/free-solid-svg-icons';
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
  userFullName: string = '';
  menuToggled: boolean = false;

  constructor(
    public screenSizeHelper: ScreenSizeHelper,
    private notificationService: NotificationService,
    private authenticationService: AuthenticationService
  ) {}

  ngOnInit(): void {
    this.notificationsCount$ = this.notificationService.notificationsCount$;
    this.userFullName = this.buildUserFullName();

    const navItems = document.querySelectorAll('.nav-item');
    Array.from(navItems).map((x) =>
      x.addEventListener('click', () => {
        this.toggleMenu();
      })
    );
  }

  toggleMenu(): void {
    const sidebar = document.querySelector('.sidebar');
    sidebar?.classList.toggle('is-active');

    const navbar = document.querySelector('.navbar');
    navbar?.classList.toggle('is-active');

    this.menuToggled = !this.menuToggled;

    const media = window.matchMedia('(max-width: 768px)');
    if (media.matches) {
      this.blurContent();
    }
  }

  logout() {
    this.authenticationService.logout();
  }

  private blurContent(): void {
    const content = document.querySelector('.content') as HTMLDivElement;

    if (this.menuToggled) {
      content.style.opacity = '0.1';
    } else {
      content.style.opacity = '1';
    }
  }

  private buildUserFullName(): string {
    return (
      this.authenticationService.authenticationResponseValue.firstName +
      ' ' +
      this.authenticationService.authenticationResponseValue.lastName
    );
  }
}
