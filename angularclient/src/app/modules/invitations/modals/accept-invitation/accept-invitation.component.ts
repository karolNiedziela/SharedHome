import { AcceptInvitation } from './../../models/accept-invitation';
import { InvitationService } from './../../services/invitation.service';
import { ModalConfig } from './../../../../shared/components/modals/modal/modal.config';
import { ModalComponent } from './../../../../shared/components/modals/modal/modal.component';
import { Modalable } from './../../../../core/models/modalable';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-accept-invitation',
  templateUrl: './accept-invitation.component.html',
  styleUrls: ['./accept-invitation.component.scss'],
})
export class AcceptInvitationComponent implements Modalable, OnInit {
  @Input() houseGroupId!: number;
  @Input() houseGroupName!: string;

  @ViewChild('modal')
  private modal!: ModalComponent;
  public modalConfig: ModalConfig = {
    modalTitle: 'Accept invitation',
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

  errorMessages: string[] = [];

  acceptInvitationForm!: FormGroup;

  constructor(private invitationService: InvitationService) {}

  ngOnInit(): void {
    this.acceptInvitationForm = new FormGroup({
      houseGroupName: new FormControl({
        value: '',
        disabled: true,
      }),
    });
  }

  openModal(): void {
    this.acceptInvitationForm.patchValue({
      houseGroupName: this.houseGroupName,
    });
    this.modal.open();
  }
  onSave(): void {
    if (this.acceptInvitationForm.invalid) {
      this.acceptInvitationForm.markAllAsTouched();

      return;
    }

    const acceptInvitation: AcceptInvitation = {
      houseGroupId: this.houseGroupId,
    };

    this.invitationService.accept(acceptInvitation).subscribe({
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
