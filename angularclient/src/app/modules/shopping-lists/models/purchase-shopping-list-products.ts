import { Money } from './../../../core/models/money';
export class PurchaseShoppingListProducts {
  shoppingListId!: number;
  priceByProductNames!: Record<string, Money>;

  constructor(
    shoppingListId: number,
    priceByProductNames: Record<string, Money>
  ) {
    this.shoppingListId = shoppingListId;
    this.priceByProductNames = priceByProductNames;
  }
}
