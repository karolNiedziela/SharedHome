import { Modalable } from './../../../../core/models/modalable';
import { AddShoppingList } from './../../models/add-shopping-list';
import { ShoppingListsService } from './../../services/shopping-lists.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import {
  Component,
  ComponentRef,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import { ShoppingListProduct } from '../../models/shopping-list-product';
import { FormModalComponent } from 'src/app/shared/components/modals/form-modal/form-modal.component';
import { FormModalConfig } from 'src/app/shared/components/modals/form-modal/form-modal.config';
import { AddShoppingListProductFormComponent } from '../../forms/add-shopping-list-product-form/add-shopping-list-product-form.component';
import { AddManyShoppingListProductsFormComponent } from '../../forms/add-many-shopping-list-products-form/add-many-shopping-list-products-form.component';

@Component({
  selector: 'app-add-shopping-list',
  templateUrl: './add-shopping-list.component.html',
  styleUrls: ['./add-shopping-list.component.scss'],
})
export class AddShoppingListComponent implements OnInit, Modalable, OnChanges {
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
    onReset: () => this.onReset(),
  };

  constructor(private shoppingListService: ShoppingListsService) {}

  ngOnChanges(changes: SimpleChanges) {
    console.log(changes);
  }

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
      products: [],
    };

    this.modal.save(this.shoppingListService.add(addShoppingList));
  }

  onReset(): void {
    this.addManyShoppingListProducts.ngOnDestroy();
  }
}
