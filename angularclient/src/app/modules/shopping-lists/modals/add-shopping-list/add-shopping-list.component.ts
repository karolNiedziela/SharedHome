import { Modalable } from './../../../../core/models/modalable';
import { AddManyShoppingListProductsFormComponent } from './../../forms/add-many-shopping-list-products-form/add-many-shopping-list-products-form.component';
import { AddShoppingList } from './../../models/add-shopping-list';
import { ShoppingListsService } from './../../services/shopping-lists.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { ModalConfig } from 'app/shared/components/modals/modal/modal.config';
import { ModalComponent } from 'app/shared/components/modals/modal/modal.component';
import { ShoppingListProduct } from '../../models/shopping-list-product';

@Component({
  selector: 'app-add-shopping-list',
  templateUrl: './add-shopping-list.component.html',
  styleUrls: ['./add-shopping-list.component.scss'],
})
export class AddShoppingListComponent implements OnInit, Modalable {
  @Input() year!: number;
  @Input() month!: number;
  addShoppingListForm!: FormGroup;
  errorMessages: string[] = [];

  @ViewChild('addManyShoppingListProducts')
  addManyShoppingListProducts!: AddManyShoppingListProductsFormComponent;

  @ViewChild('modal') private modal!: ModalComponent;

  public modalConfig: ModalConfig = {
    modalTitle: 'Add shopping list',
    onSave: () => this.onSave(),
    onClose: () => this.onClose(),
    onDismiss: () => this.onDismiss(),
  };

  constructor(private shoppingListService: ShoppingListsService) {}

  ngOnInit(): void {
    this.addShoppingListForm = new FormGroup({
      name: new FormControl('', [
        Validators.required,
        Validators.maxLength(20),
      ]),
    });
  }

  openModal(): void {
    this.modal.open();
  }

  onSave(): void {
    this.addManyShoppingListProducts.markAllAsTouchedOnSave();

    if (this.addShoppingListForm.invalid) {
      this.addShoppingListForm.markAllAsTouched();
      return;
    }

    const name = this.addShoppingListForm.get('name')?.value;

    const products: ShoppingListProduct[] =
      this.addManyShoppingListProducts.getProducts();

    const addShoppingList: AddShoppingList = {
      name: name,
      products: products,
    };

    this.shoppingListService.add(addShoppingList).subscribe(
      () => {
        this.resetForm();

        this.modal.close();
      },
      (error: string[]) => {
        this.errorMessages = error;
      }
    );
  }

  onClose(): void {
    this.resetForm();
  }

  onDismiss(): void {
    this.resetForm();
  }

  private resetForm(): void {
    this.addShoppingListForm.reset();
    this.addManyShoppingListProducts.ngOnDestroy();
  }
}
