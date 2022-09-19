import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from 'app/shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InvitationRoutingModule } from './invitation-routing.module';
import { InvitatonsListComponent } from './invitatons-list/invitatons-list.component';
import { AcceptInvitationComponent } from './modals/accept-invitation/accept-invitation.component';
import { RejectInvitationComponent } from './modals/reject-invitation/reject-invitation.component';

@NgModule({
  declarations: [InvitatonsListComponent, AcceptInvitationComponent, RejectInvitationComponent],
  imports: [
    CommonModule,
    InvitationRoutingModule,
    SharedModule,
    TranslateModule,
  ],
})
export class InvitationModule {}
