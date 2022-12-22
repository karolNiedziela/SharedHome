import { ConfirmationModalConfig } from './../../../shared/components/modals/confirmation-modal/confirmation-modal.config';
import { ConfirmationModalComponent } from './../../../shared/components/modals/confirmation-modal/confirmation-modal.component';
import { RejectInvitationComponent } from './../modals/reject-invitation/reject-invitation.component';
import { AcceptInvitationComponent } from './../modals/accept-invitation/accept-invitation.component';
import { AcceptInvitation } from './../models/accept-invitation';
import { Subscription } from 'rxjs/internal/Subscription';
import { FormGroup, FormControl } from '@angular/forms';
import { PopupMenuConfig } from 'app/shared/components/menus/popup-menu/popup-menu.config';
import { ColumnSetting } from './../../../shared/components/tables/column-setting';
import { ApiResponse } from './../../../core/models/api-response';
import { map, Observable } from 'rxjs';
import {
  AfterViewInit,
  Component,
  OnInit,
  ViewChild,
  OnDestroy,
} from '@angular/core';
import { InvitationService } from '../services/invitation.service';
import { Invitation } from '../models/invitation';
import { CellPipeFormat } from 'app/shared/components/tables/cell-pipe-format';
import { InvitationStatus } from '../enums/invitation-status';
import { EnumSelectComponent } from 'app/shared/components/selects/enum-select/enum-select.component';

@Component({
  selector: 'app-invitatons-list',
  templateUrl: './invitatons-list.component.html',
  styleUrls: ['./invitatons-list.component.scss'],
})
export class InvitatonsListComponent
  implements OnInit, AfterViewInit, OnDestroy
{
  invitations$!: Observable<Invitation[]>;

  @ViewChild('statusSelected')
  private statusSelect!: EnumSelectComponent;

  @ViewChild('acceptInvitationModal')
  acceptInvitationModal!: AcceptInvitationComponent;

  @ViewChild('rejectInvitationModal')
  rejectInvitationModal!: RejectInvitationComponent;

  @ViewChild('deleteInvitationConfirmationModal')
  deleteInvitationConfirmationModal!: ConfirmationModalComponent;
  deleteInvitationConfirmationModalConfig: ConfirmationModalConfig = {
    modalTitle: 'Delete invitation',
    confirmationText: 'Are you sure to delete the invitation?',
  };

  invitationForm!: FormGroup;
  public invitationStatus: typeof InvitationStatus = InvitationStatus;
  statusSelected: number = InvitationStatus.Pending;

  statusSelectedSubscription!: Subscription;

  invitationsSubscription!: Subscription;

  columnSettings: ColumnSetting[] = [];

  constructor(private invitationService: InvitationService) {}

  ngOnInit(): void {
    this.getInvitations(InvitationStatus.Pending);

    this.invitationForm = new FormGroup({
      status: new FormControl(InvitationStatus.Pending),
    });

    this.invitationsSubscription =
      this.invitationService.allInvitationsRefreshNeeded.subscribe(() => {
        this.getInvitations(this.statusSelected);
      });

    this.setColumnsBasedOnStatus();
  }

  ngAfterViewInit(): void {
    this.statusSelectedSubscription =
      this.statusSelect.selectedChanged.subscribe((selectedValue: number) => {
        this.onStatusChange(selectedValue!);
        this.statusSelected = selectedValue;
        this.setColumnsBasedOnStatus();
      });
  }

  ngOnDestroy(): void {
    this.statusSelectedSubscription.unsubscribe();
    this.invitationsSubscription.unsubscribe();
  }

  getInvitations(status?: number) {
    this.invitations$ = this.invitationService.getByStatus(status).pipe(
      map((response: ApiResponse<Invitation[]>) => {
        return response.data;
      })
    );
  }

  getPopupMenuConfigs(invitations: Invitation[]): PopupMenuConfig[] | null {
    const popupMenuConfigs: PopupMenuConfig[] = [];

    for (let invitation of invitations) {
      let popupMenuConfig: PopupMenuConfig = {
        isHidden: false,
        isDeleteVisible: true,
        onDelete: () => {
          this.deleteInvitationConfirmationModalConfig.onSave = () => {
            this.invitationService.delete(invitation.houseGroupId).subscribe();
          };
          this.deleteInvitationConfirmationModal.open();
        },
        isEditVisible: false,
      };

      switch (Number(invitation.invitationStatus)) {
        case InvitationStatus.Pending:
          popupMenuConfig.additionalPopupMenuItems = [
            {
              text: 'Accept',
              onClick: () => {
                this.acceptInvitationModal.houseGroupId =
                  invitation.houseGroupId;
                this.acceptInvitationModal.houseGroupName =
                  invitation.houseGroupName;

                this.acceptInvitationModal.openModal();
              },
            },
            {
              text: 'Reject',
              onClick: () => {
                this.rejectInvitationModal.houseGroupId =
                  invitation.houseGroupId;
                this.rejectInvitationModal.houseGroupName =
                  invitation.houseGroupName;

                this.rejectInvitationModal.openModal();
              },
            },
          ];

          popupMenuConfigs.push(popupMenuConfig);
          break;

        case InvitationStatus.Rejected:
          popupMenuConfigs.push(popupMenuConfig);
          break;

        case InvitationStatus.Accepted:
          popupMenuConfigs.push(popupMenuConfig);
          break;
      }
    }

    return popupMenuConfigs.length == 0 ? null : popupMenuConfigs;
  }

  onStatusChange(selectedStatus: number): void {
    if (this.invitationForm.invalid) {
      return;
    }

    this.getInvitations(selectedStatus);
  }

  setColumnsBasedOnStatus(): void {
    this.columnSettings = [
      {
        propertyName: 'houseGroupName',
        header: 'House Group Name',
      },
      {
        propertyName: 'invitationStatus',
        header: 'Status',
        format: CellPipeFormat.ENUM,
        enumType: InvitationStatus,
      },
    ];

    if (this.statusSelected == InvitationStatus.Sent) {
      this.columnSettings.push({
        propertyName: 'sentByFullName',
        header: 'Sent To',
      });
    } else {
      this.columnSettings.push({
        propertyName: 'sentByFullName',
        header: 'Sent By',
      });
    }
  }
}
