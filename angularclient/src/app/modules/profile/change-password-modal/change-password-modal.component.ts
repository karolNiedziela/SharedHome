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

  constructor() {}

  ngOnInit(): void {}

  openModal(): void {
    this.uploadProfileImageModal.open();
  }
  onSave(): void {}
  onClose(): void {}
  onDismiss(): void {}
}
