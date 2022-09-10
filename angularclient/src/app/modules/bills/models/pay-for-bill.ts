import { Money } from './../../../core/models/money';
export interface PayForBill {
  billId: number;
  cost: Money;
}
