import { Subscription } from 'rxjs/internal/Subscription';
import { ErrorService } from '../../../../core/services/error.service';
import { FormGroup } from '@angular/forms';
import { faXmark } from '@fortawesome/free-solid-svg-icons';
import {
  AfterViewInit,
  Component,
  Input,
  OnDestroy,
  OnInit,
  SimpleChanges,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { FormModalConfig } from './form-modal.config';

@Component({
  selector: 'app-form-modal',
  templateUrl: './form-modal.component.html',
  styleUrls: ['../modal.scss', './form-modal.component.scss'],
})
export class FormModalComponent implements OnInit, OnDestroy {
  @Input() public modalConfig!: FormModalConfig;
  @Input() public formGroup?: FormGroup | null;
  @Input() public resetForm: boolean = true;

  @ViewChild('modal') private modalContent!: TemplateRef<FormModalComponent>;

  private modalRef!: NgbModalRef;

  dismissIcon = faXmark;
  disabled: boolean = false;
  errorMessages: string[] = [];
  errorSubscription!: Subscription;
  formGroupSubscription: undefined | Subscription;

  constructor(
    private modalService: NgbModal,
    private errorService: ErrorService
  ) {}

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

    this.errorSubscription = this.errorService.errors$.subscribe({
      next: (errors: string[]) => {
        this.errorMessages = errors;
      },
    });

    this.formGroupSubscription = this.formGroup?.valueChanges.subscribe({
      next: () => {
        this.disabled = this.formGroup?.invalid ? true : false;
      },
    });
  }

  ngOnDestroy(): void {
    this.errorSubscription.unsubscribe();
    this.formGroupSubscription?.unsubscribe();
  }

  open(): void {
    this.errorService.clearErrors();

    this.modalRef = this.modalService.open(this.modalContent, {
      beforeDismiss: () => this.beforeDismiss(),
      backdrop: 'static',
      keyboard: false,
    });
  }

  save(): void {
    this.disabled = true;

    if (this.formGroup?.invalid) {
      this.formGroup.markAllAsTouched();
      return;
    }

    this.errorService.clearErrors();

    this.modalConfig.onSave();

    this.disabled = false;
  }

  close(): void {
    this.beforeCloseModal();

    if (this.modalConfig?.onClose != undefined) {
      this.modalConfig.onClose();
    }

    this.modalRef.close();

    this.afterModalClose();
  }

  dismiss(): void {
    this.beforeCloseModal();

    if (this.modalConfig?.onDismiss != undefined) {
      this.modalConfig.onDismiss();
    }

    this.modalRef.dismiss();

    this.afterModalClose();
  }

  beforeDismiss(): boolean {
    this.close();
    return true;
  }

  private beforeCloseModal(): void {}

  private afterModalClose(): void {
    if (this.resetForm) {
      this.formGroup?.reset();
      this.modalConfig?.onReset?.();
    }
    this.errorService.clearErrors();
  }
}
