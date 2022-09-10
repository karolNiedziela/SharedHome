import { Money } from './../../../../core/models/money';
import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { UserService } from 'app/core/services/user.service';

@Component({
  selector: 'app-purchase-shopping-list-product-form',
  templateUrl: './purchase-shopping-list-product-form.component.html',
  styleUrls: ['./purchase-shopping-list-product-form.component.scss'],
})
export class PurchaseShoppingListProductFormComponent implements OnInit {
  @Input() productName?: string;

  public purchaseShoppingListProductForm!: FormGroup;

  public errorMessages: string[] = [];

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.purchaseShoppingListProductForm = new FormGroup({
      money: new FormGroup({}),
    });
  }

  public getPurchasedProduct(): Money {
    const price = this.purchaseShoppingListProductForm
      .get('money')
      ?.get('price')?.value;
    const currency = this.purchaseShoppingListProductForm
      .get('money')
      ?.get('currency')?.value;

    return {
      price: price,
      currency: currency,
    };
  }

  public resetForm() {
    this.purchaseShoppingListProductForm.reset();
    this.purchaseShoppingListProductForm.patchValue({
      currency: this.userService.currentUser.defaultCurrency,
    });
  }
}
