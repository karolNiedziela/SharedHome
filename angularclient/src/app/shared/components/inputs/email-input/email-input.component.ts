import {
  AbstractControl,
  ControlValueAccessor,
  NgControl,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { Component, Input, OnInit, Optional, Self } from '@angular/core';

@Component({
  selector: 'app-email-input',
  templateUrl: './email-input.component.html',
  styleUrls: ['../input.scss'],
})
export class EmailInputComponent implements OnInit, ControlValueAccessor {
  labelText: string = 'Email';
  placeholder: string = 'Email';

  onChanged: (value: string) => void = () => {};
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

  constructor(@Self() @Optional() private controlDir: NgControl) {
    controlDir.valueAccessor = this;
  }

  ngOnInit(): void {
    let validators: ValidatorFn[] = this.control?.validator
      ? [this.control.validator]
      : [];

    validators.push(Validators.email);
    this.control?.setValidators(validators);
    this.control?.updateValueAndValidity();
  }

  writeValue(value: string): void {
    if (this.control && this.control?.value != value) {
      this.controlDir.control?.setValue(value);
    }
  }

  registerOnChange(onChanged: (value: any) => void): void {
    this.onChanged = onChanged;
  }

  registerOnTouched(onTouched: () => void): void {
    this.onTouched = onTouched;
  }
}
