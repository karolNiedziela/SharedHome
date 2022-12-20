import { formatDate } from '@angular/common';
import {
  Component,
  OnInit,
  Optional,
  Self,
  OnDestroy,
  Input,
} from '@angular/core';
import {
  NgControl,
  ControlValueAccessor,
  AbstractControl,
  ValidatorFn,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'app-date-input',
  templateUrl: './date-input.component.html',
  styleUrls: ['../input.scss'],
})
export class DateInputComponent
  implements OnInit, ControlValueAccessor, OnDestroy
{
  @Input() labelText: string = 'label';
  @Input() placeholder: string = 'placeholder';

  disabled!: boolean;

  onChanged: (value: any) => void = () => {};
  onTouched: () => void = () => {};

  get isRequired(): boolean {
    if (this.control?.validator) {
      return this.control.validator!({} as AbstractControl)!['required'];
    }

    return false;
  }

  get control(): AbstractControl<any, any> {
    return this.controlDir.control!;
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
    if (this.control && this.control?.value != value) {
      this.controlDir.control?.setValue(value, { emitEvent: false });
    }
  }

  registerOnChange(onChanged: (value: any) => void): void {
    this.onChanged = onChanged;
  }

  registerOnTouched(onTouched: () => void): void {
    this.onTouched = onTouched;
  }

  setDisabledState?(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }
}
