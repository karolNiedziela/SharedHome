import { ShoppingListProduct } from './shopping-list-product';
export class ShoppingList {
  id!: string;
  name!: string;
  isDone!: boolean;
  createdByFullName!: string;
  products: ShoppingListProduct[] = [];

  constructor(shoppingList?: ShoppingList) {
    Object.assign(this, shoppingList ?? {});
  }
}
