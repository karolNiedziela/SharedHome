import { RejectInvitation } from './../../models/reject-invitation';
import { InvitationService } from './../../services/invitation.service';
import { ModalConfig } from './../../../../shared/components/modals/modal/modal.config';
import { ModalComponent } from './../../../../shared/components/modals/modal/modal.component';
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
  private modal!: ModalComponent;
  public modalConfig: ModalConfig = {
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

  errorMessages: string[] = [];

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
    if (this.rejectInvitationForm.invalid) {
      this.rejectInvitationForm.markAllAsTouched();

      return;
    }

    const rejectInvitation: RejectInvitation = {
      houseGroupId: this.houseGroupId,
    };

    this.invitationService.reject(rejectInvitation).subscribe({
      next: () => {
        this.modal.close();
      },
      error: (errors: string[]) => {
        this.errorMessages = errors;
      },
    });
  }
  onClose(): void {}
  onDismiss(): void {}
}
