import { LanguageService } from './../../../../core/services/language.service';
import { Component, EventEmitter, OnInit, Output, Self } from '@angular/core';
import {
  AbstractControl,
  ControlValueAccessor,
  NgControl,
} from '@angular/forms';

@Component({
  selector: 'app-currency-select',
  templateUrl: './currency-select.component.html',
  styleUrls: ['./currency-select.component.scss'],
})
export class CurrencySelectComponent implements OnInit, ControlValueAccessor {
  @Output() currencyChanged: EventEmitter<string> = new EventEmitter<string>();

  public currencies: string[] = ['zł', '€'];
  public selectedCurrency!: string;

  onChanged: (value: any) => void = () => {};
  onTouched: () => void = () => {};

  get control(): AbstractControl<any, any> | null {
    return this.controlDir.control;
  }

  get isRequired(): boolean {
    if (this.control?.validator) {
      return this.control.validator!({} as AbstractControl)!['required'];
    }

    return false;
  }

  constructor(
    @Self() private controlDir: NgControl,
    private languageService: LanguageService
  ) {
    controlDir.valueAccessor = this;
  }

  ngOnInit(): void {}

  writeValue(value: any): void {
    if (value == null) {
      this.selectedCurrency =
        this.languageService.language == LanguageService.pl
          ? this.currencies[0]
          : this.currencies[1];
    } else {
      this.selectedCurrency = value;
    }
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

    this.currencyChanged.emit(this.selectedCurrency);
  }
}
