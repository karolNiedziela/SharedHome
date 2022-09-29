import { AppNotification } from './../../../core/models/app-notification';
import { BehaviorSubject, Observable, map, tap } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';

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
    console.log(this._notifications.value);
    if (this._notifications.value == null) {
      this.get();
    }
  }

  get() {
    this.http
      .get<AppNotification[]>(this.notificationsUrl, this.defaultHttpOptions)
      .subscribe({
        next: (notifications: AppNotification[]) => {
          this._notifications.next(notifications);
          this._notificationsCount.next(notifications.length);
        },
      });
  }

  add(notification: AppNotification) {
    this._notifications.next([...this._notifications.value, notification]);
    console.log(this._notifications.value);
    this._notificationsCount.next(this._notificationsCount.value + 1);
    console.log(this._notificationsCount.value);
  }
}
