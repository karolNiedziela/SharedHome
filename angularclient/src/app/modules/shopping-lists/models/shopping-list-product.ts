import { NetContentType } from './../enums/net-content-type';
export interface ShoppingListProduct {
  name: string;
  quantity: number;
  price?: number;
  currency?: string;
  netContent?: string;
  netContentType?: NetContentType;
  isBought: boolean;
}
