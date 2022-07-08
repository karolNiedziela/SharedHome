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

    if (!this.modalConfig.saveButtonLabel) {
      this.modalConfig.saveButtonLabel = 'Save';
    }
  }

  async open(): Promise<void> {
    if (this.modalConfig?.onOpen != undefined) {
      const result = await this.modalConfig.onOpen();

      this.modalService.open(this.modalContent);
      return;
    }

    this.modalRef = this.modalService.open(this.modalContent);
  }

  async save(): Promise<void> {
    if (this.modalConfig?.onSave != undefined) {
      const result = await this.modalConfig.onSave();

      this.modalRef.close(result);
      return;
    }

    this.modalRef.close();
  }

  async close(): Promise<void> {
    if (this.modalConfig?.onClose != undefined) {
      const result = await this.modalConfig.onClose();

      this.modalRef.close(result);
      return;
    }
    this.modalRef.close();
  }

  dismiss(): void {
    this.modalRef.dismiss();
  }
}
