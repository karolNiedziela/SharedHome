import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from 'app/shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NotificationsRoutingModule } from './notifications-routing.module';
import { ListNotificationsComponent } from './list-notifications/list-notifications.component';

@NgModule({
  declarations: [
    ListNotificationsComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    TranslateModule,
    NotificationsRoutingModule,
  ],
})
export class NotificationsModule {}
