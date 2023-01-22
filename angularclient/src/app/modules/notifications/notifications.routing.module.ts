import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/core/guards/auth.guard';
import { ListNotificationsComponent } from './list-notifications/list-notifications.component';

const routes: Routes = [
  {
    path: 'notifications',
    component: ListNotificationsComponent,
    canActivate: [AuthGuard],
    title: 'notifications.module',
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class NotificationsRoutingModule {}
