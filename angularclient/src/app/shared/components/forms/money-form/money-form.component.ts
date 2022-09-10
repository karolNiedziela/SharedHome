import { Subscription } from 'rxjs/internal/Subscription';
import { UserService } from './../../../../core/services/user.service';
import { Component, forwardRef, OnInit, OnDestroy, Input } from '@angular/core';
import {
  FormGroup,
  NG_VALUE_ACCESSOR,
  FormControl,
  Validators,
} from '@angular/forms';
import { Money } from 'app/core/models/money';

@Component({
  selector: 'app-money-form',
  templateUrl: './money-form.component.html',
  styleUrls: ['./money-form.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => MoneyFormComponent),
      multi: true,
    },
  ],
})
export class MoneyFormComponent implements OnInit, OnDestroy {
  @Input() moneyFormGroup!: FormGroup;

  subscriptions: Subscription[] = [];

  get priceControl() {
    return this.moneyFormGroup.controls['price'];
  }

  get currencyControl() {
    return this.moneyFormGroup.controls['currency'];
  }

  get money(): Money {
    return {
      price: this.priceControl.value,
      currency: this.currencyControl.value,
    };
  }

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.moneyFormGroup.addControl(
      'price',
      new FormControl(null, [Validators.required, Validators.min(0)])
    );

    this.moneyFormGroup.addControl(
      'currency',
      new FormControl(this.userService.currentUser.defaultCurrency, [
        Validators.required,
      ])
    );

    this.subscriptions.push(
      this.moneyFormGroup.valueChanges.subscribe((value: any) => {
        if (this.currencyControl.value == null) {
          this.currencyControl.patchValue(
            this.userService.currentUser.defaultCurrency
          );
        }
      })
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((s) => s.unsubscribe());
    this.moneyFormGroup.reset();
  }
}
