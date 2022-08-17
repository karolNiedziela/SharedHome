import { ShoppingListProduct } from './shopping-list-product';
export class ShoppingList {
  id!: number;
  name!: string;
  isDone!: boolean;
  createdByFirstName!: string;
  createdByLastName!: string;
  products: ShoppingListProduct[] = [];

  constructor(shoppingList?: ShoppingList) {
    Object.assign(this, shoppingList ?? {});
  }

  countBoughtProducts() {
    return this.products?.filter((p) => p.isBought).length as number;
  }

  countTotalPrice() {
    return this.products?.reduce(
      (sum, product) => sum + product.price! * product.quantity ?? 0,
      0
    );
  }
}
