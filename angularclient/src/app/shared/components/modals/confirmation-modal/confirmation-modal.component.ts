import {
  Component,
  Input,
  OnInit,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ConfirmationModalConfig } from './confirmation-modal.config';

@Component({
  selector: 'app-confirmation-modal',
  templateUrl: './confirmation-modal.component.html',
  styleUrls: ['./confirmation-modal.component.scss'],
})
export class ConfirmationModalComponent implements OnInit {
  @Input() confirmationModalConfig!: ConfirmationModalConfig;
  @ViewChild('confirmationModal')
  private modalContent!: TemplateRef<ConfirmationModalComponent>;

  private dialogRef!: MatDialogRef<any, any>;

  constructor(private dialog: MatDialog) {}

  ngOnInit(): void {
    if (!this.confirmationModalConfig.modalTitle) {
      this.confirmationModalConfig.modalTitle =
        'shared.operations.delete_confirmation';
    }

    if (!this.confirmationModalConfig.confirmationText) {
      this.confirmationModalConfig.confirmationText =
        'shared.operations.delete_confirmation_text';
    }

    if (!this.confirmationModalConfig.yesButtonText) {
      this.confirmationModalConfig.yesButtonText = 'shared.operations.confirm';
    }

    if (!this.confirmationModalConfig.noButtonText) {
      this.confirmationModalConfig.noButtonText = 'shared.operations.cancel';
    }
  }

  open(): void {
    this.dialogRef = this.dialog.open(this.modalContent, {
      panelClass: ['md:w-3/5', 'lg:w-2/6', 'w-full'],
      maxHeight: '85vh',
      hasBackdrop: true,
      autoFocus: false,
    });
  }

  confirm(): void {
    this.confirmationModalConfig.onConfirm();
    this.close();
  }

  close(): void {
    this.dialogRef.close();
  }

  getJoinedConfirmationProperties(): string {
    return this.confirmationModalConfig.confirmationProperties
      ? this.confirmationModalConfig.confirmationProperties.join(', ')
      : '';
  }
}
