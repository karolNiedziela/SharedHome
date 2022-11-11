import { AppNotification } from '../../../../modules/notifications/models/app-notification';
import { Observable, tap } from 'rxjs';
import { NotificationService } from 'app/modules/notifications/services/notification.service';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import {
  faBell,
  faCartShopping,
  faEnvelope,
  faFileInvoice,
  faQuestion,
  faUser,
  faUserGroup,
} from '@fortawesome/free-solid-svg-icons';
import { TargetType } from 'app/modules/notifications/constants/target-type';
import { IconProp } from '@fortawesome/fontawesome-svg-core';

@Component({
  selector: 'app-notification-bell',
  templateUrl: './notification-bell.component.html',
  styleUrls: ['./notification-bell.component.scss'],
})
export class NotificationBellComponent implements OnInit, AfterViewInit {
  notifications$!: Observable<AppNotification[]>;
  notificationIcon = faBell;
  notificationsCount$!: Observable<number>;
  notificationsCount: number = 0;

  iconByTargetType: Record<string, IconProp> = {
    [TargetType[TargetType.Other]]: faQuestion,
    [TargetType[TargetType.ShoppingList]]: faCartShopping,
    [TargetType[TargetType.Bill]]: faFileInvoice,
    [TargetType[TargetType.Invitation]]: faEnvelope,
    [TargetType[TargetType.HouseGroup]]: faUserGroup,
  };

  constructor(private notificationService: NotificationService) {}

  ngOnInit(): void {
    this.notificationsCount$ =
      this.notificationService.notificationsCount$.pipe(
        tap((count: number) => {
          this.notificationsCount = count;
        })
      );
    this.notifications$ = this.notificationService.notifications$;
  }

  ngAfterViewInit(): void {
    const notificationDropdown = document.getElementById(
      'notificationDropdown'
    ) as HTMLElement;
    notificationDropdown.addEventListener('hide.bs.dropdown', () => {
      this.markAsRead();
    });
  }

  markAsRead(): void {
    if (this.notificationsCount == 0) {
      return;
    }
    this.notificationService.markAsRead().subscribe();
  }
}
