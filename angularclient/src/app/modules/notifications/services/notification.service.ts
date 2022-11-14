import { AppNotification } from '../models/app-notification';
import { BehaviorSubject, Observable, map, tap } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
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

  add(notification: AppNotification) {
    this._notifications.next([...this._notifications.value, notification]);
    this._notificationsCount.next(this._notificationsCount.value + 1);
  }
}
