import { UploadImageComponent } from './../../../../shared/components/inputs/upload-image/upload-image.component';
import { AuthenticationService } from 'app/modules/identity/services/authentication.service';
import { Modalable } from './../../../../core/models/modalable';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalComponent } from 'app/shared/components/modals/modal/modal.component';
import { ModalConfig } from 'app/shared/components/modals/modal/modal.config';
@Component({
  selector: 'app-upload-profile-image-modal',
  templateUrl: './upload-profile-image-modal.component.html',
  styleUrls: ['./upload-profile-image-modal.component.scss'],
})
export class UploadProfileImageModalComponent implements OnInit, Modalable {
  @ViewChild('modal')
  private uploadProfileImageModal!: ModalComponent;

  @ViewChild('uploadImage')
  private uploadImage!: UploadImageComponent;

  profileImage?: File | null;

  public modalConfig: ModalConfig = {
    modalTitle: 'Upload profile image',
    onSave: () => this.onSave(),
    onClose: () => this.onClose(),
    onDismiss: () => this.onDismiss(),
  };

  constructor(private authenticationService: AuthenticationService) {}

  ngOnInit(): void {}

  openModal(): void {
    this.uploadProfileImageModal.ngOnInit();
    this.uploadProfileImageModal.open();
  }
  onSave(): void {
    if (!this.profileImage) {
      return;
    }

    const formData = new FormData();
    formData.append('file', this.profileImage, this.profileImage.name);

    this.authenticationService.uploadProfileImage(formData);

    this.uploadProfileImageModal.close();
  }

  onClose(): void {
    this.uploadImage.deleteFile();
  }

  onDismiss(): void {
    this.uploadImage.deleteFile();
  }

  getProfileImage(profileImage: File): void {
    this.profileImage = profileImage;
  }
}
