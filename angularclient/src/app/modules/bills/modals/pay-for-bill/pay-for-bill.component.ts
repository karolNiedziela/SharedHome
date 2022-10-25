import { MoneyFormComponent } from './../../../../shared/components/forms/money-form/money-form.component';
import { BillService } from './../../services/bill.service';
import { Modalable } from './../../../../core/models/modalable';
import {
  ChangeDetectionStrategy,
  Component,
  Input,
  OnInit,
  ViewChild,
} from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ModalComponent } from 'app/shared/components/modals/modal/modal.component';
import { ModalConfig } from 'app/shared/components/modals/modal/modal.config';
import { PayForBill } from '../../models/pay-for-bill';

@Component({
  selector: 'app-pay-for-bill',
  templateUrl: './pay-for-bill.component.html',
  styleUrls: ['./pay-for-bill.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PayForBillComponent implements Modalable, OnInit {
  @Input() billId!: string;

  @ViewChild('modal') private modal!: ModalComponent;
  public modalConfig: ModalConfig = {
    modalTitle: 'Pay for bill',
    onSave: () => this.onSave(),
    onClose: () => this.onClose(),
    onDismiss: () => this.onDismiss(),
  };

  payForBillForm!: FormGroup;

  public errorMessages: string[] = [];

  constructor(private billService: BillService) {}

  ngOnInit(): void {
    this.payForBillForm = new FormGroup({
      money: new FormGroup({}),
    });
  }

  openModal(): void {
    this.modal.open();
  }
  onSave(): void {
    if (this.payForBillForm.invalid) {
      this.payForBillForm.markAllAsTouched();

      return;
    }

    const cost = this.payForBillForm.get('money')?.get('price')?.value;

    const currency = this.payForBillForm.get('money')?.get('currency')?.value;

    const payForBill: PayForBill = {
      billId: this.billId,
      cost: {
        price: cost,
        currency: currency,
      },
    };

    this.billService.payForBill(payForBill).subscribe({
      next: () => {
        this.modal.close();
        this.resetForm();
      },
      error: (errors: string[]) => {
        this.errorMessages = errors;
      },
    });
  }

  onClose(): void {
    this.resetForm();
  }

  onDismiss(): void {
    this.resetForm();
  }

  private resetForm(): void {
    this.payForBillForm.reset();
  }
}
