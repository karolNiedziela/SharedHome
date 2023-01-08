import {
  Component,
  Input,
  OnInit,
  Optional,
  Self,
  ViewEncapsulation,
} from '@angular/core';
import {
  AbstractControl,
  ControlValueAccessor,
  NgControl,
  ValidatorFn,
} from '@angular/forms';

import {
  DateAdapter,
  MAT_DATE_FORMATS,
  MAT_DATE_LOCALE,
} from '@angular/material/core';
import {
  MAT_MOMENT_DATE_ADAPTER_OPTIONS,
  MomentDateAdapter,
} from '@angular/material-moment-adapter';
import { MatDatepicker } from '@angular/material/datepicker';

import * as _moment from 'moment';
import { default as _rollupMoment, Moment } from 'moment';

export const MY_FORMATS = {
  parse: {
    dateInput: 'MM/YYYY',
  },
  display: {
    dateInput: 'MM/YYYY',
    monthYearLabel: 'MMM YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM YYYY',
  },
};

@Component({
  selector: 'app-year-month-input',
  templateUrl: './year-month-input.component.html',
  styleUrls: ['./year-month-input.component.scss'],
  providers: [
    {
      provide: DateAdapter,
      useClass: MomentDateAdapter,
      deps: [MAT_DATE_LOCALE, MAT_MOMENT_DATE_ADAPTER_OPTIONS],
    },

    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
  ],
  encapsulation: ViewEncapsulation.None,
})
export class YearMonthInputComponent implements OnInit, ControlValueAccessor {
  @Input() label: string = 'label';

  disabled: boolean = false;

  onChanged: (value: any) => void = () => {};
  onTouched: () => void = () => {};

  get control(): AbstractControl | null {
    return this.controlDir.control!;
  }

  get isRequired(): boolean {
    if (this.control?.validator) {
      const validator = this.control?.validator({} as AbstractControl)!;
      if (validator && validator['required']) {
        return true;
      }
    }

    return false;
  }

  constructor(@Self() @Optional() private controlDir: NgControl) {
    if (this.controlDir != null) {
      this.controlDir.valueAccessor = this;
    }
  }

  ngOnInit(): void {
    let validators: ValidatorFn[] = this.control?.validator
      ? [this.control.validator]
      : [];

    this.control?.setValidators(validators);
    this.control?.updateValueAndValidity();
  }

  writeValue(value: any): void {
    if (this.controlDir.control && this.controlDir.control?.value != value) {
      this.controlDir.control?.setValue(value);
    }
  }

  registerOnChange(onChanged: (value: any) => void): void {
    this.onChanged = onChanged;
  }

  registerOnTouched(onTouched: () => void): void {
    this.onTouched = onTouched;
  }

  setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

  setMonthAndYear(
    normalizedMonthAndYear: Moment,
    datepicker: MatDatepicker<Moment>
  ) {
    const ctrlValue = this.control!.value!;
    ctrlValue.month(normalizedMonthAndYear.month());
    ctrlValue.year(normalizedMonthAndYear.year());
    this.control!.setValue(ctrlValue);
    datepicker.close();
  }
}
