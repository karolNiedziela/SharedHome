import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  QueryList,
  ViewChild,
  ViewChildren,
} from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Observable, Subscription, tap } from 'rxjs';
import { ApiResponse } from 'src/app/core/models/api-response';
import {
  AdditionalPopupMenuItem,
  PopupMenuConfig,
} from 'src/app/shared/components/menus/popup-menu/popup-menu.config';
import { ConfirmationModalComponent } from 'src/app/shared/components/modals/confirmation-modal/confirmation-modal.component';
import { ConfirmationModalConfig } from 'src/app/shared/components/modals/confirmation-modal/confirmation-modal.config';
import { AddShoppingListProductComponent } from '../modals/add-shopping-list-product/add-shopping-list-product.component';
import { EditShoppingListModalComponent } from '../modals/edit-shopping-list-modal/edit-shopping-list-modal.component';
import { PurchaseShoppingListProductsModalComponent } from '../modals/purchase-shopping-list-products-modal/purchase-shopping-list-products-modal.component';
import { MarkAsDone } from '../models/mark-as-done';
import { ShoppingList } from '../models/shopping-list';
import { ShoppingListProduct } from '../models/shopping-list-product';
import { ShoppingListsService } from '../services/shopping-lists.service';
import { ShoppingListProductComponent } from '../shopping-list-product/shopping-list-product.component';

@Component({
  selector: 'app-shopping-list-details',
  templateUrl: './shopping-list-details.component.html',
  styleUrls: [
    './shopping-list-details.component.scss',
    '../shopping-lists-list-view/single-shopping-list/single-shopping-list.component.scss',
  ],
})
export class ShoppingListDetailsComponent
  implements OnInit, AfterViewInit, OnDestroy
{
  shoppingListId!: string;
  shoppingListProductsSelected: ShoppingListProduct[] = [];

  shoppingList$: Observable<ApiResponse<ShoppingList>> = new Observable(null!);
  shoppingList?: ShoppingList;

  productsSubscription!: Subscription;
  shoppingListSubscription!: Subscription;

  @ViewChildren('product') products!: QueryList<ShoppingListProduct>;

  @ViewChild('addShoppingListProductForm')
  addShoppingListProductForm!: AddShoppingListProductComponent;
  headerPopupMenuConfig: PopupMenuConfig = {};
  multipleItemsSelectedPopupMenuConfig: PopupMenuConfig = {};

  @ViewChild('markAsDoneModal')
  markAsDoneModal!: ConfirmationModalComponent;
  markAsDoneModalConfig: ConfirmationModalConfig = {
    modalTitle: 'shopping_lists.mark_shopping_list_as_done',
    confirmationText: 'shopping_lists.mark_shopping_list_as_done_text',
    onConfirm: () => {
      this.markAsDone(true);
    },
  };

  @ViewChild('markAsUndoneModal')
  markAsUndoneModal!: ConfirmationModalComponent;
  markAsUndoneModalConfig: ConfirmationModalConfig = {
    modalTitle: 'shopping_lists.mark_shopping_list_as_undone',
    confirmationText: 'shopping_lists.mark_shopping_list_as_undone_text',
    onConfirm: () => {
      this.markAsDone(false);
    },
  };

  @ViewChild('deleteShoppingList')
  private deleteShoppingListModal!: ConfirmationModalComponent;
  deleteShoppingListModalConfig: ConfirmationModalConfig = {
    modalTitle: 'shopping_lists.delete_shopping_list',
    confirmationText: 'shopping_lists.delete_shopping_list_text',
    onConfirm: () => {
      this.deleteShoppingList(this.shoppingListId);
    },
  };

  @ViewChild('editShoppingListModal')
  private editShoppingListModal!: EditShoppingListModalComponent;

  @ViewChild('deleteSelectedProductsModal')
  deleteSelectedProductsModal!: ConfirmationModalComponent;
  deleteSelectedProductsModalConfig: ConfirmationModalConfig = {
    modalTitle: 'shopping_lists.delete_shopping_list_products',
    confirmationText: '',
    onConfirm: () => {
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
        this.deselectProducts();
        this.generateMultipleSelectedItemsPopupMenuConfig();
      });
  }

  ngAfterViewInit(): void {
    this.productsSubscription = this.products.changes.subscribe(
      (productComponents: QueryList<ShoppingListProductComponent>) => {
        this.addOnProductSelectedListener(productComponents);
      }
    );

    this.generateHeaderPopupMenuConfig();
    this.generateMultipleSelectedItemsPopupMenuConfig();
  }

  ngOnDestroy(): void {
    this.productsSubscription.unsubscribe();
    this.shoppingListSubscription.unsubscribe();
  }

  private getShoppingList() {
    this.shoppingList$ = this.shoppingListService.get(this.shoppingListId).pipe(
      tap((response: ApiResponse<ShoppingList>) => {
        this.shoppingList = response.data;
      })
    );
  }

  private markAsDone(isDone: boolean): void {
    const markAsDone: MarkAsDone = {
      shoppingListId: this.shoppingListId,
      isDone: isDone,
    };
    this.shoppingListService.markAsDone(markAsDone, true).subscribe();
  }

  private openDeleteSelectedProductsModal(): void {
    this.deleteSelectedProductsModal.open();
  }

  private addOnProductSelectedListener(
    productComponents: QueryList<ShoppingListProductComponent>
  ): void {
    productComponents.toArray().forEach((product) => {
      const productHtmlElement = product.element.nativeElement.querySelector(
        '.product'
      ) as HTMLElement;

      if (!this.shoppingList?.isDone) {
        productHtmlElement.addEventListener('click', (e) => {
          const targetHtmlElement = e.target as HTMLElement;

          if (!['svg', 'path'].includes(targetHtmlElement.nodeName)) {
            productHtmlElement
              .querySelector('.product-name')!
              .classList.toggle('selected');

            this.onProductSelected(product);
          }
        });
      }
    });
  }

  private generateHeaderPopupMenuConfig(): void {
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
  }

  private generateMultipleSelectedItemsPopupMenuConfig(): void {
    this.multipleItemsSelectedPopupMenuConfig = {
      isEditVisible: false,
      isDeleteVisible: false,
      additionalPopupMenuItems: [
        {
          text: 'shopping_lists.purchase_selected',
          onClick: () => {
            this.purchaseProductsModal.openModal();
          },
        },
        {
          text: 'shopping_lists.deselect_all',
          onClick: () => {
            this.deselectProducts();
          },
        },
        {
          text: 'shopping_lists.delete_selected',
          onClick: () => {
            this.openDeleteSelectedProductsModal();
          },
        },
      ],
    };
  }

  private getAdditionalPopupMenuItems(): AdditionalPopupMenuItem[] {
    const additionalPopupMenuItems: AdditionalPopupMenuItem[] = [];
    if (this.shoppingList?.isDone) {
      additionalPopupMenuItems.push({
        text: 'shopping_lists.mark_as_done',
        onClick: () => {
          this.markAsUndoneModal.open();
        },
      });
    } else {
      additionalPopupMenuItems.push({
        text: 'shopping_lists.add_products',
        onClick: () => {
          this.addShoppingListProductForm.openModal();
        },
      });

      additionalPopupMenuItems.push({
        text: 'shopping_lists.mark_as_undone',
        onClick: () => {
          this.markAsDoneModal.open();
        },
      });
    }

    return additionalPopupMenuItems;
  }

  private deleteShoppingList(shoppingListId: string): void {
    this.shoppingListService.delete(shoppingListId).subscribe({
      next: () => {
        this.router.navigate(['shoppinglists']);
      },
    });
  }

  private onProductSelected(product: ShoppingListProductComponent): void {
    const selectedProductIndex = this.shoppingListProductsSelected.indexOf(
      product.shoppingListProduct
    );
    if (selectedProductIndex > -1) {
      this.removeSelectedProduct(product, selectedProductIndex);
      return;
    }

    this.addSelectedProduct(product);
  }

  private addSelectedProduct(product: ShoppingListProductComponent): void {
    this.shoppingListProductsSelected.push(product.shoppingListProduct);

    this.showOrHidePurchaseSelectedOption(product);
  }

  private removeSelectedProduct(
    product: ShoppingListProductComponent,
    indexToRemove: number
  ): void {
    this.shoppingListProductsSelected.splice(indexToRemove, 1);

    this.showOrHidePurchaseSelectedOption(product);
  }

  private showOrHidePurchaseSelectedOption(
    product: ShoppingListProductComponent
  ): void {
    const allProductsUnbought = this.shoppingList?.products
      .filter((product) => this.shoppingListProductsSelected.includes(product))
      .every((product) => !product.isBought);

    if (allProductsUnbought) {
      this.showPurchaseSelectedOptions();
      return;
    }

    this.hidePurchaseSelectedOption();
  }

  private showPurchaseSelectedOptions(): void {
    const purchaseSelectedIndex = this.getPurchaseSelectedOptionIndex();
    if (purchaseSelectedIndex > -1) {
      return;
    }

    this.multipleItemsSelectedPopupMenuConfig.additionalPopupMenuItems?.unshift(
      {
        text: 'shopping_lists.purchase_selected',
        onClick: () => {
          this.purchaseProductsModal.openModal();
        },
      }
    );
  }

  private hidePurchaseSelectedOption(): void {
    const purchaseSelectedOptionIndex = this.getPurchaseSelectedOptionIndex();
    if (purchaseSelectedOptionIndex > -1) {
      this.multipleItemsSelectedPopupMenuConfig.additionalPopupMenuItems!.splice(
        purchaseSelectedOptionIndex,
        1
      );

      return;
    }
  }

  private getPurchaseSelectedOptionIndex(): number {
    return this.multipleItemsSelectedPopupMenuConfig.additionalPopupMenuItems?.findIndex(
      (x) => x.text == 'shopping_lists.purchase_selected'
    )!;
  }

  private deselectProducts(): void {
    this.shoppingListProductsSelected = [];
    document
      .querySelectorAll('.product-name.selected')
      .forEach((productSelected) => {
        productSelected.classList.remove('selected');
      });
  }

  private deleteSelectedProducts(): void {
    this.shoppingListService
      .deleteShoppingListProducts(
        this.shoppingListId,
        this.shoppingListProductsSelected.map(
          (shoppingListProduct) => shoppingListProduct.name
        )
      )
      .subscribe({
        next: () => {
          this.shoppingListProductsSelected = [];
        },
        error: () => {},
      });
  }

  countBoughtProducts(shoppingList: ShoppingList) {
    return shoppingList.products?.filter((p) => p.isBought).length as number;
  }

  countTotalPrice(shoppingList: ShoppingList) {
    return shoppingList.products
      ?.filter((p) => p.price != null)
      .reduce(
        (sum, product) => sum + product.price!.price * product.quantity ?? 0,
        0
      );
  }
}
