import { AppNotification } from '../../../../modules/notifications/models/app-notification';
import { Observable } from 'rxjs';
import { NotificationService } from 'app/modules/notifications/services/notification.service';
import { Component, OnInit, ViewChild } from '@angular/core';
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
  selector: 'app-list-notifications',
  templateUrl: './list-notifications.component.html',
  styleUrls: ['./list-notifications.component.scss'],
})
export class ListNotificationsComponent implements OnInit {
  notifications$!: Observable<AppNotification[]>;
  notificationIcon = faBell;
  notificationsCount$!: Observable<number>;

  iconByTargetType: Record<string, IconProp> = {
    [TargetType[TargetType.Other]]: faQuestion,
    [TargetType[TargetType.ShoppingList]]: faCartShopping,
    [TargetType[TargetType.Bill]]: faFileInvoice,
    [TargetType[TargetType.Invitation]]: faEnvelope,
    [TargetType[TargetType.HouseGroup]]: faUserGroup,
    [TargetType[TargetType.User]]: faUser,
  };

  constructor(private notificationService: NotificationService) {}

  ngOnInit(): void {
    this.notificationsCount$ = this.notificationService.notificationsCount$;
    this.notifications$ = this.notificationService.notifications$;
  }

  loadNotifications(): void {
    console.log('load');
  }
}
