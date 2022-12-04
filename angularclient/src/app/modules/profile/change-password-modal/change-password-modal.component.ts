import { ChangePassword } from './../models/change-password';
import { AuthenticationService } from 'app/modules/identity/services/authentication.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Modalable } from './../../../core/models/modalable';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormModalComponent } from 'app/shared/components/modals/form-modal/form-modal.component';
import { FormModalConfig } from 'app/shared/components/modals/form-modal/form-modal.config';
import { PasswordsFormComponent } from 'app/shared/components/forms/passwords-form/passwords-form.component';

@Component({
  selector: 'app-change-password-modal',
  templateUrl: './change-password-modal.component.html',
  styleUrls: ['./change-password-modal.component.scss'],
})
export class ChangePasswordModalComponent implements OnInit, Modalable {
  @ViewChild('modal')
  private changePasswordModal!: FormModalComponent;
  @ViewChild('passwordForm') passwordForm!: PasswordsFormComponent;

  public modalConfig: FormModalConfig = {
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
    this.changePasswordModal.open();
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

    this.authenticationService.changePassword(changePassword).subscribe({
      next: () => {
        this.changePasswordModal.close();
      },
    });
  }
  onClose(): void {}
  onDismiss(): void {}
}
