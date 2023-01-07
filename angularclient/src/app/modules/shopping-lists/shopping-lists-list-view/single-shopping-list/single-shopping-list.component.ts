import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import {
  AdditionalPopupMenuItem,
  PopupMenuConfig,
} from 'src/app/shared/components/menus/popup-menu/popup-menu.config';
import { ConfirmationModalComponent } from 'src/app/shared/components/modals/confirmation-modal/confirmation-modal.component';
import { ConfirmationModalConfig } from 'src/app/shared/components/modals/confirmation-modal/confirmation-modal.config';
import { ScreenSizeHelper } from 'src/app/shared/helpers/screen-size-helper';
import { EditShoppingListModalComponent } from '../../modals/edit-shopping-list-modal/edit-shopping-list-modal.component';
import { MarkAsDone } from '../../models/mark-as-done';
import { ShoppingList } from '../../models/shopping-list';
import { ShoppingListsService } from '../../services/shopping-lists.service';

@Component({
  selector: 'app-single-shopping-list',
  templateUrl: './single-shopping-list.component.html',
  styleUrls: ['./single-shopping-list.component.scss'],
})
export class SingleShoppingListComponent implements OnInit {
  @Input() shoppingList!: ShoppingList;
  errorMessages: string[] = [];

  @ViewChild('deleteShoppingList')
  private deleteShoppingListModal!: ConfirmationModalComponent;
  deleteShoppingListModalConfig: ConfirmationModalConfig = {
    modalTitle: 'Delete shopping list',
    onConfirm: () => {
      this.deleteShoppingList(this.shoppingList.id);
    },
  };

  headerPopupMenuConfig!: PopupMenuConfig;

  @ViewChild('markAsDoneModal')
  markAsDoneModal!: ConfirmationModalComponent;
  markAsDoneModalConfig: ConfirmationModalConfig = {
    modalTitle: 'Mark shopping list as done',
    confirmationText: 'Are you sure to mark this shopping list as done?',
    onConfirm: () => {
      this.markAsDone(true);
    },
  };

  @ViewChild('markAsUndoneModal')
  markAsUndoneModal!: ConfirmationModalComponent;
  markAsUndoneModalConfig: ConfirmationModalConfig = {
    modalTitle: 'Mark shopping list as undone',
    confirmationText: 'Are you sure to mark this shopping list as undone?',
    onConfirm: () => {
      this.markAsDone(false);
    },
  };

  @ViewChild('editShoppingListModal')
  private editShoppingListModal!: EditShoppingListModalComponent;

  constructor(
    public screenSizeHelper: ScreenSizeHelper,
    private router: Router,
    private shoppingListService: ShoppingListsService
  ) {}

  ngOnInit(): void {
    this.headerPopupMenuConfig = {};
    this.headerPopupMenuConfig = {
      isEditVisible: !this.shoppingList.isDone,
      onEdit: () => {
        this.editShoppingListModal.openModal();
      },
      isDeleteVisible: !this.shoppingList.isDone,
      onDelete: () => {
        this.deleteShoppingListModal.open();
      },
      additionalPopupMenuItems: this.getAdditionalPopupMenuItems(),
    };
  }

  openShoppingList(shoppingListId: string): void {
    this.router.navigate(['shoppinglists', shoppingListId]);
  }

  deleteShoppingList(shoppingListId: string): void {
    this.shoppingListService.delete(shoppingListId).subscribe();
  }

  markAsDone(isDone: boolean): void {
    const markAsDone: MarkAsDone = {
      shoppingListId: this.shoppingList.id,
      isDone: isDone,
    };
    this.shoppingListService.markAsDone(markAsDone, false).subscribe();
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
        text: 'Mark as done',
        onClick: () => {
          this.markAsDoneModal.open();
        },
      });
    }

    return additionalPopupMenuItems;
  }

  countBoughtProducts() {
    return this.shoppingList.products?.filter((p) => p.isBought)
      .length as number;
  }

  countTotalPrice() {
    return this.shoppingList.products
      ?.filter((p) => p.price != null)
      .reduce(
        (sum, product) => sum + product.price!.price * product.quantity ?? 0,
        0
      );
  }
}
