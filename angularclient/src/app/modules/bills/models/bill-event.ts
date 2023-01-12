import { CalendarEvent } from 'angular-calendar';
import { Money } from 'src/app/core/models/money';
import { BillType } from '../enums/bill-type';

export interface BillEvent extends CalendarEvent {
  isPaid: boolean;
  billType: BillType;
  serviceProvider: string;
  cost: Money | null;
  dateOfPayment: Date;
  createdByFullName: string;
}
