import { HouseGroupMember } from './../../models/housegroup-member';
import { tap } from 'rxjs';
import { HouseGroupService } from './../../services/housegroup.service';
import { HandOwnerRoleOver } from './../../models/hand-owner-role-over';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Modalable } from './../../../../core/models/modalable';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { ModalComponent } from 'app/shared/components/modals/modal/modal.component';
import { ModalConfig } from 'app/shared/components/modals/modal/modal.config';

@Component({
  selector: 'app-hand-owner-role-over',
  templateUrl: './hand-owner-role-over.component.html',
  styleUrls: ['./hand-owner-role-over.component.scss'],
})
export class HandOwnerRoleOverComponent implements Modalable, OnInit {
  @Input() houseGroupId!: number;
  @Input() member!: HouseGroupMember;

  @ViewChild('modal')
  private modal!: ModalComponent;
  public modalConfig: ModalConfig = {
    modalTitle: 'Change house group owner',
    onSave: () => this.onSave(),
    onClose: () => this.onClose(),
    onDismiss: () => this.onDismiss(),
  };

  errorMessages: string[] = [];

  handOwnerRoleOverForm!: FormGroup;

  constructor(private houseGroupService: HouseGroupService) {}

  ngOnInit(): void {
    this.handOwnerRoleOverForm = new FormGroup({
      newOwnerPerson: new FormControl({ value: '', disabled: true }),
    });

    this.handOwnerRoleOverForm.patchValue({
      newOwnerPerson: this.member.fullName,
    });
  }

  openModal(): void {
    this.modal.open();
  }
  onSave(): void {
    if (this.handOwnerRoleOverForm.invalid) {
      this.handOwnerRoleOverForm.markAllAsTouched();

      return;
    }

    const handOwnerRoleOver: HandOwnerRoleOver = {
      houseGroupId: this.houseGroupId,
      newOwnerPersonId: this.member.personId,
    };

    this.houseGroupService.handOwnerRoleOver(handOwnerRoleOver).subscribe({
      next: () => {
        this.onClose();
      },
      error: (errors: string[]) => {
        this.errorMessages = errors;
      },
    });
  }

  onClose(): void {
    this.resetForm();
  }
  onDismiss(): void {
    this.resetForm();
  }

  private resetForm(): void {
    this.handOwnerRoleOverForm.reset();
    this.handOwnerRoleOverForm.patchValue({
      newOwnerPerson: this.member.fullName,
    });
  }
}
