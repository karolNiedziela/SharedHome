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

  public buyShoppingListProductForm!: FormGroup;

  public errorMessages: string[] = [];

  constructor() {}

  ngOnInit(): void {
    this.buyShoppingListProductForm = new FormGroup({
      price: new FormControl(null, [Validators.required]),
      currency: new FormControl('', [Validators.required]),
    });
  }

  onSave(): void {}

  onClose(): void {
    this.resetForm();
  }

  onDismiss(): void {
    this.resetForm();
  }

  private resetForm() {}
}
