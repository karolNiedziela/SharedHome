import { UpdateBill } from './../../models/update-bill';
import { BillEvent } from './../../models/bill-event';
import { Modalable } from './../../../../core/models/modalable';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { BillService } from '../../services/bill.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BillType } from '../../enums/bill-type';
import { formatDate } from '@angular/common';
import { FormModalComponent } from 'src/app/shared/components/modals/form-modal/form-modal.component';
import { FormModalConfig } from 'src/app/shared/components/modals/form-modal/form-modal.config';

@Component({
  selector: 'app-edit-bill',
  templateUrl: './edit-bill.component.html',
  styleUrls: ['./edit-bill.component.scss'],
})
export class EditBillComponent implements OnInit, Modalable {
  @Input() bill!: BillEvent;

  @ViewChild('modal') private modal!: FormModalComponent;
  public modalConfig: FormModalConfig = {
    modalTitle: 'Edit bill',
    onSave: () => this.onSave(),
  };

  editBillForm!: FormGroup;

  public billTypeType: typeof BillType = BillType;

  constructor(private billService: BillService) {}

  ngOnInit(): void {
    this.editBillForm = new FormGroup({
      billType: new FormControl(null, [Validators.required]),
      serviceProviderName: new FormControl('', [Validators.required]),
      money: new FormGroup({}),
      dateOfPayment: new FormControl(),
    });
  }

  openModal(): void {
    this.editBillForm.patchValue({
      serviceProviderName: this.bill?.serviceProvider,
      billType: this.bill?.billType,
      money: this.bill?.cost,
      dateOfPayment: formatDate(
        this.bill?.dateOfPayment,
        'yyyy-MM-dd',
        'en-US'
      ),
    });
    this.modal.open();
  }
  onSave(): void {
    const updateBill: UpdateBill = {
      billId: this.bill.id!.toString(),
      serviceProviderName:
        this.editBillForm.controls['serviceProviderName'].value,
      billType: this.editBillForm.controls['billType'].value,
      cost: this.editBillForm.controls['cost']?.value,
      dateOfPayment: this.editBillForm.controls['dateOfPayment']?.value,
    };

    this.modal.save(this.billService.updateBill(updateBill));
  }
}
