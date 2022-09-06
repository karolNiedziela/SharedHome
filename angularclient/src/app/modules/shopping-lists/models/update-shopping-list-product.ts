export interface UpdateShoppingListProduct {
  shoppingListId: number;
  currentProductName: string;
  newProductName: string;
  quantity: number;
  netContent?: string;
  netContentType?: number;
  isBought: boolean;
}
