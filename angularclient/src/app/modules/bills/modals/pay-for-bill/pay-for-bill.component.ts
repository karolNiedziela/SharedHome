import { BillService } from './../../services/bill.service';
import { Modalable } from './../../../../core/models/modalable';
import {
  ChangeDetectionStrategy,
  Component,
  Input,
  OnInit,
  ViewChild,
} from '@angular/core';
import { FormGroup } from '@angular/forms';
import { PayForBill } from '../../models/pay-for-bill';
import { FormModalComponent } from 'app/shared/components/modals/form-modal/form-modal.component';
import { FormModalConfig } from 'app/shared/components/modals/form-modal/form-modal.config';

@Component({
  selector: 'app-pay-for-bill',
  templateUrl: './pay-for-bill.component.html',
  styleUrls: ['./pay-for-bill.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PayForBillComponent implements Modalable, OnInit {
  @Input() billId!: string;

  @ViewChild('modal') private modal!: FormModalComponent;
  public modalConfig: FormModalConfig = {
    modalTitle: 'Pay for bill',
    onSave: () => this.onSave(),
  };

  payForBillForm!: FormGroup;

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
    const cost = this.payForBillForm.get('money')?.get('price')?.value;

    const currency = this.payForBillForm.get('money')?.get('currency')?.value;

    const payForBill: PayForBill = {
      billId: this.billId,
      cost: {
        price: cost,
        currency: currency,
      },
    };

    this.modal.save(this.billService.payForBill(payForBill));
  }
}
