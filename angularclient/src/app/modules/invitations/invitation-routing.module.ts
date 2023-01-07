import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/core/guards/auth.guard';
import { InvitatonsListComponent } from './invitatons-list/invitatons-list.component';

const routes: Routes = [
  {
    path: 'invitations',
    component: InvitatonsListComponent,
    canActivate: [AuthGuard],
    title: 'Invitations',
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class InvitationRoutingModule {}
