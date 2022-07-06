import { ShoppingListProduct } from './shopping-list-product';
export class ShoppingList {
  id!: number;
  name!: string;
  isDone!: boolean;
  createdByFirstName!: string;
  createdByLastName!: string;
  products?: ShoppingListProduct[];

  constructor(
    id: number,
    name: string,
    isDone: boolean,
    createdByFirstName: string,
    createdByLastName: string,
    products: ShoppingListProduct[]
  ) {
    this.id = id;
    this.name = name;
    (this.isDone = isDone), (this.createdByFirstName = createdByFirstName);
    this.createdByLastName = createdByLastName;
    this.products = products;
  }

  countBoughtProducts() {
    return this.products?.filter((p) => p.isBought).length as number;
  }

  countTotalPrice() {
    return this.products?.reduce(
      (sum, product) => sum + product.price! ?? 0,
      0
    );
  }
}
