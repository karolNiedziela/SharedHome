import { Money } from 'src/app/core/models/money';
import { BillType } from '../enums/bill-type';

export interface Bill {
  id: string;
  isPaid: boolean;
  billType: BillType;
  serviceProvider: string;
  createdByFullName: string;
  cost?: Money;
  dateOfPayment: Date;
}
