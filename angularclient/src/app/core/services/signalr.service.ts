import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { environment } from 'environments/environment';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SignalrService {
  connection!: signalR.HubConnection;
  hubHelloMessage!: BehaviorSubject<any>;

  constructor() {
    this.hubHelloMessage = new BehaviorSubject<string>(null!);
  }

  public initiateSignalRConnection(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl(`${environment.withoutApiUrl}/notify`, {
          skipNegotiation: true,
          transport: signalR.HttpTransportType.WebSockets,
        })
        .build();

      this.getNotifications();

      this.connection
        .start()
        .then(() => {
          console.log(
            `SignalR connection success! connectionId: ${this.connection.connectionId}`
          );
          resolve();
        })
        .catch((error) => {
          console.log(`SignalR connection error: ${error}`);
          reject();
        });
    });
  }

  private getNotifications(): void {
    this.connection.on('BroadcastNotification', (message: any) => {
      this.hubHelloMessage.next(message);
    });
  }
}
