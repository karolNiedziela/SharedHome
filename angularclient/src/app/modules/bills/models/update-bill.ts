import { Money } from 'app/core/models/money';

export interface UpdateBill {
  id: number;
  billType: number;
  serviceProviderName: string;
  dateOfPayment: Date;
  cost: Money | null;
}
