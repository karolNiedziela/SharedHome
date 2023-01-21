import { ThemeService } from './../../../core/services/theme/theme.service';
import { NavigationEnd, Router } from '@angular/router';
import { Subscription } from 'rxjs/internal/Subscription';
import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { filter, Observable } from 'rxjs';
import { ScreenSizeHelper } from '../../helpers/screen-size-helper';
import { NotificationService } from 'src/app/modules/notifications/services/notification.service';
import { AuthenticationService } from 'src/app/modules/identity/services/authentication.service';
import { AuthenticationResponse } from 'src/app/core/models/authentication-response';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  @Output()
  darkModeSwitched: EventEmitter<boolean> = new EventEmitter<boolean>();

  notificationsCount$!: Observable<number>;
  menuToggled: boolean = false;
  authenticationSubscription!: Subscription;
  authenticationResponse: AuthenticationResponse = null!;

  constructor(
    public screenSizeHelper: ScreenSizeHelper,
    public themeService: ThemeService,
    private notificationService: NotificationService,
    private authenticationService: AuthenticationService,
    private router: Router
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
    this.router.events
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe(() => {
        const sidenav = document.querySelector('.sidenav');
        if (sidenav?.classList.contains('is-active')) {
          this.toggleMenu();
        }
      });
  }

  switchTheme(): void {
    this.darkModeSwitched.emit(true);
  }

  toggleMenu(): void {
    const sidenav = document.querySelector('.sidenav');
    sidenav?.classList.toggle('is-active');

    // const navbar = document.querySelector('.navbar');
    // navbar?.classList.toggle('is-active');

    this.menuToggled = !this.menuToggled;

    const media = window.matchMedia('(max-width: 768px)');
    if (media.matches) {
      this.blurContent();
      this.addHamburgerOpenClass();
      this.hideScrollbar();
    }
  }

  private blurContent(): void {
    const content = document.querySelector('.content') as HTMLDivElement;

    content.style.opacity = this.menuToggled ? '0' : '1';
  }

  private addHamburgerOpenClass(): void {
    const hambuger = document.querySelector('.hamburger');
    hambuger?.classList.toggle('open');
  }

  private hideScrollbar(): void {
    const main = document.querySelector('.main') as HTMLDivElement;

    main.style.overflowY = this.menuToggled ? 'hidden' : 'auto';
  }
}
