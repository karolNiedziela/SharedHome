import { ChangePassword } from './../models/change-password';
import { AuthenticationService } from 'app/modules/identity/services/authentication.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Modalable } from './../../../core/models/modalable';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalComponent } from 'app/shared/components/modals/modal/modal.component';
import { ModalConfig } from 'app/shared/components/modals/modal/modal.config';
import { PasswordsFormComponent } from 'app/shared/components/forms/passwords-form/passwords-form.component';

@Component({
  selector: 'app-change-password-modal',
  templateUrl: './change-password-modal.component.html',
  styleUrls: ['./change-password-modal.component.scss'],
})
export class ChangePasswordModalComponent implements OnInit, Modalable {
  @ViewChild('modal')
  private changePasswordModal!: ModalComponent;
  @ViewChild('passwordForm') passwordForm!: PasswordsFormComponent;

  public modalConfig: ModalConfig = {
    modalTitle: 'Change password',
    onSave: () => this.onSave(),
    onClose: () => this.onClose(),
    onDismiss: () => this.onDismiss(),
  };

  changePasswordForm!: FormGroup;
  errorMessages: string[] = [];

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
    if (this.changePasswordForm.invalid) {
      this.changePasswordForm.markAllAsTouched();
      this.passwordForm.passwordForm.markAllAsTouched();
      return;
    }

    const currentPassword: string =
      this.changePasswordForm.controls['currentPassword'].value;
    const newPassword: string =
      this.changePasswordForm.controls['newPassword'].value;

    const changePassword: ChangePassword = {
      currentPassword: currentPassword,
      newPassword: newPassword,
      confirmNewPassword: newPassword,
    };

    console.log(changePassword);

    this.authenticationService.changePassword(changePassword).subscribe({
      next: () => {
        this.changePasswordForm.reset();
        this.changePasswordModal.close();
      },
      error: (errors: string[]) => {
        this.errorMessages = errors;
      },
    });
  }
  onClose(): void {
    this.changePasswordForm.reset();
  }
  onDismiss(): void {
    this.changePasswordForm.reset();
  }
}
