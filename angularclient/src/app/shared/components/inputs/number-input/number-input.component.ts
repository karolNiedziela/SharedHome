import { Component, Input, OnDestroy, OnInit, Self } from '@angular/core';
import {
  AbstractControl,
  ControlValueAccessor,
  NgControl,
  ValidatorFn,
} from '@angular/forms';

@Component({
  selector: 'app-number-input',
  templateUrl: './number-input.component.html',
  styleUrls: ['./number-input.component.scss', '../input.scss'],
})
export class NumberInputComponent
  implements OnInit, ControlValueAccessor, OnDestroy
{
  @Input() label: string = 'label';
  @Input() placeholder: string = 'placeholder';
  @Input() showErrorMessage: boolean = true;

  disabled: boolean = false;

  onChanged: (value: any) => void = () => {};
  onTouched: () => void = () => {};

  get control(): AbstractControl<any, any> | null {
    return this.controlDir.control;
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

  constructor(@Self() private controlDir: NgControl) {
    controlDir.valueAccessor = this;
  }

  ngOnInit(): void {
    let validators: ValidatorFn[] = this.control?.validator
      ? [this.control.validator]
      : [];

    this.control?.setValidators(validators);
    this.control?.updateValueAndValidity();
  }

  ngOnDestroy(): void {
    this.controlDir.control?.clearValidators();
    this.controlDir.control?.markAsPristine();
  }

  writeValue(value: any): void {
    if (this.controlDir.control && this.controlDir.control?.value != value) {
      this.controlDir.control?.setValue(value, { emitEvent: false });
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
}
