import { HouseGroupMemberComponent } from './house-group-member/house-group-member.component';
import { HouseGroupMember } from './models/housegroup-member';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HousegroupRoutingModule } from './housegroup-routing.module';
import { HousegroupMembersComponent } from './housegroup-members/housegroup-members.component';
import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from 'app/shared/shared.module';

@NgModule({
  declarations: [HousegroupMembersComponent, HouseGroupMemberComponent],
  imports: [
    CommonModule,
    HousegroupRoutingModule,
    SharedModule,
    TranslateModule,
  ],
})
export class HousegroupModule {}
