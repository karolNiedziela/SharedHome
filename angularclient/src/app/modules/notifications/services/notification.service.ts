import { AppNotification } from '../models/app-notification';
import { BehaviorSubject, Observable, map, tap } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Paged } from 'app/core/models/paged';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  private notificationsUrl: string = `${environment.apiUrl}/notifications`;

  private defaultHttpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  private _notifications: BehaviorSubject<AppNotification[]> =
    new BehaviorSubject<AppNotification[]>(null!);
  public notifications$: Observable<AppNotification[]> =
    this._notifications.asObservable();

  private _notificationsCount: BehaviorSubject<number> =
    new BehaviorSubject<number>(0);
  public notificationsCount$: Observable<number> =
    this._notificationsCount.asObservable();

  constructor(private http: HttpClient) {
    if (this._notifications.value == null) {
      this.get();
    }
  }

  get() {
    this.http
      .get<Paged<AppNotification>>(
        this.notificationsUrl,
        this.defaultHttpOptions
      )
      .subscribe({
        next: (notifications: Paged<AppNotification>) => {
          console.log(notifications);
          this._notifications.next(notifications.items);
          this._notificationsCount.next(notifications.customTotalItems!);
        },
      });
  }

  add(notification: AppNotification) {
    console.log(notification);
    this._notifications.next([...this._notifications.value, notification]);
    this._notificationsCount.next(this._notificationsCount.value + 1);
  }
}
