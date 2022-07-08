import { ConfirmationModalConfig } from './confirmation-modal.config';
import {
  Component,
  Input,
  OnInit,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { faXmark } from '@fortawesome/free-solid-svg-icons';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-confirmation-modal',
  templateUrl: './confirmation-modal.component.html',
  styleUrls: ['../modal.scss', './confirmation-modal.component.scss'],
})
export class ConfirmationModalComponent implements OnInit {
  @Input() confirmationModalConfig!: ConfirmationModalConfig;
  @ViewChild('confirmationModal')
  private modalContent!: TemplateRef<ConfirmationModalComponent>;
  private modalRef!: NgbModalRef;

  dismissIcon = faXmark;

  constructor(private modalService: NgbModal) {}

  ngOnInit(): void {
    if (!this.confirmationModalConfig.modalTitle) {
      this.confirmationModalConfig.modalTitle = 'Delete confirmation';
    }

    if (!this.confirmationModalConfig.confirmationText) {
      this.confirmationModalConfig.confirmationText =
        'Are you sure to delete this item?';
    }

    if (!this.confirmationModalConfig.yesButtonText) {
      this.confirmationModalConfig.yesButtonText = 'Confirm';
    }

    if (!this.confirmationModalConfig.noButtonText) {
      this.confirmationModalConfig.noButtonText = 'No';
    }
  }

  async open(): Promise<void> {
    if (this.confirmationModalConfig?.onOpen != undefined) {
      const result = await this.confirmationModalConfig.onOpen();

      this.modalService.open(this.modalContent);
      return;
    }

    this.modalRef = this.modalService.open(this.modalContent);
  }

  async save(): Promise<void> {
    if (this.confirmationModalConfig?.onSave != undefined) {
      const result = await this.confirmationModalConfig.onSave();

      this.modalRef.close(result);
      return;
    }

    this.modalRef.close();
  }

  async close(): Promise<void> {
    if (this.confirmationModalConfig?.onClose != undefined) {
      const result = await this.confirmationModalConfig.onClose();

      this.modalRef.close(result);
      return;
    }
    this.modalRef.close();
  }

  dismiss(): void {
    this.modalRef.dismiss();
  }
}
