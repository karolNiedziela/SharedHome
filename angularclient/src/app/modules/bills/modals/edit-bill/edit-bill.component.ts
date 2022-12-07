import { UpdateBill } from './../../models/update-bill';
import { BillEvent } from './../../models/bill-event';
import { Bill } from './../../models/bill';
import { Modalable } from './../../../../core/models/modalable';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormModalComponent } from 'app/shared/components/modals/form-modal/form-modal.component';
import { BillService } from '../../services/bill.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FormModalConfig } from 'app/shared/components/modals/form-modal/form-modal.config';
import { BillType } from '../../enums/bill-type';

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
    onClose: () => this.onClose(),
    onDismiss: () => this.onDismiss(),
  };

  editBillForm!: FormGroup;

  public billTypeType: typeof BillType = BillType;

  constructor(private billService: BillService) {}

  ngOnInit(): void {
    this.editBillForm = new FormGroup({
      billType: new FormControl(null, [Validators.required]),
      serviceProviderName: new FormControl('', [Validators.required]),
      money: new FormGroup({}),
    });
  }

  openModal(): void {
    this.editBillForm.patchValue({
      serviceProviderName: this.bill.serviceProvider,
      billType: this.bill.billType,
      money: this.bill?.cost,
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
    };

    this.billService.updateBill(updateBill).subscribe({
      next: () => {
        this.modal.close();
      },
    });
  }
  onClose(): void {}
  onDismiss(): void {}
}
