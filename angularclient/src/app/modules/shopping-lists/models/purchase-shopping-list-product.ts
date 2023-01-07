import { Money } from 'src/app/core/models/money';

export interface PurchaseShoppingListProduct {
  shoppingListId: string;
  productName: string;
  price: Money;
}
