import { UpdateShoppingList } from './../../models/update-shopping-list';
import { ShoppingListsService } from './../../services/shopping-lists.service';
import { ShoppingList } from './../../models/shopping-list';
import { Modalable } from './../../../../core/models/modalable';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import {
  Component,
  Input,
  OnInit,
  OnChanges,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import { FormModalConfig } from 'src/app/shared/components/modals/form-modal/form-modal.config';
import { FormModalComponent } from 'src/app/shared/components/modals/form-modal/form-modal.component';

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

  @ViewChild('modal')
  private editShoppingListModal!: FormModalComponent;

  public modalConfig: FormModalConfig = {
    modalTitle: 'Edit shopping list',
    onSave: () => this.onSave(),
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
    this.setFormValues();
    this.editShoppingListModal.open();
  }

  onSave(): void {
    const name = this.editShoppingListForm.get('name')?.value;

    const updateShoppingList: UpdateShoppingList = {
      shoppingListId: this.shoppingList.id,
      name: name,
    };

    this.editShoppingListModal.save(
      this.shoppingListService.update(updateShoppingList, this.isSingleRefresh)
    );
  }

  private setFormValues(): void {
    if (this.shoppingList == null) {
      return;
    }

    this.editShoppingListForm?.patchValue({ name: this.shoppingList.name });
  }
}
