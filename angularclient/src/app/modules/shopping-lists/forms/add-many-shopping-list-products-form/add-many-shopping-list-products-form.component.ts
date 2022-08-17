import { Subscription } from 'rxjs/internal/Subscription';
import {
  Component,
  ComponentRef,
  OnDestroy,
  OnInit,
  ViewChild,
  ViewContainerRef,
} from '@angular/core';
import { ShoppingListProduct } from '../../models/shopping-list-product';
import { AddShoppingListProductFormComponent } from '../add-shopping-list-product-form/add-shopping-list-product-form.component';

@Component({
  selector: 'app-add-many-shopping-list-products-form',
  templateUrl: './add-many-shopping-list-products-form.component.html',
  styleUrls: ['./add-many-shopping-list-products-form.component.scss'],
})
export class AddManyShoppingListProductsFormComponent
  implements OnInit, OnDestroy
{
  productInstanceSubscription!: Subscription;

  @ViewChild('container', { read: ViewContainerRef })
  container!: ViewContainerRef;

  productComponents: ComponentRef<AddShoppingListProductFormComponent>[] = [];

  constructor() {}

  ngOnInit(): void {}

  ngOnDestroy(): void {
    this.productComponents.map((x) => x.destroy());
    if (this.productInstanceSubscription)
      this.productInstanceSubscription.unsubscribe();
  }

  addProduct() {
    const product = this.container.createComponent(
      AddShoppingListProductFormComponent
    );
    product.instance.uniqueKey = this.productComponents.length + 1;
    this.productInstanceSubscription = product.instance.delete.subscribe(
      (uniqueKey: number) => {
        this.removeProduct(uniqueKey);
      }
    );

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

  getProducts(): ShoppingListProduct[] {
    return this.productComponents.map((p) => {
      return p.instance.getProduct();
    });
  }

  markAllAsTouchedOnSave() {
    this.productComponents.map((p) => {
      if (p.instance.addShoppingListProductForm.invalid) {
        p.instance.addShoppingListProductForm.markAllAsTouched();
      }
    });
  }

  clearProducts(): void {
    this.productComponents.map((x) => x.destroy());
  }
}
