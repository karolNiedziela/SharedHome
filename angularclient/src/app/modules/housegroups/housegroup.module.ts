import { MaterialModule } from './../../material.module';
import { HouseGroupMemberComponent } from './house-group-member/house-group-member.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HousegroupRoutingModule } from './housegroup-routing.module';
import { HousegroupMembersComponent } from './housegroup-members/housegroup-members.component';
import { TranslateModule } from '@ngx-translate/core';
import { AddHouseGroupComponent } from './modals/add-house-group/add-house-group.component';
import { HandOwnerRoleOverComponent } from './modals/hand-owner-role-over/hand-owner-role-over.component';
import { InviteToHouseGroupComponent } from './modals/invite-to-house-group/invite-to-house-group.component';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  declarations: [
    HousegroupMembersComponent,
    HouseGroupMemberComponent,
    AddHouseGroupComponent,
    HandOwnerRoleOverComponent,
    InviteToHouseGroupComponent,
  ],
  imports: [
    CommonModule,
    HousegroupRoutingModule,
    SharedModule,
    TranslateModule,
    MaterialModule,
  ],
})
export class HousegroupModule {}
