import { NetContent } from './net-content';
import { Money } from './../../../core/models/money';
export interface ShoppingListProduct {
  name: string;
  quantity: number;
  price?: Money;
  netContent?: NetContent;
  isBought: boolean;
}
