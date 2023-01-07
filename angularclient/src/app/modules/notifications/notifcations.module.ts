import { TranslateModule } from '@ngx-translate/core';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { NotificationsRoutingModule } from './notifications.routing.module';
import { ListNotificationsComponent } from './list-notifications/list-notifications.component';

@NgModule({
  declarations: [ListNotificationsComponent],
  imports: [
    CommonModule,
    SharedModule,
    TranslateModule,
    NotificationsRoutingModule,
  ],
})
export class NotificationsModule {}
