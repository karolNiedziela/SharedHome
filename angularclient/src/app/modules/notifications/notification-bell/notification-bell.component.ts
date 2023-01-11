import { Component, OnInit } from '@angular/core';
import { map, Observable } from 'rxjs';
import { AppNotification } from '../models/app-notification';
import { NotificationService } from '../services/notification.service';

@Component({
  selector: 'app-notification-bell',
  templateUrl: './notification-bell.component.html',
  styleUrls: ['./notification-bell.component.scss'],
})
export class NotificationBellComponent implements OnInit {
  notifications$!: Observable<AppNotification[]>;
  notificationsCount$!: Observable<string>;
  notificationsCount: string = '0';

  constructor(private notificationService: NotificationService) {}

  ngOnInit(): void {
    this.notificationsCount$ =
      this.notificationService.notificationsCount$.pipe(
        map((count: number) => {
          this.notificationsCount = count > 10 ? '10+' : count.toString();
          return this.notificationsCount;
        })
      );

    this.notifications$ = this.notificationService.notifications$;
  }

  onMenuClose(event: any): void {
    this.markAsRead();
  }

  markAsRead(): void {
    if (this.notificationsCount == '0') {
      return;
    }

    this.notificationService.markAsRead().subscribe();
  }
}
