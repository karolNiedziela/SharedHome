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
export class PurchaseShoppingListProductComponent implements OnInit {
  @Input() shoppingListId!: number;
  @Input() productName!: string;
  @ViewChild('purchaseShoppingListProductModal') public modal!: ModalComponent;
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
      price: new FormControl(null, [Validators.required]),
      currency: new FormControl(this.userService.currentUser.defaultCurrency, [
        Validators.required,
      ]),
    });
  }

  onSave(): void {
    if (this.purchaseShoppingListProductForm.invalid) {
      this.purchaseShoppingListProductForm.markAllAsTouched();
      return;
    }

    const price = this.purchaseShoppingListProductForm.get('price')?.value;
    const currency =
      this.purchaseShoppingListProductForm.get('currency')?.value;

    const purchaseShoppingListProduct: PurchaseShoppingListProduct = {
      shoppingListId: this.shoppingListId,
      productName: this.productName,
      price: price,
      currency: currency,
    };

    this.shoppingListService
      .purchaseProduct(purchaseShoppingListProduct)
      .subscribe({
        next: (response) => {
          this.resetForm();

          this.modal.close();
        },
        error: (error) => {
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

  private resetForm() {}
}
