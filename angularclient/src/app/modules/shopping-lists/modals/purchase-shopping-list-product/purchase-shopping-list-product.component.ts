import { Modalable } from './../../../../core/models/modalable';
import { ShoppingListsService } from './../../services/shopping-lists.service';
import { PurchaseShoppingListProduct } from './../../models/purchase-shopping-list-product';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FormModalConfig } from 'src/app/shared/components/modals/form-modal/form-modal.config';
import { FormModalComponent } from 'src/app/shared/components/modals/form-modal/form-modal.component';

@Component({
  selector: 'app-purchase-shopping-list-product',
  templateUrl: './purchase-shopping-list-product.component.html',
  styleUrls: ['./purchase-shopping-list-product.component.scss'],
})
export class PurchaseShoppingListProductComponent implements OnInit, Modalable {
  @Input() shoppingListId!: string;
  @Input() productName!: string;
  @ViewChild('purchaseShoppingListProductModal')
  private modal!: FormModalComponent;
  public modalConfig: FormModalConfig = {
    modalTitle: 'Purchase shopping list product',
    onSave: () => this.onSave(),
  };

  public purchaseShoppingListProductForm!: FormGroup;

  constructor(private shoppingListService: ShoppingListsService) {}

  ngOnInit(): void {
    this.purchaseShoppingListProductForm = new FormGroup({
      money: new FormGroup({}),
    });
  }

  openModal(): void {
    this.modal.open();
  }

  onSave(): void {
    const price = this.purchaseShoppingListProductForm
      .get('money')
      ?.get('price')?.value;
    const currency = this.purchaseShoppingListProductForm
      ?.get('money')
      ?.get('currency')?.value;

    const purchaseShoppingListProduct: PurchaseShoppingListProduct = {
      shoppingListId: this.shoppingListId,
      productName: this.productName,
      price: {
        price: price,
        currency: currency,
      },
    };

    this.modal.save(
      this.shoppingListService.purchaseProduct(purchaseShoppingListProduct)
    );
  }
}
