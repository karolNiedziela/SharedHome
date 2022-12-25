import { AcceptInvitation } from './../../models/accept-invitation';
import { InvitationService } from './../../services/invitation.service';
import { FormModalConfig } from '../../../../shared/components/modals/form-modal/form-modal.config';
import { FormModalComponent } from '../../../../shared/components/modals/form-modal/form-modal.component';
import { Modalable } from './../../../../core/models/modalable';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-accept-invitation',
  templateUrl: './accept-invitation.component.html',
  styleUrls: ['./accept-invitation.component.scss'],
})
export class AcceptInvitationComponent implements Modalable, OnInit {
  @Input() houseGroupId!: string;
  @Input() houseGroupName!: string;

  @ViewChild('modal')
  private modal!: FormModalComponent;
  public modalConfig: FormModalConfig = {
    modalTitle: 'Accept invitation',
    onSave: () => {
      this.onSave();
    },
  };
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
    const acceptInvitation: AcceptInvitation = {
      houseGroupId: this.houseGroupId,
    };

    this.modal.save(this.invitationService.accept(acceptInvitation));
  }
}
