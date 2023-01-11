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
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { NetContentType } from '../../enums/net-content-type';
import { ShoppingListProduct } from '../../models/shopping-list-product';
import { FormModalComponent } from 'src/app/shared/components/modals/form-modal/form-modal.component';
import { FormModalConfig } from 'src/app/shared/components/modals/form-modal/form-modal.config';

@Component({
  selector: 'app-edit-shopping-list-product-modal',
  templateUrl: './edit-shopping-list-product-modal.component.html',
  styleUrls: ['./edit-shopping-list-product-modal.component.scss'],
})
export class EditShoppingListProductModalComponent
  implements Modalable, OnInit, OnChanges
{
  @Input() shoppingListId!: string;
  @Input() shoppingListProduct?: ShoppingListProduct;

  @ViewChild('editShoppingListProductModal') private modal!: FormModalComponent;
  public modalConfig: FormModalConfig = {
    modalTitle: 'shopping_lists.edit_shopping_list_product',
    onSave: () => this.onSave(),
  };

  editShoppingListProductForm!: FormGroup;

  public netContentType: typeof NetContentType = NetContentType;

  constructor(private shoppingListService: ShoppingListsService) {}

  ngOnInit(): void {
    this.editShoppingListProductForm = new FormGroup({
      productName: new FormControl('', [Validators.required]),
      quantity: new FormControl(1, [Validators.required]),
    });

    this.setFormValues();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['shoppingListProduct']) {
      this.setFormValues();
    }
  }

  openModal(): void {
    this.setFormValues();
    this.modal.open();
  }

  onSave(): void {
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
      netContent: {
        netContent: netContent,
        netContentType: netContentType,
      },
      isBought: this.shoppingListProduct!.isBought,
    };

    this.modal.save(
      this.shoppingListService.updateShoppingListProduct(
        updateShoppingListProduct
      )
    );
  }

  private setFormValues(): void {
    if (this.shoppingListProduct == null) {
      return;
    }

    this.editShoppingListProductForm?.patchValue({
      productName: this.shoppingListProduct.name,
      quantity: this.shoppingListProduct.quantity,
      netContent: this.shoppingListProduct.netContent?.netContent,
      netContentType: this.shoppingListProduct.netContent?.netContentType,
    });
  }
}
