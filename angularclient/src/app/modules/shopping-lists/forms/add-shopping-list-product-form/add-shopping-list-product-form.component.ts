import { ShoppingListProduct } from './../../models/shopping-list-product';
import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NetContentType } from '../../enums/net-content-type';

@Component({
  selector: 'app-add-shopping-list-product-form',
  templateUrl: './add-shopping-list-product-form.component.html',
  styleUrls: ['./add-shopping-list-product-form.component.scss'],
})
export class AddShoppingListProductFormComponent implements OnInit {
  @Input() uniqueKey!: number;
  @Output() delete: EventEmitter<any> = new EventEmitter();
  public addShoppingListProductForm!: FormGroup;

  public netContentType: typeof NetContentType = NetContentType;

  constructor() {}

  ngOnInit(): void {
    this.addShoppingListProductForm = new FormGroup({
      productName: new FormControl('', [Validators.required]),
      quantity: new FormControl(1, [Validators.required]),
      netContent: new FormGroup({}),
    });
  }

  public getProduct(): ShoppingListProduct {
    const productName =
      this.addShoppingListProductForm.get('productName')?.value;
    const quantity = this.addShoppingListProductForm.get('quantity')?.value;
    const netContent = this.addShoppingListProductForm
      ?.get('netContent')
      ?.get('netContent')?.value;
    const netContentType = this.addShoppingListProductForm
      ?.get('netContent')
      ?.get('netContentType')?.value;

    const shoppingListProduct: ShoppingListProduct = {
      name: productName,
      quantity: quantity,
      netContent: {
        netContent: netContent,
        netContentType: netContentType,
      },
      isBought: false,
    };

    return shoppingListProduct;
  }

  removeProduct() {
    this.delete.emit(this.uniqueKey);
  }
}
