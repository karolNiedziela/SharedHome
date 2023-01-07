import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { IconProp } from '@fortawesome/fontawesome-svg-core';
import {
  faBell,
  faCartShopping,
  faEnvelope,
  faFileInvoice,
  faQuestion,
  faUserGroup,
} from '@fortawesome/free-solid-svg-icons';
import { map, Observable } from 'rxjs';
import { TargetType } from '../constants/target-type';
import { AppNotification } from '../models/app-notification';
import { NotificationService } from '../services/notification.service';

@Component({
  selector: 'app-notification-bell',
  templateUrl: './notification-bell.component.html',
  styleUrls: ['./notification-bell.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class NotificationBellComponent implements OnInit {
  notifications$!: Observable<AppNotification[]>;
  notificationIcon = faBell;
  notificationsCount$!: Observable<string>;
  notificationsCount: string = '0';

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
        map((count: number) => {
          this.notificationsCount = count > 10 ? '10+' : count.toString();
          return this.notificationsCount;
        })
      );

    this.notifications$ = this.notificationService.notifications$;
  }

  ngAfterViewInit(): void {
    // const notificationDropdown = document.getElementById(
    //   'notificationDropdown'
    // ) as HTMLElement;
    // notificationDropdown.addEventListener('hide.bs.dropdown', () => {
    //   this.markAsRead();
    // });
  }

  markAsRead(): void {
    if (this.notificationsCount == '0') {
      return;
    }
    this.notificationService.markAsRead().subscribe();
  }
}
