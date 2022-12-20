import { RejectInvitation } from './../../models/reject-invitation';
import { InvitationService } from './../../services/invitation.service';
import { FormModalConfig } from '../../../../shared/components/modals/form-modal/form-modal.config';
import { FormModalComponent } from '../../../../shared/components/modals/form-modal/form-modal.component';
import { Modalable } from './../../../../core/models/modalable';
import { FormControl, FormGroup } from '@angular/forms';
import { Component, Input, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-reject-invitation',
  templateUrl: './reject-invitation.component.html',
  styleUrls: ['./reject-invitation.component.scss'],
})
export class RejectInvitationComponent implements Modalable, OnInit {
  @Input() houseGroupId!: string;
  @Input() houseGroupName!: string;

  @ViewChild('modal')
  private modal!: FormModalComponent;
  public modalConfig: FormModalConfig = {
    modalTitle: 'Reject invitation',
    onSave: () => {
      this.onSave();
    },
    onClose: () => {
      this.onClose();
    },
    onDismiss: () => {
      this.onDismiss();
    },
  };

  rejectInvitationForm!: FormGroup;

  constructor(private invitationService: InvitationService) {}

  ngOnInit(): void {
    this.rejectInvitationForm = new FormGroup({
      houseGroupName: new FormControl({
        value: '',
        disabled: true,
      }),
    });
  }

  openModal(): void {
    this.rejectInvitationForm.patchValue({
      houseGroupName: this.houseGroupName,
    });

    this.modal.open();
  }
  onSave(): void {
    const rejectInvitation: RejectInvitation = {
      houseGroupId: this.houseGroupId,
    };

    this.invitationService.reject(rejectInvitation).subscribe({
      next: () => {
        this.modal.close();
      },
    });
  }
  onClose(): void {}
  onDismiss(): void {}
}
