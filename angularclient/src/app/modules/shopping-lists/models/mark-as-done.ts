import { ShoppingListStatus } from './../enums/shopping-list-status';
export interface MarkAsDone {
  shoppingListId: string;
  status: ShoppingListStatus;
}
