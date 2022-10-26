import { Money } from './../../../core/models/money';
export class PurchaseShoppingListProducts {
  shoppingListId!: string;
  priceByProductNames!: Record<string, Money>;

  constructor(
    shoppingListId: string,
    priceByProductNames: Record<string, Money>
  ) {
    this.shoppingListId = shoppingListId;
    this.priceByProductNames = priceByProductNames;
  }
}
