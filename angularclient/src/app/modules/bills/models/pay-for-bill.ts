import { Money } from './../../../core/models/money';
export interface PayForBill {
  billId: string;
  cost: Money;
}
