import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { Component, OnInit, forwardRef, AfterViewInit } from '@angular/core';

@Component({
  selector: 'app-currency-select',
  templateUrl: './currency-select.component.html',
  styleUrls: ['../select.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => CurrencySelectComponent),
      multi: true,
    },
  ],
})
export class CurrencySelectComponent implements OnInit, ControlValueAccessor {
  public currencies: string[] = ['zł', '€'];
  public selectedCurrency!: string;

  onChanged: (value: any) => void = () => {};
  onTouched: () => void = () => {};

  constructor() {}

  ngOnInit(): void {}

  writeValue(value: any): void {
    this.selectedCurrency = value;
  }
  registerOnChange(onChanged: (value: any) => void): void {
    this.onChanged = onChanged;
  }
  registerOnTouched(onTouched: () => void): void {
    this.onTouched = onTouched;
  }

  selectCurrency(currency: string) {
    this.selectedCurrency = currency;

    this.onChanged(currency);
    this.onTouched();
  }
}
