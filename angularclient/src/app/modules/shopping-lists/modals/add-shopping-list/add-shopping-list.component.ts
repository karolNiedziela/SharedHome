import { AddShoppingListProductFormComponent } from './../../forms/add-shopping-list-product-form/add-shopping-list-product-form.component';
import { Modalable } from './../../../../core/models/modalable';
import { AddManyShoppingListProductsFormComponent } from './../../forms/add-many-shopping-list-products-form/add-many-shopping-list-products-form.component';
import { AddShoppingList } from './../../models/add-shopping-list';
import { ShoppingListsService } from './../../services/shopping-lists.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import {
  Component,
  ComponentRef,
  Input,
  OnInit,
  ViewChild,
} from '@angular/core';
import { FormModalConfig } from 'app/shared/components/modals/form-modal/form-modal.config';
import { FormModalComponent } from 'app/shared/components/modals/form-modal/form-modal.component';
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

  @ViewChild('modal') private modal!: FormModalComponent;

  public modalConfig: FormModalConfig = {
    modalTitle: 'Add shopping list',
    onSave: () => this.onSave(),
    onClose: () => this.onClose(),
    onDismiss: () => this.onDismiss(),
    onReset: () => this.onReset(),
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
    if (
      this.addManyShoppingListProducts.addShoppingListProductComponents.some(
        (
          shoppingListProductForm: ComponentRef<AddShoppingListProductFormComponent>
        ) => shoppingListProductForm.instance.addShoppingListProductForm.invalid
      )
    ) {
      this.addManyShoppingListProducts.markAllAsTouchedOnSave();
      return;
    }

    const name = this.addShoppingListForm.get('name')?.value;

    const products: ShoppingListProduct[] =
      this.addManyShoppingListProducts.getProducts();

    const addShoppingList: AddShoppingList = {
      name: name,
      products: products,
    };

    this.shoppingListService.add(addShoppingList).subscribe({
      next: () => {
        this.modal.close();
      },
    });
  }

  onClose(): void {}

  onDismiss(): void {}

  onReset(): void {
    this.addManyShoppingListProducts.ngOnDestroy();
  }
}
