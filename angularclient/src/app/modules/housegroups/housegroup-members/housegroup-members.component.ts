import { faSignOut, faStar, faTrash } from '@fortawesome/free-solid-svg-icons';
import { AuthenticationService } from './../../identity/services/authentication.service';
import { Subscription } from 'rxjs/internal/Subscription';
import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { Observable, map } from 'rxjs';
import { HouseGroup } from '../models/housegroup';
import { HouseGroupService } from '../services/housegroup.service';
import { ApiResponse } from 'src/app/core/models/api-response';
import { ConfirmationModalConfig } from 'src/app/shared/components/modals/confirmation-modal/confirmation-modal.config';
import { ConfirmationModalComponent } from 'src/app/shared/components/modals/confirmation-modal/confirmation-modal.component';
import { AuthenticationResponse } from 'src/app/core/models/authentication-response';
import { HouseGroupMember } from '../models/housegroup-member';

@Component({
  selector: 'app-housegroup-members',
  templateUrl: './housegroup-members.component.html',
  styleUrls: ['./housegroup-members.component.scss'],
})
export class HousegroupMembersComponent implements OnInit, OnDestroy {
  houseGroup$?: Observable<ApiResponse<HouseGroup>>;
  ownerIcon = faStar;
  subscriptions: Subscription[] = [];
  personId: string = '';
  isCurrentUserOwner: boolean = false;
  houseGroupId!: string;
  loading: boolean = true;

  leaveHouseGroupIcon = faSignOut;
  deleteHouseGroupIcon = faTrash;

  @ViewChild('leaveHouseGroup')
  leaveHouseGroupModal!: ConfirmationModalComponent;
  leaveHouseGroupModalConfig!: ConfirmationModalConfig;

  @ViewChild('deleteHouseGroup')
  deleteHouseGroupModal!: ConfirmationModalComponent;
  deleteHouseGroupModalConfig!: ConfirmationModalConfig;

  constructor(
    private houseGroupService: HouseGroupService,
    private authenticationService: AuthenticationService
  ) {}

  ngOnInit(): void {
    this.subscriptions.push(
      this.authenticationService.authenticationResponse.subscribe(
        (result: AuthenticationResponse) => {
          this.personId = result?.userId;
        }
      )
    );

    this.getHouseGroup();

    this.subscriptions.push(
      this.houseGroupService.houseGroupRefreshNeeded.subscribe(() => {
        this.getHouseGroup();
      })
    );

    this.leaveHouseGroupModalConfig = {
      modalTitle: 'house_groups.leave_house_group',
      confirmationText: 'house_groups.leave_house_group_text',
      onConfirm: () => {
        this.houseGroupService.leaveHouseGroup(this.houseGroupId).subscribe();
      },
    };

    this.deleteHouseGroupModalConfig = {
      modalTitle: 'house_groups.delete_house_group',
      confirmationText: 'house_groups.delete_house_group_text',
      onConfirm: () => {
        this.houseGroupService.delete(this.houseGroupId).subscribe();
      },
    };
  }

  getHouseGroup(): void {
    this.houseGroup$ = this.houseGroupService.get().pipe(
      map((response: ApiResponse<HouseGroup>) => {
        if (response == null) {
          this.loading = false;
          return response;
        }
        this.isCurrentUserOwner = response.data?.members.some(
          (member: HouseGroupMember) =>
            member.personId == this.personId && member.isOwner
        );

        this.houseGroupId = response.data?.id;

        this.loading = false;
        return response;
      })
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((subscription) => subscription.unsubscribe());
  }
}
