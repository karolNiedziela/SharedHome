import { ApiResponse } from './../../../../core/models/api-response';
import { SendInvitation } from '../../../invitations/models/send-invitation';
import { FormModalConfig } from '../../../../shared/components/modals/form-modal/form-modal.config';
import { FormModalComponent } from '../../../../shared/components/modals/form-modal/form-modal.component';
import { Modalable } from './../../../../core/models/modalable';
import { InvitationService } from '../../../invitations/services/invitation.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { Invitation } from 'app/modules/invitations/models/invitation';

@Component({
  selector: 'app-invite-to-house-group',
  templateUrl: './invite-to-house-group.component.html',
  styleUrls: ['./invite-to-house-group.component.scss'],
})
export class InviteToHouseGroupComponent implements Modalable, OnInit {
  @Input() houseGroupId!: string;

  @ViewChild('modal')
  private modal!: FormModalComponent;
  public modalConfig: FormModalConfig = {
    modalTitle: 'Invite to house group',
    onSave: () => this.onSave(),
    onClose: () => this.onClose(),
    onDismiss: () => this.onDismiss(),
  };

  inviteToHouseGroupForm!: FormGroup;

  errorMessages: string[] = [];

  constructor(private invitationService: InvitationService) {}

  ngOnInit(): void {
    this.inviteToHouseGroupForm = new FormGroup({
      requestedToPersonEmail: new FormControl('', [Validators.required]),
    });
  }

  openModal(): void {
    this.modal.open();
  }
  onSave(): void {
    const requestToPersonEmail = this.inviteToHouseGroupForm.get(
      'requestedToPersonEmail'
    )?.value;

    const sendInvitation: SendInvitation = {
      houseGroupId: this.houseGroupId,
      requestedToPersonEmail: requestToPersonEmail,
    };

    this.invitationService.send(sendInvitation).subscribe({
      next: (response: ApiResponse<Invitation>) => {
        this.modal.close();
      },
    });
  }
  onClose(): void {}
  onDismiss(): void {}
}
