import { ShoppingList } from './models/shopping-list';
import { ShoppingListsService } from './services/shopping-lists.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import {
  faCheck,
  faCircleInfo,
  faEllipsisVertical,
  faXmark,
} from '@fortawesome/free-solid-svg-icons';
import { Component, OnInit } from '@angular/core';
import { yearAndMonthFormat } from 'app/shared/validators/dateformat.validator';
import { Paged } from 'app/core/models/paged';

@Component({
  selector: 'app-shopping-lists',
  templateUrl: './shopping-lists.component.html',
  styleUrls: ['./shopping-lists.component.scss'],
})
export class ShoppingListsComponent implements OnInit {
  previewIcon = faCircleInfo;
  moreOptionsIcon = faEllipsisVertical;
  boughtIcon = faCheck;
  notBoughtIcon = faXmark;
  year: number;
  month: number;
  shoppingListForm!: FormGroup;
  shoppingLists: ShoppingList[] = [];

  constructor(private shoppingListService: ShoppingListsService) {
    this.year = new Date().getFullYear();
    this.month = new Date().getMonth() + 1;
    const currentYearAndMonth = `${this.year} ${this.month}`;
    this.shoppingListForm = new FormGroup({
      yearAndMonth: new FormControl(currentYearAndMonth, [
        Validators.required,
        yearAndMonthFormat,
      ]),
    });
  }

  ngOnInit(): void {
    this.shoppingListService.refreshNeeded.subscribe(() => {
      this.getAllShoppingLists();
    });

    this.getAllShoppingLists();
  }

  private getAllShoppingLists() {
    this.shoppingListService
      .getAllByYearAndMonthAndIsDone(this.year, this.month, false)
      .subscribe({
        next: (response: Paged<ShoppingList>) => {
          this.shoppingLists = response?.items?.map((item) => {
            return new ShoppingList(
              item.id,
              item.name,
              item.isDone,
              item.createdByFirstName,
              item.createdByLastName,
              item.products!
            );
          });
        },
      });
  }

  onCurrentYearAndMonthChanged(): void {}
}
