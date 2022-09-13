import { RemoveMember } from './../models/remove-member';
import { HouseGroupService } from './../services/housegroup.service';
import { HouseGroupMember } from './../models/housegroup-member';
import { Component, Input, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { faStar } from '@fortawesome/free-regular-svg-icons';
import { PopupMenuConfig } from 'app/shared/components/menus/popup-menu/popup-menu.config';
import { ConfirmationModalComponent } from 'app/shared/components/modals/confirmation-modal/confirmation-modal.component';
import { ConfirmationModalConfig } from 'app/shared/components/modals/confirmation-modal/confirmation-modal.config';

@Component({
  selector: 'app-house-group-member',
  templateUrl: './house-group-member.component.html',
  styleUrls: ['./house-group-member.component.scss'],
})
export class HouseGroupMemberComponent implements OnInit {
  @Input() member!: HouseGroupMember;
  @Input() isCurrentUserOwner!: boolean;
  @Input() houseGroupId!: number;

  ownerIcon = faStar;
  popupMenuConfig!: PopupMenuConfig;

  @ViewChild('removeMemberModal')
  removeMemberModal!: ConfirmationModalComponent;
  removeMemberModalConfig: ConfirmationModalConfig = {
    modalTitle: 'Remove member',
  };

  constructor(private houseGroupService: HouseGroupService) {}

  ngOnInit(): void {
    this.popupMenuConfig = {
      isDeleteVisible: false,
      isEditVisible: false,
      additionalPopupMenuItems: [
        {
          text: 'Remove member',
          onClick: () => {
            this.removeMemberModal.open();
          },
        },
        {
          text: 'Hand over the owner',
          onClick: () => {},
        },
      ],
    };

    this.removeMemberModalConfig.confirmationText =
      'Are you sure to remove member: ';
    this.removeMemberModalConfig.confirmationProperties = [
      this.member.fullName,
    ];
    const removeMember: RemoveMember = {
      houseGroupId: this.houseGroupId,
      personToRemoveId: this.member.personId,
    };

    console.log(removeMember);
    this.removeMemberModalConfig.onSave = () => {
      this.houseGroupService.removeMember(removeMember).subscribe(() => {});
    };
  }
}
