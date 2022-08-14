import { NetContentType } from './../../enums/net-content-type';
import { ShoppingListsService } from './../../services/shopping-lists.service';
import { AddShoppingListProduct } from './../../models/add-shopping-list-product';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {
  Component,
  ComponentFactoryResolver,
  Input,
  OnInit,
  ViewChild,
} from '@angular/core';
import { ModalComponent } from 'app/shared/components/modals/modal/modal.component';
import { ModalConfig } from 'app/shared/components/modals/modal/modal.config';
import { ShoppingListProduct } from '../../models/shopping-list-product';
import { AddManyShoppingListProductsFormComponent } from '../../forms/add-many-shopping-list-products-form/add-many-shopping-list-products-form.component';

@Component({
  selector: 'app-add-shopping-list-product',
  templateUrl: './add-shopping-list-product.component.html',
  styleUrls: ['./add-shopping-list-product.component.scss'],
})
export class AddShoppingListProductComponent implements OnInit {
  @Input() shoppingListId!: number;
  @ViewChild('addShoppingListProductModal') public modal!: ModalComponent;
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
      netContent: new FormControl(''),
      netContenType: new FormControl(null),
    });
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
    const netContent = this.addShoppingListProductForm.get('netContent')?.value;
    const netContentType =
      this.addShoppingListProductForm.get('netContenType')?.value;

    const firstShoppingListProduct: ShoppingListProduct = {
      name: productName,
      quantity: quantity,
      netContent: netContent,
      netContentType: netContentType,
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
      .addShoppingListProduct(addShoppingListProduct)
      .subscribe({
        next: (response) => {
          console.log(response);
          this.resetForm();

          this.modal.close();
        },
        error: (error) => {
          console.log(error);
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
