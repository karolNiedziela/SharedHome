import { PopupMenuConfig } from './../../../../shared/components/menus/popup-menu/popup-menu.config';
import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable, Subscription, tap } from 'rxjs';
import { Paged } from 'src/app/core/models/paged';
import { Paginatable } from 'src/app/core/models/paginatable';
import { TableColumn } from 'src/app/shared/components/tables/column-setting';
import { ShoppingListStatus } from '../../enums/shopping-list-status';
import { ShoppingList } from '../../models/shopping-list';
import { ShoppingListsService } from '../../services/shopping-lists.service';
import { SingleEnumSelectComponent } from 'src/app/shared/components/selects/single-enum-select/single-enum-select.component';
import moment from 'moment';

@Component({
  selector: 'app-shopping-lists-list',
  templateUrl: './shopping-lists-list.component.html',
  styleUrls: ['./shopping-lists-list.component.scss'],
})
export class ShoppingListsListComponent
  implements OnInit, AfterViewInit, OnDestroy, Paginatable
{
  public shoppingListStatus: typeof ShoppingListStatus = ShoppingListStatus;

  @ViewChild('statusSelect')
  private statusSelect!: SingleEnumSelectComponent;
  year!: number;
  month!: number;
  isDone: boolean = false;
  shoppingListForm!: FormGroup;
  paged$!: Observable<Paged<ShoppingList>>;
  singleRefreshSubscription!: Subscription;
  yearAndMonthSubscription!: Subscription;
  statusSelectSubscription!: Subscription;

  loading$!: Observable<boolean>;

  public currentPage: number = 1;

  shoppings: ShoppingList[] = [];

  shoppingsTableColumns: TableColumn[] = [];

  popupMenuConfigs: PopupMenuConfig[] = [
    {
      isEditVisible: true,
      onEdit: () => alert('Edit 1'),
      isDeleteVisible: true,
      onDelete: () => alert('Delete 1'),
    },
    {
      isEditVisible: true,
      onEdit: () => alert('Edit 2'),
      isDeleteVisible: true,
      onDelete: () => alert('Delete 2'),
    },
  ];

  constructor(
    private shoppingListService: ShoppingListsService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.year = new Date().getFullYear();
    this.month = new Date().getMonth() + 1;
    this.shoppingListForm = new FormGroup({
      yearAndMonth: new FormControl(moment()),
      status: new FormControl(ShoppingListStatus['To do']),
    });
    this.getAllShoppingLists();

    this.singleRefreshSubscription =
      this.shoppingListService.allShoppingListRefreshNeeded.subscribe(() => {
        this.getAllShoppingLists();
      });
  }

  ngAfterViewInit(): void {
    this.yearAndMonthSubscription = this.shoppingListForm.controls[
      'yearAndMonth'
    ].valueChanges.subscribe((selectedDate: any) => {
      const date: Date = selectedDate.toDate();
      this.year = date.getFullYear();
      this.month = date.getMonth() + 1;
      this.getAllShoppingLists();
    });

    this.statusSelectSubscription = this.shoppingListForm.controls[
      'status'
    ].valueChanges.subscribe((selectedStatus: number) => {
      this.isDone = selectedStatus == 0;
      this.getAllShoppingLists();
    });
  }

  ngOnDestroy(): void {
    this.singleRefreshSubscription.unsubscribe();
    this.yearAndMonthSubscription.unsubscribe();
    this.statusSelectSubscription.unsubscribe();
  }

  public onPrevious(): void {
    this.currentPage -= 1;

    this.getAllShoppingLists();
  }

  public onNext(): void {
    this.currentPage += 1;

    this.getAllShoppingLists();
  }

  public goTo(page: number): void {
    this.currentPage = page;

    this.getAllShoppingLists();
  }

  public openShoppingList(shoppingListId: number): void {
    this.router.navigate(['shoppinglists', shoppingListId]);
  }

  private getAllShoppingLists() {
    this.paged$ = this.shoppingListService
      .getAllByYearAndMonthAndIsDone(
        this.year,
        this.month,
        this.isDone,
        this.currentPage
      )
      .pipe(
        tap((paged: Paged<ShoppingList>) => {
          this.currentPage = paged.currentPage;
        })
      );
  }
}
