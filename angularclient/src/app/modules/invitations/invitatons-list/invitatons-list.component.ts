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

  invitationForm!: FormGroup;
  public invitationStatus: typeof InvitationStatus = InvitationStatus;

  statusSelectedSubscription!: Subscription;

  columnSettings: ColumnSetting[] = [
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
    {
      propertyName: 'sentByFullName',
      header: 'Send By',
    },
  ];

  constructor(private invitationService: InvitationService) {}

  ngOnInit(): void {
    this.getInvitations(InvitationStatus.Pending);

    this.invitationForm = new FormGroup({
      status: new FormControl(InvitationStatus.Pending),
    });
  }

  ngAfterViewInit(): void {
    this.statusSelectedSubscription =
      this.statusSelect.selectedChanged.subscribe(
        (selectedValue: number | undefined) => {
          this.onStatusChange(selectedValue!);
        }
      );
  }

  ngOnDestroy(): void {
    this.statusSelectedSubscription.unsubscribe();
  }

  getInvitations(status?: number) {
    this.invitations$ = this.invitationService.getByStatus(status).pipe(
      map((response: ApiResponse<Invitation[]>) => {
        return response.data;
      })
    );
  }

  getPopupMenuConfigs(invitations: Invitation[]): PopupMenuConfig[] {
    const popupMenuConfigs: PopupMenuConfig[] = [];

    return popupMenuConfigs;
  }

  onStatusChange(selectedStatus: number): void {
    if (this.invitationForm.invalid) {
      return;
    }

    this.getInvitations(selectedStatus);
  }
}
