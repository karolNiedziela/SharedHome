import { CalendarEvent } from 'angular-calendar';
import { BillType } from '../enums/bill-type';

export interface BillEvent extends CalendarEvent {
  isPaid: boolean;
  billType: BillType;
  serviceProvider: string;
  cost?: number;
  currency?: string;
  dateOfPayment: Date;
}
