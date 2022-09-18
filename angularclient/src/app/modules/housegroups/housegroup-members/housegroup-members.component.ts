import { faSignOut } from '@fortawesome/free-solid-svg-icons';
import { AuthenticationService } from './../../identity/services/authentication.service';
import { Subscription } from 'rxjs/internal/Subscription';
import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { ApiResponse } from 'app/core/models/api-response';
import { Observable, map } from 'rxjs';
import { HouseGroup } from '../models/housegroup';
import { HouseGroupService } from '../services/housegroup.service';
import { faStar } from '@fortawesome/free-regular-svg-icons';
import { AuthenticationResponse } from 'app/core/models/authenticationResponse';
import { ConfirmationModalComponent } from 'app/shared/components/modals/confirmation-modal/confirmation-modal.component';
import { ConfirmationModalConfig } from 'app/shared/components/modals/confirmation-modal/confirmation-modal.config';

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
  houseGroupId!: number;

  leaveHouseGroupIcon = faSignOut;

  @ViewChild('leaveHouseGroup')
  leaveHouseGroupModal!: ConfirmationModalComponent;
  leaveHouseGroupModalConfig: ConfirmationModalConfig = {
    modalTitle: 'Leave house group',
    confirmationText: 'Are you sure to leave the house group?',
  };

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

    this.leaveHouseGroupModalConfig.onSave = () => {
      this.houseGroupService.leaveHouseGroup(this.houseGroupId).subscribe();
    };
  }

  getHouseGroup(): void {
    this.houseGroup$ = this.houseGroupService.get().pipe(
      map((response: ApiResponse<HouseGroup>) => {
        if (response == null) {
          return response;
        }
        this.isCurrentUserOwner = response.data?.members.some(
          (x) => x.personId == this.personId && x.isOwner
        );

        this.houseGroupId = response.data?.id;

        return response;
      })
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((subscription) => subscription.unsubscribe());
  }
}
