import { RemoveMember } from './../models/remove-member';
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

@Component({
  selector: 'app-housegroup-members',
  templateUrl: './housegroup-members.component.html',
  styleUrls: ['./housegroup-members.component.scss'],
})
export class HousegroupMembersComponent implements OnInit, OnDestroy {
  houseGroup$!: Observable<ApiResponse<HouseGroup>>;
  ownerIcon = faStar;
  subscriptions: Subscription[] = [];
  personId: string = '';
  isCurrentUserOwner: boolean = false;
  houseGroupId!: number;

  constructor(
    private houseGroupService: HouseGroupService,
    private authenticationService: AuthenticationService
  ) {}

  ngOnInit(): void {
    this.subscriptions.push(
      this.authenticationService.authenticationResponse.subscribe(
        (result: AuthenticationResponse) => {
          this.personId = result.userId;
        }
      )
    );

    this.getHouseGroup();

    this.subscriptions.push(
      this.houseGroupService.houseGroupRefreshNeeded.subscribe(() => {
        this.getHouseGroup();
      })
    );
  }

  getHouseGroup(): void {
    this.houseGroup$ = this.houseGroupService.get().pipe(
      tap((response: ApiResponse<HouseGroup>) => {
        this.isCurrentUserOwner = response.data.members.some(
          (x) => x.personId == this.personId && x.isOwner
        );

        this.houseGroupId = response.data.id;
      })
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((subscription) => subscription.unsubscribe());
  }
}
