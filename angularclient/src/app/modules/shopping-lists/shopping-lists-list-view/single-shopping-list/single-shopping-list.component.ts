import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import {
  AdditionalPopupMenuItem,
  PopupMenuConfig,
} from 'src/app/shared/components/menus/popup-menu/popup-menu.config';
import { ConfirmationModalComponent } from 'src/app/shared/components/modals/confirmation-modal/confirmation-modal.component';
import { ConfirmationModalConfig } from 'src/app/shared/components/modals/confirmation-modal/confirmation-modal.config';
import { ScreenSizeHelper } from 'src/app/shared/helpers/screen-size-helper';
import { ShoppingListStatus } from '../../enums/shopping-list-status';
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
    modalTitle: 'shopping_lists.mark_shopping_list_as_done',
    confirmationText: 'shopping_lists.mark_shopping_list_as_done_text',
    onConfirm: () => {
      this.markAsDone(ShoppingListStatus.Done);
    },
  };

  @ViewChild('markAsUndoneModal')
  markAsUndoneModal!: ConfirmationModalComponent;
  markAsUndoneModalConfig: ConfirmationModalConfig = {
    modalTitle: 'shopping_lists.mark_shopping_list_as_undone',
    confirmationText: 'shopping_lists.mark_shopping_list_as_undone_text',
    onConfirm: () => {
      this.markAsDone(ShoppingListStatus['To do']);
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
      isEditVisible: this.shoppingList.status == ShoppingListStatus['To do'],
      onEdit: () => {
        this.editShoppingListModal.openModal();
      },
      isDeleteVisible: this.shoppingList.status == ShoppingListStatus['To do'],
      onDelete: () => {
        this.deleteShoppingListModal.open();
      },
      additionalPopupMenuItems: this.getAdditionalPopupMenuItems(),
    };
  }

  public openShoppingList(event: any, iconName?: string): void {
    if (event.target.tagName == 'MAT-ICON' && iconName != 'info') {
      event.preventDefault();
      return;
    }
    this.router.navigate(['shoppinglists', this.shoppingList.id]);
  }

  public deleteShoppingList(shoppingListId: string): void {
    this.shoppingListService.delete(shoppingListId).subscribe();
  }

  public markAsDone(status: ShoppingListStatus): void {
    const markAsDone: MarkAsDone = {
      shoppingListId: this.shoppingList.id,
      status: status,
    };
    this.shoppingListService.markAsDone(markAsDone, false).subscribe();
  }

  public countBoughtProducts(): number {
    return this.shoppingList.products?.filter((p) => p.isBought)
      .length as number;
  }

  public countTotalPrice(): number {
    return this.shoppingList.products
      ?.filter((p) => p.price != null)
      .reduce(
        (sum, product) => sum + product.price!.price * product.quantity ?? 0,
        0
      );
  }

  private getAdditionalPopupMenuItems(): AdditionalPopupMenuItem[] {
    const additionalPopupMenuItems: AdditionalPopupMenuItem[] = [];
    if (this.shoppingList?.status == ShoppingListStatus.Done) {
      additionalPopupMenuItems.push({
        text: 'shopping_lists.mark_shopping_list_as_undone',
        onClick: () => {
          this.markAsUndoneModal.open();
        },
      });
    } else {
      additionalPopupMenuItems.push({
        text: 'shopping_lists.mark_shopping_list_as_done',
        onClick: () => {
          this.markAsDoneModal.open();
        },
      });
    }

    return additionalPopupMenuItems;
  }
}
