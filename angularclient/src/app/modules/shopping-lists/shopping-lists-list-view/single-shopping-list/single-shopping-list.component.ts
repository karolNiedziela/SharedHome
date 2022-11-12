import { ShoppingListsService } from './../../services/shopping-lists.service';
import { ShoppingList } from './../../models/shopping-list';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { faCheck, faList, faXmark } from '@fortawesome/free-solid-svg-icons';
import { Router } from '@angular/router';
import {
  AdditionalPopupMenuItem,
  PopupMenuConfig,
} from 'app/shared/components/menus/popup-menu/popup-menu.config';
import { ConfirmationModalComponent } from 'app/shared/components/modals/confirmation-modal/confirmation-modal.component';
import { ConfirmationModalConfig } from 'app/shared/components/modals/confirmation-modal/confirmation-modal.config';
import { MarkAsDone } from '../../models/mark-as-done';
import { EditShoppingListModalComponent } from '../../modals/edit-shopping-list-modal/edit-shopping-list-modal.component';

@Component({
  selector: 'app-single-shopping-list',
  templateUrl: './single-shopping-list.component.html',
  styleUrls: ['./single-shopping-list.component.scss'],
})
export class SingleShoppingListComponent implements OnInit {
  @Input() shoppingList!: ShoppingList;
  detailsIcon = faList;
  boughtIcon = faCheck;
  notBoughtIcon = faXmark;
  errorMessages: string[] = [];

  @ViewChild('deleteShoppingList')
  private deleteShoppingListModal!: ConfirmationModalComponent;
  deleteShoppingListModalConfig: ConfirmationModalConfig = {
    modalTitle: 'Delete shopping list',
    onSave: () => {
      this.deleteShoppingList(this.shoppingList.id);
    },
  };
  headerPopupMenuConfig!: PopupMenuConfig;

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

  @ViewChild('editShoppingListModal')
  private editShoppingListModal!: EditShoppingListModalComponent;

  constructor(
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

  isMobile(): boolean {
    const width =
      window.innerWidth ||
      document.documentElement.clientWidth ||
      document.body.clientWidth;

    return width < 992;
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
}
