import { UpdateShoppingList } from './../../models/update-shopping-list';
import { ShoppingListsService } from './../../services/shopping-lists.service';
import { ShoppingList } from './../../models/shopping-list';
import { Modalable } from './../../../../core/models/modalable';
import { ModalComponent } from 'app/shared/components/modals/modal/modal.component';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import {
  Component,
  Input,
  OnInit,
  OnChanges,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import { ModalConfig } from 'app/shared/components/modals/modal/modal.config';

@Component({
  selector: 'app-edit-shopping-list-modal',
  templateUrl: './edit-shopping-list-modal.component.html',
  styleUrls: ['./edit-shopping-list-modal.component.scss'],
})
export class EditShoppingListModalComponent
  implements Modalable, OnInit, OnChanges
{
  @Input() shoppingList!: ShoppingList;
  @Input() isSingleRefresh!: boolean;
  editShoppingListForm!: FormGroup;
  errorMessages: string[] = [];

  @ViewChild('modal')
  private editShoppingListModal!: ModalComponent;

  public modalConfig: ModalConfig = {
    modalTitle: 'Edit shopping list',
    onSave: () => this.onSave(),
    onClose: () => this.onClose(),
    onDismiss: () => this.onDismiss(),
  };

  constructor(private shoppingListService: ShoppingListsService) {}

  ngOnInit(): void {
    this.editShoppingListForm = new FormGroup({
      name: new FormControl('', [
        Validators.required,
        Validators.maxLength(20),
      ]),
    });

    this.setFormValues();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['shoppingList']) {
      this.setFormValues();
    }
  }

  openModal(): void {
    this.editShoppingListModal.open();
  }

  onSave(): void {
    if (this.editShoppingListForm.invalid) {
      this.editShoppingListForm.markAllAsTouched();

      return;
    }

    const name = this.editShoppingListForm.get('name')?.value;

    const updateShoppingList: UpdateShoppingList = {
      id: this.shoppingList.id,
      name: name,
    };

    this.shoppingListService
      .update(updateShoppingList, this.isSingleRefresh)
      .subscribe({
        next: () => {
          this.resetForm();

          this.editShoppingListModal.close();
        },
        error: (error: string[]) => {
          this.errorMessages = error;
        },
      });
  }

  onClose(): void {
    this.resetForm();
  }

  onDismiss(): void {
    this.resetForm();
  }

  private setFormValues(): void {
    if (this.shoppingList == null) {
      return;
    }

    this.editShoppingListForm?.patchValue({ name: this.shoppingList.name });
  }

  private resetForm(): void {
    this.setFormValues();
  }
}
