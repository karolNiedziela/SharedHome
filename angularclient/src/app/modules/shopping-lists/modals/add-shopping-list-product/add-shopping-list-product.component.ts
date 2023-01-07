import { Modalable } from './../../../../core/models/modalable';
import { NetContentType } from './../../enums/net-content-type';
import { ShoppingListsService } from './../../services/shopping-lists.service';
import { AddShoppingListProduct } from './../../models/add-shopping-list-product';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { ShoppingListProduct } from '../../models/shopping-list-product';
import { AddManyShoppingListProductsFormComponent } from '../../forms/add-many-shopping-list-products-form/add-many-shopping-list-products-form.component';
import { FormModalComponent } from 'src/app/shared/components/modals/form-modal/form-modal.component';
import { FormModalConfig } from 'src/app/shared/components/modals/form-modal/form-modal.config';

@Component({
  selector: 'app-add-shopping-list-product',
  templateUrl: './add-shopping-list-product.component.html',
  styleUrls: ['./add-shopping-list-product.component.scss'],
})
export class AddShoppingListProductComponent implements OnInit, Modalable {
  @Input() shoppingListId!: string;
  @ViewChild('addShoppingListProductModal') private modal!: FormModalComponent;
  public modalConfig: FormModalConfig = {
    modalTitle: 'Add shopping list product',
    onSave: () => this.onSave(),
    onReset: () => this.onReset(),
  };
  @ViewChild('addManyShoppingListProducts')
  addManyShoppingListProducts!: AddManyShoppingListProductsFormComponent;

  public addShoppingListProductForm!: FormGroup;

  public netContentType: typeof NetContentType = NetContentType;

  constructor(private shoppingListService: ShoppingListsService) {}

  ngOnInit(): void {
    this.addShoppingListProductForm = new FormGroup({
      productName: new FormControl('', [Validators.required]),
      quantity: new FormControl(1, [Validators.required]),
      netContent: new FormGroup({}),
    });
  }

  openModal(): void {
    this.addShoppingListProductForm.patchValue({ quantity: 1 });
    this.modal.open();
  }

  onSave(): void {
    this.addManyShoppingListProducts.markAllAsTouchedOnSave();

    if (this.addShoppingListProductForm.invalid) {
      this.addShoppingListProductForm.markAllAsTouched();
      return;
    }

    const productName =
      this.addShoppingListProductForm.get('productName')?.value;
    const quantity = this.addShoppingListProductForm.get('quantity')?.value;
    const netContent = this.addShoppingListProductForm
      ?.get('netContent')
      ?.get('netContent')?.value;
    const netContentType = this.addShoppingListProductForm
      ?.get('netContent')
      ?.get('netContentType')?.value;

    const firstShoppingListProduct: ShoppingListProduct = {
      name: productName,
      quantity: quantity ?? 1,
      netContent: {
        netContent: netContent,
        netContentType: netContentType,
      },
      isBought: false,
    };

    const products: ShoppingListProduct[] = [];
    products.push(firstShoppingListProduct);

    products.push(...this.addManyShoppingListProducts.getProducts());
    this.addManyShoppingListProducts.getProducts();

    const addShoppingListProduct: AddShoppingListProduct = {
      shoppingListId: this.shoppingListId,
      products: products,
    };

    this.modal.save(
      this.shoppingListService.addShoppingListProducts(addShoppingListProduct)
    );
  }

  onReset(): void {
    this.addManyShoppingListProducts.ngOnDestroy();
  }
}
