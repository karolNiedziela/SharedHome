import { first } from 'rxjs';
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

  shoppingListForm!: FormGroup;
  shoppingLists: ShoppingList[] = [];

  constructor(private shoppingListService: ShoppingListsService) {
    const currentYearAndMonth = `${new Date().getFullYear()} ${new Date().getMonth()}`;
    this.shoppingListForm = new FormGroup({
      yearAndMonth: new FormControl(currentYearAndMonth, [
        Validators.required,
        yearAndMonthFormat,
      ]),
    });
  }

  ngOnInit(): void {
    console.log('init');
    this.shoppingListService
      .getAllByYearAndMonthAndIsDone(2022, 5, false)
      .subscribe((response) => {
        this.shoppingLists = response.items.map((item) => {
          return new ShoppingList(
            item.id,
            item.name,
            item.isDone,
            item.createdByFirstName,
            item.createdByLastName,
            item.products!
          );
        });
        console.log(response);
        console.log(this.shoppingLists);
      });
  }

  onCurrentYearAndMonthChanged(): void {
    console.log('hello');
  }
}
