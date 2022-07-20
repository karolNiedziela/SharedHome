import { ShoppingList } from './../models/shopping-list';
import { ShoppingListsService } from './../services/shopping-lists.service';
import { ActivatedRoute, Params } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import {
  faCheck,
  faEllipsisVertical,
  faXmark,
} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-shopping-list',
  templateUrl: './shopping-list.component.html',
  styleUrls: ['./shopping-list.component.scss'],
})
export class ShoppingListComponent implements OnInit {
  moreOptionsIcon = faEllipsisVertical;
  boughtIcon = faCheck;
  notBoughtIcon = faXmark;

  shoppingListId!: number;
  shoppingList!: ShoppingList;

  constructor(
    private activatedRoute: ActivatedRoute,
    private shoppingListService: ShoppingListsService
  ) {}

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(
      (params: Params) => (this.shoppingListId = params['shoppingListId'])
    );

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
