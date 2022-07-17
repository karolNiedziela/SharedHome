import {
  ControlValueAccessor,
  NgControl,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { Component, OnInit, Self } from '@angular/core';

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

  constructor(@Self() public controlDir: NgControl) {
    controlDir.valueAccessor = this;
  }
  ngOnInit(): void {
    const control = this.controlDir.control;
    let validators: ValidatorFn[] = control?.validator
      ? [control.validator]
      : [];

    validators.push(Validators.email);
    control?.setValidators(validators);
    control?.updateValueAndValidity();
  }

  writeValue(value: string): void {
    this.controlDir.control?.setValue(value);
  }

  registerOnChange(onChanged: (value: any) => void): void {
    this.onChanged = onChanged;
  }

  registerOnTouched(onTouched: () => void): void {
    this.onTouched = onTouched;
  }
}
