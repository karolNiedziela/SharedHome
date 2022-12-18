import { Subscription } from 'rxjs/internal/Subscription';
import { ScreenSizeHelper } from 'app/core/helpers/screen-size-helper';
import { Component, OnInit } from '@angular/core';
import { faBell } from '@fortawesome/free-solid-svg-icons';
import { NotificationService } from 'app/modules/notifications/services/notification.service';
import { Observable } from 'rxjs';
import { AuthenticationService } from 'app/modules/identity/services/authentication.service';
import { AuthenticationResponse } from 'app/core/models/authentication-response';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  notificationIcon = faBell;
  notificationsCount$!: Observable<number>;
  menuToggled: boolean = false;
  authenticationSubscription!: Subscription;
  authenticationResponse: AuthenticationResponse = null!;

  constructor(
    public screenSizeHelper: ScreenSizeHelper,
    private notificationService: NotificationService,
    private authenticationService: AuthenticationService
  ) {}

  ngOnInit(): void {
    this.notificationsCount$ = this.notificationService.notificationsCount$;

    const navItems = document.querySelectorAll('.nav-item');
    Array.from(navItems).map((x) =>
      x.addEventListener('click', () => {
        this.toggleMenu();
      })
    );

    this.authenticationSubscription =
      this.authenticationService?.authenticationResponse.subscribe(
        (result: AuthenticationResponse) =>
          (this.authenticationResponse = result)
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

  private blurContent(): void {
    const content = document.querySelector('.content') as HTMLDivElement;

    if (this.menuToggled) {
      content.style.opacity = '0.1';
    } else {
      content.style.opacity = '1';
    }
  }
}
