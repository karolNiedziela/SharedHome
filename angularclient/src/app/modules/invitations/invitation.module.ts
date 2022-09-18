import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from 'app/shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InvitationRoutingModule } from './invitation-routing.module';
import { InvitatonsListComponent } from './invitatons-list/invitatons-list.component';

@NgModule({
  declarations: [InvitatonsListComponent],
  imports: [
    CommonModule,
    InvitationRoutingModule,
    SharedModule,
    TranslateModule,
  ],
})
export class InvitationModule {}
