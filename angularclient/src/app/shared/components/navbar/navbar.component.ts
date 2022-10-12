import { Component, OnInit } from '@angular/core';
import { faBell } from '@fortawesome/free-solid-svg-icons';
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

  constructor(private notificationService: NotificationService) {}

  ngOnInit(): void {
    this.notificationsCount$ = this.notificationService.notificationsCount$;
  }
}
