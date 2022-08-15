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
  ViewChild,
  AfterViewInit,
} from '@angular/core';
import { ModalConfig } from 'app/shared/components/modals/modal/modal.config';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-edit-shopping-list-modal',
  templateUrl: './edit-shopping-list-modal.component.html',
  styleUrls: ['./edit-shopping-list-modal.component.scss'],
})
export class EditShoppingListModalComponent implements Modalable, OnInit {
  @Input() shoppingList$!: BehaviorSubject<ShoppingList>;
  @Input() isSingleRefresh!: boolean;

  shoppingList!: ShoppingList;
  editShoppingListForm!: FormGroup;

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

    this.shoppingList$.asObservable().subscribe((res) => {
      this.shoppingList = res;

      this.setFormValues();
    });
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
        next: (response) => {
          console.log(response);

          this.resetForm();

          this.editShoppingListModal.close();
        },
        error: (error) => {
          console.log(error);
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

    this.editShoppingListForm.patchValue({ name: this.shoppingList.name });
  }

  private resetForm(): void {
    this.setFormValues();
  }
}
