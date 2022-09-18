import { ApiResponse } from './../../../../core/models/api-response';
import { SendInvitation } from '../../../invitations/models/send-invitation';
import { ModalConfig } from './../../../../shared/components/modals/modal/modal.config';
import { ModalComponent } from './../../../../shared/components/modals/modal/modal.component';
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
  @Input() houseGroupId!: number;

  @ViewChild('modal')
  private modal!: ModalComponent;
  public modalConfig: ModalConfig = {
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
    if (this.inviteToHouseGroupForm.invalid) {
      this.inviteToHouseGroupForm.markAllAsTouched();

      return;
    }

    const requestToPersonEmail = this.inviteToHouseGroupForm.get(
      'requestedToPersonEmail'
    )?.value;

    const sendInvitation: SendInvitation = {
      houseGroupId: this.houseGroupId,
      requestedToPersonEmail: requestToPersonEmail,
    };

    this.invitationService.send(sendInvitation).subscribe({
      next: (response: ApiResponse<Invitation>) => {
        this.inviteToHouseGroupForm.reset();
        this.modal.close();
      },
      error: (errors: string[]) => {
        this.errorMessages = errors;
      },
    });
  }
  onClose(): void {
    this.inviteToHouseGroupForm.reset();
  }
  onDismiss(): void {
    this.inviteToHouseGroupForm.reset();
  }
}
