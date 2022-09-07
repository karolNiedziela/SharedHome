import { BillType } from './../../enums/bill-type';
import { AddBill } from './../../models/add-bill';
import { BillService } from './../../services/bill.service';
import { ModalConfig } from './../../../../shared/components/modals/modal/modal.config';
import { Modalable } from './../../../../core/models/modalable';
import { ModalComponent } from './../../../../shared/components/modals/modal/modal.component';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-add-bill',
  templateUrl: './add-bill.component.html',
  styleUrls: ['./add-bill.component.scss'],
})
export class AddBillComponent implements Modalable, OnInit {
  addBillForm!: FormGroup;
  errorMessages: string[] = [];

  public billTypeType: typeof BillType = BillType;

  @ViewChild('modal') private modal!: ModalComponent;

  public modalConfig: ModalConfig = {
    modalTitle: 'Add bill',
    onSave: () => this.onSave(),
    onClose: () => this.onClose(),
    onDismiss: () => this.onDismiss(),
  };

  constructor(private billService: BillService) {}

  ngOnInit(): void {
    this.addBillForm = new FormGroup({
      billType: new FormControl(null, [Validators.required]),
      serviceProviderName: new FormControl('', [Validators.required]),
      dateOfPayment: new FormControl('', [Validators.required]),
      cost: new FormControl(null),
      currency: new FormControl(null),
    });
  }

  openModal(): void {
    this.modal.open();
  }
  onSave(): void {
    if (this.addBillForm.invalid) {
      this.addBillForm.markAllAsTouched();
      return;
    }

    const billType = this.addBillForm.get('billType')?.value;
    const serviceProviderName = this.addBillForm.get(
      'serviceProviderName'
    )?.value;
    const dateOfPayment = this.addBillForm.get('dateOfPayment')?.value;
    const cost = this.addBillForm.get('cost')?.value;
    const currency = this.addBillForm.get('currency')?.value;

    const addBill: AddBill = {
      billType: billType,
      serviceProviderName: serviceProviderName,
      dateOfPayment: dateOfPayment,
      cost: cost,
      currency: currency,
    };

    this.billService.addBill(addBill).subscribe({
      next: () => {
        this.resetForm();
        this.modal.close();
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
    this.addBillForm.reset();
  }
}
