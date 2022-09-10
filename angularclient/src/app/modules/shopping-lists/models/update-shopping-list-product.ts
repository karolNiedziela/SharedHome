import { NetContent } from './net-content';
export interface UpdateShoppingListProduct {
  shoppingListId: number;
  currentProductName: string;
  newProductName: string;
  quantity: number;
  netContent?: NetContent;
  isBought: boolean;
}
