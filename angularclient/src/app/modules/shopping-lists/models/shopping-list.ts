import { ShoppingListStatus } from '../enums/shopping-list-status';
import { ShoppingListProduct } from './shopping-list-product';
export class ShoppingList {
  id!: string;
  name!: string;
  status!: ShoppingListStatus;
  createdByFullName!: string;
  products: ShoppingListProduct[] = [];

  constructor(shoppingList?: ShoppingList) {
    Object.assign(this, shoppingList ?? {});
  }
}
