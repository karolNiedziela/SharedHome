import { ShoppingListProduct } from './shopping-list-product';
export interface ShoppingList {
  id: number;
  name: string;
  isDone: boolean;
  createdByFirstName: string;
  createdByLastName: string;
  products?: ShoppingListProduct[];
}
