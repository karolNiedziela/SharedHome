import { ConfirmationModalComponent } from 'app/shared/components/modals/confirmation-modal/confirmation-modal.component';
import { PopupMenuConfig } from './../../../shared/components/menus/popup-menu/popup-menu.config';
import { AuthenticationService } from './../../identity/services/authentication.service';
import { Subscription } from 'rxjs/internal/Subscription';
import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { ApiResponse } from 'app/core/models/api-response';
import { Observable, tap } from 'rxjs';
import { HouseGroup } from '../models/housegroup';
import { HouseGroupService } from '../services/housegroup.service';
import { faStar } from '@fortawesome/free-regular-svg-icons';
import { AuthenticationResponse } from 'app/core/models/authenticationResponse';
import { ConfirmationModalConfig } from 'app/shared/components/modals/confirmation-modal/confirmation-modal.config';
@Component({
  selector: 'app-housegroup-members',
  templateUrl: './housegroup-members.component.html',
  styleUrls: ['./housegroup-members.component.scss'],
})
export class HousegroupMembersComponent implements OnInit, OnDestroy {
  houseGroup$!: Observable<ApiResponse<HouseGroup>>;
  ownerIcon = faStar;
  subscription!: Subscription;
  personId: string = '';
  isCurrentUserOwner: boolean = false;

  @ViewChild('removeMemberModal')
  removeMemberModal!: ConfirmationModalComponent;
  removeMemberModalConfig: ConfirmationModalConfig = {
    modalTitle: 'Remove member',
  };
  constructor(
    private houseGroupService: HouseGroupService,
    private authenticationService: AuthenticationService
  ) {}

  ngOnInit(): void {
    this.subscription =
      this.authenticationService.authenticationResponse.subscribe(
        (result: AuthenticationResponse) => {
          this.personId = result.userId;
        }
      );

    this.houseGroup$ = this.houseGroupService.get().pipe(
      tap((response: ApiResponse<HouseGroup>) => {
        this.isCurrentUserOwner = response.data.members.some(
          (x) => x.personId == this.personId && x.isOwner
        );
      })
    );
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  getPopupMenuConfig(): PopupMenuConfig | null {
    if (this.isCurrentUserOwner) {
      const popupMenuConfig: PopupMenuConfig = {
        isDeleteVisible: false,
        isEditVisible: false,
        additionalPopupMenuItems: [
          {
            text: 'Remove member',
            onClick: () => {},
          },
          {
            text: 'Hand over the owner',
            onClick: () => {},
          },
        ],
      };

      return popupMenuConfig;
    }

    return null;
  }
}
