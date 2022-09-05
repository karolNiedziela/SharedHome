import { BillType } from '../enums/bill-type';

export interface Bill {
  id: number;
  isPaid: boolean;
  billType: BillType;
  serviceProvider: string;
  cost?: number;
  currency?: string;
  dateOfPayment: Date;
}
