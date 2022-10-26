import { ShoppingListsService } from './../../services/shopping-lists.service';
import { Modalable } from './../../../../core/models/modalable';
import {
  Component,
  Input,
  OnInit,
  QueryList,
  ViewChild,
  ViewChildren,
} from '@angular/core';
import { ModalComponent } from 'app/shared/components/modals/modal/modal.component';
import { ModalConfig } from 'app/shared/components/modals/modal/modal.config';
import { PurchaseShoppingListProductFormComponent } from '../../forms/purchase-shopping-list-product-form/purchase-shopping-list-product-form.component';
import { PurchaseShoppingListProducts } from '../../models/purchase-shopping-list-products';
@Component({
  selector: 'app-purchase-shopping-list-products-modal',
  templateUrl: './purchase-shopping-list-products-modal.component.html',
  styleUrls: ['./purchase-shopping-list-products-modal.component.scss'],
})
export class PurchaseShoppingListProductsModalComponent
  implements Modalable, OnInit
{
  @Input() shoppingListProductNames: string[] = [];
  @Input() shoppingListId!: string;

  @ViewChild('purchaseShoppingListProductModal') private modal!: ModalComponent;
  public modalConfig: ModalConfig = {
    modalTitle: 'Purchase shopping list products',
    onSave: () => this.onSave(),
    onClose: () => this.onClose(),
    onDismiss: () => this.onDismiss(),
  };

  @ViewChildren(PurchaseShoppingListProductFormComponent)
  purchaseProductsViewChildren!: QueryList<PurchaseShoppingListProductFormComponent>;

  public errorMessages: string[] = [];

  constructor(private shoppingListService: ShoppingListsService) {}

  ngOnInit(): void {}

  openModal(): void {
    this.modal.open();
  }
  onSave(): void {
    this.purchaseProductsViewChildren.map(
      (component: PurchaseShoppingListProductFormComponent) =>
        component.purchaseShoppingListProductForm.markAllAsTouched()
    );

    if (
      this.purchaseProductsViewChildren.some(
        (component: PurchaseShoppingListProductFormComponent) =>
          component.purchaseShoppingListProductForm.invalid
      )
    ) {
      return;
    }

    const purchaseShoppingListProducts: PurchaseShoppingListProducts = {
      shoppingListId: this.shoppingListId!,
      priceByProductNames: {},
    };
    this.purchaseProductsViewChildren.forEach(
      (component: PurchaseShoppingListProductFormComponent) => {
        const productPrice = component.getProductPrice();

        purchaseShoppingListProducts.priceByProductNames[
          component.productName!
        ] = {
          price: productPrice.price,
          currency: productPrice.currency,
        };
      }
    );

    this.shoppingListService
      .purchaseProducts(purchaseShoppingListProducts)
      .subscribe({
        next: () => {
          this.resetForms();

          this.modal.close();
        },
      });
  }

  onClose(): void {
    this.resetForms();
  }
  onDismiss(): void {
    this.resetForms();
  }

  private resetForms(): void {
    this.purchaseProductsViewChildren.map(
      (component: PurchaseShoppingListProductFormComponent) => {
        component.purchaseShoppingListProductForm.reset();
      }
    );
  }
}
