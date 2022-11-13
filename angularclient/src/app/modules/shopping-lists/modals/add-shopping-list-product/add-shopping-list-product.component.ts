import { Modalable } from './../../../../core/models/modalable';
import { NetContentType } from './../../enums/net-content-type';
import { ShoppingListsService } from './../../services/shopping-lists.service';
import { AddShoppingListProduct } from './../../models/add-shopping-list-product';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { ModalComponent } from 'app/shared/components/modals/modal/modal.component';
import { ModalConfig } from 'app/shared/components/modals/modal/modal.config';
import { ShoppingListProduct } from '../../models/shopping-list-product';
import { AddManyShoppingListProductsFormComponent } from '../../forms/add-many-shopping-list-products-form/add-many-shopping-list-products-form.component';

@Component({
  selector: 'app-add-shopping-list-product',
  templateUrl: './add-shopping-list-product.component.html',
  styleUrls: ['./add-shopping-list-product.component.scss'],
})
export class AddShoppingListProductComponent implements OnInit, Modalable {
  @Input() shoppingListId!: string;
  @ViewChild('addShoppingListProductModal') private modal!: ModalComponent;
  public modalConfig: ModalConfig = {
    modalTitle: 'Add shopping list product',
    onSave: () => this.onSave(),
    onClose: () => this.onClose(),
    onDismiss: () => this.onDismiss(),
  };
  @ViewChild('addManyShoppingListProducts')
  addManyShoppingListProducts!: AddManyShoppingListProductsFormComponent;

  public addShoppingListProductForm!: FormGroup;

  public netContentType: typeof NetContentType = NetContentType;

  public errorMessages: string[] = [];

  constructor(private shoppingListService: ShoppingListsService) {}

  ngOnInit(): void {
    this.addShoppingListProductForm = new FormGroup({
      productName: new FormControl('', [Validators.required]),
      quantity: new FormControl(1, [Validators.required]),
      netContent: new FormGroup({}),
    });
  }

  openModal(): void {
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

    this.shoppingListService
      .addShoppingListProducts(addShoppingListProduct)
      .subscribe({
        next: () => {
          this.resetForm();

          this.modal.close();
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

  private resetForm() {
    this.addShoppingListProductForm.reset();
    this.addShoppingListProductForm.patchValue({ quantity: 1 });
    this.addManyShoppingListProducts.ngOnDestroy();
    this.errorMessages = [];
  }
}
