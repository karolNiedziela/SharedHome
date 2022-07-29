import { NetContentType } from '../enums/net-content-type';

export interface AddShoppingListProduct {
  shoppingListId: number;
  productName: string;
  quantity: number;
  netContent?: string;
  netContentType?: number;
}
