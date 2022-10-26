import { Money } from 'app/core/models/money';

export interface PurchaseShoppingListProduct {
  shoppingListId: string;
  productName: string;
  price: Money;
}
