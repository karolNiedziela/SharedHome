import { Money } from 'app/core/models/money';
import { BillType } from '../enums/bill-type';

export interface Bill {
  id: number;
  isPaid: boolean;
  billType: BillType;
  serviceProvider: string;
  createdBy: string;
  cost?: Money;
  dateOfPayment: Date;
}
