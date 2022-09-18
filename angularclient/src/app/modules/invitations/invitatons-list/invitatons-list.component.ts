import { ColumnSetting } from './../../../shared/components/tables/column-setting';
import { ApiResponse } from './../../../core/models/api-response';
import { map, Observable } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { InvitationService } from '../services/invitation.service';
import { Invitation } from '../models/invitation';
import { CellPipeFormat } from 'app/shared/components/tables/cell-pipe-format';
import { InvitationStatus } from '../enums/invitation-status';

@Component({
  selector: 'app-invitatons-list',
  templateUrl: './invitatons-list.component.html',
  styleUrls: ['./invitatons-list.component.scss'],
})
export class InvitatonsListComponent implements OnInit {
  invitations$!: Observable<Invitation[]>;

  columnSettings: ColumnSetting[] = [
    {
      propertyName: 'houseGroupId',
      header: 'House Group Id',
    },
    {
      propertyName: 'invitationStatus',
      header: 'Status',
      format: CellPipeFormat.ENUM,
      enumType: InvitationStatus,
    },
    {
      propertyName: 'sentByFirstName',
      header: 'Send By',
    },
    {
      propertyName: 'sentByLastName',
      header: 'Send To',
    },
  ];

  constructor(private invitationService: InvitationService) {}

  ngOnInit(): void {
    this.invitations$ = this.invitationService.getByStatus().pipe(
      map((response: ApiResponse<Invitation[]>) => {
        return response.data;
      })
    );
  }
}
