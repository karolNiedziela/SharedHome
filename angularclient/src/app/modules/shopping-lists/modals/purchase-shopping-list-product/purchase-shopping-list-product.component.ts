import { Modalable } from './../../../../core/models/modalable';
import { ErrorResponse } from './../../../../core/models/error-response';
import { ShoppingListsService } from './../../services/shopping-lists.service';
import { UserService } from './../../../../core/services/user.service';
import { PurchaseShoppingListProduct } from './../../models/purchase-shopping-list-product';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ModalComponent } from 'app/shared/components/modals/modal/modal.component';
import { ModalConfig } from 'app/shared/components/modals/modal/modal.config';

@Component({
  selector: 'app-purchase-shopping-list-product',
  templateUrl: './purchase-shopping-list-product.component.html',
  styleUrls: ['./purchase-shopping-list-product.component.scss'],
})
export class PurchaseShoppingListProductComponent implements OnInit, Modalable {
  @Input() shoppingListId!: number;
  @Input() productName!: string;
  @ViewChild('purchaseShoppingListProductModal') private modal!: ModalComponent;
  public modalConfig: ModalConfig = {
    modalTitle: 'Purchase shopping list product',
    onSave: () => this.onSave(),
    onClose: () => this.onClose(),
    onDismiss: () => this.onDismiss(),
  };

  public purchaseShoppingListProductForm!: FormGroup;

  public errorMessages: string[] = [];

  constructor(
    private shoppingListService: ShoppingListsService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.purchaseShoppingListProductForm = new FormGroup({
      money: new FormGroup({}),
    });
  }

  openModal(): void {
    this.modal.open();
  }

  onSave(): void {
    if (this.purchaseShoppingListProductForm.invalid) {
      this.purchaseShoppingListProductForm.markAllAsTouched();
      return;
    }

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

    this.shoppingListService
      .purchaseProduct(purchaseShoppingListProduct)
      .subscribe({
        next: () => {
          this.resetForm();

          this.modal.close();
        },
        error: (errors: string[]) => {
          this.errorMessages = errors;
        },
      });
  }

  onClose(): void {
    this.resetForm();
  }

  onDismiss(): void {
    this.resetForm();
  }

  private resetForm() {}
}
