import { Paginatable } from './../../../../core/models/paginatable';
import { EnumSelectComponent } from './../../../../shared/components/selects/enum-select/enum-select.component';
import { Subscription } from 'rxjs/internal/Subscription';
import { Observable, tap } from 'rxjs';
import { ShoppingListStatus } from './../../enums/shopping-list-status';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { faList } from '@fortawesome/free-solid-svg-icons';
import {
  Component,
  OnInit,
  ViewChild,
  AfterViewInit,
  OnDestroy,
} from '@angular/core';
import { yearAndMonthFormat } from 'app/shared/validators/dateformat.validator';
import { Paged } from 'app/core/models/paged';
import { ShoppingList } from '../../models/shopping-list';
import { ShoppingListsService } from '../../services/shopping-lists.service';
@Component({
  selector: 'app-shopping-lists-list',
  templateUrl: './shopping-lists-list.component.html',
  styleUrls: ['./shopping-lists-list.component.scss'],
})
export class ShoppingListsComponent
  implements OnInit, AfterViewInit, OnDestroy, Paginatable
{
  detailsIcon = faList;
  public shoppingListStatus: typeof ShoppingListStatus = ShoppingListStatus;

  @ViewChild('statusSelect')
  private statusSelect!: EnumSelectComponent;
  year!: number;
  month!: number;
  isDone: boolean = false;
  shoppingListForm!: FormGroup;
  paged$!: Observable<Paged<ShoppingList>>;
  singleRefreshSubscription!: Subscription;
  statusSelectSubscription!: Subscription;

  loading$!: Observable<boolean>;

  public currentPage: number = 1;

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
