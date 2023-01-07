import { EditShoppingListModalComponent } from './../modals/edit-shopping-list-modal/edit-shopping-list-modal.component';
import { CancelPurchaseOfProduct } from './../models/cancel-purchase-of-product';
import {
  AdditionalPopupMenuItem,
  PopupMenuConfig,
} from './../../../shared/components/menus/popup-menu/popup-menu.config';
import { NetContentType } from './../enums/net-content-type';
import { ShoppingListsService } from './../services/shopping-lists.service';
import { ShoppingListProduct } from './../models/shopping-list-product';
import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { faCheck, faXmark } from '@fortawesome/free-solid-svg-icons';
import { ConfirmationModalComponent } from 'src/app/shared/components/modals/confirmation-modal/confirmation-modal.component';
import { ConfirmationModalConfig } from 'src/app/shared/components/modals/confirmation-modal/confirmation-modal.config';
import { PurchaseShoppingListProductComponent } from '../modals/purchase-shopping-list-product/purchase-shopping-list-product.component';

@Component({
  selector: 'app-shopping-list-product',
  templateUrl: './shopping-list-product.component.html',
  styleUrls: ['./shopping-list-product.component.scss'],
})
export class ShoppingListProductComponent implements OnInit {
  @Input() shoppingListProduct!: ShoppingListProduct;
  @Input() shoppingListId!: string;
  @Input() isDone!: boolean;

  public netContentType: typeof NetContentType = NetContentType;

  @ViewChild('purchaseShoppingListProductForm')
  purchaseShoppingListProductForm!: PurchaseShoppingListProductComponent;

  @ViewChild('deleteShoppingListProduct')
  private deleteShoppingListProductModal!: ConfirmationModalComponent;
  deleteShoppingListProductModalConfig: ConfirmationModalConfig = {
    modalTitle: 'Delete shopping list product',
    onConfirm: () => {
      this.deleteShoppingListProduct();
    },
  };

  @ViewChild('cancelPurchaseShoppingListProduct')
  private cancelPurchaseShoppingListProductModal!: ConfirmationModalComponent;
  cancelPurchaseShoppingListProductModalConfig: ConfirmationModalConfig = {
    modalTitle: 'Cancel purchase of shopping list product',
    confirmationText: 'Are you sure to cancel purchase?',
    onConfirm: () => {
      this.cancelPurchaseOfShoppingListProduct();
    },
  };

  @ViewChild('editShoppingListProduct')
  private editShoppingListProductModal!: EditShoppingListModalComponent;

  boughtIcon = faCheck;
  notBoughtIcon = faXmark;
  productPopupMenuConfig!: PopupMenuConfig;

  constructor(
    private shoppingListService: ShoppingListsService,
    public element: ElementRef
  ) {}

  ngOnInit(): void {
    this.productPopupMenuConfig = {
      // isHidden: this.isDone,
      onDelete: () => {
        this.deleteShoppingListProductModal.open();
      },
      onEdit: () => {
        this.editShoppingListProductModal.openModal();
      },
      additionalPopupMenuItems: this.getAdditionalPopupMenuItems(),
    };
  }

  deleteShoppingListProduct() {
    this.shoppingListService
      .deleteShoppingListProduct(
        this.shoppingListId,
        this.shoppingListProduct!.name
      )
      .subscribe();
  }

  cancelPurchaseOfShoppingListProduct() {
    const cancelShoppingListProduct: CancelPurchaseOfProduct = {
      shoppingListId: this.shoppingListId,
      productName: this.shoppingListProduct!.name,
    };
    this.shoppingListService
      .cancelPurchaseOfProduct(cancelShoppingListProduct)
      .subscribe();
  }

  private getAdditionalPopupMenuItems(): AdditionalPopupMenuItem[] {
    const additionalPopupMenuItems: AdditionalPopupMenuItem[] = [];
    if (this.isDone) {
      return additionalPopupMenuItems;
    }

    if (this.shoppingListProduct?.isBought) {
      additionalPopupMenuItems.push({
        text: 'Cancel purchase',
        onClick: () => {
          this.cancelPurchaseShoppingListProductModal.open();
        },
      });
    } else {
      additionalPopupMenuItems.push({
        text: 'Purchase',
        onClick: () => {
          this.purchaseShoppingListProductForm.openModal();
        },
      });
    }

    return additionalPopupMenuItems;
  }
}
