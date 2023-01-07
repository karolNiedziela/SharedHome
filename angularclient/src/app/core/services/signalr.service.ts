import { NotificationService } from './../../modules/notifications/services/notification.service';
import { AppNotification } from '../../modules/notifications/models/app-notification';
import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class SignalrService {
  connection!: signalR.HubConnection;
  notification!: BehaviorSubject<AppNotification>;

  constructor(private notificationService: NotificationService) {
    this.notification = new BehaviorSubject<AppNotification>(null!);
  }

  public initiateSignalRConnection(accessToken: string): Promise<void> {
    return new Promise((resolve, reject) => {
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl(`${environment.withoutApiUrl}/notify`, {
          accessTokenFactory: () => accessToken,
        })
        .withAutomaticReconnect()
        .configureLogging(signalR.LogLevel.Information)
        .build();

      this.notificationService.get();

      this.listenNotifications();

      this.connection
        .start()
        .then(() => {
          resolve();
        })
        .catch((error) => {
          console.log(`SignalR connection error: ${error}`);
          reject();
        });
    });
  }

  public closeConnection(): void {
    this.connection.stop();
    this.notificationService.clear();
  }

  private listenNotifications(): void {
    this.connection.on(
      'BroadcastNotificationAsync',
      (notification: AppNotification) => {
        this.notification.next(notification);
        this.notificationService.add(notification);
      }
    );
  }
}
