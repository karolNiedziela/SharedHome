import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormModalComponent } from 'src/app/shared/components/modals/form-modal/form-modal.component';
import { Modalable } from 'src/app/core/models/modalable';
import { FormModalConfig } from 'src/app/shared/components/modals/form-modal/form-modal.config';
import { AuthenticationService } from 'src/app/modules/identity/services/authentication.service';
import { ChangePassword } from '../../models/change-password';
import { PasswordFormComponent } from 'src/app/shared/forms/password-form/password-form.component';

@Component({
  selector: 'app-change-password-modal',
  templateUrl: './change-password-modal.component.html',
  styleUrls: ['./change-password-modal.component.scss'],
})
export class ChangePasswordModalComponent implements OnInit, Modalable {
  @ViewChild('modal')
  private changePasswordModal!: FormModalComponent;
  @ViewChild('passwordForm') passwordForm!: PasswordFormComponent;

  public modalConfig: FormModalConfig = {
    modalTitle: 'Change password',
    onSave: () => this.onSave(),
  };

  changePasswordForm!: FormGroup;

  constructor(private authenticationService: AuthenticationService) {}

  ngOnInit(): void {
    this.changePasswordForm = new FormGroup({
      currentPassword: new FormControl('', [Validators.required]),
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

    this.changePasswordModal.save(
      this.authenticationService.changePassword(changePassword)
    );
  }
}
