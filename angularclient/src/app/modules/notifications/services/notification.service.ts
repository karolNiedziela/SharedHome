import { BehaviorSubject, Observable, tap } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AppNotification } from '../models/app-notification';
import { Paged } from 'src/app/core/models/paged';

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

  constructor(private http: HttpClient) {}

  get() {
    if (this._notifications != null && this._notifications.value?.length > 0) {
      return;
    }

    this.http
      .get<Paged<AppNotification>>(
        this.notificationsUrl,
        this.defaultHttpOptions
      )
      .subscribe({
        next: (notifications: Paged<AppNotification>) => {
          this._notifications.next(notifications.items);
          this._notificationsCount.next(notifications.customTotalItems!);
        },
      });
  }

  getByType(
    targetType: number,
    notificationType: number,
    pageNumber: number
  ): Observable<Paged<AppNotification>> {
    return this.http.post<Paged<AppNotification>>(
      `${this.notificationsUrl}`,
      {
        targetType,
        notificationType,
        pageNumber,
        pageSize: 10,
      },
      this.defaultHttpOptions
    );
  }

  markAsRead(): Observable<any> {
    return this.http
      .patch(`${this.notificationsUrl}/markasread`, {}, this.defaultHttpOptions)
      .pipe(
        tap(() => {
          const allAsRead = this._notifications.value.map((x) => {
            x.isRead = true;
            return x;
          });

          this._notifications.next(allAsRead);
          this._notificationsCount.next(0);
        })
      );
  }

  add(notification: AppNotification): void {
    this._notifications.next([notification, ...this._notifications.value]);
    this._notificationsCount.next(this._notificationsCount.value + 1);
  }

  clear(): void {
    this._notifications.next([]);
    this._notificationsCount.next(0);
  }
}
