import { MarkAsDone } from './../models/mark-as-done';
import { ConfirmationModalConfig } from './../../../shared/components/modals/confirmation-modal/confirmation-modal.config';
import { ConfirmationModalComponent } from './../../../shared/components/modals/confirmation-modal/confirmation-modal.component';
import { ShoppingList } from '../models/shopping-list';
import { ShoppingListsService } from '../services/shopping-lists.service';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Component, OnInit, ViewChild } from '@angular/core';
import {
  AdditionalPopupMenuItem,
  PopupMenuConfig,
} from 'app/shared/components/menus/popup-menu/popup-menu.config';
import { AddShoppingListProductComponent } from '../modals/add-shopping-list-product/add-shopping-list-product.component';

@Component({
  selector: 'app-shopping-list-details',
  templateUrl: './shopping-list-details.component.html',
  styleUrls: ['./shopping-list-details.component.scss'],
})
export class ShoppingListComponent implements OnInit {
  shoppingListId!: number;
  shoppingList?: ShoppingList;
  @ViewChild('addShoppingListProductForm')
  addShoppingListProductForm!: AddShoppingListProductComponent;
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

  @ViewChild('deleteShoppingList')
  private deleteShoppingListModal!: ConfirmationModalComponent;
  deleteShoppingListModalConfig: ConfirmationModalConfig = {
    modalTitle: 'Delete shopping list',
    onSave: () => {
      this.deleteShoppingList(this.shoppingListId);
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

    this.headerPopupMenuConfig = {};
  }

  getShoppingList() {
    this.shoppingListService.get(this.shoppingListId).subscribe((response) => {
      this.shoppingList = new ShoppingList(
        response.data.id,
        response.data.name,
        response.data.isDone,
        response.data.createdByFirstName,
        response.data.createdByLastName,
        response.data.products!
      );

      this.headerPopupMenuConfig = {
        isEditVisible: !this.shoppingList.isDone,
        isDeleteVisible: true,
        onDelete: () => {
          this.deleteShoppingListModal.open();
        },
        additionalPopupMenuItems: this.getAdditionalPopupMenuItems(),
      };
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
        text: 'Add product',
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
}
