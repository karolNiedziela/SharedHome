import { ShoppingList } from '../models/shopping-list';
import { ShoppingListsService } from '../services/shopping-lists.service';
import { ActivatedRoute, Params } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { PopupMenuConfig } from 'app/shared/components/menus/popup-menu/popup-menu.config';

@Component({
  selector: 'app-shopping-list-details',
  templateUrl: './shopping-list-details.component.html',
  styleUrls: ['./shopping-list-details.component.scss'],
})
export class ShoppingListComponent implements OnInit {
  shoppingListId!: number;
  shoppingList?: ShoppingList;
  headearPopupMenuConfig: PopupMenuConfig = {
    additionalPopupMenuItems: [{ text: 'Add product', onClick: () => {} }],
  };

  constructor(
    private activatedRoute: ActivatedRoute,
    private shoppingListService: ShoppingListsService
  ) {}

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(
      (params: Params) => (this.shoppingListId = params['shoppingListId'])
    );

    this.shoppingListService.singleShoppingListRefreshNeeded.subscribe(() => {
      this.getShoppingList();
    });

    this.getShoppingList();
  }

  getShoppingList() {
    this.shoppingListService.get(this.shoppingListId).subscribe((response) => {
      this.shoppingList = new ShoppingList(
        response.data.id,
        response.data.name,
        response.data.isDone,
        response.data.createdByFirstName,
        response.data.createdByLastName,
        response.data.products!
      );
    });
  }
}
