import { NetContentType } from '../enums/net-content-type';
import { ShoppingListProduct } from './shopping-list-product';

export interface AddShoppingListProduct {
  shoppingListId: number;
  products: ShoppingListProduct[];
}
