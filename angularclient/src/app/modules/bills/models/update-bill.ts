import { Money } from 'src/app/core/models/money';

export interface UpdateBill {
  billId: string;
  billType: number;
  serviceProviderName: string;
  cost: Money | null;
  dateOfPayment: Date;
}
