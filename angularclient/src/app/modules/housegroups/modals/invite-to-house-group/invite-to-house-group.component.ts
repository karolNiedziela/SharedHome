import { FormModalConfig } from '../../../../shared/components/modals/form-modal/form-modal.config';
import { FormModalComponent } from '../../../../shared/components/modals/form-modal/form-modal.component';
import { Modalable } from './../../../../core/models/modalable';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { InvitationService } from 'src/app/modules/invitations/services/invitation.service';
import { SendInvitation } from 'src/app/modules/invitations/models/send-invitation';

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
    modalTitle: 'house_groups.invite_to_house_group',
    onSave: () => this.onSave(),
  };

  inviteToHouseGroupForm!: FormGroup;

  errorMessages: string[] = [];

  constructor(private invitationService: InvitationService) {}

  ngOnInit(): void {
    this.inviteToHouseGroupForm = new FormGroup({
      requestedToPersonEmail: new FormControl('', [
        Validators.required,
        Validators.email,
      ]),
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

    this.modal.save(this.invitationService.send(sendInvitation));
  }
}
