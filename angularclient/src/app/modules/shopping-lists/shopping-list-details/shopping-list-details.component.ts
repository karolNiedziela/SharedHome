import { PurchaseShoppingListProductsModalComponent } from './../modals/purchase-shopping-list-products-modal/purchase-shopping-list-products-modal.component';
import { Subscription } from 'rxjs/internal/Subscription';
import { ShoppingListProductComponent } from './../shopping-list-product/shopping-list-product.component';
import { MarkAsDone } from './../models/mark-as-done';
import { ConfirmationModalConfig } from './../../../shared/components/modals/confirmation-modal/confirmation-modal.config';
import { ConfirmationModalComponent } from './../../../shared/components/modals/confirmation-modal/confirmation-modal.component';
import { ShoppingList } from '../models/shopping-list';
import { ShoppingListsService } from '../services/shopping-lists.service';
import { ActivatedRoute, Params, Router } from '@angular/router';
import {
  Component,
  OnInit,
  ViewChild,
  QueryList,
  ViewChildren,
  AfterViewInit,
  OnDestroy,
} from '@angular/core';
import {
  AdditionalPopupMenuItem,
  PopupMenuConfig,
} from 'app/shared/components/menus/popup-menu/popup-menu.config';
import { AddShoppingListProductComponent } from '../modals/add-shopping-list-product/add-shopping-list-product.component';
import { EditShoppingListModalComponent } from '../modals/edit-shopping-list-modal/edit-shopping-list-modal.component';
import { Observable, tap } from 'rxjs';
import { ApiResponse } from 'app/core/models/api-response';

@Component({
  selector: 'app-shopping-list-details',
  templateUrl: './shopping-list-details.component.html',
  styleUrls: ['./shopping-list-details.component.scss'],
})
export class ShoppingListComponent implements OnInit, AfterViewInit, OnDestroy {
  shoppingListId!: number;
  shoppingListProductNamesSelected: string[] = [];

  shoppingList$: Observable<ApiResponse<ShoppingList>> = new Observable(null!);
  shoppingList?: ShoppingList;

  productsSubscription!: Subscription;
  shoppingListSubscription!: Subscription;

  @ViewChildren('product') products!: QueryList<ShoppingListProductComponent>;

  @ViewChild('addShoppingListProductForm')
  addShoppingListProductForm!: AddShoppingListProductComponent;
  headerPopupMenuConfig: PopupMenuConfig = {};
  multipleItemsSelectedPopupMenuConfig: PopupMenuConfig = {};

  @ViewChild('markAsDoneModal')
  markAsDoneModal!: ConfirmationModalComponent;
  markAsDoneModalConfig: ConfirmationModalConfig = {
    modalTitle: 'Mark shopping list as done',
    confirmationText: 'Are you sure to mark this shopping list as done?',
    onSave: () => {
      this.markAsDone(true);
    },
  };

  @ViewChild('markAsUndoneModal')
  markAsUndoneModal!: ConfirmationModalComponent;
  markAsUndoneModalConfig: ConfirmationModalConfig = {
    modalTitle: 'Mark shopping list as undone',
    confirmationText: 'Are you sure to mark this shopping list as undone?',
    onSave: () => {
      this.markAsDone(false);
    },
  };

  @ViewChild('deleteShoppingList')
  private deleteShoppingListModal!: ConfirmationModalComponent;
  deleteShoppingListModalConfig: ConfirmationModalConfig = {
    modalTitle: 'Delete shopping list',
    onSave: () => {
      this.deleteShoppingList(this.shoppingListId);
    },
  };

  @ViewChild('editShoppingListModal')
  private editShoppingListModal!: EditShoppingListModalComponent;

  @ViewChild('deleteSelectedProductsModal')
  deleteSelectedProductsModal!: ConfirmationModalComponent;
  deleteSelectedProductsModalConfig: ConfirmationModalConfig = {
    modalTitle: 'Delete shopping list products',
    confirmationText: '',
    onSave: () => {
      this.deleteSelectedProducts();
    },
  };

  @ViewChild('purchaseShoppingListProductsModal')
  purchaseProductsModal!: PurchaseShoppingListProductsModalComponent;

  constructor(
    private activatedRoute: ActivatedRoute,
    private shoppingListService: ShoppingListsService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(
      (params: Params) => (this.shoppingListId = params['shoppingListId'])
    );

    this.getShoppingList();

    this.shoppingListSubscription =
      this.shoppingListService.singleShoppingListRefreshNeeded.subscribe(() => {
        this.getShoppingList();
      });
  }

  ngAfterViewInit(): void {
    this.productsSubscription = this.products.changes.subscribe(
      (c: QueryList<ShoppingListProductComponent>) => {
        c.toArray().forEach((product) => {
          const productHtmlElement =
            product.element.nativeElement.querySelector(
              '.product'
            ) as HTMLElement;

          if (!this.shoppingList?.isDone) {
            productHtmlElement.addEventListener('click', (e) => {
              const targetHtmlElement = e.target as HTMLElement;
              if (!['svg', 'path'].includes(targetHtmlElement.nodeName)) {
                productHtmlElement.classList.toggle('selected');
                this.onProductSelect(product);
              }
            });
          }
        });
      }
    );

    this.headerPopupMenuConfig = {
      isEditVisible: !this.shoppingList?.isDone,
      isDeleteVisible: !this.shoppingList?.isDone,
      onDelete: () => {
        this.deleteShoppingListModal.open();
      },
      onEdit: () => {
        this.editShoppingListModal.openModal();
      },
      additionalPopupMenuItems: this.getAdditionalPopupMenuItems(),
    };

    this.multipleItemsSelectedPopupMenuConfig = {
      isEditVisible: false,
      isDeleteVisible: false,
      additionalPopupMenuItems: [
        {
          text: 'Purchase selected',
          onClick: () => {
            this.purchaseProductsModal.openModal();
          },
        },
        {
          text: 'Deselect all',
          onClick: () => {
            this.deselectProducts();
          },
        },
        {
          text: 'Delete selected',
          onClick: () => {
            this.openDeleteSelectedProductsModal();
          },
        },
      ],
    };
  }

  ngOnDestroy(): void {
    this.productsSubscription.unsubscribe();
    this.shoppingListSubscription.unsubscribe();
    ('');
  }

  getShoppingList() {
    this.shoppingList$ = this.shoppingListService.get(this.shoppingListId).pipe(
      tap((response: ApiResponse<ShoppingList>) => {
        this.shoppingList = response.data;
      })
    );
  }

  markAsDone(isDone: boolean): void {
    const markAsDone: MarkAsDone = {
      shoppingListId: this.shoppingListId,
      isDone: isDone,
    };
    this.shoppingListService.markAsDone(markAsDone, true).subscribe();
  }

  openDeleteSelectedProductsModal(): void {
    this.deleteSelectedProductsModalConfig.confirmationText = `Are you sure you want to delete ${this.shoppingListProductNamesSelected.join(
      ', '
    )}?`;

    this.deleteSelectedProductsModal.open();
  }

  private getAdditionalPopupMenuItems(): AdditionalPopupMenuItem[] {
    const additionalPopupMenuItems: AdditionalPopupMenuItem[] = [];
    if (this.shoppingList?.isDone) {
      additionalPopupMenuItems.push({
        text: 'Mark as undone',
        onClick: () => {
          this.markAsUndoneModal.open();
        },
      });
    } else {
      additionalPopupMenuItems.push({
        text: 'Add products',
        onClick: () => {
          this.addShoppingListProductForm.openModal();
        },
      });

      additionalPopupMenuItems.push({
        text: 'Mark as done',
        onClick: () => {
          this.markAsDoneModal.open();
        },
      });
    }

    return additionalPopupMenuItems;
  }

  private deleteShoppingList(shoppingListId: number): void {
    this.shoppingListService.delete(shoppingListId).subscribe({
      next: () => {
        this.router.navigate(['shoppinglists']);
      },
    });
  }

  private onProductSelect(product: ShoppingListProductComponent): void {
    const index = this.shoppingListProductNamesSelected.indexOf(
      product.shoppingListProduct!.name
    );
    if (index > -1) {
      this.shoppingListProductNamesSelected.splice(index, 1);
      return;
    }

    this.shoppingListProductNamesSelected.push(
      product.shoppingListProduct!.name
    );

    if (product.shoppingListProduct!.isBought) {
      const purchaseSelectedIndex =
        this.multipleItemsSelectedPopupMenuConfig.additionalPopupMenuItems?.findIndex(
          (x) => x.text == 'Purchase selected'
        )!;
      if (purchaseSelectedIndex > -1) {
        this.multipleItemsSelectedPopupMenuConfig.additionalPopupMenuItems!.splice(
          purchaseSelectedIndex,
          1
        );
        return;
      }
    }
  }

  private deselectProducts(): void {
    this.shoppingListProductNamesSelected = [];
    document
      .querySelectorAll('.product.selected')
      .forEach((productSelected) => {
        productSelected.classList.remove('selected');
      });
  }

  private deleteSelectedProducts(): void {
    this.shoppingListService
      .deleteShoppingListProducts(
        this.shoppingListId,
        this.shoppingListProductNamesSelected
      )
      .subscribe({
        next: () => {
          this.shoppingListProductNamesSelected = [];
        },
        error: () => {},
      });
  }
}
