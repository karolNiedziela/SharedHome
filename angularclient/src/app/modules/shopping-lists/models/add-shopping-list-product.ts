import { NetContentType } from '../enums/net-content-type';

export interface AddShoppingListProduct {
  shoppingListId: number;
  name: string;
  quantity: number;
  netContent?: string;
  netContentType?: number;
}
