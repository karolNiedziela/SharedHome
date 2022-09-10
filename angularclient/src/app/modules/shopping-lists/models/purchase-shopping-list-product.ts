import { Money } from 'app/core/models/money';

export interface PurchaseShoppingListProduct {
  shoppingListId: number;
  productName: string;
  price: Money;
}
