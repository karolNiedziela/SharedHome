import { UploadImageComponent } from './../../../../shared/components/inputs/upload-image/upload-image.component';
import { Modalable } from './../../../../core/models/modalable';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormModalComponent } from 'src/app/shared/components/modals/form-modal/form-modal.component';
import { FormModalConfig } from 'src/app/shared/components/modals/form-modal/form-modal.config';
import { AuthenticationService } from 'src/app/modules/identity/services/authentication.service';
@Component({
  selector: 'app-upload-profile-image-modal',
  templateUrl: './upload-profile-image-modal.component.html',
  styleUrls: ['./upload-profile-image-modal.component.scss'],
})
export class UploadProfileImageModalComponent implements OnInit, Modalable {
  @ViewChild('modal')
  private uploadProfileImageModal!: FormModalComponent;

  @ViewChild('uploadImage')
  private uploadImage!: UploadImageComponent;

  profileImage?: File | null;
  error?: string;

  public modalConfig: FormModalConfig = {
    modalTitle: 'profile.profile_image',
    onSave: () => this.onSave(),
    onReset: () => this.onReset(),
  };

  constructor(private authenticationService: AuthenticationService) {}

  ngOnInit(): void {}

  openModal(): void {
    this.uploadProfileImageModal.open();
  }
  onSave(): void {
    if (!this.profileImage) {
      this.error = 'shared.inputs.field_required';
      return;
    }

    const formData = new FormData();
    formData.append('file', this.profileImage, this.profileImage.name);

    this.authenticationService.uploadProfileImage(formData);

    this.uploadProfileImageModal.close();
  }

  onReset(): void {
    this.uploadImage.deleteFile();
  }

  getProfileImage(profileImage: File): void {
    this.profileImage = profileImage;
  }
}
