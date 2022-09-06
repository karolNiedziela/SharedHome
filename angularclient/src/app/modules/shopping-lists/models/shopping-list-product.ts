import { Money } from './../../../core/models/money';
import { NetContentType } from './../enums/net-content-type';
export interface ShoppingListProduct {
  name: string;
  quantity: number;
  price?: Money;
  netContent?: string;
  netContentType?: NetContentType;
  isBought: boolean;
}
