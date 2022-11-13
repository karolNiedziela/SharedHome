import { Subscription } from 'rxjs/internal/Subscription';
import {
  Component,
  ComponentRef,
  OnDestroy,
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
export class AddManyShoppingListProductsFormComponent implements OnDestroy {
  productInstanceSubscription!: Subscription;

  @ViewChild('additionalAddShoppingListForms', { read: ViewContainerRef })
  additionalAddShoppingListForms!: ViewContainerRef;

  addShoppingListProductComponents: ComponentRef<AddShoppingListProductFormComponent>[] =
    [];

  ngOnDestroy(): void {
    this.destroyProductComponents();
    if (this.productInstanceSubscription)
      this.productInstanceSubscription.unsubscribe();
  }

  public addProduct(): void {
    const product = this.additionalAddShoppingListForms.createComponent(
      AddShoppingListProductFormComponent
    );
    product.instance.uniqueKey =
      this.addShoppingListProductComponents.length + 1;
    this.productInstanceSubscription =
      product.instance.removedProduct.subscribe((uniqueKey: number) => {
        this.removeProduct(uniqueKey);
      });

    this.addShoppingListProductComponents.push(product);
  }

  public removeProduct(uniqueKey: number): void {
    const productComponent: ComponentRef<AddShoppingListProductFormComponent> =
      this.addShoppingListProductComponents.find(
        (componentRef: ComponentRef<AddShoppingListProductFormComponent>) =>
          componentRef.instance.uniqueKey == uniqueKey
      )!;
    const productComponentIndex =
      this.addShoppingListProductComponents.indexOf(productComponent);

    if (productComponentIndex !== -1) {
      this.additionalAddShoppingListForms.remove(
        this.addShoppingListProductComponents.indexOf(productComponent)
      );
      this.addShoppingListProductComponents.splice(productComponentIndex, 1);
    }
  }

  getProducts(): ShoppingListProduct[] {
    return this.addShoppingListProductComponents.map(
      (componentRef: ComponentRef<AddShoppingListProductFormComponent>) => {
        return componentRef.instance.getProduct();
      }
    );
  }

  markAllAsTouchedOnSave(): void {
    this.addShoppingListProductComponents.map(
      (componentRef: ComponentRef<AddShoppingListProductFormComponent>) => {
        if (componentRef.instance.addShoppingListProductForm.invalid) {
          componentRef.instance.addShoppingListProductForm.markAllAsTouched();
        }
      }
    );
  }

  destroyProductComponents(): void {
    this.addShoppingListProductComponents.map(
      (componentRef: ComponentRef<AddShoppingListProductFormComponent>) =>
        componentRef.destroy()
    );

    this.addShoppingListProductComponents = [];
  }
}
