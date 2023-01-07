import { Paginatable } from './../../../core/models/paginatable';
import { NotificationType } from './../constants/notification-type';
import { TargetType } from './../constants/target-type';
import { FormGroup, FormControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { AppNotification } from '../models/app-notification';
import { debounceTime, Observable } from 'rxjs';
import { Paged } from 'src/app/core/models/paged';
import { NotificationService } from '../services/notification.service';

@Component({
  selector: 'app-list-notifications',
  templateUrl: './list-notifications.component.html',
  styleUrls: ['./list-notifications.component.scss'],
})
export class ListNotificationsComponent implements OnInit, Paginatable {
  notificationsForm!: FormGroup;
  notifications$!: Observable<Paged<AppNotification>>;

  public targetType: typeof TargetType = TargetType;
  public notificationType: typeof NotificationType = NotificationType;

  public currentPage: number = 1;
  private formValues!: any;

  constructor(private notificationService: NotificationService) {}

  ngOnInit(): void {
    this.notificationsForm = new FormGroup({
      targetType: new FormControl(TargetType.All),
      notificationType: new FormControl(NotificationType.All),
    });

    this.notificationsForm.valueChanges.pipe(debounceTime(300)).subscribe({
      next: (formValues: any) => {
        this.formValues = formValues;
        this.getNotifications();
      },
    });
  }

  public onPrevious(): void {
    this.currentPage -= 1;

    this.getNotifications();
  }

  public onNext(): void {
    this.currentPage += 1;

    this.getNotifications();
  }

  public goTo(page: number): void {
    this.currentPage = page;

    this.getNotifications();
  }

  private getNotifications(): void {
    const targetType: number = this.formValues.targetType;
    const notificationType: number = this.formValues.notificationType;
    this.notifications$ = this.notificationService.getByType(
      targetType,
      notificationType,
      this.currentPage
    );
  }
}
