import { faXmark } from '@fortawesome/free-solid-svg-icons';
import {
  Component,
  Input,
  OnInit,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ModalConfig } from './modal.config';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['../modal.scss', './modal.component.scss'],
})
export class ModalComponent implements OnInit {
  @Input() public modalConfig!: ModalConfig;
  @ViewChild('modal') private modalContent!: TemplateRef<ModalComponent>;
  private modalRef!: NgbModalRef;

  dismissIcon = faXmark;

  constructor(private modalService: NgbModal) {}

  ngOnInit(): void {
    if (!this.modalConfig.closeButtonLabel) {
      this.modalConfig.closeButtonLabel = 'Close';
    }

    if (!this.modalConfig.isCloseButtonVisible) {
      this.modalConfig.isCloseButtonVisible = true;
    }

    if (!this.modalConfig.saveButtonLabel) {
      this.modalConfig.saveButtonLabel = 'Save';
    }

    if (!this.modalConfig.isSaveButtonVisible) {
      this.modalConfig.isSaveButtonVisible = true;
    }
  }

  open(): void {
    this.modalRef = this.modalService.open(this.modalContent, {
      beforeDismiss: () => this.beforeDismiss(),
      backdrop: 'static',
      keyboard: false,
    });
  }

  save(): void {
    if (this.modalConfig?.onSave != undefined) {
      const result = this.modalConfig.onSave();
    }
  }

  close(): void {
    if (this.modalConfig?.onClose != undefined) {
      const result = this.modalConfig.onClose();
    }

    this.modalRef.close();
  }

  dismiss(): void {
    if (this.modalConfig?.onDismiss != undefined) {
      const result = this.modalConfig.onDismiss();
    }
    this.modalRef.dismiss();
  }

  beforeDismiss(): boolean {
    this.close();
    return true;
  }
}
