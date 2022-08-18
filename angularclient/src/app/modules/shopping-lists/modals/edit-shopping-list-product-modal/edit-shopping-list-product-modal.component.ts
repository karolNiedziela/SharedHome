import { UpdateShoppingListProduct } from './../../models/update-shopping-list-product';
import { ShoppingListsService } from './../../services/shopping-lists.service';
import { Modalable } from './../../../../core/models/modalable';
import {
  Component,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import { ModalComponent } from 'app/shared/components/modals/modal/modal.component';
import { ModalConfig } from 'app/shared/components/modals/modal/modal.config';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { NetContentType } from '../../enums/net-content-type';
import { ShoppingListProduct } from '../../models/shopping-list-product';

@Component({
  selector: 'app-edit-shopping-list-product-modal',
  templateUrl: './edit-shopping-list-product-modal.component.html',
  styleUrls: ['./edit-shopping-list-product-modal.component.scss'],
})
export class EditShoppingListProductModalComponent
  implements Modalable, OnInit, OnChanges
{
  @Input() shoppingListId!: number;
  @Input() shoppingListProduct?: ShoppingListProduct;

  @ViewChild('editShoppingListProductModal') private modal!: ModalComponent;
  public modalConfig: ModalConfig = {
    modalTitle: 'Edit shopping list product',
    onSave: () => this.onSave(),
    onClose: () => this.onClose(),
    onDismiss: () => this.onDismiss(),
  };

  errorMessages: string[] = [];

  editShoppingListProductForm!: FormGroup;

  public netContentType: typeof NetContentType = NetContentType;

  constructor(private shoppingListService: ShoppingListsService) {}

  ngOnInit(): void {
    this.editShoppingListProductForm = new FormGroup({
      productName: new FormControl('', [Validators.required]),
      quantity: new FormControl(1, [Validators.required]),
      netContent: new FormControl(''),
      netContentType: new FormControl(null),
    });

    this.setFormValues();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['shoppingListProduct']) {
      this.setFormValues();
    }
  }

  openModal(): void {
    this.modal.open();
  }
  onSave(): void {
    if (this.editShoppingListProductForm.invalid) {
      this.editShoppingListProductForm.markAllAsTouched();

      return;
    }

    const productName =
      this.editShoppingListProductForm.get('productName')?.value;
    const quantity = this.editShoppingListProductForm.get('quantity')?.value;
    const netContent =
      this.editShoppingListProductForm.get('netContent')?.value;
    const netContentType =
      this.editShoppingListProductForm.get('netContentType')?.value;

    const updateShoppingListProduct: UpdateShoppingListProduct = {
      shoppingListId: this.shoppingListId,
      currentProductName: this.shoppingListProduct!.name,
      newProductName: productName,
      quantity: quantity,
      netContent: netContent,
      netContentType: netContentType,
    };

    this.shoppingListService
      .updateShoppingListProduct(updateShoppingListProduct)
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

  private setFormValues(): void {
    if (this.shoppingListProduct == null) {
      return;
    }

    this.editShoppingListProductForm?.patchValue({
      productName: this.shoppingListProduct.name,
      quantity: this.shoppingListProduct.quantity,
      netContent: this.shoppingListProduct.netContent,
      netContentType: this.shoppingListProduct.netContentType,
    });
  }

  private resetForm(): void {
    this.setFormValues();
  }
}
