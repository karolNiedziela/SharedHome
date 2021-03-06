import { TabConfig } from './../../../../shared/components/tabs/default-tab/tab.config';
import { ConfirmationModalComponent } from '../../../../shared/components/modals/confirmation-modal/confirmation-modal.component';
import { Router } from '@angular/router';

import { FormGroup, FormControl, Validators } from '@angular/forms';
import { faList } from '@fortawesome/free-solid-svg-icons';
import { Component, OnInit, ViewChild } from '@angular/core';
import { yearAndMonthFormat } from 'app/shared/validators/dateformat.validator';
import { Paged } from 'app/core/models/paged';
import { ConfirmationModalConfig } from 'app/shared/components/modals/confirmation-modal/confirmation-modal.config';
import { ShoppingList } from '../../models/shopping-list';
import { ShoppingListsService } from '../../services/shopping-lists.service';
import {
  debounce,
  debounceTime,
  switchMap,
  distinctUntilChanged,
  map,
} from 'rxjs';

@Component({
  selector: 'app-shopping-lists-list',
  templateUrl: './shopping-lists-list.component.html',
  styleUrls: ['./shopping-lists-list.component.scss'],
})
export class ShoppingListsComponent implements OnInit {
  detailsIcon = faList;

  @ViewChild('deleteShoppingList')
  private deleteShoppingListModal!: ConfirmationModalComponent;
  deleteShoppingListModalConfig: ConfirmationModalConfig = {
    modalTitle: 'Delete shopping list',
    onSave: () => {},
  };
  tabConfig: TabConfig = {
    tabItems: [
      {
        text: 'Undone',
        onClick: () => {
          this.getAllShoppingLists(this.year, this.month, false);
        },
        isActive: true,
      },
      {
        text: 'Done',
        onClick: () => {
          this.getAllShoppingLists(this.year, this.month, true);
        },
      },
    ],
  };

  year: number;
  month: number;
  isDone: boolean = false;
  shoppingListForm!: FormGroup;
  shoppingLists: ShoppingList[] = [];

  constructor(
    private shoppingListService: ShoppingListsService,
    private router: Router
  ) {
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
    this.shoppingListService.allShoppingListRefreshNeeded.subscribe(() => {
      this.getAllShoppingLists(this.year, this.month, this.isDone);
    });

    this.getAllShoppingLists(this.year, this.month, this.isDone);
  }

  private getAllShoppingLists(
    year: number,
    month: number,
    isDone: boolean = false
  ) {
    this.shoppingListService
      .getAllByYearAndMonthAndIsDone(year, month, isDone)
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

  onCurrentYearAndMonthChanged(): void {
    if (this.shoppingListForm.invalid) {
      return;
    }
    const yearAndMonth: string =
      this.shoppingListForm.get('yearAndMonth')?.value;

    const yearAndMonthSplitted = yearAndMonth.split(' ');
    this.year = +yearAndMonthSplitted[0];
    this.month = +yearAndMonthSplitted[1];

    this.getAllShoppingLists(this.year, this.month, this.isDone);
  }

  openShoppingList(shoppingListId: number): void {
    this.router.navigate(['shoppinglists', shoppingListId]);
  }
}
