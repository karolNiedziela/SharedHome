import { CancelPurchaseOfProduct } from './../models/cancel-purchase-of-product';
import { AdditionalPopupMenuItem } from './../../../shared/components/menus/popup-menu/popup-menu.config';
import { NetContentType } from './../enums/net-content-type';
import { ShoppingListsService } from './../services/shopping-lists.service';
import { ShoppingListProduct } from './../models/shopping-list-product';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { faCheck, faXmark } from '@fortawesome/free-solid-svg-icons';
import { PopupMenuConfig } from 'app/shared/components/menus/popup-menu/popup-menu.config';
import { ConfirmationModalComponent } from 'app/shared/components/modals/confirmation-modal/confirmation-modal.component';
import { ConfirmationModalConfig } from 'app/shared/components/modals/confirmation-modal/confirmation-modal.config';
import { PurchaseShoppingListProductComponent } from '../forms/purchase-shopping-list-product/purchase-shopping-list-product.component';

@Component({
  selector: 'app-shopping-list-product',
  templateUrl: './shopping-list-product.component.html',
  styleUrls: ['./shopping-list-product.component.scss'],
})
export class ShoppingListProductComponent implements OnInit {
  @Input() shoppingListProduct?: ShoppingListProduct;
  @Input() shoppingListId!: number;
  @Input() isDone!: boolean;

  public netContentType: typeof NetContentType = NetContentType;

  @ViewChild('purchaseShoppingListProductForm')
  purchaseShoppingListProductForm!: PurchaseShoppingListProductComponent;

  @ViewChild('deleteShoppingListProduct')
  private deleteShoppingListProductModal!: ConfirmationModalComponent;
  deleteShoppingListProductModalConfig: ConfirmationModalConfig = {
    modalTitle: 'Delete shopping list product',
    onSave: () => {
      this.deleteShoppingListProduct();
    },
  };

  @ViewChild('cancelPurchaseShoppingListProduct')
  private cancelPurchaseShoppingListProductModal!: ConfirmationModalComponent;
  cancelPurchaseShoppingListProductModalConfig: ConfirmationModalConfig = {
    modalTitle: 'Cancel purchase of shopping list product',
    confirmationText: 'Are you sure to cancel purchase?',
    onSave: () => {
      this.cancelPurchaseOfShoppingListProduct();
    },
  };

  boughtIcon = faCheck;
  notBoughtIcon = faXmark;
  productPopupMenuConfig!: PopupMenuConfig;

  constructor(private shoppingListService: ShoppingListsService) {}

  ngOnInit(): void {
    this.productPopupMenuConfig = {
      onDelete: () => {
        this.deleteShoppingListProductModal.open();
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
      .subscribe({
        next: (response) => {
          console.log(response);
        },
        error: (error) => {
          console.log(error);
        },
      });
  }

  cancelPurchaseOfShoppingListProduct() {
    const cancelShoppingListProduct: CancelPurchaseOfProduct = {
      shoppingListId: this.shoppingListId,
      productName: this.shoppingListProduct!.name,
    };
    this.shoppingListService
      .cancelPurchaseOfProduct(cancelShoppingListProduct)
      .subscribe({
        next: (response) => {
          console.log(response);
        },
        error: (error) => {
          console.log(error);
        },
      });
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
          this.purchaseShoppingListProductForm.modal.open();
        },
      });
    }

    return additionalPopupMenuItems;
  }
}
