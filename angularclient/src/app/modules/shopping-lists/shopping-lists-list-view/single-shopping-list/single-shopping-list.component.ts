import { ShoppingListsService } from './../../services/shopping-lists.service';
import { ShoppingList } from './../../models/shopping-list';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { faCheck, faList, faXmark } from '@fortawesome/free-solid-svg-icons';
import { Router } from '@angular/router';
import { PopupMenuConfig } from 'app/shared/components/menus/popup-menu/popup-menu.config';
import { ConfirmationModalComponent } from 'app/shared/components/modals/confirmation-modal/confirmation-modal.component';
import { ConfirmationModalConfig } from 'app/shared/components/modals/confirmation-modal/confirmation-modal.config';

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
  popupMenuConfig: PopupMenuConfig = {
    onDelete: () => {
      this.deleteShoppingListModal.open();
    },
    additionalPopupMenuItems: [{ text: 'Make done', onClick: () => {} }],
  };
  @ViewChild('deleteShoppingList')
  private deleteShoppingListModal!: ConfirmationModalComponent;
  deleteShoppingListModalConfig: ConfirmationModalConfig = {
    modalTitle: 'Delete shopping list',
    onSave: () => {
      this.deleteShoppingList(this.shoppingList.id);
    },
  };

  constructor(
    private router: Router,
    private shoppingListService: ShoppingListsService
  ) {}

  ngOnInit(): void {}

  openShoppingList(shoppingListId: number): void {
    this.router.navigate(['shoppinglists', shoppingListId]);
  }

  deleteShoppingList(shoppingListId: number): void {
    this.shoppingListService.delete(shoppingListId).subscribe({
      next: (response) => {
        console.log(response);
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
}
