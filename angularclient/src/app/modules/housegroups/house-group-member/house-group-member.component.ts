import { HandOwnerRoleOverComponent } from './../modals/hand-owner-role-over/hand-owner-role-over.component';
import { RemoveMember } from './../models/remove-member';
import { HouseGroupService } from './../services/housegroup.service';
import { HouseGroupMember } from './../models/housegroup-member';
import { Component, Input, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { ConfirmationModalConfig } from 'src/app/shared/components/modals/confirmation-modal/confirmation-modal.config';
import { PopupMenuConfig } from 'src/app/shared/components/menus/popup-menu/popup-menu.config';
import { faStar } from '@fortawesome/free-solid-svg-icons';
import { ConfirmationModalComponent } from 'src/app/shared/components/modals/confirmation-modal/confirmation-modal.component';

@Component({
  selector: 'app-house-group-member',
  templateUrl: './house-group-member.component.html',
  styleUrls: ['./house-group-member.component.scss'],
})
export class HouseGroupMemberComponent implements OnInit {
  @Input() member!: HouseGroupMember;
  @Input() isCurrentUserOwner!: boolean;
  @Input() houseGroupId!: string;

  ownerIcon = faStar;
  popupMenuConfig!: PopupMenuConfig;

  @ViewChild('removeMemberModal')
  removeMemberModal!: ConfirmationModalComponent;
  removeMemberModalConfig!: ConfirmationModalConfig;

  @ViewChild('handOwnerRoleOverModal')
  handOwnerRoleOverModal!: HandOwnerRoleOverComponent;

  constructor(private houseGroupService: HouseGroupService) {}

  ngOnInit(): void {
    this.popupMenuConfig = {
      isDeleteVisible: false,
      isEditVisible: false,
      additionalPopupMenuItems: [
        {
          text: 'house_groups.remove_member',
          onClick: () => {
            this.removeMemberModal.open();
          },
        },
        {
          text: 'house_groups.hand_over_the_owner',
          onClick: () => {
            this.handOwnerRoleOverModal.openModal();
          },
        },
      ],
    };

    this.removeMemberModalConfig = {
      modalTitle: 'house_groups.remove_member',
      confirmationText: 'house_groups.remove_member_text',
      confirmationProperties: [this.member.fullName],
      onConfirm: () => {
        this.houseGroupService.removeMember(removeMember).subscribe(() => {});
      },
    };

    const removeMember: RemoveMember = {
      houseGroupId: this.houseGroupId,
      personToRemoveId: this.member.personId,
    };
  }
}
