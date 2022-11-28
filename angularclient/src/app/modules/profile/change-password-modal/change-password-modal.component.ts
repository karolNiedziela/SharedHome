import { ChangePassword } from './../models/change-password';
import { AuthenticationService } from 'app/modules/identity/services/authentication.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Modalable } from './../../../core/models/modalable';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalComponent } from 'app/shared/components/modals/modal/modal.component';
import { ModalConfig } from 'app/shared/components/modals/modal/modal.config';

@Component({
  selector: 'app-change-password-modal',
  templateUrl: './change-password-modal.component.html',
  styleUrls: ['./change-password-modal.component.scss'],
})
export class ChangePasswordModalComponent implements OnInit, Modalable {
  @ViewChild('modal')
  private uploadProfileImageModal!: ModalComponent;

  public modalConfig: ModalConfig = {
    modalTitle: 'Change password',
    onSave: () => this.onSave(),
    onClose: () => this.onClose(),
    onDismiss: () => this.onDismiss(),
  };

  changePasswordForm!: FormGroup;

  constructor(private authenticationService: AuthenticationService) {}

  ngOnInit(): void {
    this.changePasswordForm = new FormGroup({
      currentPassword: new FormControl(''),
      newPassword: new FormControl(''),
    });
  }

  openModal(): void {
    this.uploadProfileImageModal.open();
  }
  onSave(): void {
    const currentPassword: string =
      this.changePasswordForm.controls['currentPassword'].value;
    const newPassword: string =
      this.changePasswordForm.controls['newPassword'].value;

    const changePassword: ChangePassword = {
      currentPassword: currentPassword,
      newPassword: newPassword,
      confirmNewPassword: newPassword,
    };
  }
  onClose(): void {}
  onDismiss(): void {}
}
