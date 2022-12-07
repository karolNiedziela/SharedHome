import { Money } from 'app/core/models/money';

export interface UpdateBill {
  billId: string;
  billType: number;
  serviceProviderName: string;
  cost: Money | null;
}
