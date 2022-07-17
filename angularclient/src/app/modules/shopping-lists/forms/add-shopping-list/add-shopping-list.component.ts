import { tap } from 'rxjs';
import { AddShoppingList } from './../../models/add-shopping-list';
import { ShoppingListsService } from './../../services/shopping-lists.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, Input, OnInit, Output, ViewChild } from '@angular/core';
import { ModalConfig } from 'app/shared/components/modals/modal/modal.config';
import { ModalComponent } from 'app/shared/components/modals/modal/modal.component';

@Component({
  selector: 'app-add-shopping-list',
  templateUrl: './add-shopping-list.component.html',
  styleUrls: ['./add-shopping-list.component.scss'],
})
export class AddShoppingListComponent implements OnInit {
  @Input() year!: number;
  @Input() month!: number;
  addShoppingListForm!: FormGroup;

  @ViewChild('modal') private modal!: ModalComponent;

  public modalConfig: ModalConfig = {
    modalTitle: 'Add shopping list',
    onSave: () => this.onSave(),
    onClose: () => this.onClose(),
    onDismiss: () => this.onDismiss(),
  };

  constructor(private shoppingListService: ShoppingListsService) {
    this.addShoppingListForm = new FormGroup({
      name: new FormControl('', [
        Validators.required,
        Validators.maxLength(20),
      ]),
    });
  }

  ngOnInit(): void {}

  openModal(): void {
    this.modal.open();
  }

  onSave(): void {
    if (this.addShoppingListForm.invalid) {
      this.addShoppingListForm.markAllAsTouched();
      return;
    }

    const name = this.addShoppingListForm.get('name')?.value;
    const addShoppingList: AddShoppingList = {
      name: name,
    };
    this.shoppingListService.add(addShoppingList).subscribe(
      (response) => {
        console.log(response);
      },
      (error) => {
        console.log(error);
      }
    );

    this.addShoppingListForm.reset();

    this.modal.close();
  }

  onClose(): void {
    this.addShoppingListForm.reset();
  }

  onDismiss(): void {
    this.addShoppingListForm.reset();
  }
}
