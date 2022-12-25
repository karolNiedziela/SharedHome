import { BillType } from './../../enums/bill-type';
import { AddBill } from './../../models/add-bill';
import { BillService } from './../../services/bill.service';
import { Modalable } from './../../../../core/models/modalable';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormModalComponent } from 'app/shared/components/modals/form-modal/form-modal.component';
import { FormModalConfig } from 'app/shared/components/modals/form-modal/form-modal.config';

@Component({
  selector: 'app-add-bill',
  templateUrl: './add-bill.component.html',
  styleUrls: ['./add-bill.component.scss'],
})
export class AddBillComponent implements Modalable, OnInit {
  addBillForm!: FormGroup;

  public billTypeType: typeof BillType = BillType;

  @ViewChild('modal') private modal!: FormModalComponent;

  public modalConfig: FormModalConfig = {
    modalTitle: 'Add bill',
    onSave: () => this.onSave(),
  };

  constructor(private billService: BillService) {}

  ngOnInit(): void {
    this.addBillForm = new FormGroup({
      billType: new FormControl(null, [Validators.required]),
      serviceProviderName: new FormControl('', [Validators.required]),
      dateOfPayment: new FormControl('', [Validators.required]),
    });
  }

  openModal(): void {
    this.modal.open();
  }
  onSave(): void {
    const billType = this.addBillForm.get('billType')?.value;
    const serviceProviderName = this.addBillForm.get(
      'serviceProviderName'
    )?.value;
    const dateOfPayment = this.addBillForm.get('dateOfPayment')?.value;

    const addBill: AddBill = {
      billType: billType,
      serviceProviderName: serviceProviderName,
      dateOfPayment: dateOfPayment,
    };

    this.modal.save(this.billService.addBill(addBill));
  }
}
