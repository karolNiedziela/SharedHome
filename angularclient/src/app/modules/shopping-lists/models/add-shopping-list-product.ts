import { ShoppingListProduct } from './shopping-list-product';

export interface AddShoppingListProduct {
  shoppingListId: string;
  products: ShoppingListProduct[];
}
