import { ConfirmationModalComponent } from 'src/app/shared/components/modals/confirmation-modal/confirmation-modal.component';
import {
  Component,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { finalize, Observable, Subscription } from 'rxjs';
import { ErrorService } from 'src/app/core/services/error.service';
import { FormModalConfig } from './form-modal.config';

@Component({
  selector: 'app-form-modal',
  templateUrl: './form-modal.component.html',
  styleUrls: ['./form-modal.component.scss'],
})
export class FormModalComponent implements OnInit {
  @Input() public modalConfig!: FormModalConfig;
  @Input() public formGroup?: FormGroup | null;
  @Input() public resetForm: boolean = true;

  @ViewChild('modal')
  private modalContent!: TemplateRef<ConfirmationModalComponent>;

  private dialogRef!: MatDialogRef<any, any>;

  disabled!: boolean;
  errorMessages: string[] = [];
  errorSubscription!: Subscription;
  formGroupSubscription: undefined | Subscription;
  loadingSaveButton: boolean = false;

  constructor(private errorService: ErrorService, private dialog: MatDialog) {}

  ngOnInit(): void {
    if (!this.modalConfig.closeButtonLabel) {
      this.modalConfig.closeButtonLabel = 'shared.operations.close';
    }

    if (!this.modalConfig.isCloseButtonVisible) {
      this.modalConfig.isCloseButtonVisible = true;
    }

    if (!this.modalConfig.saveButtonLabel) {
      this.modalConfig.saveButtonLabel = 'shared.operations.save';
    }

    if (!this.modalConfig.isSaveButtonVisible) {
      this.modalConfig.isSaveButtonVisible = true;
    }

    this.errorSubscription = this.errorService.errors$.subscribe({
      next: (errors: string[]) => {
        this.errorMessages = errors;
      },
    });

    this.disabled = false;
  }

  open(): void {
    this.errorService.clearErrors();

    this.formGroupSubscription = this.formGroup?.valueChanges.subscribe({
      next: () => {
        this.disabled = this.formGroup?.invalid ? true : false;
      },
    });

    this.dialogRef = this.dialog.open(this.modalContent, {
      panelClass: ['md:w-3/5', 'lg:w-1/2', 'w-full'],
      hasBackdrop: true,
      autoFocus: false,
    });

    this.dialogRef.beforeClosed().subscribe((result) => {
      this.beforeCloseModal();
    });

    this.dialogRef.afterClosed().subscribe((result) => {
      this.afterModalClose();
    });
  }

  onSave(): void {
    this.modalConfig.onSave();
  }

  save(saveOperation: Observable<any>): void {
    this.disabled = true;

    if (this.formGroup?.invalid) {
      this.formGroup.markAllAsTouched();
      this.formGroup.updateValueAndValidity();
      this.disabled = this.formGroup?.invalid ? true : false;
      return;
    }

    this.dialogRef.disableClose = true;

    this.loadingSaveButton = true;

    this.errorService.clearErrors();

    saveOperation
      ?.pipe(
        finalize(() => {
          this.disabled = false;
          this.loadingSaveButton = false;
        })
      )
      .subscribe({
        next: () => {
          this.dialogRef.disableClose = false;
          this.close();
        },
      });
  }

  close(): void {
    if (this.dialogRef.disableClose) {
      return;
    }

    this.beforeCloseModal();

    if (this.modalConfig?.onClose != undefined) {
      this.modalConfig.onClose();
    }

    this.dialogRef.close();

    this.afterModalClose();
  }

  private beforeCloseModal(): void {}

  private afterModalClose(): void {
    if (this.resetForm) {
      this.formGroup?.reset();
      this.modalConfig?.onReset?.();
    }
    this.errorService.clearErrors();

    this.disabled = false;
  }
}
