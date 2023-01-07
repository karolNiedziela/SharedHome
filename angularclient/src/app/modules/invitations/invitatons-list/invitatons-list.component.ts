import { SingleEnumSelectComponent } from 'src/app/shared/components/selects/single-enum-select/single-enum-select.component';
import { ConfirmationModalConfig } from './../../../shared/components/modals/confirmation-modal/confirmation-modal.config';
import { ConfirmationModalComponent } from './../../../shared/components/modals/confirmation-modal/confirmation-modal.component';
import { RejectInvitationComponent } from './../modals/reject-invitation/reject-invitation.component';
import { AcceptInvitationComponent } from './../modals/accept-invitation/accept-invitation.component';
import { Subscription } from 'rxjs/internal/Subscription';
import { FormGroup, FormControl } from '@angular/forms';
import { TableColumn } from './../../../shared/components/tables/column-setting';
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
import { InvitationStatus } from '../enums/invitation-status';
import { PopupMenuConfig } from 'src/app/shared/components/menus/popup-menu/popup-menu.config';
import { CellPipeFormat } from 'src/app/shared/components/tables/cell-pipe-format';
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
  private statusSelect!: SingleEnumSelectComponent;

  @ViewChild('acceptInvitationModal')
  acceptInvitationModal!: AcceptInvitationComponent;

  @ViewChild('rejectInvitationModal')
  rejectInvitationModal!: RejectInvitationComponent;

  @ViewChild('deleteInvitationConfirmationModal')
  deleteInvitationConfirmationModal!: ConfirmationModalComponent;
  deleteInvitationConfirmationModalConfig!: ConfirmationModalConfig;

  invitationForm!: FormGroup;
  public invitationStatus: typeof InvitationStatus = InvitationStatus;
  statusSelected: number = InvitationStatus.Pending;

  statusSelectedSubscription!: Subscription;

  invitationsSubscription!: Subscription;

  invitationsTableColumns: TableColumn[] = [];

  popupMenuConfigs: PopupMenuConfig[] = [];

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

    this.deleteInvitationConfirmationModalConfig = {
      modalTitle: 'Delete invitation',
      confirmationText: 'Are you sure to delete the invitation?',
      onConfirm: () => {},
    };
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
        this.getPopupMenuConfigs(response.data);
        return response.data;
      })
    );
  }

  getPopupMenuConfigs(invitations: Invitation[]): void {
    const popupMenuConfigs: PopupMenuConfig[] = [];

    for (let invitation of invitations) {
      let popupMenuConfig: PopupMenuConfig = {
        isDeleteVisible: true,
        onDelete: () => {
          (this.deleteInvitationConfirmationModalConfig.onConfirm = () =>
            this.invitationService.delete(invitation.houseGroupId).subscribe()),
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

        case InvitationStatus.Sent:
          popupMenuConfigs.push({
            isDeleteVisible: false,
            isEditVisible: false,
            isHidden: true,
          });
      }
    }

    this.popupMenuConfigs =
      popupMenuConfigs.length == 0 ? [] : popupMenuConfigs;
  }

  onStatusChange(selectedStatus: number): void {
    if (this.invitationForm.invalid) {
      return;
    }

    this.getInvitations(selectedStatus);
  }

  setColumnsBasedOnStatus(): void {
    this.invitationsTableColumns = [
      {
        name: 'House Group Name',
        dataKey: 'houseGroupName',
      },
    ];

    if (this.statusSelected == InvitationStatus.Sent) {
      this.invitationsTableColumns.push({
        name: 'Sent To',
        dataKey: 'sentByFullName',
      });
    } else {
      this.invitationsTableColumns.push(
        {
          name: 'Status',
          dataKey: 'invitationStatus',
          enumType: InvitationStatus,
          format: CellPipeFormat.ENUM,
        },
        {
          name: 'Sent By',
          dataKey: 'sentByFullName',
        }
      );
    }
  }
}
