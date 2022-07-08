export interface ShoppingListProduct {
  name: string;
  quantity: number;
  price?: number;
  currency?: string;
  netContent?: string;
  netContentType?: string;
  isBought: boolean;
}
