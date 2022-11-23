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

  public modalConfig: ModalConfig = {
    modalTitle: 'Upload profile image',
    onSave: () => this.onSave(),
    onClose: () => this.onClose(),
    onDismiss: () => this.onDismiss(),
  };

  constructor() {}
  openModal(): void {
    this.uploadProfileImageModal.open();
  }
  onSave(): void {}
  onClose(): void {}
  onDismiss(): void {}

  ngOnInit(): void {}

  getProfileImage(profileImage: File): void {
    console.log(profileImage);
  }
}
