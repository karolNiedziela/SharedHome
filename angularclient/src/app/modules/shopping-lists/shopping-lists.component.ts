import { ConfirmationModalConfig } from './../../shared/components/modals/confirmation-modal/confirmation-modal.config';
import { ModalComponent } from './../../shared/components/modals/modal/modal.component';
import { ShoppingList } from './models/shopping-list';
import { ShoppingListsService } from './services/shopping-lists.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import {
  faCheck,
  faCircleInfo,
  faEllipsisVertical,
  faXmark,
} from '@fortawesome/free-solid-svg-icons';
import { Component, OnInit, ViewChild } from '@angular/core';
import { yearAndMonthFormat } from 'app/shared/validators/dateformat.validator';
import { ModalConfig } from 'app/shared/components/modals/modal/modal.config';
import { ConfirmationModalComponent } from 'app/shared/components/modals/confirmation-modal/confirmation-modal.component';

@Component({
  selector: 'app-shopping-lists',
  templateUrl: './shopping-lists.component.html',
  styleUrls: ['./shopping-lists.component.scss'],
})
export class ShoppingListsComponent implements OnInit {
  previewIcon = faCircleInfo;
  moreOptionsIcon = faEllipsisVertical;
  boughtIcon = faCheck;
  notBoughtIcon = faXmark;

  shoppingListForm!: FormGroup;
  shoppingLists: ShoppingList[] = [];

  @ViewChild('modal') private modal!: ModalComponent;

  public modalConfig: ModalConfig = {
    modalTitle: 'Add shopping list',
  };

  @ViewChild('confirmationModal')
  private confirmationModal!: ConfirmationModalComponent;
  public confirmationModalConfig: ConfirmationModalConfig = {};

  constructor(private shoppingListService: ShoppingListsService) {
    const currentYearAndMonth = `${new Date().getFullYear()} ${new Date().getMonth()}`;
    this.shoppingListForm = new FormGroup({
      yearAndMonth: new FormControl(currentYearAndMonth, [
        Validators.required,
        yearAndMonthFormat,
      ]),
    });
  }

  ngOnInit(): void {
    this.shoppingListService
      .getAllByYearAndMonthAndIsDone(2022, 7, false)
      .subscribe((response) => {
        this.shoppingLists = response.items.map((item) => {
          return new ShoppingList(
            item.id,
            item.name,
            item.isDone,
            item.createdByFirstName,
            item.createdByLastName,
            item.products!
          );
        });
        console.log(response);
        console.log(this.shoppingLists);
      });
  }

  onCurrentYearAndMonthChanged(): void {
    console.log('hello');
  }

  openModal() {
    this.modal.open();
  }

  openConfirmationModal() {
    this.confirmationModal.open();
  }
}
