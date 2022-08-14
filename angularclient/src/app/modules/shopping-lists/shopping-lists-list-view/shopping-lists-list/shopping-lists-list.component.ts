import { SingleSelectComponent } from './../../../../shared/components/selects/single-select/single-select.component';
import { ShoppingListStatus } from './../../enums/shopping-list-status';
import { ConfirmationModalComponent } from '../../../../shared/components/modals/confirmation-modal/confirmation-modal.component';
import { Router } from '@angular/router';

import { FormGroup, FormControl, Validators } from '@angular/forms';
import { faList } from '@fortawesome/free-solid-svg-icons';
import {
  Component,
  OnInit,
  ViewChild,
  ViewEncapsulation,
  AfterViewInit,
} from '@angular/core';
import { yearAndMonthFormat } from 'app/shared/validators/dateformat.validator';
import { Paged } from 'app/core/models/paged';
import { ConfirmationModalConfig } from 'app/shared/components/modals/confirmation-modal/confirmation-modal.config';
import { ShoppingList } from '../../models/shopping-list';
import { ShoppingListsService } from '../../services/shopping-lists.service';
@Component({
  selector: 'app-shopping-lists-list',
  templateUrl: './shopping-lists-list.component.html',
  styleUrls: ['./shopping-lists-list.component.scss'],
})
export class ShoppingListsComponent implements OnInit, AfterViewInit {
  detailsIcon = faList;

  public shoppingListStatus: typeof ShoppingListStatus = ShoppingListStatus;
  @ViewChild('statusSelect')
  private statusSelect!: SingleSelectComponent;

  @ViewChild('deleteShoppingList')
  private deleteShoppingListModal!: ConfirmationModalComponent;
  deleteShoppingListModalConfig: ConfirmationModalConfig = {
    modalTitle: 'Delete shopping list',
    onSave: () => {},
  };

  year!: number;
  month!: number;
  isDone: boolean = false;
  shoppingListForm!: FormGroup;
  shoppingLists: ShoppingList[] = [];

  constructor(
    private shoppingListService: ShoppingListsService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.year = new Date().getFullYear();
    this.month = new Date().getMonth() + 1;
    const currentYearAndMonth = `${this.year} ${this.month}`;
    this.shoppingListForm = new FormGroup({
      yearAndMonth: new FormControl(currentYearAndMonth, [
        Validators.required,
        yearAndMonthFormat,
      ]),
      status: new FormControl(ShoppingListStatus['To done']),
    });

    this.shoppingListService.allShoppingListRefreshNeeded.subscribe(() => {
      this.getAllShoppingLists(this.year, this.month, this.isDone);
    });

    this.getAllShoppingLists(this.year, this.month, this.isDone);
  }

  ngAfterViewInit(): void {
    this.statusSelect.selectedChanged.subscribe(
      (selectedValue: number | undefined) => {
        this.onStatusChange(selectedValue!);
      }
    );
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

  onCurrentYearAndMonthChange(): void {
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

  onStatusChange(selectedStatus: number): void {
    if (this.shoppingListForm.invalid) {
      return;
    }
    this.isDone = selectedStatus == 0;

    this.getAllShoppingLists(this.year, this.month, this.isDone);
  }

  openShoppingList(shoppingListId: number): void {
    this.router.navigate(['shoppinglists', shoppingListId]);
  }
}
