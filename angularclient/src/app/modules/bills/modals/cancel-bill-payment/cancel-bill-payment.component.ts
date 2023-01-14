import { ConfirmationModalConfig } from './../../../../shared/components/modals/confirmation-modal/confirmation-modal.config';
import { ConfirmationModalComponent } from './../../../../shared/components/modals/confirmation-modal/confirmation-modal.component';
import { Modalable } from './../../../../core/models/modalable';
import { BillService } from './../../services/bill.service';
import { Component, Input, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-cancel-bill-payment',
  templateUrl: './cancel-bill-payment.component.html',
  styleUrls: ['./cancel-bill-payment.component.scss'],
})
export class CancelBillPaymentComponent implements Modalable, OnInit {
  @Input() billId!: string;

  @ViewChild('modal') private modal!: ConfirmationModalComponent;
  public modalConfig: ConfirmationModalConfig = {
    modalTitle: 'bills.cancel_payment',
    confirmationText: 'bills.cancel_payment_text',
    onConfirm: () => {
      this.onSave();
    },
  };

  constructor(private billService: BillService) {}

  ngOnInit(): void {}

  openModal(): void {
    this.modal.open();
  }

  onSave(): void {
    this.billService.cancelBillPayment(this.billId).subscribe();
  }
}
