import { PopupMenuConfig } from './../../../../shared/components/menus/popup-menu/popup-menu.config';
import { ShoppingListProduct } from './../../models/shopping-list-product';
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
    const currentYearAndMonth = `${this.year} ${this.month}`;

    this.shoppingListForm = new FormGroup({
      status: new FormControl(ShoppingListStatus['To do']),
    });

    this.getAllShoppingLists();

    this.singleRefreshSubscription =
      this.shoppingListService.allShoppingListRefreshNeeded.subscribe(() => {
        this.getAllShoppingLists();
      });
  }

  ngAfterViewInit(): void {
    this.statusSelectSubscription = this.statusSelect.selectedChanged.subscribe(
      (selectedValue: number | undefined) => {
        this.onStatusChange(selectedValue!);
      }
    );
  }

  ngOnDestroy(): void {
    this.singleRefreshSubscription.unsubscribe();
    this.statusSelectSubscription.unsubscribe();
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

  onCurrentYearAndMonthChange(): void {
    if (this.shoppingListForm.invalid) {
      return;
    }

    const yearAndMonth: string =
      this.shoppingListForm.get('yearAndMonth')?.value;

    const yearAndMonthSplitted = yearAndMonth.split(' ');
    this.year = +yearAndMonthSplitted[0];
    this.month = +yearAndMonthSplitted[1];

    this.getAllShoppingLists();
  }

  onStatusChange(selectedStatus: number): void {
    if (this.shoppingListForm.invalid) {
      return;
    }
    this.isDone = selectedStatus == 0;

    this.getAllShoppingLists();
  }

  openShoppingList(shoppingListId: number): void {
    this.router.navigate(['shoppinglists', shoppingListId]);
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
}
