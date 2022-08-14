import { NetContentType } from './../../enums/net-content-type';
import { ShoppingListsService } from './../../services/shopping-lists.service';
import { AddShoppingListProduct } from './../../models/add-shopping-list-product';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {
  Component,
  ComponentRef,
  Input,
  OnInit,
  ViewChild,
  ViewContainerRef,
} from '@angular/core';
import { ModalComponent } from 'app/shared/components/modals/modal/modal.component';
import { ModalConfig } from 'app/shared/components/modals/modal/modal.config';
import { AddShoppingListProductFormComponent } from '../../forms/add-shopping-list-product-form/add-shopping-list-product-form.component';
import { ShoppingListProduct } from '../../models/shopping-list-product';

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
  @ViewChild('container', { read: ViewContainerRef })
  container!: ViewContainerRef;

  public addShoppingListProductForm!: FormGroup;

  public netContentType: typeof NetContentType = NetContentType;

  public errorMessages: string[] = [];

  productComponents: ComponentRef<AddShoppingListProductFormComponent>[] = [];

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

    const products: ShoppingListProduct[] = this.productComponents.map((p) => {
      return p.instance.getProduct();
    });

    products.push(firstShoppingListProduct);

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

  addProduct() {
    const product = this.container.createComponent(
      AddShoppingListProductFormComponent
    );
    product.instance.uniqueKey = this.productComponents.length + 1;
    product.instance.delete.subscribe((uniqueKey: number) => {
      this.removeProduct(uniqueKey);
    });

    this.productComponents.push(product);
  }

  removeProduct(uniqueKey: number) {
    const productComponent: ComponentRef<AddShoppingListProductFormComponent> =
      this.productComponents.find((pc) => pc.instance.uniqueKey == uniqueKey)!;
    const productComponentIndex =
      this.productComponents.indexOf(productComponent);

    if (productComponentIndex !== -1) {
      this.container.remove(this.productComponents.indexOf(productComponent));
      this.productComponents.splice(productComponentIndex, 1);
    }
  }

  private resetForm() {
    this.addShoppingListProductForm.reset();
    this.addShoppingListProductForm.patchValue({ quantity: 1 });
    this.productComponents.map((x) => x.destroy());
    this.errorMessages = [];
  }
}
