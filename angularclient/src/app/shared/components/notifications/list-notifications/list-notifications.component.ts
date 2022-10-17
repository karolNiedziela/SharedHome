import { AppNotification } from '../../../../core/models/app-notification';
import { Observable } from 'rxjs';
import { NotificationService } from 'app/modules/notifications/services/notification.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { faBell } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-list-notifications',
  templateUrl: './list-notifications.component.html',
  styleUrls: ['./list-notifications.component.scss'],
})
export class ListNotificationsComponent implements OnInit {
  notifications$!: Observable<AppNotification[]>;
  notificationIcon = faBell;
  notificationsCount$!: Observable<number>;

  constructor(private notificationService: NotificationService) {}

  ngOnInit(): void {
    this.notificationsCount$ = this.notificationService.notificationsCount$;
    this.notifications$ = this.notificationService.notifications$;
  }

  loadNotifications(): void {
    console.log('load');
  }
}
