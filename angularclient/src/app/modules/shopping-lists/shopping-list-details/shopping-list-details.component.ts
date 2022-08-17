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
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-shopping-list-details',
  templateUrl: './shopping-list-details.component.html',
  styleUrls: ['./shopping-list-details.component.scss'],
})
export class ShoppingListComponent implements OnInit, AfterViewInit, OnDestroy {
  shoppingListId!: number;
  shoppingListProductNamesSelected: string[] = [];

  shoppingList?: ShoppingList;

  productsSubscription!: Subscription;

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

  constructor(
    private activatedRoute: ActivatedRoute,
    private shoppingListService: ShoppingListsService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(
      (params: Params) => (this.shoppingListId = params['shoppingListId'])
    );

    this.shoppingListService.singleShoppingListRefreshNeeded.subscribe(() => {
      this.getShoppingList();
    });

    this.getShoppingList();
  }

  ngAfterViewInit(): void {
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
          text: 'Deselect',
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

    this.productsSubscription = this.products.changes.subscribe(
      (c: QueryList<ShoppingListProductComponent>) => {
        c.toArray().forEach((product) => {
          const productHtmlElement =
            product.element.nativeElement.querySelector(
              '.product'
            ) as HTMLElement;

          productHtmlElement.addEventListener('click', (e) => {
            const targetHtmlElement = e.target as HTMLElement;
            if (!['svg', 'path'].includes(targetHtmlElement.nodeName)) {
              productHtmlElement.classList.toggle('selected');
              this.onProductSelect(product);
            }
          });
        });
      }
    );
  }

  ngOnDestroy(): void {
    this.productsSubscription.unsubscribe();
  }

  getShoppingList() {
    this.shoppingListService.get(this.shoppingListId).subscribe((response) => {
      this.shoppingList = new ShoppingList(response.data);
    });
  }

  markAsDone(isDone: boolean): void {
    const markAsDone: MarkAsDone = {
      shoppingListId: this.shoppingListId,
      isDone: isDone,
    };
    this.shoppingListService.markAsDone(markAsDone, true).subscribe({
      next: (response) => {},
      error: (error) => {
        console.log(error);
      },
    });
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
          this.addShoppingListProductForm.modal.open();
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
      next: (response) => {
        console.log(response);
        this.router.navigate(['shoppinglists']);
      },
      error: (error) => {
        console.log(error);
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
  }

  private deselectProducts(): void {
    this.shoppingListProductNamesSelected = [];
    document
      .querySelectorAll('.product.selected')
      .forEach((productSelected) => {
        productSelected.classList.remove('selected');
      });
  }

  openDeleteSelectedProductsModal(): void {
    this.deleteSelectedProductsModalConfig.confirmationText = `Are you sure you want to delete ${this.shoppingListProductNamesSelected.join(
      ', '
    )}?`;

    this.deleteSelectedProductsModal.open();
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
