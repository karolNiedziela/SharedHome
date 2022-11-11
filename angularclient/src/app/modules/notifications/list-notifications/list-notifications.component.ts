import { NotificationService } from 'app/modules/notifications/services/notification.service';
import { NotificationType } from './../constants/notification-type';
import { TargetType } from './../constants/target-type';
import { FormGroup, FormControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { AppNotification } from '../models/app-notification';
import { debounceTime, distinctUntilChanged, Observable } from 'rxjs';
import { Paged } from 'app/core/models/paged';

@Component({
  selector: 'app-list-notifications',
  templateUrl: './list-notifications.component.html',
  styleUrls: ['./list-notifications.component.scss'],
})
export class ListNotificationsComponent implements OnInit {
  notificationsForm!: FormGroup;
  notifications$!: Observable<Paged<AppNotification>>;

  public targetType: typeof TargetType = TargetType;
  public notificationType: typeof NotificationType = NotificationType;

  constructor(private notificationService: NotificationService) {}

  ngOnInit(): void {
    this.notificationsForm = new FormGroup({
      targetType: new FormControl(TargetType.All),
      notificationType: new FormControl(NotificationType.All),
    });

    this.notificationsForm.valueChanges.pipe(debounceTime(300)).subscribe({
      next: (formValues: any) => {
        this.getNotifications(formValues);
      },
    });
  }

  private getNotifications(formValues: any): void {
    const targetType: number = formValues.targetType;
    const notificationType: number = formValues.notificationType;
    this.notifications$ = this.notificationService.getByType(
      targetType,
      notificationType
    );
  }
}
