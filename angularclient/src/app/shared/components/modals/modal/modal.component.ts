import { faXmark } from '@fortawesome/free-solid-svg-icons';
import {
  Component,
  ContentChild,
  ContentChildren,
  ElementRef,
  Input,
  OnInit,
  QueryList,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ModalConfig } from './modal.config';
import { ContentRef } from '@ng-bootstrap/ng-bootstrap/util/popup';

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
    });
  }

  async save(): Promise<void> {
    if (this.modalConfig?.onSave != undefined) {
      const result = await this.modalConfig.onSave();
    }

    this.modalRef.close();
  }

  async close(): Promise<void> {
    if (this.modalConfig?.onClose != undefined) {
      const result = await this.modalConfig.onClose();
    }

    this.modalRef.close();
  }

  dismiss(): void {
    if (this.modalConfig?.onDismiss != undefined) {
      const result = this.modalConfig.onDismiss();
    }
    this.modalRef.dismiss();
  }

  beforeDismiss(): Promise<boolean> {
    this.close();
    return Promise.resolve(true);
  }
}
